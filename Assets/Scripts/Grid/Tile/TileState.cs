namespace CartClash.Grid.Tile
{
    /// <summary>
    /// Represents the logical state of a grid tile used for movement and pathfinding.
    /// </summary>
    public enum TileState
    {
        /// <summary>
        /// Tile is  walkable.
        /// </summary>
        WALKABLE,

        /// <summary>
        /// Tile is currently occupied by a unit.
        /// </summary>
        OCCUPIED,

        /// <summary>
        /// Tile is permanently blocked by an obstacle.
        /// </summary>
        BLOCKED
    }
}