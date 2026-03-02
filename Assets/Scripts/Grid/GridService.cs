using CartClash.Grid.Tile;
using CartClash.Grid.Tile.View;
using CartClash.Grid.Controller;
using CartClash.Grid.Model;
using CartClash.Utilities;
using UnityEngine;

namespace CartClash.Grid.Service
{
    /// <summary>
    /// Provides centralized management and operations for a grid-based environment, including grid initialization,
    /// tile state control, and position calculations.
    /// </summary>
    public class GridService : GenericMonoSingleton<GridService>
    {
        [Header("Grid")]
        [SerializeField] private int gridX = 10;
        [SerializeField] private int gridZ = 10;
        [SerializeField] private float tileSpacing = 1f;

        [Header("Tile View")]
        [SerializeField] private TileView tilePrefab;
        [SerializeField] private Transform tileParent;

        private GridModel gridModel;
        private GridController gridController;

        protected override void Awake()
        {
            base.Awake();

            gridModel = new GridModel();
            gridController = new GridController(gridModel);
        }

        public void InitializeGrid()
        {
            for (int x = 0; x < gridX; x++)
            {
                for (int z = 0; z < gridZ; z++)
                {
                    GridNode gridPos = new(x, z);
                    Vector3 worldPos = GetWorldPosition(gridPos);

                    TileView view = Instantiate(tilePrefab, worldPos, Quaternion.identity, tileParent);
                    gridController.CreateTile(gridPos, view);
                }
            }
        }

        public bool IsTileWalkable(GridNode gridPos) => gridController.IsTileWalkable(gridPos);

        public void SetTileBlocked(GridNode gridPos, bool value) => gridController.SetTileBlocked(gridPos, value);

        public void SetOccupied(GridNode gridPos, bool value) => gridController.SetTileOccupied(gridPos, value);

        public Vector3 GetWorldPosition(GridNode gridPos) => new Vector3(gridPos.x * tileSpacing, 0f, gridPos.y * tileSpacing);

        public bool[,] GetWalkableGrid() => gridController.GetWalkableGrid(gridX, gridZ);

        public TileState GetTileState(GridNode gridPos) => gridController.GetTileState(gridPos);
    }
}