using UnityEngine;
using System.Collections;

public class DoorMovement : MonoBehaviour {

    bool isOpened = true;
    Rigidbody doorRigidbody;
    int playersInRange = 0;

    void Awake()
    {
        doorRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (playersInRange > 0 && (Input.GetButtonDown("Action_1") || Input.GetButtonDown("Action_2")))
        {
           Move();
        }
        if (isOpened)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * 10f);
        }
        else
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0f, 90f, 0f), Time.deltaTime * 10f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playersInRange++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playersInRange--;
        }
    }

    void Move()
    {
        /*if (isOpened)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * 0.2f);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 90f, 0f), Time.deltaTime * 2f);
        }*/
        isOpened = !isOpened;
    }
}
