using Tools.StateMachine;
using UnityEngine;

namespace IntoTheWilds
{
    public class GoblinIdleState : IState
    {
        public int ID { get; }

        private readonly GoblinStateMachine _stateMachine;
        private readonly IMove _inputMove;
        private readonly IAttack _inputAttack;
        private readonly Rigidbody2D _rigidbody2D;

        private readonly float _maxAcceleration = 15f;

        public GoblinIdleState(int id, GoblinStateMachine stateMachine, IMove inputMove, IAttack inputAttack, Rigidbody2D rigidbody2D)
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
