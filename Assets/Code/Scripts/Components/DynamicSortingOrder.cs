using UnityEngine;

namespace IntoTheWilds
{
    public class DynamicSortingOrder : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void FixedUpdate()
        {
            _spriteRenderer.sortingOrder = -(int)(gameObject.transform.position.y * 1000);
        }
    }
}
