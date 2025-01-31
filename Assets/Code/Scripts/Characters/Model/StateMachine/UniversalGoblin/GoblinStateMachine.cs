using Tools.StateMachine;
using UnityEngine;
using VContainer.Unity;

namespace IntoTheWilds
{
    public class GoblinStateMachine : StateMachine, ITickable, IStartable, IFixedTickable
    {
        public IState IdleState;
        public IState MoveState;
        public IState AttackState;
        public IState DeadState;

        public GoblinStateMachine(IMove goblinInputMove, IAttack goblinInputAttack, Rigidbody2D rigidbody2D, HealthComponent healthComponent)
        {
            IdleState = new GoblinIdleState(1, this, goblinInputMove, goblinInputAttack, rigidbody2D);
            MoveState = new GoblinMoveState(2, this, goblinInputMove, goblinInputAttack, rigidbody2D);
            AttackState = new GoblinAttackState(3, this, goblinInputAttack, rigidbody2D);
            DeadState = new GoblinDeadState(4, this, rigidbody2D, healthComponent);
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
