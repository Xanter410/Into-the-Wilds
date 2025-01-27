using UnityEngine;

namespace IntoTheWilds
{
    public class StaticSortingOrderComponent : MonoBehaviour
    {
        [SerializeField] private float _offsetY = 0f;

        private void Start()
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

            spriteRenderer.sortingOrder = -(int)((gameObject.transform.position.y + _offsetY) * 1000);
        }
    }
}
