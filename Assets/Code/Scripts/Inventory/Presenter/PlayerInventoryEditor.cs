using IntoTheWilds.Inventory;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

#if UNITY_EDITOR

[CustomEditor(typeof(PlayerInventory))]
public class PlayerInventoryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PlayerInventory playerInventory = (PlayerInventory)target;

        if (GUILayout.Button("Inventory In Console"))
        {
            playerInventory.InventoryToConsole();
        }

        if (GUILayout.Button("ThrowOut Slot - 0"))
        {
            playerInventory.ThrowOutInt(0);
        }

        if (GUILayout.Button("ThrowOut Slot - 1"))
        {
            playerInventory.ThrowOutSlot(1);
        }

        if (GUILayout.Button("ThrowOut Slot - 2"))
        {
            playerInventory.ThrowOutInt(2);
        }
    }
}

#endif