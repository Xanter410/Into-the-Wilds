using UnityEngine;

namespace IntoTheWilds
{
    public class TreePresenter : MonoBehaviour
    {
        [SerializeField] private Sprite _deadSprite;
        private Animator _animator;

        private static readonly string _animationIdleStateName = "Idle";

        private void Start()
        {
            _animator = GetComponent<Animator>();

            float randomOffset = Random.Range(0f, 1f);

            _animator.Play(_animationIdleStateName, 0, randomOffset);
        }

        public void OnTreeDead()
        {
            _animator.enabled = false;

            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = _deadSprite;
        }

        public void TakeHit()
        {
            _animator.SetTrigger("takeHit");
        }
    }
}
