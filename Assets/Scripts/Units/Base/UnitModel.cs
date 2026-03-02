using CartClash.Grid;
using CartClash.Units.Interface.Model;

namespace CartClash.Units.Base.Model
{
    public abstract class UnitModel : IUnitModel
    {
        public GridNode CurrentNode { get; set; }
        public float MoveSpeed { get; set; }

        protected UnitModel(GridNode startNode, float speed)
        {
            CurrentNode = startNode;
            MoveSpeed = speed;
        }
    }
}