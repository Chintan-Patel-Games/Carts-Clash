namespace CartClash.Grid
{
    /// <summary>
    /// Represents a node in a two-dimensional grid, defined by its X and Y coordinates.
    /// </summary>
    /// <remarks>
    /// Used throughout the grid, pathfinding, and unit movement systems to represent
    /// discrete tile positions.
    /// </remarks>
    public struct GridNode
    {
        /// <summary>
        /// X coordinate.
        /// </summary>
        public int x;

        /// <summary>
        /// Y coordinate.
        /// </summary>
        public int y;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridNode"/> struct with the specified coordinates.
        /// </summary>
        /// <param name="x"> X coordinate. </param>
        /// <param name="y"> Y coordinate. </param>
        public GridNode(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}