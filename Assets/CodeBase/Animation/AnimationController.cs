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
           _animator.SetTrigger(AnimationConstans.ReactionOnHit);
        }
        
        public void StartAnimationAttack()
        {
            _animator.SetTrigger(AnimationConstans.Attack);
        }

        public void StartAnimationDeath()
        {
            _animator.SetTrigger(AnimationConstans.Death);
        }

        public void StartAnimationIdle()
        {
            _animator.SetTrigger(AnimationConstans.Idle);
        }
    }
}