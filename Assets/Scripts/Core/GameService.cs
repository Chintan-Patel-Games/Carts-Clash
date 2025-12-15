using CartClash.Core.Events;
using CartClash.Grid;
using CartClash.Obstacles;
using CartClash.PathFinding;
using CartClash.Units.Player;
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

        [Header("Player")]
        [SerializeField] private GameObject playerPrefab;

        [Header("UI")]
        [SerializeField] private UIService uiService;
        public UIService UIService => uiService;

        public PathFindingService PathFindingService {  get; private set; }
        public PlayerUnitService PlayerUnitService {  get; private set; }
        public EventService EventService { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            EventService = new();
            PathFindingService = new();
            PlayerUnitService = new(playerPrefab);
        }

        private void OnEnable() => PathFindingService.OnEnable();

        private void OnDisable() => PathFindingService.OnDisable();

        private void Start()
        {
            GridService.InitializeGrid();
            ObstacleService.ApplyObstacles();

            GridNode spawnPos = new GridNode(1,3);
            PlayerUnitService.SpawnUnit(spawnPos);

            PathFindingService.Initialize();
        }

        private void Update() => PlayerUnitService.TickUpdate();
    }
}