using UnityEngine;

public class PlayAudioSceneTransition : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClipOpening;
    [SerializeField, Range(0f, 1f)] protected float _volumeOpening = 1f;
    [SerializeField] private AudioClip _audioClipEnding;
    [SerializeField, Range(0f, 1f)] protected float _volumeEnding = 1f;

    public void PlayClipOpening()
    {
        _audioSource.PlayOneShot(_audioClipOpening, _volumeOpening);
    }

    public void PlayClipEnding()
    {
        _audioSource.PlayOneShot(_audioClipEnding, _volumeEnding);
    }
}