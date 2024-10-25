using Tools.StateMachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace IntoTheWilds
{
    public class SheepLifetimeScope : LifetimeScope
    {
        [SerializeField] private Rigidbody2D _unitRigidbody2D;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_unitRigidbody2D);

            builder.UseEntryPoints(entryPoints =>
            {
                entryPoints.Add<SheepAI>().AsSelf();
                entryPoints.Add<SheepStateMachine>().As<StateMachine>();
            });
        }
    }
}
