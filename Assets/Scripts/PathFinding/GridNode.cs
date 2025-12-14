namespace CartClash.PathFinding
{
    // Represents a node in the pathfinding grid
    public struct GridNode
    {
        public int x, y;

        public GridNode(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}