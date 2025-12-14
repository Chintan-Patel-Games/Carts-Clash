using CartClash.PathFinding;
using System.Collections.Generic;

namespace CartClash.Units.Interface
{
    // Interface for unit controllers
    public interface IUnitController
    {
        public void StartMove();
        public bool UpdateMovement();
        public void RequestArrived();
        public void OnArrived();

        // Sets the destination for the unit to move towards
        public void SetDestination(GridNode target);

        // Sets the path for the unit to follow
        public void SetPath(List<GridNode> path);
        public void TickUpdate();
    }
}