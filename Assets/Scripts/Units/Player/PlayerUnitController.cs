using CartClash.Core;
using CartClash.Core.StateMachine;
using CartClash.Grid;
using CartClash.Units.Interface;
using System.Collections.Generic;

namespace CartClash.Units.Player
{
    public class PlayerUnitController : IUnitController
    {
        private PlayerUnitModel unitModel;
        private PlayerUnitView unitView;
        private PlayerStateMachine stateMachine;

        private List<GridNode> path;

        public PlayerUnitController(PlayerUnitModel unitModel, PlayerUnitView unitView)
        {
            this.unitModel = unitModel;
            this.unitView = unitView;

            stateMachine = new PlayerStateMachine(this);  // Initialize player state machine
            unitView.SetPosition(unitModel.CurrentNode);

            GameService.Instance.GridService.SetOccupied(unitModel.CurrentNode, true); // Register tile occupancy
        }

        // Method to be called in Proceed State
        public void StartMove()
        {
            if (path == null || path.Count == 0) return;

            GameService.Instance.GridService.SetOccupied(path[^1], true); // Register tile occupancy

            unitView.MoveAlongPath(path, unitModel.MoveSpeed);
            stateMachine.ChangeState(UnitStates.MOVING);  // Changing player state to Moving

            // Disabling mouse click input
            GameService.Instance.InputService.ToggleInput(false);
        }

        // Method to be called in Moving State
        public bool UpdateMovement() => unitView.IsMovingComplete();

        // Method to be called in Arrived State to check arrived or not
        public void RequestArrived() => stateMachine.ChangeState(UnitStates.ARRIVED);

        // Method to be called in Arrived State
        public void OnArrived()
        {
            GameService.Instance.GridService.SetOccupied(unitModel.CurrentNode, false); // Release tile occupancy

            unitModel.CurrentNode = path[^1];

            stateMachine.ChangeState(UnitStates.IDLE);  // Changing player state to Idle

            // Invoking event for enemy to start chasing player after player reaches its destination
            GameService.Instance.EventService.StartChasingPlayer.InvokeEvent(unitModel.CurrentNode);
        }

        // Sets the path for the player to move along
        public void SetPath(List<GridNode> newPath)
        {
            if (newPath == null || newPath.Count == 0) return;

            path = newPath;
            stateMachine.ChangeState(UnitStates.PROCEED);  // Changing player state to Proceed
        }

        // TickUpdate method to be called in Unity Update lifecycle method
        public void TickUpdate() => stateMachine.Update();

        // Getter method for fetching current position of player
        public GridNode GetCurrentPlayerNode() => unitModel.CurrentNode;
    }
}