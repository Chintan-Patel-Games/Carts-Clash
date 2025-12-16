using CartClash.Core;
using CartClash.Grid;
using CartClash.PathFinding;
using CartClash.Units.Player;

namespace CartClash.AI
{
    public class PlayerUnitAI
    {
        private PlayerUnitService player;
        private PathFindingService pathfinder;

        // Constructor for setting player instance
        public PlayerUnitAI(PlayerUnitService player, PathFindingService pathfinder)
        {
            this.player = player;
            this.pathfinder = pathfinder;
        }

        // Subscribe to events
        public void OnEnable() =>
            GameService.Instance.EventService.OnTileSelected.AddListener(OnTileSelected);

        // UnSubscribe to events
        public void OnDisable() =>
            GameService.Instance.EventService.OnTileSelected.RemoveListener(OnTileSelected);

        // Method for the OnTileSelected event to call during invoke
        private void OnTileSelected(GridNode targetNode)
        {
            if (player == null) return;

            GridNode startNode = player.GetCurrentPlayerNode();

            bool[,] walkableGrid = GameService.Instance.GridService.GetWalkableGrid;

            var path = pathfinder.FindPathWithBFS(startNode, targetNode, walkableGrid);

            if (path == null || path.Count == 0) return;

            player.SetPath(path);
        }
    }
}