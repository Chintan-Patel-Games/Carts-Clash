using System.Collections.Generic;
using CartClash.Grid.Tile.Model;

namespace CartClash.Grid.Model
{
    /// <summary>
    /// Represents a grid structure that manages a collection of tiles, each identified by a grid position.
    /// </summary>
    public class GridModel
    {
        /// <summary>
        /// Represents the mapping of grid nodes to their associated tile models.
        /// </summary>
        private readonly Dictionary<GridNode, TileModel> tiles = new();

        public void AddTile(GridNode node, TileModel tile) => tiles[node] = tile;

        public TileModel GetTile(GridNode node) => tiles.TryGetValue(node, out var tile) ? tile : null;

        public bool HasTile(GridNode node) => tiles.ContainsKey(node);
    }
}