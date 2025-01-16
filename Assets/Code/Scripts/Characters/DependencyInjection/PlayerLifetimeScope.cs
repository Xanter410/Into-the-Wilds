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

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_playerRigidbody2D);

            builder.Register<PlayerInventory>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<InventoryHud>();

            builder.RegisterComponent(_playerInput);

            builder.UseEntryPoints(entryPoints =>
            {
                entryPoints.Add<PlayerStateMachine>().As<StateMachine>();
            });
        }
    }
}

