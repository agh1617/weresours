using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;

    PlayerMovement playerMovement;
    bool isDead;
    bool damaged;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        playerMovement.enabled = false;
    }
}
