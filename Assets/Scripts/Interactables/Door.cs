using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    public float openDoorSpeed = 5f;
    public float closeDoorSpeed = 10f;
    public int initialDoorHealth = 200;
    public bool close = true;
    public AudioClip openClip;
    public AudioClip closeClip;

    int playersInRange = 0;
    int doorHealth;
    AudioSource audioSource;

    void Awake()
    {
        doorHealth = initialDoorHealth;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playersInRange > 0 && (Input.GetButtonDown("Action_1") || Input.GetButtonDown("Action_2")))
        {
            close = !close;
            PlaySound();
        }
        Move();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") playersInRange++;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") playersInRange--;
    }

    public void TakeDamage(int amount)
    {
        doorHealth -= amount;
        if (doorHealth <= 0)
        {
            close = false;
            doorHealth = initialDoorHealth;
            //gameObject.SetActive(false);
        }
    }

    void Move()
    {
        if (close)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * closeDoorSpeed);
        }
        else
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0f, 90f, 0f), Time.deltaTime * openDoorSpeed);
        }
    }

    void PlaySound()
    {
        if (close) audioSource.clip = closeClip;
        else audioSource.clip = openClip;
        audioSource.Play();
    }
}
