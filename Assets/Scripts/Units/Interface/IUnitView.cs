using CartClash.PathFinding;
using System.Collections.Generic;

namespace CartClash.Units.Interface
{
    // Interface for unit views
    public interface IUnitView
    {
        // Sets the unit's position to the specified grid node
        public void SetPosition(GridNode node);

        // Moves the unit along the specified path at the given speed
        public void MoveAlongPath(List<GridNode> path, float speed);
    }
}