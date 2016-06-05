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

        public void Start()
        {
            Wall wall;

            wall = createWall(width, Directions.North, new Vector3(0.0f, 0.0f, 0.0f));
            addDoors(wall, Directions.North);

            wall = createWall(height, Directions.East, new Vector3(width, 0.0f, 0.0f));
            addDoors(wall, Directions.East);

            wall = createWall(width, Directions.South, new Vector3(width, 0.0f, -height));
            addDoors(wall, Directions.South);

            wall = createWall(height, Directions.West, new Vector3(0.0f, 0.0f, -height));
            addDoors(wall, Directions.West);
        }

        public void SubtractRoom(Room other)
        {
            var wallComponents = GetComponentsInChildren<WallComponent>();

            foreach (var wallComponent in wallComponents)
            {
                wallComponent.SubtractRoom(other);
            }
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
