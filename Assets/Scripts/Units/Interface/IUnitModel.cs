using CartClash.PathFinding;

namespace CartClash.Units.Interface
{
    // Interface for unit models
    public interface IUnitModel
    {
        // The current position of the unit in grid coordinates
        public GridNode CurrentNode { get; set; }

        // The movement speed of the unit
        public float MoveSpeed { get; set; }
    }
}