using UnityEngine;
using Tools.StateMachine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace IntoTheWilds
{
    public class PlayerStateMachine : StateMachine, ITickable, IStartable, IFixedTickable
    {
        public readonly IState IdleState;
        public readonly IState MoveState;
        public readonly IState AttackState;

        public PlayerStateMachine(PlayerInput playerInput, Rigidbody2D rigidbody2D)
        {
            IdleState = new PlayerIdleState(1, this, playerInput, rigidbody2D);
            MoveState = new PlayerMoveState(2, this, playerInput, rigidbody2D);
            AttackState = new PlayerAttackState(3, this, playerInput, rigidbody2D);
        }

        void IStartable.Start()
        {
            Initialize(IdleState);
        }

        void ITickable.Tick()
        {
            UpdateState(Time.deltaTime);
        }
        void IFixedTickable.FixedTick()
        {
            FixedUpdateState(Time.fixedDeltaTime);
        }

    }
}