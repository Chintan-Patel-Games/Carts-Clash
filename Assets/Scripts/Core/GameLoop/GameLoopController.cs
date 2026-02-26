using CartClash.Command;
using CartClash.Core.StateMachine;
using CartClash.Grid;
using CartClash.Units.Enemy;
using CartClash.Units.Player;

namespace CartClash.Core.GameLoop
{
    public class GameLoopController
    {
        private GameLoopStateMachine stateMachine;

        private PlayerUnitService playerService;
        private EnemyUnitService enemyService;
        private CommandInvoker commandInvoker;

        private GameService gameService = GameService.Instance;

        private GridNode playerSpawnNode;
        private GridNode enemySpawnNode;

        public GameLoopController(PlayerUnitService playerService, EnemyUnitService enemyService, CommandInvoker commandInvoker)
        {
            this.playerService = playerService;
            this.enemyService = enemyService;
            this.commandInvoker = commandInvoker;

            stateMachine = new(this);  // Initialize game loop state machine
        }

        public void SubscribeToEvents()
        {
            gameService.EventService.StartChasingPlayer.AddListener(ProcessEnemyTurn);
            gameService.EventService.SwitchToPlayerTurn.AddListener(SwitchToPlayerTurn);
        }

        public void UnSubscribeToEvents()
        {
            gameService.EventService.StartChasingPlayer.RemoveListener(ProcessEnemyTurn);
            gameService.EventService.SwitchToPlayerTurn.RemoveListener(SwitchToPlayerTurn);
        }

        public void StartGame()
        {
            stateMachine.Initialize(GameLoopState.SELECT_PLAYER_SPAWN);
            gameService.UIService.UpdateCurrentStateText(GameLoopState.SELECT_PLAYER_SPAWN.ToString());
        }

        public void PlayerSpawnState() => gameService.UIService.ShowPlayerSpawnPanel();

        public void EnemySpawnState() => gameService.UIService.ShowEnemySpawnPanel();

        public void OnTileSelected(GameLoopState state, GridNode node)
        {
            if (!gameService.GridService.IsWalkable(node)) return;

            switch (state)
            {
                case GameLoopState.SELECT_PLAYER_SPAWN:
                    ProcessPlayerSpawn(node);
                    break;

                case GameLoopState.SELECT_ENEMY_SPAWN:
                    if (!TryProcessEnemySpawn(node))
                        return;
                    break;

                case GameLoopState.PLAYER_TURN:
                    ProcessPlayerTurn(node);
                    break;

                default:
                    break;
            }
        }

        public void OnPlayerTurn() => gameService.UIService.HideSpawnPanel();

        private void ProcessPlayerSpawn(GridNode spawnNode)
        {
            gameService.UIService.ToggleUndoButton(false);
            playerSpawnNode = spawnNode;

            ICommand commandToProcess = new SpawnPlayerCommand(playerService, playerSpawnNode);
            commandInvoker.ProcessCommand(commandToProcess);
            stateMachine.ChangeState(GameLoopState.SELECT_ENEMY_SPAWN);
            gameService.UIService.UpdateCurrentStateText(GameLoopState.SELECT_ENEMY_SPAWN.ToString());
        }

        private bool TryProcessEnemySpawn(GridNode spawnNode)
        {
            enemySpawnNode = spawnNode;

            ICommand commandToProcess = new SpawnEnemyCommand(enemyService, enemySpawnNode, playerSpawnNode);
            commandInvoker.ProcessCommand(commandToProcess);

            if (enemyService.GetUnitController() == null)
            {
                commandToProcess = null;
                return false;
            }

            stateMachine.ChangeState(GameLoopState.PLAYER_TURN);
            gameService.UIService.UpdateCurrentStateText(GameLoopState.PLAYER_TURN.ToString());
            return true;
        }

        private void ProcessPlayerTurn(GridNode targetNode)
        {
            gameService.UIService.ToggleUndoButton(false);
            ICommand commandToProcess = new PlayerMoveCommand(playerService, targetNode);
            commandInvoker.ProcessCommand(commandToProcess);
        }

        public void ProcessEnemyTurn()
        {
            gameService.UIService.ToggleUndoButton(false);
            stateMachine.ChangeState(GameLoopState.ENEMY_TURN);
            gameService.UIService.UpdateCurrentStateText(GameLoopState.ENEMY_TURN.ToString());

            ICommand commandToProcess = new EnemyChaseCommand(enemyService, playerService.GetCurrentPlayerNode());
            commandInvoker.ProcessCommand(commandToProcess);
        }

        public void SwitchToPlayerTurn()
        {
            gameService.UIService.ToggleUndoButton(true);
            stateMachine.ChangeState(GameLoopState.PLAYER_TURN);
            gameService.UIService.UpdateCurrentStateText(GameLoopState.PLAYER_TURN.ToString());
        }

        public void OnUndo()
        {
            gameService.CommandInvoker.Undo();
            stateMachine.ChangeState(GameLoopState.UNDO);
            gameService.UIService.UpdateCurrentStateText(GameLoopState.UNDO.ToString());
        }

        public void TickUpdate() => stateMachine.Update();

        public GameLoopState GetCurrentState() => (GameLoopState)stateMachine.GetCurrentStateKey();
    }
}