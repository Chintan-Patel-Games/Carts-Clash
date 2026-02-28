using CartClash.Core.StateMachine;

namespace CartClash.Core.GameLoop.States
{
    public class EnemyTurnState<T> : IState<T> where T : GameLoopController
    {
        public T Owner { get; set; }

        public void OnEnterState() => Owner.BeginEnemyTurn();
        public void UpdateState() { }
        public void OnExitState() { }
    }
}