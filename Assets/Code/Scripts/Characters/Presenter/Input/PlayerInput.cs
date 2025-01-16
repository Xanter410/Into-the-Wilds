using IntoTheWilds.UI;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;


namespace IntoTheWilds
{
    [CreateAssetMenu(fileName = "PlayerInputAction", menuName = "InputPresenter/PlayerInputAction")]
    public class PlayerInput : ScriptableObject
    {
        private InputSystem_Actions _inputActions;
        private event Action _attackPressed;

        private InventoryHud _inventoryHud;

        private void OnEnable()
        {
            _inputActions = new InputSystem_Actions();

            _inputActions.Player.Attack.started += AttackPressed;

            _inputActions.Player.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Player.Attack.started -= AttackPressed;

            _inputActions.Player.Disable();
            _inputActions = null;
        }

        [Inject]
        public void Constuct(InventoryHud inventoryHud)
        {
            _inventoryHud = inventoryHud;
        }

        public Vector2 RetrieveMoveInput()
        {
            return _inputActions.Player.Move.ReadValue<Vector2>();
        }

        private void AttackPressed(InputAction.CallbackContext _)
        {
            if (_inventoryHud.IsUiUnderPointer())
            {
                return;
            }

            _attackPressed?.Invoke();
        }

        public void RegisterCallbackAttack(Action callbackHandler)
        {
            _attackPressed += callbackHandler;
        }

        public void UnRegisterCallbackAttack(Action callbackHandler)
        {
            _attackPressed -= callbackHandler;
        }
    }
}
