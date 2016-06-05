using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Level
{
    class RoomBuilder
    {
        List<Room> previousRooms;
        GameObject level;

        public RoomBuilder(List<Room> previousRooms, GameObject level)
        {
            this.previousRooms = previousRooms;
            this.level = level;
        }

        public Room Build()
        {
            if (this.previousRooms.Count == 0)
            {
                return createFirstRoom();
            }
            else
            {
                return createNextRoom();
            }
        }

        private Room createFirstRoom()
        {
            var room = initializeRoom();
            room.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            
            movePlayers(room);

            return room;
        }

        private  Room createNextRoom()
        {
            var room = initializeRoom();
            room.transform.localPosition = new Vector3(5.0f, 0.0f, -2.0f);

            foreach (var otherRoom in this.previousRooms)
            {
                room.SubtractRoom(otherRoom);
            }

            return room;
        }

        private Room initializeRoom()
        {
            var room = level.AddComponent<Room>();
            room.width = getRandomRoomSize();
            room.height = getRandomRoomSize();
            room.roomObject = new GameObject("Room");
            
            room.roomObject.transform.parent = level.transform;
            room.roomObject.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

            room.doors[Directions.North] = new int[] { getRandomDoorPosition(room.height) };
            room.doors[Directions.East] = new int[] { getRandomDoorPosition(room.width) };
            room.doors[Directions.West] = new int[] { getRandomDoorPosition(room.width) };
            room.doors[Directions.South] = new int[] { getRandomDoorPosition(room.height) };

            return room;
        }

        private void movePlayers(Room room)
        {
            Vector3 location = room.transform.position + new Vector3(room.width / 2, 0, -room.height / 2);

            var spawnPoint = GameObject.Find("PlayerSpawnPoint1");
            spawnPoint.transform.position = location;

            spawnPoint = GameObject.Find("PlayerSpawnPoint2");
            spawnPoint.transform.position = location;
        }

        private int getRandomRoomSize()
        {
            return Mathf.RoundToInt(Random.Range(10.0f, 20.0f));
        }

        private int getRandomDoorPosition(int size)
        {
            return Mathf.RoundToInt(Random.Range(2, size - 4));
        }

        private List<Transform> getPotentialNextRoomPositions()
        {
            var positions = new List<Transform>();

            foreach (Room room in this.previousRooms)
            {
                
            }

            return positions;
        }
    }
}
