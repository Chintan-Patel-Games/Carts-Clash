using CartClash.Core.StateMachine;

namespace CartClash.Core.GameLoop.States
{
    public class SelectPlayerSpawnState<T> : IState<T> where T : GameLoopController
    {
        public T Owner { get; set; }

        public void OnEnterState() => Owner.PlayerSpawnState();
        public void UpdateState() { }
        public void OnExitState() { }
    }
}