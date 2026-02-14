namespace CartClash.Grid.Tile
{
    /// <summary>
    /// Represents a single tile within the grid and its gameplay-relevant state.
    /// </summary>
    /// <remarks>
    /// <see cref="TileModel"/> is used by movement, pathfinding, and occupancy systems.
    /// Walkability and occupancy may change dynamically during gameplay.
    /// </remarks>
    public class TileModel
    {
        /// <summary>
        /// Grid position of this tile.
        /// </summary>
        public GridNode TilePosition { get; private set; }

        /// <summary>
        /// Indicates whether this tile can be traversed.
        /// </summary>
        public bool IsWalkable { get; private set; }

        /// <summary>
        /// Indicates whether this tile is currently occupied by a unit.
        /// </summary>
        public bool IsOccupied { get; private set; }

        /// <summary>
        /// Creates a new tile model with an initial state.
        /// </summary>
        public TileModel(GridNode tilePosition, bool isWalkable, bool isOccupied)
        {
            TilePosition = tilePosition;
            IsWalkable = isWalkable;
            IsOccupied = isOccupied;
        }

        /// <summary>
        /// Updates the walkable state of the tile.
        /// </summary>
        /// <remarks>
        /// Walkable tiles represents the unit can walk on this tile
        /// and are known as walkable by pathfinding systems.
        /// </remarks>
        public void SetWalkable(bool isWalkable) => IsWalkable = isWalkable;

        /// <summary>
        /// Updates the occupied state of the tile.
        /// </summary>
        /// <remarks>
        /// Occupied tiles represents units standing on this particular tile
        /// and are known as non-walkable by pathfinding systems.
        /// </remarks>
        public void SetOccupied(bool isOccupied) => IsOccupied = isOccupied;
    }
}