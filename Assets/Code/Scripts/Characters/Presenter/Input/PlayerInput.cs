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

            // TODO: ��������� ������� ��������.
            // ����� ������������ ������ (��������, � ������ ������ ���������)
            // ����� OnEnable �� ������������� �����.
            // _inputActions �������� ���������� ��� � ��������,
            // � ��� _inputActions.Player.Enable() �� ����������
            // (�.�. �� ��� �������� � ������ ����� ����� �������������)
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
            // TODO: ��������.
            // � ������ ������ �� ��������������
            // ����������� ��������� ������
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
