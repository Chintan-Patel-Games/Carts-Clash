using CartClash.Command;
using CartClash.Core.Events.Service;
using CartClash.Core.GameLoop;
using CartClash.Core.InputSystem.Service;
using CartClash.Grid.Service;
using CartClash.Obstacles.Service;
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
        [Header("Obstacle")]
        [SerializeField] private ObstacleService obstacleService;
        public ObstacleService ObstacleService => obstacleService;

        [Header("Grid")]
        [SerializeField] private GridService gridService;
        public GridService GridService => gridService;

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
        public GameLoopService GameLoopService { get; private set; }

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
            GameLoopService = new(PlayerUnitService, EnemyUnitService, CommandInvoker);
        }

        private void OnEnable() => GameLoopService.SubscribeToEvents();

        private void OnDisable() =>GameLoopService.UnSubscribeToEvents();

        private void Start()
        {
            GridService.InitializeGrid();
            ObstacleService.ApplyObstacles();
            GameLoopService.StartGameLoop();
        }

        private void Update()
        {
            PlayerUnitService.TickUpdate();
            EnemyUnitService.TickUpdate();
            GameLoopService.TickUpdate();
        }
    }
}