using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public float startSpeed = 6f;
    public float maxSpeed = 20f;

    Animator animator;
    int playerId;
    Vector3 movement;
    Rigidbody playerRigidbody;
    float rotation = 0f;
    int floorMask;
    float camRayLength = 100f;
    float speed;

    public void StaminaUp(float duration)
    {
        speed = Math.Min(speed * 1.5f, maxSpeed);

        CancelInvoke("ResetSpeed");
        Invoke("ResetSpeed", duration);
    }

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        floorMask = LayerMask.GetMask("Floor");
        playerId = GetComponent<PlayerState>().playerId;
        playerRigidbody = GetComponent<Rigidbody>();
        speed = startSpeed;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal_" + playerId);
        float v = Input.GetAxisRaw("Vertical_" + playerId);

        Move(h, v);
        Turning();
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;

        animator.SetFloat("Speed", movement.magnitude);

        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        if (!(playerId == 1 && MouseTurning()))
            KeyboardPadTurning();
    }

    bool MouseTurning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
            return true;
        }
        return false;
    }

    void KeyboardPadTurning()
    {
        rotation += Input.GetAxis("Rotate_" + playerId) * speed;
        Quaternion newRotation = Quaternion.Euler(0f, rotation, 0f);
        playerRigidbody.MoveRotation(newRotation);
    }

    void ResetSpeed()
    {
        speed = startSpeed;
    }
}
