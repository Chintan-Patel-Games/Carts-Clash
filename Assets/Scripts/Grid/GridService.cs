using CartClash.Grid.Tile;
using CartClash.Utilities;
using UnityEngine;

namespace CartClash.Grid
{
    // Service responsible for initializing and managing the grid
    public class GridService : GenericMonoSingleton<GridService>
    {
        [Header("Grid")]
        [SerializeField] private int gridX = 10;
        [SerializeField] private int gridZ = 10;
        [SerializeField] private float tileSpacing = 1f;

        [Header("Tile View")]
        [SerializeField] private TileView tilePrefab;
        [SerializeField] private Transform tileParent;

        private bool[,] walkableGrid;

        private GridModel gridModel;
        private GridController gridController;

        protected override void Awake()
        {
            base.Awake();

            walkableGrid = new bool[gridX, gridZ];
            gridModel = new GridModel();
            gridController = new GridController();
        }

        // Initializes the grid by creating tiles based on the specified dimensions
        public void InitializeGrid()
        {
            for (int x = 0; x < gridX; x++)
            {
                for (int z = 0; z < gridZ; z++)
                {
                    GridNode gridPos = new(x, z);
                    Vector3 worldPos = new(x * tileSpacing, 0f, z * tileSpacing);
                    walkableGrid[x, z] = true;
                    CreateTile(gridPos, worldPos);
                }
            }
        }

        // Creates a tile at the specified grid and world positions
        private void CreateTile(GridNode gridPos, Vector3 worldPos)
        {
            TileModel tileModel = new TileModel(gridPos, isWalkable: true, isOccupied: false);
            gridModel.AddTile(tileModel);

            TileView tileView = Instantiate(tilePrefab);
            tileView.transform.SetParent(tileParent);
            tileView.transform.position = worldPos;

            gridController.AddTileView(tileView, gridPos);
        }

        // Checks if the tile at the specified grid position is walkable
        public bool IsWalkable(GridNode gridPos)
        {
            TileModel tile = gridModel.GetTile(gridPos);
            if (tile == null) return false;
            return tile.isWalkable && !tile.isOccupied;
        }

        // Sets the blocked state of the tile at the specified grid position
        public void SetBlocked(GridNode gridPos, bool value)
        {
            TileModel tile = gridModel.GetTile(gridPos);
            if (tile == null) return;

            tile.SetWalkable(!value);
            walkableGrid[gridPos.x, gridPos.y] = IsWalkable(gridPos);
            gridController.SetTileBlocked(gridPos, value);
        }

        public Vector3 GetWorldPosition(GridNode gridPos) => 
            new Vector3(gridPos.x * tileSpacing, 0f, gridPos.y * tileSpacing);

        public bool[,] GetWalkableGrid => walkableGrid;
    }
}