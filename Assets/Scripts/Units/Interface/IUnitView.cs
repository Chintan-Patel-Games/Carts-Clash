using System.Collections.Generic;
using CartClash.Grid;

namespace CartClash.Units.Interface.View
{
    /// <summary>
    /// Defines methods for controlling and querying the movement state of a unit within a grid-based environment.
    /// </summary>
    public interface IUnitView
    {
        /// <summary>
        /// Moves the unit along the specified sequence of grid nodes at the given speed.
        /// </summary>
        public void MoveAlongPath(List<GridNode> path, float speed);

        /// <summary>
        /// Determines whether the unit has reached its destination and completed its movement.
        /// </summary>
        public bool IsMovingComplete();

        /// <summary>
        /// Sets the unit's position to the specified grid node.
        /// </summary>
        public void SetPosition(GridNode node);
    }
}