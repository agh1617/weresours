using UnityEngine;
using System.Collections;

public class ZombieMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    ZombieHealth zombieHealth;
    NavMeshAgent nav;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        zombieHealth = GetComponent<ZombieHealth>();
        nav = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        if (zombieHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
