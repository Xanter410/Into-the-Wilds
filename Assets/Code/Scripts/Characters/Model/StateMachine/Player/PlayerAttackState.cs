using Tools.StateMachine;
using UnityEngine;

namespace IntoTheWilds
{
    public class PlayerAttackState : IState
    {
        public int ID { get; }

        private readonly PlayerStateMachine _stateMachine;
        private readonly IAttack _input;
        private readonly Rigidbody2D _rigidbody2D;

        private readonly float _timeAttackDuration = 0.45f;
        private float _currentTimeAttackDuration;

        private readonly float _maxAcceleration = 15f;

        public PlayerAttackState(int id, PlayerStateMachine stateMachine, IAttack unitInput, Rigidbody2D rigidbody2D)
        {
            ID = id;
            _stateMachine = stateMachine;
            _input = unitInput;
            _rigidbody2D = rigidbody2D;
        }

        void IState.Enter()
        {
            _currentTimeAttackDuration = _timeAttackDuration;

            _input.AttackPressed += Input_AttackPressed;
        }

        void IState.Exit()
        {
            _currentTimeAttackDuration = 0;

            _input.AttackPressed -= Input_AttackPressed;
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

        void IState.Update(float deltaTime)
        {
            if (IsAttackEnded(deltaTime))
            {
                _stateMachine.TransitionTo(_stateMachine.IdleState);
            }
        }

        private bool IsAttackEnded(float deltaTime)
        {
            _currentTimeAttackDuration -= deltaTime;

            if (_currentTimeAttackDuration <= 0)
            {
                return true;
            }

            return false;
        }

        private void Input_AttackPressed()
        {
            _currentTimeAttackDuration = _timeAttackDuration;
        }
    }
}