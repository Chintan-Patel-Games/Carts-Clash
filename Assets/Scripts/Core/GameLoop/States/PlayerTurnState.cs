using CartClash.Core.StateMachine;

namespace CartClash.Core.GameLoop.States
{
    public class PlayerTurnState<T> : IState<T> where T : GameLoopController
    {
        public T Owner { get; set; }

        public void OnEnterState() => Owner.OnPlayerTurn();
        public void UpdateState() { }
        public void OnExitState() { }
    }
}