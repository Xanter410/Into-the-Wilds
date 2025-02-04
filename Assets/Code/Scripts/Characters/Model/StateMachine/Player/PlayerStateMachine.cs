using UnityEngine;
using Tools.StateMachine;
using VContainer.Unity;

namespace IntoTheWilds
{
    public class PlayerStateMachine : StateMachine, ITickable, IStartable, IFixedTickable
    {
        public readonly IState IdleState;
        public readonly IState MoveState;
        public readonly IState AttackState;
        public readonly IState DeadState;

        public PlayerStateMachine(PlayerInput playerInput, IAttack playerInputAttack, IMove playerInputMove, Rigidbody2D rigidbody2D, HealthComponent healthComponent)
        {
            IdleState = new PlayerIdleState(1, this, playerInputAttack, playerInputMove, rigidbody2D);
            MoveState = new PlayerMoveState(2, this, playerInputAttack, playerInputMove, rigidbody2D);
            AttackState = new PlayerAttackState(3, this, playerInputAttack, rigidbody2D);
            DeadState = new PlayerDeadState(4, this, playerInput, rigidbody2D, healthComponent);
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