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
        public int MinDropAmount = 1;  // ����������� ���������� ���������� ���������
        [Range(0, 100)]
        public int MaxDropAmount = 3;  // ������������ ���������� ���������� ���������

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
