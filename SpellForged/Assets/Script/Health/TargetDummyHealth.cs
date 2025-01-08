using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummyHealth : BaseHealth
{
    public override void OnDeath()
    {
        Debug.Log("Dead");
        Destroy(gameObject);
    }
}
