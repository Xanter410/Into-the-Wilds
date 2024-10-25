using Tools.StateMachine;
using UnityEngine;
using VContainer.Unity;

namespace IntoTheWilds
{
    public class SheepStateMachine : StateMachine, ITickable, IStartable, IFixedTickable
    {
        public IState IdleState;
        public IState MoveState;

        public SheepStateMachine(SheepAI sheepInput, Rigidbody2D rigidbody2D)
        {
            IdleState = new SheepIdleState(1, this, sheepInput, rigidbody2D);
            MoveState = new SheepMoveState(2, this, sheepInput, rigidbody2D);
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
