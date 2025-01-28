using IntoTheWilds.UI;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;


namespace IntoTheWilds
{
    [CreateAssetMenu(fileName = "PlayerInputAction", menuName = "InputPresenter/PlayerInputAction")]
    public class PlayerInput : ScriptableObject, IStunble
    {
        private InputSystem_Actions _inputActions;
        private event Action _attackPressed;

        private InventoryHud _inventoryHud;
        private GameMenuPresenter _gameMenuHud;

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
        public void Constuct(InventoryHud inventoryHud, GameMenuPresenter gameMenuHud)
        {
            _inventoryHud = inventoryHud;
            _gameMenuHud = gameMenuHud;

            // TODO: Временное решение проблемы.
            // После перезагрузки уровня (например, в случае смерти персонажа)
            // метод OnEnable не проигрывается снова.
            // _inputActions остается актуальным как и подписки,
            // а вот _inputActions.Player.Enable() не вызывается
            // (т.к. он был выключен в другом месте перед перезагрузкой)
            _inputActions.Player.Enable(); 
        }

        public Vector2 RetrieveMoveInput()
        {
            return _inputActions.Player.Move.ReadValue<Vector2>();
        }

        private void AttackPressed(InputAction.CallbackContext _)
        {
            if (_inventoryHud.IsUiUnderPointer() || _gameMenuHud.IsUiUnderPointer())
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

        void IStunble.Stun(bool isStunned)
        {
            // TODO: Заглушка.
            // В данный момент не предполагается
            // возможность застанить игрока
            return; 
        }

        public void SetEnabled(bool value)
        {
            if (value)
            {
                _inputActions.Player.Enable();
            }
            else
            {
                _inputActions.Player.Disable();
            }
        }
    }
}
