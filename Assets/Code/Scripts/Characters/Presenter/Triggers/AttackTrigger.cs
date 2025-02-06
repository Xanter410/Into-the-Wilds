using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace IntoTheWilds
{
    public class AttackTrigger : MonoBehaviour
    {
        [SerializeField] private LayerMask _triggerLayers;

        [SerializeField] private float _timeActive = 0.1f;
        [SerializeField] private int _damagePower = 1;
        [SerializeField] private GameObject _parent;
        private float _currentTimeActive;

        private void OnEnable()
        {
            _currentTimeActive = _timeActive;
        }

        private void OnDisable()
        {
            _currentTimeActive = 0;
        }

        private void Update()
        {
            _currentTimeActive -= Time.deltaTime;

            if (_currentTimeActive <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ((_triggerLayers & (1 << collision.gameObject.layer)) != 0)
            {
                if (collision.gameObject.TryGetComponent(out HealthComponent healthComponent))
                {
                    healthComponent.Decrement(_damagePower);
                }

                if (collision.gameObject.TryGetComponent(out HitAndStunHandler hitAndStunHandler))
                {
                    hitAndStunHandler.ApplyHitAndStun(_parent.transform.position);
                }
            }
        }
    }
}
