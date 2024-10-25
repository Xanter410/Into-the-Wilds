using UnityEngine;
using UnityEngine.Events;

namespace IntoTheWilds
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] 
        private int _maxHealthPoints;

        [HideInInspector]
        public int HealthPoints { get; private set; }

        public UnityEvent _onDead;
        public UnityEvent _onTakeHit;

        private void Start()
        {
            HealthPoints = _maxHealthPoints;
        }

        public void Increment(int addHealthPoint)
        {
            if (HealthPoints + addHealthPoint < _maxHealthPoints)
            {
                HealthPoints += addHealthPoint;
            }
            else
            {
                HealthPoints = _maxHealthPoints;
            }
        }

        public void Decrement(int removeHealthPoint)
        {
            if (HealthPoints - removeHealthPoint > 0)
            {
                HealthPoints -= removeHealthPoint;

                _onTakeHit?.Invoke();
            }
            else
            {
                HealthPoints = 0;

                _onDead?.Invoke();
            }
        }
    }
}