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

        private List<GridNode> path;

        private UnitStates currentState;
        private PlayerStateMachine stateMachine;

        public PlayerUnitController(PlayerUnitModel unitModel, PlayerUnitView unitView)
        {
            this.unitModel = unitModel;
            this.unitView = unitView;

            stateMachine = new PlayerStateMachine(this);
            unitView.SetPosition(unitModel.CurrentNode);
        }

        // Method to be called in Proceed State
        public void StartMove()
        {
            if (path == null || path.Count == 0) return;

            unitView.MoveAlongPath(path, unitModel.MoveSpeed);
            stateMachine.ChangeState(UnitStates.MOVING);

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
            unitModel.CurrentNode = path[^1];
            stateMachine.ChangeState(UnitStates.IDLE);

            GameService.Instance.EventService.StartChasingPlayer.InvokeEvent(unitModel.CurrentNode);
        }

        public void SetPath(List<GridNode> newPath)
        {
            if (newPath == null || newPath.Count == 0) return;

            path = newPath;
            stateMachine.ChangeState(UnitStates.PROCEED);
        }

        public void TickUpdate() => stateMachine.Update();

        public GridNode CurrentPlayerNode() => unitModel.CurrentNode;
    }
}