using IntoTheWilds;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class SettingsMenuPresenter : MonoBehaviour
{
    private UIDocument _uiDocument;
    private PlayAudioEffectComponent _audioEffectComponent;

    private Button _volumeButton;
    private Button _plusButton;
    private Button _minusButton;
    
    private VisualElement _volumeIcon;
    private static string _volumeIconStyleOn = "volumeIconOn";
    private static string _volumeIconStyleOff = "volumeIconOff";

    private Label _volumeLabel;

    private SettingsMenuModel _settingsMenuModel;
    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] private bool _isVolumeMuted = false;
    [SerializeField] private int _volumeMaxValue;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        _audioEffectComponent = GetComponent<PlayAudioEffectComponent>();

        var root = _uiDocument.rootVisualElement;

        _volumeButton = root.Q<Button>("Settings_Button_Volume");
        _plusButton = root.Q<Button>("Settings_Button_Plus");
        _minusButton = root.Q<Button>("Settings_Button_Minus");

        _volumeLabel = root.Q<Label>("Volume_Label");

        _volumeIcon = root.Q<VisualElement>("Volume_Icon");

        _settingsMenuModel = new SettingsMenuModel(
            _audioMixer, 
            _isVolumeMuted, 
            _volumeMaxValue);
    }

    private void OnEnable()
    {
        _volumeButton.clicked += VolumeButtonClicked;
        _plusButton.clicked += PlusButtonClicked;
        _minusButton.clicked += MinusButtonClicked;
    }

    private void OnDisable()
    {
        _volumeButton.clicked -= VolumeButtonClicked;
        _plusButton.clicked -= PlusButtonClicked;
        _minusButton.clicked -= MinusButtonClicked;
    }

    public void MenuOpen()
    {
        UpdateVolumeLable();
    }

    private void VolumeButtonClicked()
    {
        _audioEffectComponent.PlayShotAudio();
        if (_isVolumeMuted == true)
        {
            SetMute(false);
        }
        else
        {
            SetMute(true);
        }
    }

    private void SetMute(bool value)
    {
        _settingsMenuModel.SetVolumeMute(value);
        _isVolumeMuted = value;

        if (_isVolumeMuted == true)
        {
            _volumeIcon.RemoveFromClassList(_volumeIconStyleOn);
            _volumeIcon.AddToClassList(_volumeIconStyleOff);
        }
        else
        {
            _volumeIcon.RemoveFromClassList(_volumeIconStyleOff);
            _volumeIcon.AddToClassList(_volumeIconStyleOn);
        }
    }

    private void PlusButtonClicked()
    {
        SetMute(false);
        _settingsMenuModel.ChangeVolume(1);
        UpdateVolumeLable();
        _audioEffectComponent.PlayShotAudio();
    }

    private void MinusButtonClicked()
    {
        SetMute(false);
        _settingsMenuModel.ChangeVolume(-1);
        UpdateVolumeLable();
        _audioEffectComponent.PlayShotAudio();
    }

    private void UpdateVolumeLable()
    {
        _volumeLabel.text = $"{_settingsMenuModel.VolumeCurrentValue}";
    }
}
