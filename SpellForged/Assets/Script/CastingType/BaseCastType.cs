using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCastType : MonoBehaviour
{
    [SerializeField] protected CastTypeDataSO cast_base_data;

    private float damage_adjusted;
    private float range_adjusted;
    private float cooldown_adjusted;
    private float crit_chance_adjusted;

    public void setupSpellStats(AttributeModifierSO modifier)
    {
        damage_adjusted = cast_base_data.damage * modifier.damage;
        range_adjusted = cast_base_data.range * modifier.range;
        cooldown_adjusted = cast_base_data.cooldown * modifier.cooldown;
        crit_chance_adjusted = cast_base_data.crit_chance * modifier.crit_chance;
    }

    public abstract void CommenceSpellCasting(Transform cast_location);
}
