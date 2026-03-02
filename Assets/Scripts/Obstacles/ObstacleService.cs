using CartClash.Core;
using CartClash.Grid;
using CartClash.Utilities;
using UnityEngine;
using CartClash.Obstacles.SO;

namespace CartClash.Obstacles.Service
{
    /// <summary>
    /// Provides functionality for applying and spawning obstacles within the grid, using configuration data and prefab
    /// references.
    /// </summary>
    public class ObstacleService : GenericMonoSingleton<ObstacleService>
    {
        [Header("Obstacle SO")]
        [SerializeField] private ObstacleSO obstacleSO;

        [Header("Obstacle Prefabs")]
        [SerializeField] private GameObject[] obstaclePrefabs;

        [SerializeField] private Transform obstacleParent;

        /// <summary>
        /// Applies obstacles to the grid based on the ObstacleSO configuration
        /// </summary>
        public void ApplyObstacles()
        {
            if (obstacleSO == null) return;

            for (int x = 0; x < obstacleSO.width; x++)
            {
                for (int y = 0; y < obstacleSO.height; y++)
                {
                    if (!obstacleSO.IsBlocked(x, y)) continue;

                    GridNode gridPos = new(x, y);

                    if (!GameService.Instance.GridService.IsTileWalkable(gridPos)) continue;

                    SpawnObstacle(gridPos);
                    GameService.Instance.GridService.SetTileBlocked(gridPos, true);
                }
            }
        }

        public void SpawnObstacle(GridNode gridPos)
        {
            Vector3 worldPos = GameService.Instance.GridService.GetWorldPosition(gridPos);
            GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            Instantiate(prefab, worldPos, Quaternion.identity, obstacleParent);
        }
    }
}