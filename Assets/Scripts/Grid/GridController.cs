using CartClash.Grid.Tile;
using System.Collections.Generic;
using UnityEngine;

namespace CartClash.Grid
{
    public class GridController
    {
        private readonly Dictionary<Vector2Int, TileView> tileViews = new();

        // Adds a TileView to the grid at the specified position
        public void AddTileView(TileView tileView, Vector2Int position)
        {
            if (tileViews.ContainsKey(position))
            {
                Debug.LogWarning($"TileView already exists at position {position}");
                return;
            }

            tileViews.Add(position, tileView);
            tileView.Initialize(position, true);
        }

        // Will be called when an obstacle is placed on the tile
        // Sets the tile at the specified position to default state
        public void SetTileBlocked(Vector2Int position, bool value)
        {
            if (!tileViews.TryGetValue(position, out TileView view)) return;
            view.SetBlocked(value);
        }

        //  Checks if a TileView exists at the given position
        public bool HasTile(Vector2Int position) => tileViews.ContainsKey(position);

        // Getter method for a TileView at the given position
        public TileView GetTileView(Vector2Int position)
        {
            tileViews.TryGetValue(position, out TileView view);
            return view;
        }
    }
}