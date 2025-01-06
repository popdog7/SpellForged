using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpell : MonoBehaviour
{
    private float damage;
    private float range;
    private float crit_chance;

    public void setupProjectile(float dmg, float rng, float crit)
    {
        damage = dmg;
        range = rng;
        crit_chance = crit;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit: " + collision.gameObject.name + ", Damage: " + damage);
        }

        Destroy(gameObject);
    }
}
