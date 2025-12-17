using CartClash.Grid;
using System.Collections.Generic;

namespace CartClash.PathFinding
{
    public class PathFindingService
    {
        private IPathfinder pathfinderBFS;

        public PathFindingService() => pathfinderBFS = new BFSPathfinder();

        public List<GridNode> FindPathWithBFS(GridNode startNode, GridNode targetNode, bool[,] walkableGrid) =>
            pathfinderBFS.FindPath(startNode, targetNode, walkableGrid);
    }
}