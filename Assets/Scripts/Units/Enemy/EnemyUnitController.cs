using CartClash.Core;
using CartClash.Core.StateMachine;
using CartClash.Grid;
using CartClash.Units.Interface;
using System.Collections.Generic;

namespace CartClash.Units.Enemy
{
    public class EnemyUnitController : IUnitController
    {
        private EnemyUnitModel unitModel;
        private EnemyUnitView unitView;

        private List<GridNode> path;

        private UnitStates currentState;
        private EnemyStateMachine stateMachine;

        public EnemyUnitController(EnemyUnitModel unitModel, EnemyUnitView unitView)
        {
            this.unitModel = unitModel;
            this.unitView = unitView;

            stateMachine = new EnemyStateMachine(this);
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
        public void RequestArrived() => stateMachine.ChangeState(UnitStates.ARRIVED);

        // Method to be called in Arrived State
        public void OnArrived()
        {
            unitModel.CurrentNode = path[^1];
            stateMachine.ChangeState(UnitStates.IDLE);

            // Enabling mouse click input
            GameService.Instance.InputService.ToggleInput(true);
        }

        public void SetPath(List<GridNode> newPath)
        {
            if (newPath == null || newPath.Count == 0) return;

            path = newPath;
            stateMachine.ChangeState(UnitStates.PROCEED);
        }

        public void TickUpdate() => stateMachine.Update();

        public GridNode CurrentEnemyNode() => unitModel.CurrentNode;
    }
}