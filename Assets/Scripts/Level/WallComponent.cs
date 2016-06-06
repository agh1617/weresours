using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Level
{
    class WallComponent : MonoBehaviour
    {
        public GameObject sourceObject;
        public float x;
        public float size;

        public GameObject wallComponentObject;

        public List<Room> roomsToRemove;

        public void Start()
        {
            var position = new Vector3(0.0f, 0.0f, 0.0f);
            var rotation = new Quaternion();

            for (int i = 0; i < size; i++)
            {
                var brick = (GameObject)Instantiate(sourceObject, position, rotation);
                brick.transform.SetParent(wallComponentObject.transform, false);
                Brick brickComponent = brick.AddComponent<Brick>();
                position.x += 1;
            }
            
            wallComponentObject.transform.localPosition = new Vector3(x, 0.5f, 0.0f);

            removeOverlappingWallComponents();
        }

        public void SubtractRoom(Room other)
        {
            this.roomsToRemove.Add(other);
        }

        private void removeOverlappingWallComponents()
        {
            //Vector3 otherRoomRelative = transform.InverseTransformDirection(other.transform.position);
            
            //var walls = GetComponents<Wall>();
            foreach (var room in this.roomsToRemove)
            {
                var otherPosition = room.roomObject.transform.position;

                foreach (Transform wall in wallComponentObject.transform)
                {
                    var position = wall.position;

                    if (otherPosition.x - 0.5 < position.x && (otherPosition.x + room.width) > position.x - 0.5)
                    {
                        if (otherPosition.z - room.height - 0.5 < position.z && (otherPosition.z) > position.z - 0.5)
                        {
                            wall.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}
