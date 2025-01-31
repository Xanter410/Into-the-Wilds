using IntoTheWilds;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuPresenter : MonoBehaviour
{
    private UIDocument _uiDocument;
    private PlayAudioEffectComponent _audioEffectComponent;

    private Button _startButton;
    private Button _settingsButton;
    private Button _exitButton;

    private SettingsMenuPresenter _settingsPresenter;
    private VisualElement _settingsMenu;
    private bool _isSettingsMenuOpen;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        _audioEffectComponent = GetComponent<PlayAudioEffectComponent>();

        var root = _uiDocument.rootVisualElement;

        _startButton = root.Q<Button>("Button_Play");
        _settingsButton = root.Q<Button>("Button_Settings");
        _exitButton = root.Q<Button>("Button_Exit");

        _settingsPresenter = GetComponent<SettingsMenuPresenter>();
        _settingsMenu = root.Q<VisualElement>("Settings");
    }

    private void OnEnable()
    {
        _startButton.clicked += StartButtonClicked;
        _settingsButton.clicked += SettingsButtonClicked;
        _exitButton.clicked += ExitButtonClicked;

        _settingsMenu.SetEnabled(false);
        _isSettingsMenuOpen = false;
    }

    private void OnDisable()
    {
        _startButton.clicked -= StartButtonClicked;
        _settingsButton.clicked -= SettingsButtonClicked;
        _exitButton.clicked -= ExitButtonClicked;
    }

    private void StartButtonClicked()
    {
        _audioEffectComponent.PlayShotAudio();
        SceneTransition.SwitchToScene(1);
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

    private void ExitButtonClicked()
    {
        _audioEffectComponent.PlayShotAudio();
        Application.Quit();
    }
}
