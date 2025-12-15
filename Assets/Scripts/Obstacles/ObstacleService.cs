using CartClash.Grid;
using CartClash.Utilities;
using UnityEngine;

namespace CartClash.Obstacles
{
    public class ObstacleService : GenericMonoSingleton<ObstacleService>
    {
        [Header("Obstacle SO")]
        [SerializeField] private ObstacleSO obstacleSO;

        [Header("Obstacle Prefabs")]
        [SerializeField] private GameObject[] obstaclePrefabs;

        [SerializeField] private Transform obstacleParent;

        // Applies obstacles to the grid based on the ObstacleSO configuration
        public void ApplyObstacles()
        {
            if (obstacleSO == null) return;

            for (int x = 0; x < obstacleSO.width; x++)
            {
                for (int y = 0; y < obstacleSO.height; y++)
                {
                    if (!obstacleSO.IsBlocked(x, y)) continue;

                    GridNode gridPos = new(x, y);

                    if (!GridService.Instance.IsWalkable(gridPos)) continue;

                    SpawnObstacle(gridPos);
                    GridService.Instance.SetBlocked(gridPos, true);
                }
            }
        }

        // Spawns an obstacle at the specified grid position
        public void SpawnObstacle(GridNode gridPos)
        {
            Vector3 worldPos = GridService.Instance.GetWorldPosition(gridPos);
            GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            Instantiate(prefab, worldPos, Quaternion.identity, obstacleParent);
        }
    }
}