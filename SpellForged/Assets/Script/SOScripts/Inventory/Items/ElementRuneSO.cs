using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementRune", menuName = "Inventory System/Items/ElementRune")]
public class ElementRuneSO : ItemSO
{
    public Color first_color;
    public Color second_color;
    [Range(0, 4)] public int element_id;

    public void Awake()
    {
        type = ItemType.element_rune;
    }
}
