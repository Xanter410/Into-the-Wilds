using Tools.StateMachine;
using UnityEngine;


namespace IntoTheWilds
{
    public class GoblinDeadState : IState
    {
        public int ID { get; }

        private readonly GoblinStateMachine _stateMachine;
        private readonly Rigidbody2D _rigidbody2D;

        private readonly float _maxAcceleration = 15f;

        public GoblinDeadState(int id, GoblinStateMachine stateMachine, Rigidbody2D rigidbody2D, HealthComponent healthComponent)
        {
            ID = id;
            _stateMachine = stateMachine;
            _rigidbody2D = rigidbody2D;

            healthComponent._onDead.AddListener(OnUnitDead);
        }

        public void Enter(){}

        public void Exit(){}

        public void FixedUpdate(float _)
        {
            if (_rigidbody2D.linearVelocity != Vector2.zero)
            {
                Vector2 velocity = _rigidbody2D.linearVelocity;

                velocity = Vector2.MoveTowards(velocity, Vector2.zero, _maxAcceleration);

                _rigidbody2D.linearVelocity = velocity;
            }
        }

        public void Update(float _){}

        private void OnUnitDead()
        {
            _stateMachine.TransitionTo(this);
        }
    }
}
