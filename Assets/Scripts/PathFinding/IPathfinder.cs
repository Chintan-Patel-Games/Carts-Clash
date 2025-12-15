using CartClash.Grid;
using System.Collections.Generic;

namespace CartClash.PathFinding
{
    public interface IPathfinder
    {
        public List<GridNode> FindPath(GridNode start, GridNode end, bool[,] grid);
    }
}