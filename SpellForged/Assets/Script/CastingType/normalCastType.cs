using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalCastType : BaseCastType
{
    public override void CommenceSpellCasting(Transform cast_location)
    {
        Transform spell_transform = Instantiate(cast_base_data.spell_prefab, cast_location.position, cast_location.rotation);
        spell_transform.GetComponent<Rigidbody>().velocity = cast_location.forward * 10f;
    }
}