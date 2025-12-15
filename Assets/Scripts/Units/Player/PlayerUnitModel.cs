using CartClash.Grid;
using CartClash.Units.Interface;

namespace CartClash.Units.Player
{
    public class PlayerUnitModel : IUnitModel
    {
        public GridNode CurrentNode { get; set; }
        public float MoveSpeed { get; set; }

        public PlayerUnitModel(GridNode startNode, float speed)
        {
            CurrentNode = startNode;
            MoveSpeed = speed;
        }
    }
}