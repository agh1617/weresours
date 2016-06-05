using UnityEngine;
using System.Collections;

public class ZombieAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;
    public AudioClip[] attackClips;

    PlayerHealth playerHealth;
    ZombieHealth zombieHealth;
    AudioSource audioSource;
    Animator animator;
    bool playerInRange;
    float timer;

    void Awake()
    {
        zombieHealth = GetComponent<ZombieHealth>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
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
            animator.Play("Attack");

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
            audioSource.clip = attackClips[Random.Range(0, attackClips.Length)];
            audioSource.Play();
            playerHealth.TakeDamage(attackDamage);
        }
    }
}