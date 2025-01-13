using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemie : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private NavMeshAgent agent;

    public float follow_distance = 1;
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
        transform.LookAt(player.transform);
        distance_to_player = Vector3.Distance(transform.position, player.transform.position);
        keepInRange();
        if (attack_timer >= attack_cooldown)
        {
            
            meleeAtack();
            attack_timer = 0;
        }
    }

    private void keepInRange()
    {
        if (distance_to_player > follow_distance)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            attack_timer += Time.deltaTime;
            agent.ResetPath();
        }
    }

    private void meleeAtack()
    {
        player_health.damageHealth(1);
    }
}
