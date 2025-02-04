using Tools.StateMachine;
using UnityEngine;

namespace IntoTheWilds
{
    internal class SheepDeadState : IState
    {
        public int ID { get; }

        private readonly SheepStateMachine _stateMachine;
        private readonly Rigidbody2D _rigidbody2D;

        private readonly float _maxAcceleration = 15f;

        public SheepDeadState(int id, SheepStateMachine stateMachine, Rigidbody2D rigidbody2D, HealthComponent healthComponent)
        {
            ID = id;
            _stateMachine = stateMachine;
            _rigidbody2D = rigidbody2D;

            healthComponent.ObjectDied.AddListener(OnUnitDead);
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
        
        }

        private void OnUnitDead()
        {
            _stateMachine.TransitionTo(this);
        }
    }
}