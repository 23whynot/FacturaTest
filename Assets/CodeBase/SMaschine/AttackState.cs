using CodeBase.Constants;
using CodeBase.Core.StateMachine;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Stickmen.SMaschine
{
    public class AttackState : IState
    {
        private Tween _tween;
        
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
            _tween = _enemyTransform.DOMove(_target.position, _durationToTarget).SetEase(Ease.Linear)
                .OnComplete(Exit);
        }

        public void Exit()
        {
            _animator.SetBool(AnimationConstans.Attack, false);
            _tween?.Kill();
        }
    }
}