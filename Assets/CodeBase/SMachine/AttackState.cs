using CodeBase.Constants;
using CodeBase.Enemy;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.SMaschine
{
    public class AttackState : IState
    {
        private Animator _animator;
        private FollowTargetByCoroutine _followTargetByCoroutine;

        public AttackState(Animator animator, FollowTargetByCoroutine followTargetByCoroutine)
        {
            _animator = animator;
            _followTargetByCoroutine = followTargetByCoroutine;
        }
        
        public void Enter()
        {
            _animator.SetBool(AnimationConstans.Attack, true);
            _followTargetByCoroutine.StartFollow();
        }

        public void Exit()
        {
            _animator.SetBool(AnimationConstans.Attack, false);
            _followTargetByCoroutine.StopFollow();
        }
    }
}