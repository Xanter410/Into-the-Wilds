using System.Collections;
using UnityEngine.Audio;
using UnityEngine;
public static class FadeMixerGroup
{
    /// <summary>
    /// Метод создает эффект плавного перехода от текущей громкости к указанной.
    /// </summary>
    public static IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
    {
        float currentTime = 0;

        _ = audioMixer.GetFloat(exposedParam, out float currentVol);

        currentVol = Mathf.Pow(10, currentVol / 20);

        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            _ = audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }

        yield break;
    }
}