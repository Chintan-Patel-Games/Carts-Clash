using CartClash.Core;
using CartClash.Grid;
using CartClash.PathFinding;
using CartClash.Units.Enemy;

namespace CartClash.AI
{
    public class EnemyUnitAI
    {
        private EnemyUnitService enemy;
        private PathFindingService pathfinder;

        // Constructor for setting enemy instance
        public EnemyUnitAI(EnemyUnitService enemy, PathFindingService pathfinder)
        {
            this.enemy = enemy;
            this.pathfinder = pathfinder;
        }

        // Subscribe to events
        public void OnEnable() =>
            GameService.Instance.EventService.StartChasingPlayer.AddListener(EnemyChasePlayer);

        // UnSubscribe to events
        public void OnDisable() =>
            GameService.Instance.EventService.StartChasingPlayer.RemoveListener(EnemyChasePlayer);

        // Method for the EnemyChasePlayer event to call during invoke
        private void EnemyChasePlayer(GridNode targetNode)
        {
            if (enemy == null) return;

            GridNode startNode = enemy.GetCurrentEnemyNode();

            bool[,] walkableGrid = GameService.Instance.GridService.GetWalkableGrid;

            var path = pathfinder.FindPathWithBFS(startNode, targetNode, walkableGrid);

            if (path == null || path.Count == 0) return;

            if (path.Count > 1)
                path.RemoveRange(path.Count - 1, 1);

            enemy.SetPath(path);
        }
    }
}