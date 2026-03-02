using CartClash.Core;
using CartClash.Core.StateMachine;
using CartClash.Grid;
using CartClash.Units.Base.Controller;

namespace CartClash.Units.Player
{
    /// <summary>
    /// Provides control logic for a player-controlled unit, managing its state transitions, movement, and interactions
    /// within the game environment.
    /// </summary>
    public class PlayerUnitController : UnitController<PlayerUnitModel, PlayerUnitView>
    {
        private PlayerStateMachine stateMachine;
        private GameService gameService => GameService.Instance;

        public PlayerUnitController(PlayerUnitModel model, PlayerUnitView view) : base(model, view) =>
            stateMachine = new PlayerStateMachine(this);  // Initialize player state machine

        protected override void ChangeState(UnitStates newState) => stateMachine.ChangeState(newState);

        protected override void UpdateStateMachine() => stateMachine.Update();

        /// <summary>
        /// Disables user input at the start of a move operation.
        /// </summary>
        protected override void OnStartMoveInternal() => gameService.InputService.ToggleInput(false);

        /// <summary>
        /// Handles arrival logic by initiating the player chase event.
        /// </summary>
        protected override void OnArrivedInternal() => gameService.EventService.StartChasingPlayer.InvokeEvent();

        public GridNode GetCurrentPlayerNode() => GetCurrentNode();
    }
}