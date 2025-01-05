using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpell : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit " + collision.gameObject);
        }

        Destroy(gameObject);
    }
}