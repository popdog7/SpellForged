using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModifierRune", menuName = "Inventory System/Items/ModifierRune")]
public class ModifierRuneSO : ItemSO
{
    public float damage;
    public float spread;
    public float cooldown;
    public float crit_chance;

    public void Awake()
    {
        type = ItemType.element_rune;
    }
}
