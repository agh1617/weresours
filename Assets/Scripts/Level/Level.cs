using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Level
{
    class Level : MonoBehaviour
    {
        public LevelRenderer levelRenderer;
        public int roomCount;

        private List<Room> rooms;

        void Start()
        {
            rooms = new List<Room>();
            generateRooms();

            transform.position = new Vector3(0, 0, 0);

            levelRenderer.Render(rooms.ToArray());
        }

        private void generateRooms()
        {
            for (int i = 0; i < this.roomCount; i++)
            {
                var builder = new RoomBuilder(this.rooms, this.gameObject);
                Room room = builder.Build();
                room.levelRenderer = this.levelRenderer;
                this.rooms.Add(room);
            }
        }
    }
}
