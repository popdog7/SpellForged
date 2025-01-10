using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseSO : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemSO[] items;
    public Dictionary<ItemSO, int> get_id = new Dictionary<ItemSO, int>();
    public Dictionary<int, ItemSO> get_item = new Dictionary<int, ItemSO>();

    public void OnAfterDeserialize()
    {
        get_id = new Dictionary<ItemSO, int>();
        get_item = new Dictionary<int, ItemSO>();
        for (int i = 0; i < items.Length; i++)
        {
            get_id.Add(items[i], i);
            get_item.Add(i, items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        
    }
}
