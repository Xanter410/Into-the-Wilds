using Tools.StateMachine;
using UnityEngine;

namespace IntoTheWilds
{
    public class SheepMoveState : IState
    {
        public int ID { get; }

        private readonly SheepStateMachine _stateMachine;
        private readonly SheepAI _input;
        private readonly Rigidbody2D _rigidbody2D;

        private Vector2 _velocity = Vector2.zero;
        private Vector2 _desiredVelocity = Vector2.zero;

        private readonly float _maxSpeed = 1f;
        private readonly float _maxAcceleration = 15f;

        public SheepMoveState(int id, SheepStateMachine stateMachine, SheepAI aiInput, Rigidbody2D rigidbody2D)
        {
            ID = id;
            _stateMachine = stateMachine;
            _input = aiInput;
            _rigidbody2D = rigidbody2D;
        }

        void IState.Enter()
        {
            
        }

        void IState.Exit()
        {
            
        }

        void IState.FixedUpdate(float fixedDeltaTime)
        {
            _velocity = _rigidbody2D.linearVelocity;
            float _maxSpeedChange = _maxAcceleration * Time.fixedDeltaTime;
            _velocity = Vector2.MoveTowards(_velocity, _desiredVelocity, _maxSpeedChange);

            _rigidbody2D.linearVelocity = _velocity;
        }

        void IState.Update(float deltaTime)
        {
            Vector2 _direction = ((IMove)_input).RetrieveMoveInput();
            _desiredVelocity = _direction * _maxSpeed;

            if (IsIdle())
            {
                _stateMachine.TransitionTo(_stateMachine.IdleState);
            }
        }

        private bool IsIdle()
        {
            if (((IMove)_input).RetrieveMoveInput() == Vector2.zero)
            {
                return true;
            }

            return false;
        }
    }
}
