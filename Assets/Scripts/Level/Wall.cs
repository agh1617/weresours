using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Assets.Scripts.Level
{
    enum Directions { North = 0, East = 90, South = 180, West = 270 };

    class Wall : MonoBehaviour
    {
        public GameObject sourceWall;
        public GameObject sourceDoor;
        public int size;
        public Directions direction;

        public GameObject wallObject;
        public IEnumerable<int> doors;

        public void Start()
        {
            doors = from Door door in wallObject.GetComponents<Door>() orderby door.position select door.position;
            Render();

            addSpawnPoint();
        }

        public void Render()
        {
            int currentPosition = 0;

            foreach (var doorPosition in doors)
            {
                AddWallComponent(sourceWall, currentPosition, doorPosition - currentPosition);
                AddWallComponent(sourceDoor, doorPosition, 2);

                currentPosition = doorPosition + 2;
            }

            AddWallComponent(sourceWall, currentPosition, size - currentPosition);
            wallObject.transform.localRotation = Quaternion.Euler(0.0f, (float)direction, 0.0f);
        }

        private void AddWallComponent(GameObject sourceObject, int position, int size)
        {
            var component = wallObject.AddComponent<WallComponent>();
            component.sourceObject = sourceObject;
            component.x = position;
            component.size = size;
            component.wallComponentObject = new GameObject("WallComponent");
            component.wallComponentObject.transform.parent = wallObject.transform;
        }

        private void addSpawnPoint()
        {
            var enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
            var spawnPoints = GameObject.Find("SpawnPoints");
            var spawnPoint = new GameObject("SpawnPoint");

            Vector3 relativePosition = new Vector3(this.size / 2, 0, 8);
            spawnPoint.transform.parent = spawnPoints.transform;
            spawnPoint.transform.position = this.wallObject.transform.TransformPoint(relativePosition);

            enemyManager.spawnPoints.Add(spawnPoint.transform);
        }
    }
}
