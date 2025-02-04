using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenuModel
{
    private readonly AudioMixer _audioMixer;

    public bool IsVolumeMuted { get; private set; }

    public int VolumeMaxValue { get; }

    public int VolumeCurrentValue => GetVolume();
    private int _volumeCurrentValue;

    public SettingsMenuModel(AudioMixer audioMixer, bool isMute, int VolumeMax)
    {
        _audioMixer = audioMixer;
        IsVolumeMuted = isMute;
        VolumeMaxValue = VolumeMax;
    }

    public void SetVolumeMute(bool value)
    {
        if (value == true)
        {
            _ = _audioMixer.SetFloat("MasterVolume", -80);
            IsVolumeMuted = true;
        }
        else
        {
            _ = _audioMixer.SetFloat("MasterVolume", GetLogarithmicCurrentVolume());
            IsVolumeMuted = false;
        }
    }

    public int GetVolume()
    {
        _ = _audioMixer.GetFloat("MasterVolume", out float currentVolume);
        float _volumeCurrentValueFloat = Mathf.Pow(10, currentVolume / 20);
        _volumeCurrentValue = Mathf.RoundToInt(_volumeCurrentValueFloat * VolumeMaxValue);

        return _volumeCurrentValue;
    }

    public void SetVolume(int value)
    {
        _volumeCurrentValue = Mathf.Clamp(value, 0, VolumeMaxValue);
        _ = _audioMixer.SetFloat("MasterVolume", GetLogarithmicCurrentVolume());
    }

    public void ChangeVolume(int deltaVolume)
    {
        int newVolume = _volumeCurrentValue + deltaVolume;

        _volumeCurrentValue = Mathf.Clamp(newVolume, 0, VolumeMaxValue);

        if (IsVolumeMuted == false)
        {
            _ = _audioMixer.SetFloat("MasterVolume", GetLogarithmicCurrentVolume());
        }
    }

    private float GetLogarithmicCurrentVolume()
    {
        float linearValue = (float)_volumeCurrentValue / VolumeMaxValue;

        return Mathf.Log10(Mathf.Clamp(linearValue, 0.0001f, 1)) * 20;
    }
}
