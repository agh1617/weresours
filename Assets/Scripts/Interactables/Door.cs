using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    public float doorSpeed = 10f;
    public int initialDoorHealth = 200;
    public bool close = true;
    
    int playersInRange = 0;
    int doorHealth;

    void Awake()
    {
        doorHealth = initialDoorHealth;
    }

    void Update()
    {
        if (playersInRange > 0 && (Input.GetButtonDown("Action_1") || Input.GetButtonDown("Action_2")))
        {
            close = !close;
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
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * doorSpeed);
        }
        else
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0f, 90f, 0f), Time.deltaTime * doorSpeed);
        }
    }
}
