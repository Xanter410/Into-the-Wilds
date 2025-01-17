using System;
using UnityEngine;
using UnityEngine.Events;

namespace IntoTheWilds
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField]
        private int maxHealthPoints;

        public int MaxHealthPoints
        {
            get => maxHealthPoints;
            private set => maxHealthPoints = value;
        }

        [HideInInspector]
        public int HealthPoints { get; private set; }

        public UnityEvent _onDead;
        public UnityEvent _onTakeHit;
        public Action<int> _onHealthChanged;

        private void OnEnable()
        {
            HealthPoints = MaxHealthPoints;
        }

        public void Increment(int addHealthPoint)
        {
            if (HealthPoints + addHealthPoint < MaxHealthPoints)
            {
                HealthPoints += addHealthPoint;
            }
            else
            {
                HealthPoints = MaxHealthPoints;
            }

            _onHealthChanged?.Invoke(HealthPoints);
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

            _onHealthChanged?.Invoke(HealthPoints);
        }
    }
}