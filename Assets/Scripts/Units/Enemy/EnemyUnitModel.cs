using CartClash.Grid;
using CartClash.Units.Interface;

namespace CartClash.Units.Enemy
{
    public class EnemyUnitModel : IUnitModel
    {
        public GridNode CurrentNode { get; set; }
        public float MoveSpeed { get; set; }

        public EnemyUnitModel(GridNode startNode, float speed)
        {
            CurrentNode = startNode;
            MoveSpeed = speed;
        }
    }
}