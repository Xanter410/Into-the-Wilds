using UnityEngine;

namespace IntoTheWilds
{
    public class PlayAudioAmbient : MonoBehaviour
    {
        [SerializeField] protected AudioSource _audioSourceAmbient;
        [SerializeField] protected AudioClip _audioClip;
        [SerializeField, Range(0f, 1f)] protected float _volumeClips = 1f;

        private void Start()
        {
            _audioSourceAmbient.PlayOneShot(_audioClip, _volumeClips);
        }
    }
}
