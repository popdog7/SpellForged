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
        for( int i =0; i < container.items.Count; i++)
        {
            if (container.items[i].item.id == _item.id)
            {
                container.items[i].AddAmount(_amount);
                return;
            }
        }

        container.items.Add(new InventorySlot(_item, _amount, _item.id));

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

    public void AddAmount(int value)
    {
        amount += value;
    }
}

[System.Serializable]
public class Inventory
{
    public List<InventorySlot> items = new List<InventorySlot>();
}
