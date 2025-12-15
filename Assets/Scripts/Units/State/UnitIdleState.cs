using CartClash.Core.StateMachine;
using CartClash.Units.Interface;

namespace CartClash.Units.States
{
    public class UnitIdleState<T> : IState<T> where T : IUnitController
    {
        public T Owner { get; set; }

        public void OnEnterState() { }
        public void UpdateState() { }
        public void OnExitState() { }
    }
}