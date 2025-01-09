using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalCastType : BaseCastType
{
    public override void CommenceSpellCasting(Transform cast_location)
    {
        if(isCooldownOver())
        {
            determineCriticalDamage();

            Transform spell_transform = Instantiate(cast_base_data.spell_prefab, cast_location.position, cast_location.rotation);
            spell_transform.GetComponent<Rigidbody>().velocity = determineSpray(cast_location.forward) * projectile_speed;
            spell_transform.GetComponent<BasicProjectile>().setupProjectile(final_damage, elemental_type);
            
            cooldown_timer = 0;
        }
    }
}
