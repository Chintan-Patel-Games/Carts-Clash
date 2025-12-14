using System.Collections.Generic;
using UnityEngine;

namespace CartClash.PathFinding
{
    // BFS Pathfinding implementation
    public class BFSPathfinder : IPathfinder
    {
        private static readonly Vector2Int[] Directions =
        {
            new Vector2Int(0, 1),   // Up
            new Vector2Int(1, 0),   // Right
            new Vector2Int(0, -1),  // Down
            new Vector2Int(-1, 0)   // Left
        };

        public List<GridNode> FindPath(GridNode start, GridNode goal, bool[,] grid)
        {
            int width = grid.GetLength(0);
            int height = grid.GetLength(1);

            Queue<GridNode> queue = new();
            Dictionary<GridNode, GridNode> cameFrom = new();
            HashSet<GridNode> visited = new();

            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                GridNode current = queue.Dequeue();

                if (current.x == goal.x && current.y == goal.y)
                    return ReconstructPath(cameFrom, current, goal);

                foreach (var direction in Directions)
                {
                    int nx = current.x + direction.x;
                    int ny = current.y + direction.y;

                    if (!IsValid(nx, ny, grid))
                        continue;

                    GridNode neighbor = new(nx, ny);

                    if (visited.Contains(neighbor))
                        continue;

                    visited.Add(neighbor);
                    cameFrom[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
            }

            return null; // No path found
        }

        private bool IsValid(int x, int y, bool[,] grid)
        {
            int width = grid.GetLength(0);
            int height = grid.GetLength(1);
            return x >= 0 && x < width && y >= 0 && y < height && grid[x, y];
        }

        private List<GridNode> ReconstructPath(Dictionary<GridNode, GridNode> cameFrom, GridNode start, GridNode goal)
        {
            List<GridNode> path = new();
            GridNode current = goal;

            while (!current.Equals(start))
            {
                path.Add(current);
                current = cameFrom[current];
            }

            path.Add(start);
            path.Reverse();
            return path;
        }
    }
}