using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] projectile_particles;
    [SerializeField] private TrailRenderer trail;

    private float damage;
    private Color first_color, second_color;

    private ElementRuneSO element_data;

    public void setupProjectile(float dmg, ElementRuneSO data)
    {
        damage = dmg;
        element_data = data;
        Debug.Log("name: " + data.name);
        Debug.Log("First Color: " + data.first_color);
        first_color = element_data.first_color;
        second_color = element_data.second_color;
        setParticleColor();
        setTrailGradientColor();
    }

    private void setParticleColor()
    {
        foreach(ParticleSystem component in projectile_particles)
        {
            var main_module = component.main;
            main_module.startColor = new ParticleSystem.MinMaxGradient(first_color, second_color);
        }
    }

    private void setTrailGradientColor()
    {
        Gradient trail_gradient = new Gradient();
        trail_gradient.SetKeys
            (
            new GradientColorKey[]
            {
                new GradientColorKey(first_color, 0f),
                new GradientColorKey(second_color, 1f)
            },
            new GradientAlphaKey[]
            {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(0f, 1f)
            }
            );
        trail.colorGradient = trail_gradient;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(transform.rotation);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            BaseHealth enemy_health = collision.gameObject.GetComponent<BaseHealth>();
            enemy_health.damageHealth(damage);
            if (element_data.element_id != 0)
            {
                enemy_health.increaseElementalPercentage(element_data.element_id);
            }

            Debug.Log("Hit: " + collision.gameObject.name + ", Damage: " + damage);
        }

        if (collision.gameObject.CompareTag("Player"))
            return;
        Destroy(gameObject);
    }
}
