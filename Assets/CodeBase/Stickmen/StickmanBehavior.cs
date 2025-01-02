using System.Collections;
using CodeBase.Animation;
using CodeBase.Services;
using CodeBase.Spawner;
using CodeBase.Stickmen.SMaschine;
using UnityEngine;
using CodeBase.Stickmen.StateMachine;

namespace CodeBase.Stickmen
{
    public class StickmanBehavior : MonoBehaviour
    {
        [SerializeField] private Stickman stickman;
        [SerializeField] private ParticleSystem particleSystemOnHit;
        [SerializeField] private Animator animator;
        [SerializeField] private DetectionArea detectionArea;
        [SerializeField] private Collider detectionAreaCollider;
        [SerializeField] private Collider boxCollider;
        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
        [SerializeField] private int health = 100;
        [SerializeField] private float durationToTarget = 3;

        private AnimationController _animationController;
        private SMaschine.StateMachine _stateMachine;
        private SpawnController _spawnController;
        private HealthService _healthService;
        private MonoBehaviour _coroutineRunner;
        private Coroutine _deathCoroutine;

        private bool _isInitialized;

        public void Init(SpawnController spawnController)
        {
            _spawnController = spawnController;

            _animationController = new AnimationController(animator);
            _stateMachine = new SMaschine.StateMachine();
            _coroutineRunner = this;

            _isInitialized = true;
        }

        private void OnEnable()
        {
            if (_isInitialized)
            {
                Idle();
            }

            _healthService = new HealthService(health);
            skinnedMeshRenderer.enabled = true;
        }

        private void Start()
        {
            _healthService.OnDeath += Death;
            stickman.OnBullet += Hit;
            detectionArea.OnDetection += Attack;
        }

        public void Death()
        {
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
            stickman.Deactivate();
        }

        private void Idle()
        {
            _stateMachine.ChangeState(new IdleState(_animationController));
        }

        private void Hit(int damage)
        {
            _healthService.Decrease(damage);
            if (_healthService.GetCurrentHealth() <= 0)
            {
                Death();
            }
            else
            {
                _stateMachine.ChangeState(new HitState(_animationController, particleSystemOnHit, boxCollider,
                    detectionAreaCollider, _coroutineRunner));
            }
        }

        private void Attack()
        {
            _stateMachine.ChangeState(new AttackState(animator, _spawnController.GetTargetTransform(), transform,
                durationToTarget));
        }

        private void OnDestroy()
        {
            _healthService.OnDeath -= Death;
            stickman.OnBullet -= Hit;
            detectionArea.OnDetection -= Attack;
        }
    }
}