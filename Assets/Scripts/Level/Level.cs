using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Level
{
    class Level : MonoBehaviour
    {
        public Room[] rooms;
        public LevelRenderer levelRenderer;

        void Start()
        {
            rooms = GetComponentsInChildren<Room>();
            levelRenderer.Render(rooms);
        }
    }
}
