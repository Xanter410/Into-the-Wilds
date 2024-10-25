using UnityEngine;
using Tools.StateMachine;

namespace IntoTheWilds
{
    public class PlayerMoveState : IState
    {
        public int ID { get; }

        private readonly PlayerStateMachine _stateMachine;
        private readonly PlayerInput _input;
        private readonly Rigidbody2D _rigidbody2D;

        private Vector2 _velocity = Vector2.zero;
        private Vector2 _desiredVelocity = Vector2.zero;

        private readonly float _maxSpeed = 4f;
        private readonly float _maxAcceleration = 15f;


        public PlayerMoveState(int id, PlayerStateMachine stateMachine, PlayerInput unitController, Rigidbody2D rigidbody2D)
        {
            ID = id;
            _stateMachine = stateMachine;
            _input = unitController;
            _rigidbody2D = rigidbody2D;
        }

        public void Enter()
        {
            _input.RegisterCallbackAttack(AttackPressed);
        }

        public void Exit()
        {
            _input.UnRegisterCallbackAttack(AttackPressed);
        }

        public void FixedUpdate(float _)
        {
            _velocity = _rigidbody2D.linearVelocity;
            float _maxSpeedChange = _maxAcceleration * Time.fixedDeltaTime;
            _velocity = Vector2.MoveTowards(_velocity, _desiredVelocity, _maxSpeedChange);

            _rigidbody2D.linearVelocity = _velocity;
        }

        public void Update(float _)
        {
            Vector2 _direction = _input.RetrieveMoveInput();
            _desiredVelocity = _direction * _maxSpeed;

            if (IsIdle())
            {
                _stateMachine.TransitionTo(_stateMachine.IdleState);
            }
        }

        private bool IsIdle()
        {
            if (_input.RetrieveMoveInput() == Vector2.zero)
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