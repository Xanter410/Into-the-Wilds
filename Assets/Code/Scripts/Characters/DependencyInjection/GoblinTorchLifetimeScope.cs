using Tools.StateMachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace IntoTheWilds
{
    public class GoblinTorchLifetimeScope : LifetimeScope
    {
        [SerializeField] private Rigidbody2D _unitRigidbody2D;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_unitRigidbody2D);

            builder.UseEntryPoints(entryPoints =>
            {
                entryPoints.Add<GoblinTorchAI>().As<IMove, IAttack>();

                entryPoints.Add<GoblinStateMachine>().As<StateMachine>();
            });
        }
    }
}