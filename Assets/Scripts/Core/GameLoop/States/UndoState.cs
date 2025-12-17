using CartClash.Core.StateMachine;

namespace CartClash.Core.GameLoop.States
{
    public class UndoState<T> : IState<T> where T : GameLoopController
    {
        public T Owner { get; set; }

        public void OnEnterState() => Owner.OnUndo();
        public void UpdateState() { }
        public void OnExitState() { }
    }
}