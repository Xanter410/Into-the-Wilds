﻿using Tools.StateMachine;
using UnityEngine;

namespace IntoTheWilds
{
    internal class PlayerDeadState : IState
    {
        public int ID { get; }

        private readonly PlayerStateMachine _stateMachine;
        private readonly PlayerInput _inputActions;
        private readonly Rigidbody2D _rigidbody2D;

        private readonly float _maxAcceleration = 15f;

        public PlayerDeadState(int id, PlayerStateMachine stateMachine, PlayerInput inputActions, Rigidbody2D rigidbody2D, HealthComponent healthComponent)
        {
            ID = id;
            _stateMachine = stateMachine;
            _inputActions = inputActions;
            _rigidbody2D = rigidbody2D;
            healthComponent.ObjectDied.AddListener(OnPlayerDead);
        }

        void IState.Enter()
        {
            _inputActions.SetEnabled(false);
        }

        void IState.Exit()
        {
            _inputActions.SetEnabled(true);
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
            return;
        }

        private void OnPlayerDead()
        {
            _stateMachine.TransitionTo(this);
        }
    }
}