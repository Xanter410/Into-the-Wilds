using System.Collections.Generic;
using UnityEngine;
using Tools;

namespace IntoTheWilds
{
    public class DropComponent : MonoBehaviour
    {
        [SerializeField] private float _spawnPositionOffsetY = 0f;
        [SerializeField] private List<DropItem> _dropItems = new();

        [Range(0, 100)]
        public int MinDropAmount = 1;  // Минимальное количество выпадаемых предметов
        [Range(0, 100)]
        public int MaxDropAmount = 3;  // Максимальное количество выпадаемых предметов

        public void DropItems()
        {
            List<GameObject> droppedItems = new();

            int totalDropped = 0;

            while (totalDropped < MaxDropAmount)
            {
                _dropItems.Shuffle();

                foreach (DropItem dropItem in _dropItems)
                {
                    if (Random.value <= dropItem.DropChance)
                    {
                        int amountToDrop = Random.Range(dropItem.MinAmount, dropItem.MaxAmount + 1);

                        // TODO: Нужно переделать механику, так чтобы amountToDrop обозначал количество элементов в стаке,
                        //       а не количество заспавненых экземпляров объекта.
                        //
                        //       Пример того как это должно примерно выглядеть
                        //
                        //       dropItem.ItemPrefab.amount = amountToDrop.
                        //       
                        //       Или в случае использования фабрики:
                        //       ItemFabric.Create(ItemPrefab, amountToDrop)
                        //
                        //

                        //for (int i = 0; i < amountToDrop && totalDropped < maxDropAmount; i++)
                        //{
                        Vector3 spawnPoint = new(
                            transform.position.x,
                            transform.position.y + _spawnPositionOffsetY,
                            transform.position.z);

                        droppedItems.Add(Instantiate(dropItem.ItemPrefab, spawnPoint, Quaternion.identity));
                        totalDropped++;
                        //}
                    }

                    if (totalDropped >= MaxDropAmount)
                    {
                        break;
                    }
                }

                if (totalDropped < MinDropAmount)
                {
                    continue;
                }

                if (totalDropped >= MinDropAmount)
                {
                    break;
                }
            }
        }
    }
}
