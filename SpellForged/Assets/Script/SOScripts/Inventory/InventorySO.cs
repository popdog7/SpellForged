using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory System/Inventory")]
public class InventorySO : ScriptableObject
{
    public string save_path;
    public ItemDatabaseSO database;
    public InterfaceType type;
    public Inventory container;
    public InventorySlot[] get_slots { get { return container.item_slots; } }

    public int EmptySlotCount
    {
        get
        {
            int counter = 0;
            for (int i = 0; i < get_slots.Length; i++)
            {
                if (get_slots[i].item.id <= -1)
                {
                    counter++;
                }
            }
            return counter;
        }
    }
    
    public bool AddItem(item _item, int _amount)
    {
        if(EmptySlotCount <= 0)
        {
            return false;
        }

        InventorySlot slot = findItemonInventory(_item);

        if (!database.item_objecs[_item.id].stackable || slot == null)
        {
            setEmptySlot(_item, _amount);
            return true;
        }

        slot.AddAmount(_amount);
        return true;
    }

    public InventorySlot findItemonInventory(item _item)
    {
        for (int i = 0; i < get_slots.Length; i++)
        {
            if (get_slots[i].item.id == _item.id)
            {
                return get_slots[i];
            }
        }

        return null;
    }

    public InventorySlot setEmptySlot(item _item, int _amount)
    {
        for (int i = 0; i < get_slots.Length; i++)
        {
            if (get_slots[i].item.id <= -1)
            {
                get_slots[i].setSlot(_item, _amount);
                return get_slots[i];
            }
        }
        return null;
    }
    
    public void swapItems(InventorySlot item_1, InventorySlot item_2)
    {

        if (item_2.canPlaceInSlot(item_1.ItemSO) && item_1.canPlaceInSlot(item_2.ItemSO))
        {
            InventorySlot temp = new InventorySlot(item_2.item, item_2.amount);
            item_2.setSlot(item_1.item, item_1.amount);
            item_1.setSlot(temp.item, temp.amount);
        }
        
        
    }

    public void RemoveItem(item _item)
    {
        for (int i = 0; i < get_slots.Length; i++)
        {
            if (get_slots[i].item == _item)
            {
                get_slots[i].setSlot(null, 0);
            }
        }
    }

    [ContextMenu("Save")]
    public void Save()
    {
        string save_data = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, save_path));
        bf.Serialize(file, save_data);
        file.Close();
    }

    [ContextMenu("Load")]
    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, save_path)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, save_path), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }

    [ContextMenu("Clear")]
    public void clear()
    {
        container.clear();
    }
}

public delegate void slotUpdated(InventorySlot _slot);

[System.Serializable]
public class InventorySlot
{
    public ItemType[] allowed_items = new ItemType[0];

    [System.NonSerialized] public UserInterface parent_interface;
    [System.NonSerialized] public GameObject slot;

    [System.NonSerialized] public slotUpdated on_after_update;
    [System.NonSerialized] public slotUpdated on_before_update;

    public item item;
    public int amount;

    public ItemSO ItemSO
    {
        get
        {
            if(item.id >= 0)
            {
                return parent_interface.inventory.database.item_objecs[item.id];
            }
            return null;
        }
    }

    public InventorySlot(item _item, int _amount)
    {
        setSlot(_item, _amount);
    }

    public InventorySlot()
    {
        setSlot(new item(), 0);
    }

    public void AddAmount(int value)
    {
        setSlot(item, amount += value);
    }

    public void removeItem()
    {
        setSlot(new item(), 0);
    }

    public void setSlot(item _item, int _amount)
    {
        on_before_update?.Invoke(this);

        item = _item;
        amount = _amount;

        on_after_update?.Invoke(this);
    }

    public bool canPlaceInSlot(ItemSO _item_obj)
    {
        if (allowed_items.Length <= 0 || _item_obj == null || _item_obj.data.id < 0) return true;

        for (int i = 0; i < allowed_items.Length; i++)
        {
            if(_item_obj.type == allowed_items[i])
            {
                return true;
            }
        }

        return false;
    }
}

[System.Serializable]
public class Inventory
{
    public InventorySlot[] item_slots = new InventorySlot[24];

    public void clear()
    {
        for (int i = 0; i < item_slots.Length; i++)
        {
            item_slots[i].removeItem();
        }
    }
}

public enum InterfaceType
{
    Inventory,
    Equipment
}
