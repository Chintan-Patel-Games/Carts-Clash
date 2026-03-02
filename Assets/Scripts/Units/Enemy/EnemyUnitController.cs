using CartClash.Core;
using CartClash.Core.StateMachine;
using CartClash.Grid;
using CartClash.Units.Base.Controller;

namespace CartClash.Units.Enemy
{
    public class EnemyUnitController : UnitController<EnemyUnitModel, EnemyUnitView>
    {
        /// <summary>
        /// Provides control logic for a enemy-controlled unit, managing its state transitions, movement, and interactions
        /// within the game environment.
        /// </summary>
        private EnemyStateMachine stateMachine;
        private GameService gameService => GameService.Instance;

        public EnemyUnitController(EnemyUnitModel model, EnemyUnitView view) : base(model, view) =>
            stateMachine = new EnemyStateMachine(this);  // Initialize enemy state machine

        protected override void ChangeState(UnitStates newState) => stateMachine.ChangeState(newState);

        protected override void UpdateStateMachine() => stateMachine.Update();

        /// <summary>
        /// Disables user input at the start of a move operation.
        /// </summary>
        protected override void OnStartMoveInternal() => gameService.InputService.ToggleInput(false);

        /// <summary>
        /// Handles the arrival event by transitioning the game state to the player's turn.
        /// </summary>
        protected override void OnArrivedInternal()
        {
            gameService.EventService.SwitchToPlayerTurn.InvokeEvent();
            gameService.InputService.ToggleInput(true);
        }

        public GridNode GetCurrentEnemyNode() => GetCurrentNode();
    }
}