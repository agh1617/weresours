using UnityEngine;

namespace Assets.Scripts.Level
{
    class LevelRenderer : MonoBehaviour
    {
        public GameObject wallComponent;
        public GameObject doorComponent;

        public void Render(Room[] rooms)
        {
            var level = new GameObject("Level");
            var room = level.AddComponent<Room>();
            room.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            room.levelRenderer = this;
            room.width = 15;
            room.height = 10;
            room.roomObject = new GameObject("Room");
            room.roomObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            room.roomObject.transform.parent = level.transform;

            room.doors[Directions.North] = new int[] { 1, 8 };
            room.doors[Directions.East] = new int[] { 4 };
            room.doors[Directions.West] = new int[] { };
            room.doors[Directions.South] = new int[] { 6 };
        }
    }
}
