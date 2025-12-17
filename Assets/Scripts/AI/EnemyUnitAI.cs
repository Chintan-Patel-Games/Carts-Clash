using CartClash.Core;
using CartClash.Grid;
using CartClash.PathFinding;
using CartClash.Units.Enemy;
using System.Collections.Generic;
using UnityEngine;

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

        public List<GridNode> GeneratePathFrom(GridNode startNode, GridNode endNode)
        {
            bool[,] walkableGrid = GameService.Instance.GridService.GetWalkableGrid;

            walkableGrid[endNode.x, endNode.y] = true;

            return pathfinder.FindPathWithBFS(startNode, endNode, walkableGrid);
        }

        // Generates a new path using BFS pathfinding algorithm
        public List<GridNode> GeneratePath(GridNode targetNode)
        {
            if (enemy == null) return null;

            GridNode startNode = enemy.GetCurrentEnemyNode();

            GridNode? chaseNode = GetAdjacentWalkableTile(targetNode, startNode);

            if (chaseNode == null) return null;

            bool[,] walkableGrid = GameService.Instance.GridService.GetWalkableGrid;

            var path = pathfinder.FindPathWithBFS(startNode, chaseNode.Value, walkableGrid);

            if (path == null || path.Count == 0) return null;

            return path;
        }

        private GridNode? GetAdjacentWalkableTile(GridNode targetNode, GridNode startNode)
        {
            GridNode[] neighbours =
            {
                new(targetNode.x + 1, targetNode.y),
                new(targetNode.x - 1, targetNode.y),
                new(targetNode.x, targetNode.y + 1),
                new(targetNode.x, targetNode.y - 1),
            };

            GridNode? best = null;
            int bestDist = int.MaxValue;

            foreach (var n in neighbours)
            {
                if (!GameService.Instance.GridService.IsWalkable(n))
                    continue;

                int dist =
                    Mathf.Abs(n.x - startNode.x) +
                    Mathf.Abs(n.y - startNode.y);

                if (dist < bestDist)
                {
                    bestDist = dist;
                    best = n;
                }
            }

            return best;
        }
    }
}