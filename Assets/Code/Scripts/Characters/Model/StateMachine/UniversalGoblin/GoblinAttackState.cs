using Tools.StateMachine;
using UnityEngine;

namespace IntoTheWilds
{
    public class GoblinAttackState : IState
    {
        public int ID { get; }

        private readonly GoblinStateMachine _stateMachine;
        private readonly IAttack _inputAttack;
        private readonly Rigidbody2D _rigidbody2D;

        private float _timeAttackDuration = 0.52f;
        private float _currentTimeAttackDuration;

        private readonly float _maxAcceleration = 15f;

        public GoblinAttackState(int id, GoblinStateMachine stateMachine, IAttack inputAttack, Rigidbody2D rigidbody2D)
        {
            ID = id;
            _stateMachine = stateMachine;
            _inputAttack = inputAttack;
            _rigidbody2D = rigidbody2D;
        }

        public void Enter()
        {
            _currentTimeAttackDuration = _timeAttackDuration;

            _inputAttack.RegisterCallbackAttack(AttackPressed);
        }

        public void Exit()
        {
            _currentTimeAttackDuration = 0;

            _inputAttack.UnRegisterCallbackAttack(AttackPressed);
        }

        public void Update(float deltaTime)
        {
            if (IsAttackEnded(deltaTime))
            {
                _stateMachine.TransitionTo(_stateMachine.IdleState);
            }
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            if (_rigidbody2D.linearVelocity != Vector2.zero)
            {
                Vector2 velocity = _rigidbody2D.linearVelocity;

                velocity = Vector2.MoveTowards(velocity, Vector2.zero, _maxAcceleration);

                _rigidbody2D.linearVelocity = velocity;
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

        private void AttackPressed()
        {
            _currentTimeAttackDuration = _timeAttackDuration;
        }
    }
}
