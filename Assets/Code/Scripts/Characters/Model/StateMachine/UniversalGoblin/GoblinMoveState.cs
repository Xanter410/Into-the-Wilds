using Tools.StateMachine;
using UnityEngine;

namespace IntoTheWilds
{
    public class GoblinMoveState : IState
    {
        public int ID { get; }

        private readonly GoblinStateMachine _stateMachine;
        private readonly IMove _inputMove;
        private readonly IAttack _inputAttack;
        private readonly Rigidbody2D _rigidbody2D;

        private Vector2 _velocity = Vector2.zero;
        private Vector2 _desiredVelocity = Vector2.zero;

        private readonly float _maxSpeed = 2f;
        private readonly float _maxAcceleration = 15f;

        public GoblinMoveState(int id, GoblinStateMachine stateMachine, IMove inputMove, IAttack inputAttack, Rigidbody2D rigidbody2D)
        {
            ID = id;
            _stateMachine = stateMachine;
            _inputMove = inputMove;
            _inputAttack = inputAttack;
            _rigidbody2D = rigidbody2D;
        }

        void IState.Enter()
        {
            _inputAttack.AttackPressed += Input_AttackPressed;
        }

        void IState.Exit()
        {
            _inputAttack.AttackPressed -= Input_AttackPressed;
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
            Vector2 _direction = _inputMove.RetrieveMoveInput();
            _desiredVelocity = _direction * _maxSpeed;

            if (IsIdle())
            {
                _stateMachine.TransitionTo(_stateMachine.IdleState);
            }
        }

        private bool IsIdle()
        {
            if (_inputMove.RetrieveMoveInput() == Vector2.zero)
            {
                return true;
            }

            return false;
        }

        private void Input_AttackPressed()
        {
            _stateMachine.TransitionTo(_stateMachine.AttackState);
        }
    }
}
