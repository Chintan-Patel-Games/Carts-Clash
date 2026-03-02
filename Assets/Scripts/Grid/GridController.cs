using CartClash.Grid.Tile;
using CartClash.Grid.Tile.Controller;
using CartClash.Grid.Tile.Model;
using CartClash.Grid.Tile.View;
using System.Collections.Generic;

namespace CartClash.Grid
{
    /// <summary>
    /// Provides functionality for managing tiles within a grid, including creation, state updates, and querying tile
    /// properties.
    /// </summary>
    public class GridController
    {
        private readonly Dictionary<GridNode, TileController> tileControllers = new();
        private readonly Dictionary<GridNode, TileView> tileViews = new();
        private readonly GridModel gridModel;

        public GridController(GridModel model) => gridModel = model;

        public void CreateTile(GridNode node, TileView view)
        {
            TileModel model = new();
            TileController controller = new(model, view);

            controller.InitializeTile(node);

            gridModel.AddTile(node, model);
            tileControllers[node] = controller;
            tileViews[node] = view;
        }

        public bool IsTileWalkable(GridNode node)
        {
            if (!tileControllers.TryGetValue(node, out var tile)) return false;
            return tile.IsWalkable();
        }

        public void SetTileBlocked(GridNode position, bool value)
        {
            if (!tileControllers.TryGetValue(position, out var tile)) return;
            tile.SetBlocked(value);
        }
        
        public void SetTileOccupied(GridNode position, bool value)
        {
            if (!tileControllers.TryGetValue(position, out var tile)) return;
            tile.SetOccupied(value);
        }

        public bool HasTile(GridNode position) => tileViews.ContainsKey(position);

        public TileView GetTileView(GridNode position) => tileViews.TryGetValue(position, out TileView view) ? view : null;

        public bool[,] GetWalkableGrid(int width, int height)
        {
            bool[,] grid = new bool[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    GridNode node = new(x, y);

                    if (!tileControllers.TryGetValue(node, out var tile))
                    {
                        grid[x, y] = false;
                        continue;
                    }

                    grid[x, y] = tile.IsWalkable();
                }
            }

            return grid;
        }

        public TileState GetTileState(GridNode position)
        {
            if (!tileControllers.TryGetValue(position, out var tile)) return TileState.BLOCKED;
            return tile.GetTileState();
        }
    }
}