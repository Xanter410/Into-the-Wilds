using UnityEngine;

namespace IntoTheWilds
{
    public class PlayAudioEffectComponent : MonoBehaviour
    {
        [SerializeField] protected AudioSource _audioSourceEffects;
        [SerializeField] protected AudioClip[] _audioClips;
        [SerializeField, Range(0f, 1f)] protected float _volumeClips = 1f;

        public void PlayShotAudio()
        {
            _audioSourceEffects.PlayOneShot(RandomClip(_audioClips), _volumeClips);
        }

        private AudioClip RandomClip(AudioClip[] audioClips)
        {
            return audioClips[Random.Range(0, audioClips.Length)];
        }
    }
}
