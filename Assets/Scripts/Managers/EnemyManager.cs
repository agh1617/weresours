using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime = 3f;
    public List<Transform> spawnPoints;
    public AudioClip[] waveSounds;

    public int startWaveSize = 10;
    public float waveGrowthFactor = 1.5f;

    AudioSource audioSource;
    int spawnedEnemies;
    int currentWaveSize;

    public EnemyManager()
    {
        this.spawnPoints = new List<Transform>();
    }

	void Start () {
        audioSource = GetComponent<AudioSource>();

        currentWaveSize = startWaveSize;
        PlayNextWaveSound();

        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }
	
	void Spawn () {
        if (GameManager.GetGameState() != GameState.started) return;

        int spawnPointIndex = Random.Range(0, spawnPoints.Count);

        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

        spawnedEnemies++;

        if (spawnedEnemies >= currentWaveSize)
            StartNextWave();
    }

    void StartNextWave()
    {
        currentWaveSize = (int)(currentWaveSize * waveGrowthFactor);
        spawnedEnemies = 0;

        PlayNextWaveSound();
    }

    void PlayNextWaveSound()
    {
        AudioClip waveSound = waveSounds[Random.Range(0, waveSounds.Length)];
        audioSource.PlayOneShot(waveSound);
    }
}