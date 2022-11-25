using Gameplay.Furniture;
using UnityEngine;

namespace Service
{
    public class LevelData : MonoBehaviour
    {
        [SerializeField] private FurniturePlace[] _furniturePlaces;

        public int Goal { get => _furniturePlaces.Length; }

        public int StarsCount { get; set; } = 0;

        public void ExecutePrelevelScripts()
        {
            var prelevelScripts = GetComponents<IPrelevelExecutable>();

            if (prelevelScripts == null)
                return;

            foreach (var script in prelevelScripts)
                script?.Execute();
        }
    }
}
