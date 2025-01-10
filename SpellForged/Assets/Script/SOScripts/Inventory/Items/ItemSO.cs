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
    public GameObject inventory_icon;
    public ItemType type;

    [TextArea(15,20)]
    public string description;
}
