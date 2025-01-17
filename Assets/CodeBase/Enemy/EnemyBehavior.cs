﻿using System.Collections;
using CodeBase.Animation;
using CodeBase.Camera;
using CodeBase.Constants;
using CodeBase.Services;
using CodeBase.Spawner;
using CodeBase.StateMachine;
using CodeBase.UI.WorldSpaceCanvas;
using UnityEngine;
using Zenject;

namespace CodeBase.Enemy
{
    public class EnemyBehavior : MonoBehaviour
    {
        [Header("Scripts")]
        [SerializeField] private Enemy enemy;
        [SerializeField] private DetectionArea detectionArea;
        [SerializeField] private HealthBarVisual healthBarVisual;
        [SerializeField] private FollowTargetByCoroutine followTargetByCoroutine;
        [SerializeField] private ScoreDisplayManager scoreDisplayManager;
        
        [Header("Components")]
        [SerializeField] private Collider detectionAreaCollider;
        [SerializeField] private Collider boxCollider;
        
        [Header("Visuals Components")]
        [SerializeField] private ParticleSystem particleSystemOnHit;
        [SerializeField] private Animator animator;
        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

        [Inject]
        public void Construct(ScoreService scoreService, AudioService audioService)
        {
            _scoreService = scoreService;
            _audioService = audioService;
        }

        private AudioService _audioService;
        private ScoreService _scoreService;
        private AnimationController _animationController;
        private StateMachine.StateMachine _stateMachine;
        private SpawnController _spawnController;
        private HealthService _healthService;
        private MonoBehaviour _coroutineRunner;
        private Coroutine _deathCoroutine;
        private bool _isInitialized;

        public void Init(SpawnController spawnController)
        {
            _spawnController = spawnController;

            _animationController = new AnimationController(animator);
            _stateMachine = new StateMachine.StateMachine();
            
            followTargetByCoroutine.Init(_spawnController, enemy.GetSpeed());

            _isInitialized = true;
        }

        private void OnEnable()
        {
            if (_isInitialized)
            {
                Idle();
            }
            
            scoreDisplayManager.ActivateScoreDisplay(0);
            healthBarVisual.DeactivateHealthBar();
            _healthService = new HealthService(enemy.GetHealth());
            skinnedMeshRenderer.enabled = true;
        }

        private void Start()
        {
            _coroutineRunner = this;
            enemy.OnBullet += Hit;
            detectionArea.OnDetection += Attack;
        }

        public void Death()
        {
            int scoreCount = enemy.GetScoreCount();
            boxCollider.enabled = false;
            skinnedMeshRenderer.enabled = false;
            _spawnController.EnemyDespawn();
            healthBarVisual.DeactivateHealthBar();
            scoreDisplayManager.ActivateScoreDisplay(scoreCount);
            _scoreService.IncrementScore(scoreCount);
            _stateMachine.ChangeState(new Death(particleSystemOnHit));
            _audioService.PlaySound(AudioConstants.EnemyDeath);
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
            scoreDisplayManager.DeactivateScoreDisplay();
            enemy.Deactivate();
        }

        private void Idle()
        {
            _stateMachine.ChangeState(new IdleState(_animationController));
        }

        private void Hit(int damage)
        {
            _healthService.Decrease(damage);
            healthBarVisual.ActivateHealthBar(enemy.GetHealth());
            healthBarVisual.SetCount(damage);
            
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
            _stateMachine.ChangeState(new AttackState(animator, followTargetByCoroutine));
        }

        private void OnDestroy()
        {
            enemy.OnBullet -= Hit;
            detectionArea.OnDetection -= Attack;
        }
    }
}