using System.Collections;
using CodeBase.Animation;
using CodeBase.Services;
using CodeBase.SMaschine;
using CodeBase.Spawner;
using CodeBase.Stickmen;
using CodeBase.Stickmen.SMaschine;
using CodeBase.Stickmen.StateMachine;
using CodeBase.UI.WorldSpaceCanvas;
using UnityEngine;

namespace CodeBase.Character
{
    public class CharacterBehavior : MonoBehaviour
    {
        [Header("Scripts")]
        [SerializeField] private Characters characters;
        [SerializeField] private DetectionArea detectionArea;
        [SerializeField] private HealthBarVisual healthBarVisual;
        
        [Header("Components")]
        [SerializeField] private Collider detectionAreaCollider;
        [SerializeField] private Collider boxCollider;
        
        [Header("Visuals Components")]
        [SerializeField] private ParticleSystem particleSystemOnHit;
        [SerializeField] private Animator animator;
        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
        


        private AnimationController _animationController;
        private StateMachine _stateMachine;
        private SpawnController _spawnController;
        private HealthService _healthService;
        private MonoBehaviour _coroutineRunner;
        private Coroutine _deathCoroutine;

        private bool _isInitialized;

        public void Init(SpawnController spawnController)
        {
            _spawnController = spawnController;

            _animationController = new AnimationController(animator);
            _stateMachine = new StateMachine();
            _coroutineRunner = this;

            _isInitialized = true;
        }

        private void OnEnable()
        {
            if (_isInitialized)
            {
                Idle();
            }
            
            healthBarVisual.DeactivateHealthBar();
            _healthService = new HealthService(characters.GetHealth());
            skinnedMeshRenderer.enabled = true;
        }

        private void Start()
        {
            _healthService.OnDeath += Death;
            characters.OnBullet += Hit;
            detectionArea.OnDetection += Attack;
        }

        public void Death()
        {
            healthBarVisual.DeactivateHealthBar();
            boxCollider.enabled = false;
            _stateMachine.ChangeState(new Death(particleSystemOnHit));
            skinnedMeshRenderer.enabled = false;
            _deathCoroutine = StartCoroutine(DeathCoroutine());
        }

        private IEnumerator DeathCoroutine()
        {
            while (particleSystemOnHit.IsAlive())
            {
                yield return null;
            }

            if (_deathCoroutine != null)
            {
                StopCoroutine(_deathCoroutine);
                _deathCoroutine = null;
            }

            particleSystemOnHit.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            boxCollider.enabled = true;
            characters.Deactivate();
        }

        private void Idle()
        {
            _stateMachine.ChangeState(new IdleState(_animationController));
        }

        private void Hit(int damage)
        {
            _healthService.Decrease(damage);
            healthBarVisual.ActivateHealthBar(_healthService.GetCurrentHealth());
            if (_healthService.GetCurrentHealth() <= 0)
            {
                Death();
            }
            else
            {
                _stateMachine.ChangeState(new HitState(_animationController, particleSystemOnHit,
                    detectionAreaCollider, _coroutineRunner));
            }
        }

        private void Attack()
        {
            _stateMachine.ChangeState(new AttackState(animator, _spawnController.GetTargetTransform(), transform,
                characters.GetDurationToTarget()));
        }

        private void OnDestroy()
        {
            _healthService.OnDeath -= Death;
            characters.OnBullet -= Hit;
            detectionArea.OnDetection -= Attack;
        }
    }
}