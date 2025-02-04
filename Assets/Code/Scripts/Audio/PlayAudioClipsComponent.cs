using UnityEngine;

namespace IntoTheWilds
{
    public class PlayAudioClipsComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSourceEffects;
        [SerializeField] private AudioClip[] audioClips;
        [SerializeField, Range(0f, 1f)] private float _volumeClips = 1f;

        public void PlayShotAudio()
        {
            _audioSourceEffects.PlayOneShot(RandomClip(audioClips), _volumeClips);
        }

        private AudioClip RandomClip(AudioClip[] audioClips)
        {
            return audioClips[Random.Range(0, audioClips.Length)];
        }
    }
}
