using UnityEngine;
using System.Collections;

public class ZombieMovement : MonoBehaviour
{
    Transform player;
    GameObject[] players;
    PlayerHealth playerHealth;
    ZombieHealth zombieHealth;
    NavMeshAgent nav;

    void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        zombieHealth = GetComponent<ZombieHealth>();
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        GameObject closest;

        if (players.Length == 0 || zombieHealth.currentHealth <= 0)
        {
            nav.enabled = false;
        }
        else
        {
            closest = GetClosestPlayer();
            
            if (closest) nav.SetDestination(closest.transform.position);
        }
    }

    private GameObject GetClosestPlayer()
    {
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(player.transform.position, currentPosition);
            if (distance < minDistance && player.GetComponent<PlayerHealth>().currentHealth > 0)
            {
                closest = player;
                minDistance = distance;
            }
        }
        return closest;
    }
}

