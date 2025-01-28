using Tools.BehaviorTree;
using Tools.StateMachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace IntoTheWilds
{
    public class GoblinTNTLifetimeScope : LifetimeScope
    {
        [SerializeField] private Rigidbody2D _unitRigidbody2D;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_unitRigidbody2D);

            builder.UseEntryPoints(entryPoints =>
            {
                entryPoints.Add<GoblinTntAI>().As<IMove, IAttack, BehaviorTree>();

                entryPoints.Add<GoblinStateMachine>().As<StateMachine>();
            });
        }
    }
}
