using CartClash.Core.StateMachine;
using CartClash.Units.Interface;

namespace CartClash.Units.States
{
    public class UnitMovingState<T> : IState<T> where T : IUnitController
    {
        public T Owner {  get; set; }

        public void OnEnterState() { }

        public void UpdateState()
        {
            if (Owner.UpdateMovement())
                Owner.RequestArrived();
        }

        public void OnExitState() { }
    }
}