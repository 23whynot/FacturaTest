using CodeBase.Constants;
using UnityEngine;

namespace CodeBase.Animation
{
    public class AnimationController
    {
        private Animator _animator;
        
        public AnimationController(Animator animator)
        {
            _animator = animator;
        }

        public void StartAnimationReactionOnHit()
        {
           _animator.SetTrigger(AnimationConstants.ReactionOnHit);
        }

        public void StartAnimationDeath()
        {
            _animator.SetTrigger(AnimationConstants.Death);
        }

        public void StartAnimationIdle()
        {
            _animator.SetTrigger(AnimationConstants.Idle);
        }
    }
}