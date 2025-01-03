using System.Collections;
using CodeBase.Animation;
using UnityEngine;

namespace CodeBase.SMaschine
{
    public class HitState : IState
    {
        private AnimationController _animationController;
        private ParticleSystem _particleSystem;

        private Collider _detectionAreaColider;
        private MonoBehaviour _coroutineRunner;
        
        public HitState(AnimationController animationController, ParticleSystem particleSystem, Collider DetectionAreaColider, MonoBehaviour coroutineRunner)
        {
            _animationController = animationController;
            _particleSystem = particleSystem;
            _detectionAreaColider = DetectionAreaColider;
            _coroutineRunner = coroutineRunner;
        }
        public void Enter()
        {
            _detectionAreaColider.enabled = false;
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