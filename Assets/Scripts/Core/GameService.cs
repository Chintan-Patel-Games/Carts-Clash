using CartClash.Command;
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
        // Getting References of Monobehaviour classes
        [Header("Grid")]
        [SerializeField] private GridService gridService;
        public GridService GridService => gridService;

        [Header("Obstacle")]
        [SerializeField] private ObstacleService obstacleService;
        public ObstacleService ObstacleService => obstacleService;

        [Header("Input")]
        [SerializeField] private InputService inputService;
        public InputService InputService => inputService;

        [Header("UI")]
        [SerializeField] private UIService uiService;
        public UIService UIService => uiService;

        // Getting references of Prefabs
        [Header("Player")]
        [SerializeField] private GameObject playerPrefab;

        [Header("Enemy")]
        [SerializeField] private GameObject enemyPrefab;

        // Getting References of Non-Monobehaviour classes
        public EventService EventService { get; private set; }
        public PathFindingService PathFindingService { get; private set; }
        public PlayerUnitService PlayerUnitService { get; private set; }
        public EnemyUnitService EnemyUnitService { get; private set; }
        public CommandInvoker CommandInvoker { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            InitializeServices();
        }

        private void InitializeServices()
        {
            EventService = new();
            PathFindingService = new();
            PlayerUnitService = new(playerPrefab, PathFindingService);
            EnemyUnitService = new(enemyPrefab, PathFindingService);
            CommandInvoker = new();
        }

        private void Start()
        {
            GridService.InitializeGrid();
            ObstacleService.ApplyObstacles();

            PlayerUnitService.SpawnUnit(new GridNode(1,3));
            EnemyUnitService.SpawnUnit(new GridNode(7,8));
        }

        private void Update()
        {
            PlayerUnitService.TickUpdate();
            EnemyUnitService.TickUpdate();
        }
    }
}