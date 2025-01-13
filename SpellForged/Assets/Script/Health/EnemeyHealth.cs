using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyHealth : BaseHealth
{
    [SerializeField] private Transform rune_prefab;

    public override void OnDeath()
    {
        Debug.Log("Dead");
        dropItem();
        Destroy(gameObject);
    }

    public void dropItem()
    {
        float rune_x = transform.position.x;
        float rune_y = 0.87f;
        float rune_z = transform.position.z;

        Transform spell_transform = Instantiate(rune_prefab, new Vector3(rune_x, rune_y, rune_z), transform.rotation);
    }
}
