using UnityEngine;

namespace Assets.Scripts.Level
{
    class LevelRenderer : MonoBehaviour
    {
        public GameObject wallComponent;
        public GameObject doorComponent;
        public GameObject floorComponent;
        public Material[] floorMaterials;

        public void Render(Room[] rooms)
        {
        }
    }
}
