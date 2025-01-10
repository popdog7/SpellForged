using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseSO : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemSO[] items;
    public Dictionary<int, ItemSO> get_item = new Dictionary<int, ItemSO>();

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].id = i;
            get_item.Add(i, items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        get_item = new Dictionary<int, ItemSO>();
    }
}
