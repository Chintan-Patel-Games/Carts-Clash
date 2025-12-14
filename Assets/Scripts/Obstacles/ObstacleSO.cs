using UnityEngine;

namespace CartClash.Obstacles
{
    [CreateAssetMenu(fileName = "ObstacleSO", menuName = "ScriptableObjects/ObstacleSO")]
    public class ObstacleSO : ScriptableObject
    {
        public int width = 10;
        public int height = 10;

        [Tooltip("true = blocked tile")]
        public bool[] blockedTiles;

        public void Initialize() => blockedTiles = new bool[width * height];

        // Returns true if the tile at (x, y) is blocked
        public bool IsBlocked(int x, int y)
        {
            int index = x + y * width;
            return blockedTiles[index];
        }

        // Sets the blocked state of the tile at (x, y)
        public void SetBlocked(int x, int y, bool isBlocked)
        {
            int index = x + y * width;
            blockedTiles[index] = isBlocked;
        }
    }
}