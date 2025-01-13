using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class BaseCastType : MonoBehaviour
{
    [SerializeField] protected CastTypeDataSO cast_base_data;
    protected ElementRuneSO elemental_type;

    protected float final_damage = 0;
    protected float damage_adjusted = 0;
    protected float spread_adjusted = 0;
    protected float cooldown_adjusted = 0;
    protected float crit_chance_adjusted = 0;

    protected float projectile_speed = 10f;
    protected float cooldown_timer;

    protected virtual void Update()
    {
        if(!isCooldownOver())
        {
            cooldown_timer += Time.deltaTime;
            //Debug.Log("Cooldown Passed: " + cooldown_timer + ", CoolDown Time: " + cooldown_adjusted);
        }
    }

    public void setupSpellStats(ModifierRuneSO modifier, ElementRuneSO element_data)
    {
        damage_adjusted = convertToTwoDecimal(cast_base_data.damage * modifier.damage);
        spread_adjusted = convertToTwoDecimal(cast_base_data.spread * modifier.spread);
        cooldown_adjusted = convertToTwoDecimal(cast_base_data.cooldown * modifier.cooldown);
        crit_chance_adjusted = convertToTwoDecimal(cast_base_data.crit_chance * modifier.crit_chance);

        elemental_type = element_data;
    }

    public void determineCriticalDamage()
    {
        float chance = UnityEngine.Random.Range(0f, 1f);

        if(chance <= crit_chance_adjusted)
        {
            final_damage = 2 * damage_adjusted;
            Debug.Log("crit");
        }
        else
        {
            final_damage = damage_adjusted;
        }
    }

    public Vector3 determineSpray(Vector3 spell_forward)
    {
        Vector3 spread_vector = new Vector3(UnityEngine.Random.Range(-spread_adjusted, spread_adjusted), 
            UnityEngine.Random.Range(-spread_adjusted, spread_adjusted), 
            0);

        return (spell_forward + spread_vector).normalized;
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
