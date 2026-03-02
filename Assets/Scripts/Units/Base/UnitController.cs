using CartClash.Core;
using CartClash.Core.StateMachine;
using CartClash.Grid;
using CartClash.Units.Base.Model;
using CartClash.Units.Base.View;
using CartClash.Units.Interface.Controller;
using System.Collections.Generic;

namespace CartClash.Units.Base.Controller
{
    public abstract class UnitController<TModel, TView> : IUnitController
        where TModel : UnitModel
        where TView : UnitView
    {
        protected TModel unitModel;
        protected TView unitView;
        protected List<GridNode> path;

        protected UnitController(TModel unitModel, TView unitView)
        {
            this.unitModel = unitModel;
            this.unitView = unitView;

            unitView.SetPosition(unitModel.CurrentNode);
            GameService.Instance.GridService.SetOccupied(unitModel.CurrentNode, true);
        }

        public virtual void StartMove()
        {
            if (path == null || path.Count == 0) return;

            GameService.Instance.GridService.SetOccupied(path[^1], true);
            unitView.MoveAlongPath(path, unitModel.MoveSpeed);
            ChangeState(UnitStates.MOVING);

            OnStartMoveInternal(); // Hook for specific derived behaviors
        }

        // Child classes must handle passing states to their specific StateMachines
        protected abstract void ChangeState(UnitStates newState);
        protected abstract void UpdateStateMachine();
        protected virtual void OnStartMoveInternal() { } // Optional override

        public virtual bool UpdateMovement() => unitView.IsMovingComplete();

        public virtual void RequestArrived() => ChangeState(UnitStates.ARRIVED);

        public virtual void OnArrived()
        {
            GameService.Instance.GridService.SetOccupied(unitModel.CurrentNode, false);
            unitModel.CurrentNode = path[^1];
            ChangeState(UnitStates.IDLE);

            OnArrivedInternal(); // Hook for specific derived behaviors
        }

        protected abstract void OnArrivedInternal(); // Mandatory override

        public virtual void SetPath(List<GridNode> newPath)
        {
            if (newPath == null || newPath.Count == 0) return;
            path = newPath;
            ChangeState(UnitStates.PROCEED);
        }

        public virtual void TickUpdate() => UpdateStateMachine();

        public virtual GridNode GetCurrentNode() => unitModel.CurrentNode;

        public virtual TView GetUnitView() => unitView;
    }
}