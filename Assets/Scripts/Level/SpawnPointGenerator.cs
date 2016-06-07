using UnityEngine;

namespace Assets.Scripts.Level
{
    class SpawnPointGenerator : MonoBehaviour
    {
        public int spawnPointCount = 16;
        public float margin = 5.0f;
        public GameObject containerComponent;

        public void Start()
        {
            var floor = this.gameObject;
            var bounds = floor.GetComponent<MeshRenderer>().bounds;
            var enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
            var spawnPoints = new GameObject("SpawnPoints");

            var max = new Vector3(bounds.max.x - this.margin, 0, bounds.max.z - this.margin);
            var min = new Vector3(bounds.min.x + this.margin, 0, bounds.min.z + this.margin);

            var width = max.x - min.x;
            var height = max.z - min.z;
            var totalFloorSideLength = (width + height) * 2;
            var interspace = totalFloorSideLength / this.spawnPointCount;

            for (int i = 0; i < this.spawnPointCount / 4; i++)
            {
                var offset = i * interspace;
                buildSpawnPoint(new Vector3(min.x + offset, 0, max.z), spawnPoints, enemyManager);
                buildSpawnPoint(new Vector3(max.x, 0, max.z - offset), spawnPoints, enemyManager);
                buildSpawnPoint(new Vector3(max.x - offset, 0, min.z), spawnPoints, enemyManager);
                buildSpawnPoint(new Vector3(min.x, 0, min.z + offset), spawnPoints, enemyManager);
            }

            Debug.Log("here");
        }

        private void buildSpawnPoint(Vector3 location, GameObject spawnPoints, EnemyManager enemyManager)
        {
            var spawnPoint = new GameObject("SpawnPoint");
            spawnPoint.transform.parent = spawnPoints.transform;
            spawnPoint.transform.position = location;
            enemyManager.spawnPoints.Add(spawnPoint.transform);

            Instantiate(containerComponent, new Vector3(location.x, 0, location.z + 5), Quaternion.Euler(0f, Random.Range(0f, 90f), 0f));
        }
    }
}
