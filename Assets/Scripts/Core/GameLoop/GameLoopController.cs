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
            GameService.Instance.EventService.StartChasingPlayer.AddListener(ProcessEnemyTurn);
            GameService.Instance.EventService.SwitchToPlayerTurn.AddListener(SwitchToPlayerTurn);
        }

        public void UnSubscribeToEvents()
        {
            GameService.Instance.EventService.StartChasingPlayer.RemoveListener(ProcessEnemyTurn);
            GameService.Instance.EventService.SwitchToPlayerTurn.RemoveListener(SwitchToPlayerTurn);
        }

        public void StartGame()
        {
            stateMachine.Initialize(GameLoopState.SELECT_PLAYER_SPAWN);
            GameService.Instance.UIService.UpdateCurrentStateText(GameLoopState.SELECT_PLAYER_SPAWN.ToString());
        }

        public void PlayerSpawnState() =>
            GameService.Instance.UIService.ShowPlayerSpawnPanel();

        public void EnemySpawnState() =>
            GameService.Instance.UIService.ShowEnemySpawnPanel();

        public void OnTileSelected(GameLoopState state, GridNode node)
        {
            if (!GameService.Instance.GridService.IsWalkable(node)) return;

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

        public void OnPlayerTurn() =>
            GameService.Instance.UIService.HideSpawnPanel();

        private void ProcessPlayerSpawn(GridNode spawnNode)
        {
            GameService.Instance.UIService.ToggleUndoButton(false);
            playerSpawnNode = spawnNode;

            ICommand commandToProcess = new SpawnPlayerCommand(playerService, playerSpawnNode);
            commandInvoker.ProcessCommand(commandToProcess);
            stateMachine.ChangeState(GameLoopState.SELECT_ENEMY_SPAWN);
            GameService.Instance.UIService.UpdateCurrentStateText(GameLoopState.SELECT_ENEMY_SPAWN.ToString());
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
            GameService.Instance.UIService.UpdateCurrentStateText(GameLoopState.PLAYER_TURN.ToString());
            return true;
        }

        private void ProcessPlayerTurn(GridNode targetNode)
        {
            GameService.Instance.UIService.ToggleUndoButton(false);
            ICommand commandToProcess = new PlayerMoveCommand(playerService, targetNode);
            commandInvoker.ProcessCommand(commandToProcess);
        }

        public void ProcessEnemyTurn()
        {
            GameService.Instance.UIService.ToggleUndoButton(false);
            stateMachine.ChangeState(GameLoopState.ENEMY_TURN);
            GameService.Instance.UIService.UpdateCurrentStateText(GameLoopState.ENEMY_TURN.ToString());

            ICommand commandToProcess = new EnemyChaseCommand(enemyService, playerService.GetCurrentPlayerNode());
            commandInvoker.ProcessCommand(commandToProcess);
        }

        public void SwitchToPlayerTurn()
        {
            GameService.Instance.UIService.ToggleUndoButton(true);
            stateMachine.ChangeState(GameLoopState.PLAYER_TURN);
            GameService.Instance.UIService.UpdateCurrentStateText(GameLoopState.PLAYER_TURN.ToString());
        }

        public void OnUndo()
        {
            GameService.Instance.CommandInvoker.Undo();
            stateMachine.ChangeState(GameLoopState.UNDO);
            GameService.Instance.UIService.UpdateCurrentStateText(GameLoopState.UNDO.ToString());
        }

        public void TickUpdate() => stateMachine.Update();

        public GameLoopState GetCurrentState() => (GameLoopState)stateMachine.GetCurrentStateKey();
    }
}