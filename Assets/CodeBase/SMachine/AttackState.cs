using CodeBase.Constants;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.SMaschine
{
    public class AttackState : IState
    {
        private Tween _moveTween;
        private Tween _lookAtTween;
        
        
        private Animator _animator;
        private Transform _target;
        private Transform _enemyTransform;
        private float _durationToTarget;

        public AttackState(Animator animator, Transform target, Transform enemyTransform, float durationToTarget)
        {
            _animator = animator;
            _target = target;
            _enemyTransform = enemyTransform;
            _durationToTarget = durationToTarget;
        }
        
        public void Enter()
        {
            _animator.SetBool(AnimationConstans.Attack, true);
            _moveTween = _enemyTransform.DOMove(_target.position, _durationToTarget).SetEase(Ease.Linear)
                .OnComplete(Exit);
            _lookAtTween = _enemyTransform.DOLookAt(_target.position, 0.1f).SetEase(Ease.Linear);
        }

        public void Exit()
        {
            _animator.SetBool(AnimationConstans.Attack, false);
            _moveTween?.Kill();
            _lookAtTween?.Kill();
        }
    }
}