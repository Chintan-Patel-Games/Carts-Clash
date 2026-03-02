namespace CartClash.Grid
{
    /// <summary>
    /// Represents a node in a two-dimensional grid, defined by its X and Y coordinates.
    /// </summary>
    public struct GridNode
    {
        public int x, y;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridNode"/> struct with the specified coordinates.
        /// </summary>
        public GridNode(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}