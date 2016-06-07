using UnityEngine;

public class BulletTrailMove : MonoBehaviour
{
    public int speed = 230;

    int shootableLayer;
    Ray movementRay;

    void Start()
    {
        Destroy(gameObject, 3f);
        shootableLayer = LayerMask.GetMask("Shootable");
    }

    void Update()
    {
        CheckObstacles();
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void CheckObstacles()
    {
        movementRay.origin = transform.position;
        movementRay.direction = transform.forward;
        RaycastHit shootableHit;

        if (Physics.Raycast(movementRay, out shootableHit, Time.deltaTime * speed, shootableLayer))
        {
            Destroy(gameObject);
        }
    }
}
