using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory System/Inventory")]
public class InventorySO : ScriptableObject
{
    public string save_path;
    public ItemDatabaseSO database;
    public Inventory container;

    
    public void AddItem(item _item, int _amount)
    {
        for( int i =0; i < container.items.Length; i++)
        {
            if (container.items[i].id == _item.id)
            {
                container.items[i].AddAmount(_amount);
                return;
            }
        }
        setEmptySlot(_item, _amount);
    }

    public InventorySlot setEmptySlot(item _item, int _amount)
    {
        for (int i = 0; i < container.items.Length; i++)
        {
            if (container.items[i].id <= -1)
            {
                container.items[i].setSlot(_item, _amount, _item.id);
                return container.items[i];
            }
        }
        return null;
    }
    
    public void moveItem(InventorySlot item_1, InventorySlot item_2)
    {
        InventorySlot temp = new InventorySlot(item_2.item, item_2.amount, item_2.id);
        item_2.setSlot(item_1.item, item_1.amount, item_1.id);
        item_1.setSlot(temp.item, temp.amount, temp.id);
    }

    public void RemoveItem(item _item)
    {
        for (int i = 0; i < container.items.Length; i++)
        {
            if (container.items[i].item == _item)
            {
                container.items[i].setSlot(null, 0, -1);
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
        container = new Inventory();
    }
}


[System.Serializable]
public class InventorySlot
{
    public int id;
    public item item;
    public int amount;

    public InventorySlot(item _item, int _amount, int _id)
    {
        id = _id;
        item = _item;
        amount = _amount;
    }

    public InventorySlot()
    {
        id = -1;
        item = null;
        amount = 0;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }

    public void setSlot(item _item, int _amount, int _id)
    {
        id = _id;
        item = _item;
        amount = _amount;
    }
}

[System.Serializable]
public class Inventory
{
    public InventorySlot[] items = new InventorySlot[24];
}
