using CartClash.PathFinding;

namespace CartClash.Units.Interface
{
    // Interface for unit models
    public class IUnitModel
    {
        // The current position of the unit in grid coordinates
        GridNode CurrentNode { get; set; }

        // The movement speed of the unit
        float MoveSpeed { get; set; }
    }
}