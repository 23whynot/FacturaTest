using CodeBase.Animation;
using UnityEngine;
using IState = CodeBase.SMaschine.IState;
using SMaschine_IState = CodeBase.SMaschine.IState;

namespace CodeBase.Stickmen.SMaschine
{
    public class Death : SMaschine_IState
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