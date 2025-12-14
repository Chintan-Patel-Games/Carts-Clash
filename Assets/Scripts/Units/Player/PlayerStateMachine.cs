using CartClash.Core.StateMachine;
using CartClash.Units.States;

namespace CartClash.Units.Player
{
    public class PlayerStateMachine : GenericStateMachine<PlayerUnitController>
    {
        public PlayerStateMachine(PlayerUnitController Owner) : base(Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            States.Add(UnitStates.IDLE, new UnitIdleState<PlayerUnitController>());
            States.Add(UnitStates.PROCEED, new UnitProceedState<PlayerUnitController>());
            States.Add(UnitStates.MOVING, new UnitMovingState<PlayerUnitController>());
            States.Add(UnitStates.ARRIVED, new UnitArrivedState<PlayerUnitController>());
        }
    }
}