using CartClash.Core.Events;
using CartClash.Core.InputSystem;
using CartClash.Grid;
using CartClash.Obstacles;
using CartClash.PathFinding;
using CartClash.Units.Enemy;
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

        [Header("Input")]
        [SerializeField] private InputService inputService;
        public InputService InputService => inputService;

        [Header("Player")]
        [SerializeField] private GameObject playerPrefab;

        [Header("Enemy")]
        [SerializeField] private GameObject enemyPrefab;

        [Header("UI")]
        [SerializeField] private UIService uiService;
        public UIService UIService => uiService;

        public PathFindingService PathFindingService { get; private set; }
        public PlayerUnitService PlayerUnitService { get; private set; }
        public EnemyUnitService EnemyUnitService { get; private set; }
        public EventService EventService { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            EventService = new();
            PlayerUnitService = new(playerPrefab);
            EnemyUnitService = new(enemyPrefab);
            PathFindingService = new();
        }

        private void OnEnable() => PathFindingService.OnEnable();

        private void OnDisable() => PathFindingService.OnDisable();

        private void Start()
        {
            GridService.InitializeGrid();
            ObstacleService.ApplyObstacles();

            PlayerUnitService.SpawnUnit(new GridNode(1,3));
            EnemyUnitService.SpawnUnit(new GridNode(7,8));

            PathFindingService.Initialize();
        }

        private void Update()
        {
            PlayerUnitService.TickUpdate();
            EnemyUnitService.TickUpdate();
        }
    }
}