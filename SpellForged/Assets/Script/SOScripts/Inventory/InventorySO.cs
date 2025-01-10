using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory System/Inventory")]
public class InventorySO : ScriptableObject
{
    public List<InventorySlot> container = new List<InventorySlot>();

    public void AddItem(ItemSO _item, int _amount)
    {
        for( int i =0; i < container.Count; i++)
        {
            if (container[i].item == _item)
            {
                container[i].AddAmount(_amount);
                return;
            }
        }

        container.Add(new InventorySlot(_item, _amount));

    }
}


[System.Serializable]
public class InventorySlot
{
    public ItemSO item;
    public int amount;

    public InventorySlot(ItemSO _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}