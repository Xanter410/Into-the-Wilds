using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuPresenter : MonoBehaviour
{
    private UIDocument _uiDocument;

    private Button _startButton;
    private Button _settingsButton;
    private Button _exitButton;

    private VisualElement _settingsMenu;
    private bool _isSettingsMenuOpen;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();

        var root = _uiDocument.rootVisualElement;

        _startButton = root.Q<Button>("Button_Play");
        _settingsButton = root.Q<Button>("Button_Settings");
        _exitButton = root.Q<Button>("Button_Exit");

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
        SceneTransition.SwitchToScene(1);
    }

    private void SettingsButtonClicked()
    {
        if (_isSettingsMenuOpen == true)
        {
            _settingsMenu.SetEnabled(false);
            _isSettingsMenuOpen = false;
        }
        else
        {
            _settingsMenu.SetEnabled(true);
            _isSettingsMenuOpen = true;
        }
    }

    private void ExitButtonClicked()
    {
        Application.Quit();
    }
}
