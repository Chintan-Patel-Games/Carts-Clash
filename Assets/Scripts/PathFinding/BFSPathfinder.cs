using CartClash.Grid;
using System.Collections.Generic;
using UnityEngine;

namespace CartClash.PathFinding
{
    // BFS Pathfinding implementation
    public class BFSPathfinder : IPathfinder
    {
        private static readonly Vector2Int[] Directions =
        {
            Vector2Int.up,     // Up
            Vector2Int.right,  // Right
            Vector2Int.down,   // Down
            Vector2Int.left    // Left
        };

        public List<GridNode> FindPath(GridNode start, GridNode goal, bool[,] grid)
        {
            int width = grid.GetLength(0);
            int height = grid.GetLength(1);

            Queue<Vector2Int> queue = new();
            Dictionary<Vector2Int, Vector2Int> cameFrom = new();
            HashSet<GridNode> visited = new();

            Vector2Int startNode = new(start.x, start.y);
            Vector2Int goalNode = new(goal.x, goal.y);

            queue.Enqueue(startNode);
            cameFrom[startNode] = startNode;

            while (queue.Count > 0)
            {
                Vector2Int current = queue.Dequeue();

                if (current == goalNode) break;

                foreach (var direction in Directions)
                {
                    Vector2Int next = current + direction;

                    if (next.x < 0 || next.x >= width ||
                        next.y < 0 || next.y >= height)
                        continue;

                    if (!grid[next.x, next.y])
                        continue;

                    if (cameFrom.ContainsKey(next))
                        continue;

                    queue.Enqueue(next);
                    cameFrom[next] = current;
                }
            }

            if (!cameFrom.ContainsKey(goalNode))
                return null; // No path found

            return ReconstructPath(cameFrom, startNode, goalNode);
        }

        private List<GridNode> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int start, Vector2Int goal)
        {
            List<GridNode> path = new();
            Vector2Int step = goal;

            while (step != start)
            {
                path.Add(new GridNode(step.x, step.y));
                step = cameFrom[step];
            }

            path.Add(new GridNode(step.x, step.y));
            path.Reverse();

            return path;
        }
    }
}