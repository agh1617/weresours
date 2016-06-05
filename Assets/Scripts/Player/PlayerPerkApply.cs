using UnityEngine;

public class PlayerPerkApply : MonoBehaviour {

    public float doubleTapDuration = 10f;
    public float staminaUpDuration = 10f;

    public AudioClip perkActivationSound;

    PlayerHealth playerHealth;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;

    AudioSource audioSource;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();

        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "Juggernog":
                playerHealth.Heal();
                Destroy(other.gameObject);
                handlePerk();
                break;

            case "StaminaUp":
                playerMovement.StaminaUp(staminaUpDuration);
                Destroy(other.gameObject);
                handlePerk();
                break;

            case "DoubleTap":
                playerShooting.DoubleTap(doubleTapDuration);
                Destroy(other.gameObject);
                handlePerk();
                break;
        }
    }

    void handlePerk()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(perkActivationSound);
    }
}
