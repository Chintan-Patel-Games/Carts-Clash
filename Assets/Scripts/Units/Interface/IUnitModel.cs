using CartClash.Grid;

namespace CartClash.Units.Interface.Model
{
    /// <summary>
    /// Defines the contract for a unit's position and movement speed within a grid-based environment.
    /// </summary>
    public interface IUnitModel
    {
        public GridNode CurrentNode { get; set; }
        public float MoveSpeed { get; set; }
    }
}