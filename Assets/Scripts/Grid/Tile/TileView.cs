using UnityEngine;

namespace CartClash.Grid.Tile
{
    public class TileView : MonoBehaviour
    {
        public GridNode gridPosition { get; private set; }

        private TileState currentState = TileState.DEFAULT;

        public void Initialize(GridNode position, bool isWalkable)
        {
            gridPosition = position;
            SetBlocked(!isWalkable);
            SetOccupied(!isWalkable);
        }

        // Sets the tile's blocked state
        public void SetBlocked(bool value) =>
            currentState = value ? TileState.BLOCKED : TileState.DEFAULT;

        // Sets the tile's occupied state
        public void SetOccupied(bool value) =>
            currentState = value ? TileState.OCCUPIED : TileState.DEFAULT;

        public TileState GetTileState() => currentState;
    }
}