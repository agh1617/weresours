using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Level
{
    class Room : MonoBehaviour
    {
        public int width;
        public int height;

        public Dictionary<Directions, int[]> doors = new Dictionary<Directions, int[]>();
        public GameObject roomObject;
        public LevelRenderer levelRenderer;
        List<Room> previousRooms;

        public void Start()
        {
            Wall wall;

            wall = createWall(width, Directions.North, new Vector3(0.0f, 0.0f, 0.0f));
            addDoors(wall, Directions.North);
            wall.Render();

            wall = createWall(height, Directions.East, new Vector3(width, 0.0f, 0.0f));
            addDoors(wall, Directions.East);
            wall.Render();

            wall = createWall(width, Directions.South, new Vector3(width, 0.0f, -height));
            addDoors(wall, Directions.South);
            wall.Render();

            wall = createWall(height, Directions.West, new Vector3(0.0f, 0.0f, -height));
            addDoors(wall, Directions.West);
            wall.Render();

            InitializeFloor();
            DistributeTerrainObjects();

            foreach (Room other in previousRooms)
            {
                SubtractRoom(other);
            }
        }

        public void SetPreviousRooms(List<Room> previousRooms)
        {
            this.previousRooms = previousRooms;
        }

        public void SubtractRoom(Room other)
        {
            var wallComponents = roomObject.GetComponentsInChildren<WallComponent>();
    
            foreach (var wallComponent in wallComponents)
            {
                wallComponent.SubtractRoom(other);
            }
        }

        void InitializeFloor()
        {
            var position = new Vector3(transform.localPosition.x + width / 2f, 0.1f, transform.localPosition.z - height / 2f);
            var rotation = new Quaternion();

            var floor = (GameObject)Instantiate(levelRenderer.floorComponent, position, rotation);
            floor.transform.SetParent(roomObject.transform, false);
            floor.transform.localScale = new Vector3(width / 10f, 1f, height / 10f);
            if (levelRenderer.floorMaterials.Length > 0)
                floor.GetComponent<Renderer>().material = levelRenderer.floorMaterials[Random.Range(0, levelRenderer.floorMaterials.Length)];

        }

        void DistributeTerrainObjects()
        {
            int objectsCount = Random.Range(3, 5);
            int randomObjectIdx;

            Transform objectToDistribute;

            Vector3 objectPosition;
            Quaternion objectRotation = new Quaternion();

            for (int i = 0; i < objectsCount; i++)
            {
                randomObjectIdx = Random.Range(0, levelRenderer.terrainObjects.Length);
                objectToDistribute = levelRenderer.terrainObjects[randomObjectIdx];
                objectPosition = RandomPositionInsideRoom();
                Instantiate(objectToDistribute, objectPosition, objectRotation);
            }
        }

        Vector3 RandomPositionInsideRoom()
        {
            int wallOffsetX = (int) 0.3 * width;
            int wallOffsetY = (int) 0.3 * height;

            int x = Random.Range(wallOffsetX, width - wallOffsetX);
            int z = -Random.Range(wallOffsetY, height - wallOffsetY);
            
            x += (int) roomObject.transform.position.x;
            z += (int) roomObject.transform.position.z;

            return new Vector3(x, 0.1f, z);
        }

        private Wall createWall(int size, Directions direction, Vector3 position)
        {
            var wall = roomObject.AddComponent<Wall>();
            wall.sourceDoor = levelRenderer.doorComponent;
            wall.sourceWall = levelRenderer.wallComponent;

            wall.size = size;
          
            wall.wallObject = new GameObject("Wall");
            wall.wallObject.transform.parent = roomObject.transform;
            wall.wallObject.transform.localPosition = position;
            wall.direction = direction;

            return wall;
        }

        private void addDoors(Wall wall, Directions direction)
        {
            foreach (var doorPosition in doors[direction])
            {
                var door = wall.wallObject.AddComponent<Door>();
                door.position = doorPosition;
            }
        }
    }
}
