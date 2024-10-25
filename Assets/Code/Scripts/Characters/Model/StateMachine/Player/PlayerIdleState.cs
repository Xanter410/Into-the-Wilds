using Tools.StateMachine;
using UnityEngine;

namespace IntoTheWilds
{
    public class PlayerIdleState : IState
    {
        public int ID { get; }

        private readonly PlayerStateMachine _stateMachine;
        private readonly PlayerInput _inputActions;
        private readonly Rigidbody2D _rigidbody2D;

        private readonly float _maxAcceleration = 15f;

        public PlayerIdleState(int id, PlayerStateMachine stateMachine, PlayerInput inputActions, Rigidbody2D rigidbody2D)
        {
            ID = id;
            _stateMachine = stateMachine;
            _inputActions = inputActions;
            _rigidbody2D = rigidbody2D;
        }

        public void Enter()
        {
            _inputActions.RegisterCallbackAttack(AttackPressed);
        }

        public void Exit()
        {
            _inputActions.UnRegisterCallbackAttack(AttackPressed);
        }

        public void FixedUpdate(float _)
        {
            if (_rigidbody2D.linearVelocity != Vector2.zero)
            {
                Vector2 velocity = _rigidbody2D.linearVelocity;

                velocity = Vector2.MoveTowards(velocity, Vector2.zero, _maxAcceleration);

                _rigidbody2D.linearVelocity = velocity;
            }
        }

        public void Update(float _)
        {
            if (IsMoved())
            {
                _stateMachine.TransitionTo(_stateMachine.MoveState);
            }
        }

        private bool IsMoved()
        {
            if (_inputActions.RetrieveMoveInput() != Vector2.zero)
            {
                return true;
            }

            return false;
        }

        private void AttackPressed()
        {
            _stateMachine.TransitionTo(_stateMachine.AttackState);
        }

    }
}