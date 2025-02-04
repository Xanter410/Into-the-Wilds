using Tools.StateMachine;
using UnityEngine;

namespace IntoTheWilds
{
    public class SheepIdleState : IState
    {
        public int ID { get; }

        private readonly SheepStateMachine _stateMachine;
        private readonly SheepAI _input;
        private readonly Rigidbody2D _rigidbody2D;

        private readonly float _maxAcceleration = 15f;

        public SheepIdleState(int id, SheepStateMachine stateMachine, SheepAI aiInput, Rigidbody2D rigidbody2D)
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
            if (_rigidbody2D.linearVelocity != Vector2.zero)
            {
                Vector2 velocity = _rigidbody2D.linearVelocity;

                velocity = Vector2.MoveTowards(velocity, Vector2.zero, _maxAcceleration);

                _rigidbody2D.linearVelocity = velocity;
            }
        }

        void IState.Update(float _)
        {
            if (IsMoved())
            {
                _stateMachine.TransitionTo(_stateMachine.MoveState);
            }
        }

        private bool IsMoved()
        {
            if (((IMove)_input).RetrieveMoveInput() != Vector2.zero)
            {
                return true;
            }

            return false;
        }
    }
}
