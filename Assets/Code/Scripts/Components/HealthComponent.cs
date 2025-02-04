using System;
using UnityEngine;
using UnityEngine.Events;

namespace IntoTheWilds
{
    public class HealthComponent : MonoBehaviour
    {
        public UnityEvent ObjectDied;
        public UnityEvent ObjectTakeHit;
        public Action<int> HealthChanged;

        [field: SerializeField] public int MaxHealthPoints { get; private set; }

        [HideInInspector] public int HealthPoints { get; private set; }

        private bool _isAlreadyDead = false;

        private void OnEnable()
        {
            HealthPoints = MaxHealthPoints;
        }

        public void Increment(int addHealthPoint)
        {
            if (_isAlreadyDead == true)
            {
                return;
            }

            if (HealthPoints + addHealthPoint < MaxHealthPoints)
            {
                HealthPoints += addHealthPoint;
            }
            else
            {
                HealthPoints = MaxHealthPoints;
            }

            HealthChanged?.Invoke(HealthPoints);
        }

        public void Decrement(int removeHealthPoint)
        {
            if (_isAlreadyDead == true)
            {
                return;
            }

            if (HealthPoints - removeHealthPoint > 0)
            {
                HealthPoints -= removeHealthPoint;

                ObjectTakeHit?.Invoke();
            }
            else
            {
                HealthPoints = 0;

                ObjectDied?.Invoke();
                _isAlreadyDead = true;
            }

            HealthChanged?.Invoke(HealthPoints);
        }
    }
}