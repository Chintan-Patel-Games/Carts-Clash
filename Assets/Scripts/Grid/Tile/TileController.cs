using CartClash.Grid.Tile.Model;
using CartClash.Grid.Tile.View;

namespace CartClash.Grid.Tile.Controller
{
    /// <summary>
    /// Provides control logic for managing a tile's state and view within a grid-based environment.
    /// </summary>
    public class TileController
    {
        private TileModel tileModel;
        private TileView tileView;

        public TileController(TileModel tileModel, TileView tileView)
        {
            this.tileModel = tileModel;
            this.tileView = tileView;
        }

        public void InitializeTile(GridNode tilePos) => tileView.Initialize(tilePos);

        public bool IsWalkable() => tileModel.IsWalkable && !tileModel.IsOccupied;

        public void SetBlocked(bool value) => tileModel.SetWalkable(!value);

        public void SetOccupied(bool value) => tileModel.SetOccupied(value);

        public TileState GetTileState() => tileModel.GetTileState();
    }
}