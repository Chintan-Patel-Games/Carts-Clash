using UnityEngine;
using System.Collections.Generic;
using CartClash.Grid.Tile;

namespace CartClash.Grid
{
    public class GridModel
    {
        // Stores the tiles in the grid
        private readonly Dictionary<Vector2Int, TileModel> tiles;

        public GridModel() => tiles = new Dictionary<Vector2Int, TileModel>();

        // Checks if the tile exists at given position
        public bool HasTile(Vector2Int position) => tiles.ContainsKey(position);

        // Adds a new tile to the grid
        public void AddTile(TileModel tile) => tiles[tile.tilePosition] = tile;

        // Getter nethod for a tile at the given position
        public TileModel GetTile(Vector2Int position)
        {
            tiles.TryGetValue(position, out var tile);
            return tile;
        }

        // Getter method for all tiles in the grid
        public IEnumerable<TileModel> GetAllTiles() => tiles.Values;
    }
}