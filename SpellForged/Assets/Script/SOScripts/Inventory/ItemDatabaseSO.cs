using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseSO : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemSO[] items;

    public void updateId()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].data.id != i)
                items[i].data.id = i;
        }
    }

    public void OnAfterDeserialize()
    {
        updateId();
    }

    public void OnBeforeSerialize()
    {
    }
}
