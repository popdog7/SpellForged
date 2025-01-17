using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    cast_rune,
    element_rune,
    modifier_rune
}

public abstract class ItemSO : ScriptableObject
{
    public Sprite inventory_icon;
    public bool stackable;
    public ItemType type;

    [TextArea(15,20)]
    public string description;
    public item data = new item();
}

[System.Serializable]
public class item
{
    public string name;
    public int id = -1;

    public item()
    {
        name = "";
        id = -1;
    }

    public item(ItemSO item)
    {
        name = item.name;
        id = item.data.id;
    }
}
