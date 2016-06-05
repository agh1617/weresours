using UnityEngine;

namespace Assets.Scripts.Level
{
    class WallComponent : MonoBehaviour
    {
        public GameObject sourceObject;
        public float x;
        public float size;

        public GameObject wallComponentObject;

        public WallComponent(GameObject sourceObject, float x, float size, Directions direction)
        {
            this.sourceObject = sourceObject;
            this.x = x;
            this.size = size;
        }

        public void Start()
        {
            var position = new Vector3(0.0f, 0.0f, 0.0f);
            var rotation = new Quaternion();

            for (int i = 0; i < size; i++)
            {
                var wallElement = (GameObject)Instantiate(sourceObject, position, rotation);
                wallElement.transform.SetParent(wallComponentObject.transform, false);
                position.x += 1;
            }
            
            wallComponentObject.transform.localPosition = new Vector3(x, 0.5f, 0.0f);
        }

        public void SubtractRoom(Room other)
        {
            Vector3 otherRoomRelative = transform.InverseTransformDirection(other.transform.position);
            
            foreach (Transform wall in transform)
            {
                var position = wall.localPosition;
                if (otherRoomRelative.x < position.x && (otherRoomRelative.x + other.width) > position.x)
                {
                    if (otherRoomRelative.y < position.y && (otherRoomRelative.y + other.height) > position.y)
                    {
                        wall.gameObject.SetActive(false);
                    }
                }
            }

        }
    }
}
