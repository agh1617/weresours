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
    Door door;
    bool playerInRange;
    bool doorInRange;
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
        else if (other.gameObject.tag == "Door")
        {
            door = other.GetComponent<Door>();
            doorInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerHealth = null;
            playerInRange = false;
        }
        else if (other.gameObject.tag == "Door")
        {
            door = null;
            doorInRange = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && (playerInRange || doorInRange) && zombieHealth.currentHealth > 0)
        {
            Attack();
            
        }
    }

    void Attack()
    {
        timer = 0f;

        animator.Play("Attack");
        audioSource.clip = attackClips[Random.Range(0, attackClips.Length)];
        audioSource.Play();

        if (playerInRange && playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);

            if (playerHealth.currentHealth <= 0) playerInRange = false;
        }
        else if (doorInRange)
        {
            door.TakeDamage(attackDamage);
        }
    }
}