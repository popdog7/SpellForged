using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private Transform throw_origin;
    [SerializeField] private Transform projectile_prefab;

    public float follow_distance = 5;
    private float distance_to_player;

    private float attack_timer = 0;
    public float attack_cooldown = 2;

    private PlayerHealth player_health;

    private void Start()
    {
        player_health = player.gameObject.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        attack_timer += Time.deltaTime;
        transform.LookAt(player.transform);
        distance_to_player = Vector3.Distance(transform.position, player.transform.position);
        keepInRange();
        if (attack_timer >= attack_cooldown)
        {
            shoot();
            attack_timer = 0;
        }
    }

    private void keepInRange()
    {
        //Debug.Log("distance to player: " + distance_to_player);
        if (distance_to_player > follow_distance)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.ResetPath();
        }
    }

    private void shoot()
    {
        Debug.Log("shot");
        /*
        GameObject thrown_object = Instantiate(projectile_prefab, throw_origin.position, throw_origin.rotation);
        Rigidbody object_rigid_body = thrown_object.GetComponent<Rigidbody>();
        object_rigid_body.velocity = 10f * (throw_origin.forward);
        object_rigid_body.angularVelocity = 20f * Vector3.one;
        */

        Transform thrown_object = Instantiate(projectile_prefab, throw_origin.position, throw_origin.rotation);
        thrown_object.GetComponent<Rigidbody>().velocity = throw_origin.forward * 20f;

    }
}
