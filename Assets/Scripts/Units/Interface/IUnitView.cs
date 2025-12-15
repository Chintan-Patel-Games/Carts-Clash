using System.Collections.Generic;
using CartClash.Grid;

namespace CartClash.Units.Interface
{
    // Interface for unit views
    public interface IUnitView
    {
        // Checks for the unit reached its destination or not
        public bool IsMovingComplete();
        // Sets the unit's position to the specified grid node
        public void SetPosition(GridNode node);

        // Moves the unit along the specified path at the given speed
        public void MoveAlongPath(List<GridNode> path, float speed);
    }
}