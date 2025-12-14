using CartClash.Core.StateMachine;
using CartClash.PathFinding;
using CartClash.Units.Interface;
using System.Collections.Generic;

namespace CartClash.Units.Player
{
    public class PlayerUnitController : IUnitController
    {
        private IUnitModel unitModel;
        private IUnitView unitView;

        private List<GridNode> path;

        private UnitStates currentState;
        private PlayerStateMachine stateMachine;

        public PlayerUnitController(IUnitModel unitModel, IUnitView unitView)
        {
            this.unitModel = unitModel;
            this.unitView = unitView;

            stateMachine = new PlayerStateMachine(this);
            stateMachine.ChangeState(UnitStates.IDLE);
            unitView.SetPosition(unitModel.CurrentNode);
        }

        // Method to be called in Proceed State
        public void StartMove()
        {
            if (path == null || path.Count == 0) return;

            unitView.MoveAlongPath(path, unitModel.MoveSpeed);
            stateMachine.ChangeState(UnitStates.MOVING);
        }

        // Method to be called in Moving State
        public bool UpdateMovement() => unitView.IsMovingComplete();

        // Method to be called in Arrived State to check arrived or not
        public void RequestArrived()
        {
            unitModel.CurrentNode = path[^1];
            stateMachine.ChangeState(UnitStates.ARRIVED);
        }

        // Method to be called in Arrived State
        public void OnArrived()
        {
            unitModel.CurrentNode = path[^1];
            stateMachine.ChangeState(UnitStates.IDLE);
        }

        public void SetDestination(GridNode target) { }

        public void SetPath(List<GridNode> newPath)
        {
            if (newPath == null || path.Count == 0) return;

            path = newPath;
            stateMachine.ChangeState(UnitStates.IDLE);
        }

        public void TickUpdate() => stateMachine.Update();
    }
}