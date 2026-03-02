using CartClash.Grid;
using CartClash.Units.Base.Model;

namespace CartClash.Units.Player
{
    public class PlayerUnitModel : UnitModel
    {
        public PlayerUnitModel(GridNode startNode, float speed) : base (startNode, speed) { }
    }
}