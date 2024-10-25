using System.Collections.Generic;
using UnityEngine;
using Tools;

namespace IntoTheWilds
{
    public class DropComponent : MonoBehaviour
    {
        [SerializeField] private List<DropItem> dropItems = new();

        [Range(0, 100)]
        public int minDropAmount = 1;  // Минимальное количество выпадаемых предметов
        [Range(0, 100)]
        public int maxDropAmount = 3;  // Максимальное количество выпадаемых предметов

        public void DropItems()
        {
            List<GameObject> droppedItems = new();

            int totalDropped = 0;

            while (totalDropped < maxDropAmount)
            {
                dropItems.Shuffle();

                foreach (DropItem dropItem in dropItems)
                {
                    if (Random.value <= dropItem.DropChance)
                    {
                        int amountToDrop = Random.Range(dropItem.minAmount, dropItem.maxAmount + 1);

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
                        droppedItems.Add(Instantiate(dropItem.ItemPrefab, transform.position, Quaternion.identity));
                        totalDropped++;
                        //}
                    }

                    if (totalDropped >= maxDropAmount)
                    {
                        break;
                    }
                }

                if (totalDropped < minDropAmount)
                {
                    continue;
                }

                if (totalDropped >= minDropAmount)
                {
                    break;
                }
            }
        }
    }
}
