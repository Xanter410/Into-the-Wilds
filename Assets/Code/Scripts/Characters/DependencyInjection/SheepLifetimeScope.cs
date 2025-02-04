using Tools.StateMachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace IntoTheWilds
{
    public class SheepLifetimeScope : LifetimeScope
    {
        [SerializeField] private Rigidbody2D _unitRigidbody2D;
        [SerializeField] private HealthComponent _healthComponent;

        protected override void Configure(IContainerBuilder builder)
        {
            _ = builder.RegisterComponent(_unitRigidbody2D);
            _ = builder.RegisterComponent(_healthComponent);

            builder.UseEntryPoints(entryPoints =>
            {
                _ = entryPoints.Add<SheepAI>().AsSelf();
                _ = entryPoints.Add<SheepStateMachine>().As<StateMachine>();
            });
        }
    }
}
