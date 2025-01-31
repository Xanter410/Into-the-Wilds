using System;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenuModel
{
    private AudioMixer _audioMixer;

    public bool IsVolumeMuted => _isVolumeMuted;
    private bool _isVolumeMuted;

    public int VolumeMaxValue => _volumeMaxValue;
    private int _volumeMaxValue;

    public int VolumeCurrentValue => GetVolume();
    private int _volumeCurrentValue;

    public SettingsMenuModel(AudioMixer audioMixer, bool isMute, int VolumeMax)
    {
        _audioMixer = audioMixer;
        _isVolumeMuted = isMute;
        _volumeMaxValue = VolumeMax;

        //_ = GetVolume();
    }

    public void SetVolumeMute(bool value)
    {
        if (value == true)
        {
            _audioMixer.SetFloat("MasterVolume", -80);
            _isVolumeMuted = true;
        }
        else
        {
            _audioMixer.SetFloat("MasterVolume", GetLogarithmicCurrentVolume());
            _isVolumeMuted = false;
        }
    }

    public int GetVolume()
    {
        _audioMixer.GetFloat("MasterVolume", out float currentVolume);
        var _volumeCurrentValueFloat = Mathf.Pow(10, currentVolume / 20);
        _volumeCurrentValue = Mathf.RoundToInt(_volumeCurrentValueFloat * _volumeMaxValue);

        return _volumeCurrentValue;
    }

    public void SetVolume(int value)
    {
        _volumeCurrentValue = Mathf.Clamp(value, 0, _volumeMaxValue);
        _audioMixer.SetFloat("MasterVolume", GetLogarithmicCurrentVolume());
    }

    public void ChangeVolume(int deltaVolume)
    {
        int newVolume = _volumeCurrentValue + deltaVolume;

        _volumeCurrentValue = Mathf.Clamp(newVolume, 0, _volumeMaxValue);

        if (_isVolumeMuted == false)
        {
            _audioMixer.SetFloat("MasterVolume", GetLogarithmicCurrentVolume());
        }
    }

    private float GetLogarithmicCurrentVolume()
    {
        float linearValue = (float)_volumeCurrentValue / _volumeMaxValue;

        return Mathf.Log10(Mathf.Clamp(linearValue, 0.0001f, 1)) * 20;
    }
}
