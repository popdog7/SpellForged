using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CastRune", menuName = "Inventory System/Items/CastRune")]
public class CastRuneSO : ItemSO
{
    public Transform spell_prefab;
    public float damage;
    public float spread;
    public float cooldown;
    public float crit_chance;

    public void Awake()
    {
        type = ItemType.cast_rune;
    }
}
