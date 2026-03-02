using UnityEngine;

namespace CartClash.Grid.Tile.View
{
    /// <summary>
    /// Represents a tile in a grid-based system, providing functionality to manage its state and position.
    /// </summary>
    public class TileView : MonoBehaviour
    {
        /// <summary>
        /// Gets the grid position associated with this instance.
        /// </summary>
        public GridNode TilePosition { get; private set; }

        /// <summary>
        /// Initializes the grid node with the specified position and walkability status.
        /// </summary>
        public void Initialize(GridNode position) => TilePosition = position;
    }
}