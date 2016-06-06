using UnityEngine;

public class BulletTrailMove : MonoBehaviour
{

    public int speed = 230;

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
