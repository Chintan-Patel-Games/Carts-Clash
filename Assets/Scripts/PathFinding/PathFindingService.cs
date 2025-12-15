using CartClash.Core;
using CartClash.Grid;
using CartClash.Units.Enemy;
using CartClash.Units.Player;
using UnityEngine;

namespace CartClash.PathFinding
{
    public class PathFindingService
    {
        private IPathfinder pathfinder;
        private PlayerUnitService player;
        private EnemyUnitService enemy;

        public PathFindingService() => pathfinder = new BFSPathfinder();

        public void OnEnable()
        {
            GameService.Instance.EventService.OnTileSelected.AddListener(OnTileSelected);
            GameService.Instance.EventService.StartChasingPlayer.AddListener(EnemyChasePlayer);
        }

        public void OnDisable()
        {
            GameService.Instance.EventService.OnTileSelected.RemoveListener(OnTileSelected);
            GameService.Instance.EventService.StartChasingPlayer.RemoveListener(EnemyChasePlayer);
        }

        public void Initialize()
        {
            player = GameService.Instance.PlayerUnitService;
            enemy = GameService.Instance.EnemyUnitService;
        }

        private void OnTileSelected(GridNode targetNode)
        {
            if (player == null) return;

            GridNode startNode = player.GetCurrentPlayerNode();

            bool[,] walkableGrid = GameService.Instance.GridService.GetWalkableGrid;

            var path = pathfinder.FindPath(startNode, targetNode, walkableGrid);

            if (path == null || path.Count == 0) return;

            player.SetPath(path);
        }

        private void EnemyChasePlayer(GridNode targetNode)
        {
            if (enemy == null) return;

            GridNode startNode = enemy.GetCurrentEnemyNode();

            bool[,] walkableGrid = GameService.Instance.GridService.GetWalkableGrid;

            var path = pathfinder.FindPath(startNode, targetNode, walkableGrid);

            if (path.Count > 1)
                path.RemoveRange(path.Count - 1, 1);

            if (path == null || path.Count == 0) return;

            enemy.SetPath(path);
        }
    }
}