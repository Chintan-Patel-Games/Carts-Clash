using UnityEngine;

namespace CartClash.Grid.Tile
{
    /// <summary>
    /// Represents a tile in a grid-based system, providing functionality to manage its state and position.
    /// </summary>
    /// <remarks>
    /// The <see cref="TileView"/> class is designed to work within a grid-based environment,
    /// where each tile has a specific position and state.
    /// It supports managing the tile's walkability, occupancy, and other state-related properties.
    /// This class is typically used in pathfinding, game grids, or similar systems.
    /// </remarks>
    public class TileView : MonoBehaviour
    {
        /// <summary>
        /// Gets the grid position associated with this instance.
        /// </summary>
        public GridNode GridPosition { get; private set; }

        /// <summary>
        /// Represents the current state of the tile.
        /// </summary>
        /// <remarks>
        /// The state is represented as a value of the <see cref="TileState"/> enum.
        /// This field is initialized to <see cref="TileState.DEFAULT"/> by default.
        /// </remarks>

        private TileState CurrentState = TileState.DEFAULT;

        /// <summary>
        /// Initializes the grid node with the specified position and walkability status.
        /// </summary>
        /// <param name="position"> The position of the grid node within the grid. </param>
        /// <param name="isWalkable"> A value indicating whether the grid node is walkable.
        /// <see langword="true"/> If the node can be traversed; otherwise, <see langword="false"/>. </param>
        public void Initialize(GridNode position, bool isWalkable)
        {
            GridPosition = position;
            SetBlocked(!isWalkable);
            SetOccupied(!isWalkable);
        }

        /// <summary>
        /// Sets the current state of the tile to either blocked or default.
        /// </summary>
        /// <param name="value">A boolean value indicating whether the tile should be blocked.  <see langword="true"/> sets the state to
        /// <see cref="TileState.BLOCKED"/>; <see langword="false"/> sets the state to <see cref="TileState.DEFAULT"/>.</param>
        public void SetBlocked(bool value) => CurrentState = value ? TileState.BLOCKED : TileState.DEFAULT;

        /// <summary>
        /// Sets the tile's state to indicate whether it is occupied.
        /// </summary>
        /// <param name="value"> A boolean value indicating the desired state of the tile.  <see langword="true"/>
        /// sets the state to <see cref="TileState.OCCUPIED"/>, while <see langword="false"/> sets it to <see cref="TileState.DEFAULT"/>. </param>
        public void SetOccupied(bool value) => CurrentState = value ? TileState.OCCUPIED : TileState.DEFAULT;

        /// <summary>
        /// Gets the current state of the tile.
        /// </summary>
        /// <returns>
        /// The current <see cref="TileState"/> representing the state of the tile.</returns>
        public TileState GetTileState() => CurrentState;
    }
}