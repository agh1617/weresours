using UnityEngine;
using System.Collections;

public class ZombieAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;
    public AudioClip[] attackClips;

    PlayerHealth playerHealth;
    ZombieHealth zombieHealth;
    AudioSource audio;
    bool playerInRange;
    float timer;

    void Awake()
    {
        zombieHealth = GetComponent<ZombieHealth>();
        audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerHealth = other.GetComponent<PlayerHealth>();
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerHealth = null;
            playerInRange = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange && zombieHealth.currentHealth > 0)
        {
            Attack();

            if (playerHealth.currentHealth <= 0)
            {
                playerInRange = false;
            }
        }
    }

    void Attack()
    {
        timer = 0f;

        if (playerHealth.currentHealth > 0)
        {
            audio.clip = attackClips[Random.Range(0, attackClips.Length)];
            audio.Play();
            playerHealth.TakeDamage(attackDamage);
        }
    }
}