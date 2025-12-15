using CartClash.Core;
using CartClash.Grid;
using CartClash.Units.Player;

namespace CartClash.PathFinding
{
    public class PathFindingService
    {
        private IPathfinder pathfinder;
        private PlayerUnitService player;

        public PathFindingService() => pathfinder = new BFSPathfinder();

        public void OnEnable() =>
            GameService.Instance.EventService.OnTileSelected.AddListener(OnTileSelected);

        public void OnDisable() =>
            GameService.Instance.EventService.OnTileSelected.RemoveListener(OnTileSelected);

        public void Initialize() => player = GameService.Instance.PlayerUnitService;

        private void OnTileSelected(GridNode targetPos)
        {
            if (player == null) return;

            GridNode startNode = player.GetCurrentPlayerNode();
            GridNode targetNode = new(targetPos.x, targetPos.y);

            bool[,] walkableGrid = GameService.Instance.GridService.GetWalkableGrid;

            var path = pathfinder.FindPath(startNode, targetNode, walkableGrid);

            if (path == null || path.Count == 0) return;

            player.SetPath(path);
        }
    }
}