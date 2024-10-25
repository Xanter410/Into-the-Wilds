using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace IntoTheWilds
{
    [CreateAssetMenu(fileName = "PlayerInputAction", menuName = "InputPresenter/PlayerInputAction")]
    public class PlayerInput : ScriptableObject
    {
        private InputSystem_Actions _inputActions;
        private event Action _attackPressed;

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

        public Vector2 RetrieveMoveInput()
        {
            return _inputActions.Player.Move.ReadValue<Vector2>();
        }

        private void AttackPressed(InputAction.CallbackContext _)
        {
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
