using UnityEngine;

namespace IntoTheWilds
{
    public class TreePresenter : MonoBehaviour
    {
        [SerializeField] private Sprite _deadSprite;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
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
