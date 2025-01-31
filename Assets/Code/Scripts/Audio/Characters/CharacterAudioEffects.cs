using UnityEngine;

namespace IntoTheWilds
{
    public class CharacterAudioEffects : MonoBehaviour
    {
        [SerializeField] protected AudioSource _audioSourceEffects;
        [SerializeField] protected AudioClip[] _audioClipMove;
        [SerializeField, Range(0f, 1f)] protected float _volumeMove = 1f;
        [SerializeField] protected AudioClip[] _audioClipAttack;
        [SerializeField, Range(0f, 1f)] protected float _volumeAttack = 1f;
        [SerializeField] protected AudioClip[] _audioClipTakeHit;
        [SerializeField, Range(0f, 1f)] protected float _volumeTakeHit = 1f;
        [SerializeField] protected AudioClip _audioClipDead;
        [SerializeField, Range(0f, 1f)] protected float _volumeDead = 1f;

        private AudioClip RandomClip(AudioClip[] audioClips)
        {
            return audioClips[Random.Range(0, audioClips.Length)];
        }

        public void PlayShotAudioMove()
        {
            _audioSourceEffects.PlayOneShot(RandomClip(_audioClipMove), _volumeMove);
        }

        public void PlayShotAudioAttack()
        {
            _audioSourceEffects.PlayOneShot(RandomClip(_audioClipAttack), _volumeAttack);
        }

        public void PlayShotAudioTakeHit()
        {
            _audioSourceEffects.PlayOneShot(RandomClip(_audioClipTakeHit), _volumeTakeHit);
        }

        public void PlayShotAudioDead()
        {
            _audioSourceEffects.PlayOneShot(_audioClipDead, _volumeDead);
        }
    }
}
