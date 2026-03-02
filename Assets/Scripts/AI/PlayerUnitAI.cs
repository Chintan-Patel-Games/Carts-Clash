using CartClash.Core;
using CartClash.Grid;
using CartClash.PathFinding;
using CartClash.Units.Player;
using System.Collections.Generic;
using UnityEngine;

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

        // Generates a new path using BFS pathfinding algorithm
        public List<GridNode> GeneratePath(GridNode targetNode)
        {
            if (player == null) return null;

            GridNode startNode = player.GetCurrentPlayerNode();

            bool[,] walkableGrid = GameService.Instance.GridService.GetWalkableGrid();

            var path = pathfinder.FindPathWithBFS(startNode, targetNode, walkableGrid);

            if (path == null || path.Count == 0)
            {
                Debug.LogWarning("No path found for player unit.");
                return null;
            }

            return path;
        }
    }
}