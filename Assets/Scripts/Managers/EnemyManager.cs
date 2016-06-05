using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime = 3f;
    public List<Transform> spawnPoints;

    public EnemyManager()
    {
        this.spawnPoints = new List<Transform>();
    }

	void Start () {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }
	
	void Spawn () {
        if (GameManager.GetGameState() != GameState.started) return;

        int spawnPointIndex = Random.Range(0, spawnPoints.Count);

        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}