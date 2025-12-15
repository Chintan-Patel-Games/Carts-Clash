using CartClash.Core.StateMachine;
using CartClash.Units.Interface;

namespace CartClash.Units.States
{
    public class UnitArrivedState<T> : IState<T> where T : IUnitController
    {
        public T Owner { get; set; }

        public void OnEnterState() => Owner.OnArrived();
        public void UpdateState() { }
        public void OnExitState() { }
    }
}