using CartClash.Command;
using CartClash.Core.StateMachine;
using CartClash.Grid;
using CartClash.Units.Enemy;
using CartClash.Units.Player;

namespace CartClash.Core.GameLoop
{
    public class GameLoopService
    {
        private GameLoopController controller;

        public GameLoopService(
            PlayerUnitService playerService,
            EnemyUnitService enemyService,
            CommandInvoker commandInvoker)
        {
            controller = new(playerService, enemyService, commandInvoker);
        }

        public void StartGameLoop() => controller.StartGame();

        public void OnTileSelected(GameLoopState state, GridNode node) => controller.OnTileSelected(state, node);

        public void SubscribeToEvents() => controller.SubscribeToEvents();

        public void UnSubscribeToEvents() => controller.UnSubscribeToEvents();

        public void TickUpdate() => controller.TickUpdate();

        public void OnUndo() => controller.OnUndo();

        public GameLoopState GetCurrentState() => controller.GetCurrentState();
    }
}