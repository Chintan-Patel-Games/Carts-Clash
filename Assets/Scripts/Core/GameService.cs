using CartClash.Grid;
using CartClash.Obstacles;
using CartClash.Utilities;
using UnityEngine;

namespace CartClash.Core
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        [Header("Grid")]
        [SerializeField] private GridService gridService;
        public GridService GridService => gridService;

        [Header("Obstacle")]
        [SerializeField] private ObstacleService obstacleService;
        public ObstacleService ObstacleService => obstacleService;

        [Header("UI")]
        [SerializeField] private UIService uiService;
        public UIService UIService => uiService;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            GridService.InitializeGrid();
            ObstacleService.ApplyObstacles();
        }
    }
}