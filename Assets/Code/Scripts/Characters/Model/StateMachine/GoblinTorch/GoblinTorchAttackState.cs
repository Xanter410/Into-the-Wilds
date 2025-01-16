using Tools.StateMachine;
using UnityEngine;

namespace IntoTheWilds
{
    public class GoblinTorchAttackState : IState
    {
        public int ID { get; }

        private readonly GoblinTorchStateMachine _stateMachine;
        private readonly GoblinTorchAI _input;
        private readonly Rigidbody2D _rigidbody2D;

        private float _timeAttackDuration = 0.52f;
        private float _currentTimeAttackDuration;

        private readonly float _maxAcceleration = 15f;

        public GoblinTorchAttackState(int id, GoblinTorchStateMachine stateMachine, GoblinTorchAI aiInput, Rigidbody2D rigidbody2D)
        {
            ID = id;
            _stateMachine = stateMachine;
            _input = aiInput;
            _rigidbody2D = rigidbody2D;
        }

        public void Enter()
        {
            _currentTimeAttackDuration = _timeAttackDuration;

            _input.RegisterCallbackAttack(AttackPressed);
        }

        public void Exit()
        {
            _currentTimeAttackDuration = 0;

            _input.UnRegisterCallbackAttack(AttackPressed);
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
