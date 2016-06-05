using UnityEngine;

public class PerkFactory : MonoBehaviour {

    public GameObject[] perks;

    public float perkProbability = 0.5f;

    public void Generate()
    {
        if (Random.value < perkProbability)
        {
            Instantiate(
                perks[Random.Range(0, perks.Length)],
                gameObject.transform.position,
                Quaternion.identity
            );
        }
    }
}
