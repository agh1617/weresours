using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    int playerId;
    Vector3 movement;
    Rigidbody playerRigidbody;
    float rotation = 0f;

    void Awake()
    {
        playerId = GetComponent<PlayerState>().playerId;
        playerRigidbody = GetComponent<Rigidbody>();
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
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        rotation += Input.GetAxis("Rotate_" + playerId) * speed;
        Quaternion newRotation = Quaternion.Euler(0f, rotation, 0f);
        playerRigidbody.MoveRotation(newRotation);
    }
}
