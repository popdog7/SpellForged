using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCastType : MonoBehaviour
{
    [SerializeField] protected CastTypeDataSO cast_base_data;

    protected float damage_adjusted = 0;
    protected float range_adjusted = 0;
    protected float cooldown_adjusted = 0;
    protected float crit_chance_adjusted = 0;

    protected float cooldown_timer;

    private void Update()
    {
        if(!isCooldownOver())
        {
            cooldown_timer += Time.deltaTime;
            Debug.Log("Cooldown Passed: " + cooldown_timer + ", CoolDown Time: " + cooldown_adjusted);
        }
    }

    public void setupSpellStats(AttributeModifierSO modifier)
    {
        damage_adjusted = convertToTwoDecimal(cast_base_data.damage * modifier.damage);
        range_adjusted = convertToTwoDecimal(cast_base_data.range * modifier.range);
        cooldown_adjusted = convertToTwoDecimal(cast_base_data.cooldown * modifier.cooldown);
        crit_chance_adjusted = convertToTwoDecimal(cast_base_data.crit_chance * modifier.crit_chance);
    }

    public void determineCriticalDamage()
    {
        float chance = Random.Range(0f, 1f);

        if(chance <= crit_chance_adjusted)
        {
            damage_adjusted *= 2;
        }
    }

    public float convertToTwoDecimal(float num)
    {
        return Mathf.Round((num) * 100) / 100;
    }

    public bool isCooldownOver()
    {
        return cooldown_timer >= cooldown_adjusted;
    }

    public abstract void CommenceSpellCasting(Transform cast_location);
}
