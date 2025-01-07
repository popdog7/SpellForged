using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpell : MonoBehaviour
{
    private float damage;
    private float range;

    public void setupProjectile(float dmg)
    {
        damage = dmg;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(transform.rotation);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit: " + collision.gameObject.name + ", Damage: " + damage);
        }

        Destroy(gameObject);
    }
}
