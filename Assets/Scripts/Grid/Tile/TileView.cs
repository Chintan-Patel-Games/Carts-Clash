using UnityEngine;

namespace CartClash.Grid.Tile
{
    public class TileView : MonoBehaviour
    {
        public Vector2Int gridPosition { get; private set; }

        private TileState currentState = TileState.DEFAULT;

        public void Initialize(Vector2Int position, bool isWalkable)
        {
            gridPosition = position;
            SetBlocked(!isWalkable);
        }

        // Sets the tile's blocked state
        public void SetBlocked(bool value) =>
            currentState = value ? TileState.BLOCKED : TileState.DEFAULT;

        public TileState GetTileState() => currentState;
    }
}