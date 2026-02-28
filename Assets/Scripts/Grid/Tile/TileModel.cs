namespace CartClash.Grid.Tile.Model
{
    /// <summary>
    /// Represents a single tile within the grid and its gameplay-relevant state.
    /// </summary>
    public class TileModel
    {
        /// <summary>
        /// Indicates whether this tile can be traversed.
        /// </summary>
        public bool IsWalkable { get; private set; } = true;

        /// <summary>
        /// Indicates whether this tile is occupied by any unit.
        /// </summary>
        public bool IsOccupied { get; private set; }

        public void SetWalkable(bool value) => IsWalkable = value;
        public void SetOccupied(bool value) => IsOccupied = value;

        public TileState GetTileState()
        {
            if (!IsWalkable)
                return TileState.BLOCKED;

            if (IsOccupied)
                return TileState.OCCUPIED;

            return TileState.WALKABLE;
        }
    }
}