using System;

namespace Tools.StateMachine
{
    public abstract class StateMachine
    {
        public event Action<IState> StateChanged;
        protected IState _currentState;

        protected void UpdateState(float deltaTime)
        {
            _currentState?.Update(deltaTime);
        }

        protected void FixedUpdateState(float fixedDeltaTime)
        {
            _currentState?.FixedUpdate(fixedDeltaTime);
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
    }
}
