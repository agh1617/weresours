using UnityEngine;
using System.Collections;

public class ZombieHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public int scoreValue = 10;
    
    ZombieBlood zombieBlood;
    PerkFactory perkFactory;
    Collider zombieCollider;
    bool isDead = false;
    bool isBleeding = false;

    void Awake()
    {
        zombieBlood = GetComponent<ZombieBlood>();
        perkFactory = GetComponent<PerkFactory>();

        zombieCollider = GetComponent<BoxCollider>(); // TODO change to more suitable collider
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

        if (currentHealth <= 0.7 * startingHealth)
        {
            StartBleeding();
        }

        if (currentHealth <= 0)
        {
            playerState.IncreaseScore(scoreValue);
            Death();
        }
    }

    void StartBleeding()
    {
        if (isBleeding) return;

        zombieBlood.Bleed();

        this.isBleeding = true;
    }

    void Death()
    {
        perkFactory.Generate();
        GetComponent<NavMeshAgent>().enabled = false;
        isDead = true;
        zombieCollider.isTrigger = true;
    }
}
