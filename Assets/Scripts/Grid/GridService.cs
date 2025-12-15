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

        private GridModel gridModel;
        private GridController gridController;

        protected override void Awake()
        {
            base.Awake();

            gridModel = new GridModel();
            gridController = new GridController();
        }

        private void Start()
        {
            InitializedGrid();
        }

        // Initializes the grid by creating tiles based on the specified dimensions
        public void InitializedGrid()
        {
            for (int x = 0; x < gridX; x++)
            {
                for (int z = 0; z < gridZ; z++)
                {
                    Vector2Int gridPos = new Vector2Int(x, z);
                    Vector3 worldPos = new Vector3(x * tileSpacing, 0f, z * tileSpacing);

                    CreateTile(gridPos, worldPos);
                }
            }
        }

        // Creates a tile at the specified grid and world positions
        private void CreateTile(Vector2Int gridPos, Vector3 worldPos)
        {
            TileModel tileModel = new TileModel(gridPos, isWalkable: true, isOccupied: false);
            gridModel.AddTile(tileModel);

            TileView tileView = Instantiate(tilePrefab);
            tileView.transform.SetParent(tileParent);
            tileView.transform.position = worldPos;

            gridController.AddTileView(tileView, gridPos);
        }
    }
}