using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseSO : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemSO[] item_objecs;

    public void updateId()
    {
        for (int i = 0; i < item_objecs.Length; i++)
        {
            if (item_objecs[i].data.id != i)
                item_objecs[i].data.id = i;
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
