using UnityEngine;

namespace IntoTheWilds
{
    public class DestroyComponent : MonoBehaviour
    {
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
