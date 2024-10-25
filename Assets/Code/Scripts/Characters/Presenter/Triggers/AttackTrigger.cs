using UnityEngine;

namespace IntoTheWilds
{
    public class AttackTrigger : MonoBehaviour
    {
        [SerializeField] private float _timeActive = 0.1f;
        [SerializeField] private int _damagePower = 1;
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
            HealthComponent healthComponent;

            if (collision.gameObject.TryGetComponent<HealthComponent>(out healthComponent))
            {
                healthComponent.Decrement(_damagePower);
            }
        }
    }
}
