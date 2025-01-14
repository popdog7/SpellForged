using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyHealth : BaseHealth
{
    [SerializeField] private Transform rune_prefab;
    [SerializeField] private GameObject floating_text_prefab;

    private float destroy_time = .3f;

    public override void OnDeath()
    {
        Debug.Log("Dead");
        dropItem();
        Destroy(gameObject);
    }

    public override void damageHealth(float amount)
    {
        if (floating_text_prefab)
        {
            showFloatingText(amount);
        }
        base.damageHealth(amount);
    }

    public void dropItem()
    {
        float rune_x = transform.position.x;
        float rune_y = 0.87f;
        float rune_z = transform.position.z;

        Transform spell_transform = Instantiate(rune_prefab, new Vector3(rune_x, rune_y, rune_z), transform.rotation);
    }

    public void showFloatingText(float damage)
    {
        float rune_x = transform.position.x;
        float rune_y = 4f;
        float rune_z = transform.position.z;
        GameObject text = Instantiate(floating_text_prefab, new Vector3(rune_x, rune_y, rune_z), transform.rotation);
        text.GetComponent<TextMesh>().text = damage.ToString();
        Destroy(text, destroy_time);
    }
}
