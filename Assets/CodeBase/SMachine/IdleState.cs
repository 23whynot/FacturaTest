

using CodeBase.Animation;
using CodeBase.SMaschine;

namespace CodeBase.Stickmen.StateMachine
{
    public class IdleState : IState
    {
        private AnimationController _animationController;
        
        public IdleState(AnimationController animationController)
        {
            _animationController = animationController;
        }
        public void Enter()
        {
           _animationController.StartAnimationIdle();
        }

        public void Exit()
        {
            
        }
    }
}