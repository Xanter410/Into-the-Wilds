using UnityEngine;

namespace IntoTheWilds
{
    public class DestroyComponent : MonoBehaviour
    {
        private float _timeBeforeDestroy;
        private bool _destroyed;

        private void Update()
        {
            if (_destroyed == true)
            {
                if (_timeBeforeDestroy > 0)
                {
                    _timeBeforeDestroy -= Time.deltaTime;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }

        public void DestroyWithTimer(float delayBeforeDestroy)
        {
            _destroyed = true;
            _timeBeforeDestroy = delayBeforeDestroy;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
