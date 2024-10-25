using UnityEngine;

namespace IntoTheWilds
{
    public class StaticSortingOrderComponent : MonoBehaviour
    {
        private void Start()
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

            spriteRenderer.sortingOrder = -(int)(gameObject.transform.position.y * 10);
        }
    }
}
