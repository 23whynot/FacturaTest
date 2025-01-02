using System.Collections;
using CodeBase.Animation;
using CodeBase.Core.StateMachine;
using UnityEngine;

namespace CodeBase.Stickmen.SMaschine
{
    public class HitState : IState
    {
        private AnimationController _animationController;
        private ParticleSystem _particleSystem;
        private Collider _collider;
        private Collider _detectionAreaColider;
        private MonoBehaviour _coroutineRunner;
        
        public HitState(AnimationController animationController, ParticleSystem particleSystem, Collider hitCollider, Collider DetectionAreaColider, MonoBehaviour coroutineRunner)
        {
            _animationController = animationController;
            _particleSystem = particleSystem;
            _collider = hitCollider;
            _detectionAreaColider = DetectionAreaColider;
            _coroutineRunner = coroutineRunner;
        }
        public void Enter()
        {
            _detectionAreaColider.enabled = false;
            _collider.enabled = false;
            _animationController.StartAnimationReactionOnHit();
            _particleSystem.Play();
            _coroutineRunner.StartCoroutine(WaitForExit());
        }

        public void Exit()
        {
            if (_coroutineRunner != null)
            {
                _coroutineRunner.StopCoroutine(WaitForExit());
                _coroutineRunner = null;
            }
            _detectionAreaColider.enabled = true;
            _collider.enabled = true;
            _particleSystem.Stop();
            _animationController.StartAnimationDeath();
        }

        private IEnumerator WaitForExit()
        {
            while (_particleSystem.IsAlive())
            {
                yield return null; 
            }

            Exit();
        }
    }
}