using IntoTheWilds;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class GameMenuPresenter : MonoBehaviour
{
    private UIDocument _uiDocument;
    private PlayAudioClipsComponent _audioEffectComponent;

    private Button _continueButton;
    private Button _settingsButton;
    private Button _exitToMainMenuButton;

    private SettingsMenuPresenter _settingsPresenter;
    private VisualElement _settingsMenu;
    private bool _isSettingsMenuOpen;

    private VisualElement _gameMenu;
    private InputSystem_Actions _inputActions;
    private bool _isGameMenuOpen;

    private VisualElement _root;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        _audioEffectComponent = GetComponent<PlayAudioClipsComponent>();

        VisualElement root = _uiDocument.rootVisualElement;
        _root = root;
        _continueButton = root.Q<Button>("Button_Play");
        _settingsButton = root.Q<Button>("Button_Settings");
        _exitToMainMenuButton = root.Q<Button>("Button_Exit");

        _settingsPresenter = GetComponent<SettingsMenuPresenter>();
        _settingsMenu = root.Q<VisualElement>("Settings");
        _gameMenu = root.Q<VisualElement>("Menu");
    }

    private void OnEnable()
    {
        CallbackRegisterOnOff(true);

        _settingsMenu.SetEnabled(false);
        _isSettingsMenuOpen = false;

        _inputActions = new InputSystem_Actions();
        _inputActions.Player.Escape.started += ExitToMainMenuButtonClicked;
        _inputActions.Player.Enable();

        SetVisible(false);
    }

    private void ExitToMainMenuButtonClicked(InputAction.CallbackContext context)
    {
        if (_isGameMenuOpen == true)
        {
            SetVisible(false);
        }
        else
        {
            SetVisible(true);
        }
    }

    private void OnDisable()
    {
        CallbackRegisterOnOff(false);

        _inputActions.Player.Escape.started -= ExitToMainMenuButtonClicked;
        _inputActions.Player.Disable();
    }

    public bool IsUiUnderPointer()
    {
        if ( _isGameMenuOpen == true )
        {
            Vector2 pointerPosition = new(
            Pointer.current.position.value.x,
            Screen.height - Pointer.current.position.value.y);

            if (_gameMenu.worldBound.Contains(pointerPosition))
            {
                return true;
            }
        }

        return false;
    }

    private void SetVisible(bool value)
    {
        if (value == true)
        {
            _root.visible = true;
            _isGameMenuOpen = true;
        }
        else
        {
            _root.visible = false;
            _isGameMenuOpen = false;
        }
    }

    private void CallbackRegisterOnOff(bool value)
    {
        if (value == true)
        {
            _continueButton.clicked += ContinueButtonClicked;
            _settingsButton.clicked += SettingsButtonClicked;
            _exitToMainMenuButton.clicked += ExitToMainMenuButtonClicked;
        }
        else
        {
            _continueButton.clicked -= ContinueButtonClicked;
            _settingsButton.clicked -= SettingsButtonClicked;
            _exitToMainMenuButton.clicked -= ExitToMainMenuButtonClicked;
        }
    }

    private void ContinueButtonClicked()
    {
        _audioEffectComponent.PlayShotAudio();
        SetVisible(false);
    }

    private void SettingsButtonClicked()
    {
        _audioEffectComponent.PlayShotAudio();
        if (_isSettingsMenuOpen == true)
        {
            _settingsMenu.SetEnabled(false);
            _isSettingsMenuOpen = false;
        }
        else
        {
            _settingsPresenter.MenuOpen();
            _settingsMenu.SetEnabled(true);
            _isSettingsMenuOpen = true;
        }
    }

    private void ExitToMainMenuButtonClicked()
    {
        _audioEffectComponent.PlayShotAudio();
        SceneTransition.SwitchToScene(0);
    }
}
