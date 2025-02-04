using IntoTheWilds.Inventory;
using IntoTheWilds.UI;
using Tools.StateMachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace IntoTheWilds
{
    public class PlayerLifetimeScope : LifetimeScope
    {
        [SerializeField] private Rigidbody2D _playerRigidbody2D;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private HealthComponent _healthComponent;

        protected override void Configure(IContainerBuilder builder)
        {
            _ = builder.RegisterComponent(_playerRigidbody2D);
            _ = builder.RegisterComponent(_healthComponent);

            _ = builder.Register<PlayerInventory>(Lifetime.Singleton);
            _ = builder.RegisterComponentInHierarchy<InventoryHud>();
            _ = builder.RegisterComponentInHierarchy<GameMenuPresenter>();

            _ = builder.RegisterComponent(_playerInput)
                .AsSelf()
                .AsImplementedInterfaces();

            builder.UseEntryPoints(entryPoints =>
            {
                _ = entryPoints.Add<PlayerStateMachine>().As<StateMachine>();
            });
        }
    }
}