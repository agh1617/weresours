using UnityEngine;

public class PerkRotation : MonoBehaviour {
	
	void Update () {
        transform.RotateAround(transform.position, Vector3.up, 20 * Time.deltaTime);
    }
}
