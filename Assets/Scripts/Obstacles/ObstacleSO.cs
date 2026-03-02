using UnityEngine;

namespace CartClash.Obstacles
{
    /// <summary>
    /// Represents a grid-based obstacle configuration for use in Unity scenes, defining blocked and unblocked tiles
    /// within a specified width and height.
    /// </summary>
    [CreateAssetMenu(fileName = "ObstacleSO", menuName = "ScriptableObjects/ObstacleSO")]
    public class ObstacleSO : ScriptableObject
    {
        public int width = 10;
        public int height = 10;

        [Tooltip("true = blocked tile")]
        public bool[] blockedTiles;

        public void Initialize() => blockedTiles = new bool[width * height];

        public bool IsBlocked(int x, int y)
        {
            int index = x + y * width;
            return blockedTiles[index];
        }

        public void SetBlocked(int x, int y, bool isBlocked)
        {
            int index = x + y * width;
            blockedTiles[index] = isBlocked;
        }
    }
}