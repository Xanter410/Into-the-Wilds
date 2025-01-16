using Tools.StateMachine;
using UnityEngine;
using VContainer.Unity;

namespace IntoTheWilds
{
    public class GoblinTorchStateMachine : StateMachine, ITickable, IStartable, IFixedTickable
    {
        public IState IdleState;
        public IState MoveState;
        public IState AttackState;

        public GoblinTorchStateMachine(GoblinTorchAI sheepInput, Rigidbody2D rigidbody2D)
        {
            IdleState = new GoblinTorchIdleState(1, this, sheepInput, rigidbody2D);
            MoveState = new GoblinTorchMoveState(2, this, sheepInput, rigidbody2D);
            AttackState = new GoblinTorchAttackState(3, this, sheepInput, rigidbody2D);
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
