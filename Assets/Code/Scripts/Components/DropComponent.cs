using System.Collections.Generic;
using UnityEngine;
using Tools;

namespace IntoTheWilds
{
    public class DropComponent : MonoBehaviour
    {
        [SerializeField] private List<DropItem> dropItems = new();

        [Range(0, 100)]
        public int minDropAmount = 1;  // ����������� ���������� ���������� ���������
        [Range(0, 100)]
        public int maxDropAmount = 3;  // ������������ ���������� ���������� ���������

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

                        // TODO: ����� ���������� ��������, ��� ����� amountToDrop ��������� ���������� ��������� � �����,
                        //       � �� ���������� ����������� ����������� �������.
                        //
                        //       ������ ���� ��� ��� ������ �������� ���������
                        //
                        //       dropItem.ItemPrefab.amount = amountToDrop.
                        //       
                        //       ��� � ������ ������������� �������:
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
