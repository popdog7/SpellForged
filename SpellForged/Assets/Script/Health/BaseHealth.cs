using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public abstract class BaseHealth : MonoBehaviour
{
    [SerializeField] protected float max_health;
    protected float min_health = 0;

    protected float health;
    private float[] elemental_percentages = new float[4];
    private float[] elemental_resistances = new float[4];

    private void Awake()
    {
        health = max_health;
    }

    public virtual void damageHealth(float amount)
    {
        Debug.Log("Hit");
        health = Mathf.Clamp(health - amount, min_health, max_health);
        if (health == min_health)
        {
            OnDeath();
        }
    }

    public void healHealth(float amount)
    {
        health = Mathf.Clamp(health + amount, min_health, max_health);
    }

    public void increaseElementalPercentage(int element_id)
    {
        elemental_percentages[element_id - 1] += 5 * elemental_resistances[element_id - 1];
        if(elemental_percentages[element_id - 1] >= 100)
        {
            Debug.Log("elemental effect");
        }
        Debug.Log("Elemental Percentage: " + elemental_percentages[element_id - 1]);
    }

    public abstract void OnDeath();
}
