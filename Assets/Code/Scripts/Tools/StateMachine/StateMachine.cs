using System;

namespace Tools.StateMachine
{
    public abstract class StateMachine
    {
        public event Action<IState> StateChanged;
        protected IState _currentState;

        private bool _isEnable = true;

        protected void UpdateState(float deltaTime)
        {
            if (_isEnable == true)
            {
                _currentState?.Update(deltaTime);
            }
        }

        protected void FixedUpdateState(float fixedDeltaTime)
        {
            if (_isEnable == true)
            {
                _currentState?.FixedUpdate(fixedDeltaTime);
            }
        }

        protected void Initialize(IState state)
        {
            _currentState = state;
            _currentState.Enter();

            StateChanged?.Invoke(state);
        }

        public void TransitionTo(IState nextState)
        {
            _currentState.Exit();

            _currentState = nextState;
            _currentState.Enter();

            StateChanged?.Invoke(nextState);
        }

        public void SetEnable(bool isEnable)
        {
            _isEnable = isEnable;
        }
    }
}
