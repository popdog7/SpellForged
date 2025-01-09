using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstCastType : BaseCastType
{
    private float burst_time_between = .5f;
    private float burst_timer = 0;

    private bool on_burst = false;

    private Transform cast_origin;

    private int burst_amount = 0;
    private int max_burst = 3;

    protected override void Update()
    {
        base.Update();

        if(on_burst)
        {
            burst_timer += Time.deltaTime;
        }

        onBurstTimerEnd();
    }

    public override void CommenceSpellCasting(Transform cast_location)
    {
        if (isCooldownOver() && !on_burst)
        {
            cooldown_timer = 0;
            cast_origin = cast_location;
            on_burst = true;
            instantiateProjectile(cast_origin);
            burst_amount++;
        }
    }

    private void instantiateProjectile(Transform cast_location)
    {
        determineCriticalDamage();

        Transform spell_transform = Instantiate(cast_base_data.spell_prefab, cast_location.position, cast_location.rotation);
        spell_transform.GetComponent<Rigidbody>().velocity = determineSpray(cast_location.forward) * projectile_speed;
        spell_transform.GetComponent<BasicProjectile>().setupProjectile(final_damage, elemental_type);
        burst_timer = 0f;
    }

    private void onBurstTimerEnd()
    {
        if (burst_timer >= burst_time_between)
        {
            burst_timer = 0f;
            burst_amount++;
            instantiateProjectile(cast_origin);

            if (burst_amount >= max_burst)
            {
                on_burst = false;
                burst_amount = 0;
                cooldown_timer = 0;
                burst_timer = 0f;
            }
        }
    }
}
