using CartClash.Core.StateMachine;

namespace CartClash.Core.GameLoop.States
{
    public class EnemyTurnState<T> : IState<T> where T : GameLoopController
    {
        public T Owner { get; set; }

        public void OnEnterState() => Owner.ProcessEnemyTurn();
        public void UpdateState() { }
        public void OnExitState() { }
    }
}