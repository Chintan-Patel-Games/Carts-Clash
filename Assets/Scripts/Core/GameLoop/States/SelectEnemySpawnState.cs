using CartClash.Core.StateMachine;

namespace CartClash.Core.GameLoop.States
{
    public class SelectEnemySpawnState<T> : IState<T> where T : GameLoopController
    {
        public T Owner { get; set; }

        public void OnEnterState() => Owner.EnemySpawnState();
        public void UpdateState() { }
        public void OnExitState() { }
    }
}