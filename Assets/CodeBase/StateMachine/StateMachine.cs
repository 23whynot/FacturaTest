﻿namespace CodeBase.StateMachine
{
    public class StateMachine
    {
        private IState _currentState;
    
        public void ChangeState(IState newState)
        {
            _currentState?.Exit(); 
            _currentState = newState;
            _currentState.Enter();  
        }
    }
}