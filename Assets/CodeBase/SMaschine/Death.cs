using CodeBase.Animation;
using UnityEngine;
using IState = CodeBase.Core.StateMachine.IState;

namespace CodeBase.Stickmen.SMaschine
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