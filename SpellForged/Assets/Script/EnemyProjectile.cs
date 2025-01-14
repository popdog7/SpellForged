using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] projectile_particles;
    [SerializeField] private TrailRenderer trail;

    [SerializeField] private float damage;
    [SerializeField] private Color first_color, second_color;

    private void Start()
    {
        damage = 1;
        setParticleColor();
        setTrailGradientColor();
    }

    private void setParticleColor()
    {
        foreach (ParticleSystem component in projectile_particles)
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
        if (collision.gameObject.CompareTag("Player"))
        {
            BaseHealth player_health = collision.gameObject.GetComponent<BaseHealth>();
            player_health.damageHealth(damage);
        }

        if (!collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("EnemyProjectile"))
            Destroy(gameObject);
    }
}
