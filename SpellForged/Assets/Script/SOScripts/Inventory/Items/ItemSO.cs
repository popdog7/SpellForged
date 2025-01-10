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
    public int id;
    public Sprite inventory_icon;
    public ItemType type;

    [TextArea(15,20)]
    public string description;
}

[System.Serializable]
public class item
{
    public string name;
    public int id;
    public item(ItemSO item)
    {
        name = item.name;
        id = item.id;
    }
}
