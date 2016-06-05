using UnityEngine;
using System;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    PlayerMovement playerMovement;
    Animator animator;
    bool isDead;
    bool damaged;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();
        currentHealth = startingHealth;
    }

    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;

        healthSlider.value = currentHealth;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
            GameManager.PlayerDied();
        }
    }

    public void Heal()
    {
        currentHealth = startingHealth;
        damaged = false;
        healthSlider.value = currentHealth;
    }

    void Death()
    {
        animator.SetBool("Dead", true);
        isDead = true;
        playerMovement.enabled = false;
    }
}
