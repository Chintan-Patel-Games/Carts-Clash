namespace CartClash.Grid.Tile
{
    // Model representing a single tile in the grid
    public class TileModel
    {
        public GridNode tilePosition { get; private set; }
        public bool isWalkable { get; private set; }
        public bool isOccupied { get; private set; }

        public TileModel(GridNode tilePosition, bool isWalkable, bool isOccupied)
        {
            this.tilePosition = tilePosition;
            this.isWalkable = isWalkable;
            this.isOccupied = isOccupied;
        }

        // Method to set walkable state
        public void SetWalkable(bool isWalkable) => this.isWalkable = isWalkable;

        // Method to set occupied state
        public void SetOccupied(bool isOccupied) => this.isOccupied = isOccupied;
    }
}