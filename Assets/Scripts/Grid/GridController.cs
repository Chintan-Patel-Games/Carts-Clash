using CartClash.Grid.Tile;
using System.Collections.Generic;
using UnityEngine;

namespace CartClash.Grid
{
    public class GridController
    {
        private readonly Dictionary<GridNode, TileView> tileViews = new();

        // Adds a TileView to the grid at the specified position
        public void AddTileView(TileView tileView, GridNode position)
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
        // Sets the tile at the specified position to blocked state
        public void SetTileBlocked(GridNode position, bool value)
        {
            if (!tileViews.TryGetValue(position, out TileView tileView)) return;
            tileView.SetBlocked(value);
        }

        // Will be called when an unit is standing on the tile
        // Sets the tile at the specified position to occupied state
        public void SetTileOccupied(GridNode position, bool value)
        {
            if (!tileViews.TryGetValue(position, out TileView tileView)) return;
            tileView.SetOccupied(value);
        }

        //  Checks if a TileView exists at the given position
        public bool HasTile(GridNode position) => tileViews.ContainsKey(position);

        // Getter method for a TileView at the given position
        public TileView GetTileView(GridNode position)
        {
            tileViews.TryGetValue(position, out TileView view);
            return view;
        }
    }
}