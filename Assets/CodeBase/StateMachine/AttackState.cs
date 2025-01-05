using CodeBase.Constants;
using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.StateMachine
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
            _animator.SetBool(AnimationConstants.Attack, true);
            _followTargetByCoroutine.StartFollow();
        }

        public void Exit()
        {
            _animator.SetBool(AnimationConstants.Attack, false);
            _followTargetByCoroutine.StopFollow();
        }
    }
}