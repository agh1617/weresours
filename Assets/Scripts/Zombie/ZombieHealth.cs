using UnityEngine;
using System.Collections;

public class ZombieHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public int scoreValue = 10;

    Collider collider;
    bool isDead = false;

    void Awake()
    {
        collider = GetComponent<BoxCollider>(); // TODO change to more suitable collider
        currentHealth = startingHealth;
    }

    void Update()
    {
        if (isDead)
        {
            Destroy(gameObject, 2f);
        }
    }

    public void TakeDamage(PlayerState playerState, int amount)
    {
        if(isDead)
            return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            playerState.score += scoreValue;
            Death();
        }
    }

    void Death()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        isDead = true;
        collider.isTrigger = true;
    }
}
