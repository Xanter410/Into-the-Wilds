using Tools.StateMachine;
using UnityEngine;

namespace IntoTheWilds
{
    public class PlayerIdleState : IState
    {
        public int ID { get; }

        private readonly PlayerStateMachine _stateMachine;
        private readonly IAttack _inputAttack;
        private readonly IMove _inputMove;
        private readonly Rigidbody2D _rigidbody2D;

        private readonly float _maxAcceleration = 15f;

        public PlayerIdleState(int id, PlayerStateMachine stateMachine, IAttack inputAttack, IMove inputMove, Rigidbody2D rigidbody2D)
        {
            ID = id;
            _stateMachine = stateMachine;
            _inputAttack = inputAttack;
            _inputMove = inputMove;
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

        void IState.FixedUpdate(float _)
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
            if (_inputMove.RetrieveMoveInput() != Vector2.zero)
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