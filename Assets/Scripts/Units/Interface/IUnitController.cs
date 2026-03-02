using CartClash.Grid;
using System.Collections.Generic;

namespace CartClash.Units.Interface.Controller
{
    /// <summary>
    /// Defines the contract for controlling the movement and arrival behavior of a unit
    /// within a grid-based environment.
    /// </summary>
    public interface IUnitController
    {
        public void StartMove();
        public bool UpdateMovement();
        public void RequestArrived();
        public void OnArrived();
        public void SetPath(List<GridNode> path);
        public void TickUpdate();
    }
}