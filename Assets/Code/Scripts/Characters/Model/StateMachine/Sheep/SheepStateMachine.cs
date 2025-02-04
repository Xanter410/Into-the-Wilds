using Tools.StateMachine;
using UnityEngine;
using VContainer.Unity;

namespace IntoTheWilds
{
    public class SheepStateMachine : StateMachine, ITickable, IStartable, IFixedTickable
    {
        public readonly IState IdleState;
        public readonly IState MoveState;
        public readonly IState DeadState;

        public SheepStateMachine(SheepAI sheepInput, Rigidbody2D rigidbody2D, HealthComponent healthComponent)
        {
            IdleState = new SheepIdleState(1, this, sheepInput, rigidbody2D);
            MoveState = new SheepMoveState(2, this, sheepInput, rigidbody2D);
            DeadState = new SheepDeadState(3, this, rigidbody2D, healthComponent);
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
