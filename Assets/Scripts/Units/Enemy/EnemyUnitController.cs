using CartClash.Core;
using CartClash.Core.StateMachine;
using CartClash.Grid;
using CartClash.Units.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace CartClash.Units.Enemy
{
    public class EnemyUnitController : IUnitController
    {
        private EnemyUnitModel unitModel;
        private EnemyUnitView unitView;

        private List<GridNode> path;

        private EnemyStateMachine stateMachine;

        public EnemyUnitController(EnemyUnitModel unitModel, EnemyUnitView unitView)
        {
            this.unitModel = unitModel;
            this.unitView = unitView;

            stateMachine = new EnemyStateMachine(this);  // Initialize enemy state machine
            unitView.SetPosition(unitModel.CurrentNode);

            GameService.Instance.GridService.SetOccupied(unitModel.CurrentNode, true); // Register tile occupancy
        }

        // Method to be called in Proceed State
        public void StartMove()
        {
            if (path == null || path.Count == 0) return;

            GameService.Instance.GridService.SetOccupied(path[^1], true); // Register tile occupancy

            unitView.MoveAlongPath(path, unitModel.MoveSpeed);
            stateMachine.ChangeState(UnitStates.MOVING);  // Changing enemy state to Moving
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
            stateMachine.ChangeState(UnitStates.IDLE);
            Debug.Log("[EnemyUnitControler] Enemy reached destination");

            // Enabling mouse click input
            GameService.Instance.InputService.ToggleInput(true);

            GameService.Instance.EventService.SwitchToPlayerTurn.InvokeEvent();
        }

        // Sets the path for the enemy to move along
        public void SetPath(List<GridNode> newPath)
        {
            if (newPath == null || newPath.Count == 0) return;

            path = newPath;
            stateMachine.ChangeState(UnitStates.PROCEED);  // Changing enemy state to Proceed
        }

        // TickUpdate method to be called in Unity Update lifecycle method
        public void TickUpdate() => stateMachine.Update();

        // Getter method for fetching current position of enemy
        public GridNode CurrentEnemyNode() => unitModel.CurrentNode;

        public EnemyUnitView GetUnitView() => unitView;
    }
}