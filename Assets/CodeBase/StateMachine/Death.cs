using UnityEngine;

namespace CodeBase.StateMachine
{
    public class Death : IState 
    {
        private ParticleSystem _particle;

        public Death(ParticleSystem particle)
        {
           _particle = particle;
        }

        public void Enter()
        {
           _particle.Play();
        }

        public void Exit()
        {
            _particle.Stop();
        }
    }
}