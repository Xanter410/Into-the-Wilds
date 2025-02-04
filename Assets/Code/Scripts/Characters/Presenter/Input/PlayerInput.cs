using IntoTheWilds.UI;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace IntoTheWilds
{
    [CreateAssetMenu(fileName = "PlayerInputAction", menuName = "InputPresenter/PlayerInputAction")]
    public class PlayerInput : ScriptableObject, IStunble, IAttack, IMove
    {
        private InputSystem_Actions _inputActions;
        public event Action AttackPressed;

        private InventoryHud _inventoryHud;
        private GameMenuPresenter _gameMenuHud;

        private void OnEnable()
        {
            _inputActions = new InputSystem_Actions();

            _inputActions.Player.Attack.started += InputAction_AttackStarted;

            _inputActions.Player.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Player.Attack.started -= InputAction_AttackStarted;

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

        void IMove.SetMoveInput(Vector2 _)
        {
            
        }

        Vector2 IMove.RetrieveMoveInput()
        {
            return _inputActions.Player.Move.ReadValue<Vector2>();
        }

        void IAttack.OnAttackPressed()
        {
            if (_inventoryHud.IsUiUnderPointer() || _gameMenuHud.IsUiUnderPointer())
            {
                return;
            }

            AttackPressed?.Invoke();
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

        private void InputAction_AttackStarted(InputAction.CallbackContext _)
        {
            ((IAttack)this).OnAttackPressed();
        }
    }
}
