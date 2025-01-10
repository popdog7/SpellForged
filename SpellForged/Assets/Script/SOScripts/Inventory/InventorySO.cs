using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory System/Inventory")]
public class InventorySO : ScriptableObject, ISerializationCallbackReceiver
{
    public string save_path;
    private ItemDatabaseSO database;
    public List<InventorySlot> container = new List<InventorySlot>();

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (ItemDatabaseSO)AssetDatabase.LoadAssetAtPath("Assets/Resources/ItemDatabase.asset", typeof(ItemDatabaseSO));
#else
        database = Resources.Load<ItemDatabaseSO>("ItemDatabase");
#endif
    }

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

        container.Add(new InventorySlot(_item, _amount, database.get_id[_item]));

    }

    public void Save()
    {
        string save_data = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, save_path));
        bf.Serialize(file, save_data);
        file.Close();
    }

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


    public void OnAfterDeserialize()
    {
        for (int i = 0; i < container.Count; i++)
            container[i].item = database.get_item[container[i].id];
    }

    public void OnBeforeSerialize() { }
}


[System.Serializable]
public class InventorySlot
{
    public int id;
    public ItemSO item;
    public int amount;

    public InventorySlot(ItemSO _item, int _amount, int _id)
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