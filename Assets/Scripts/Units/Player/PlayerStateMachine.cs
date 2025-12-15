using CartClash.Core.StateMachine;
using CartClash.Units.States;
using System;

namespace CartClash.Units.Player
{
    public class PlayerStateMachine : GenericStateMachine<PlayerUnitController>
    {
        public PlayerStateMachine(PlayerUnitController Owner) : base(Owner)
        {
            CreateStates();
            SetOwner();
            Initialize(UnitStates.IDLE);
        }

        // Initializes the player state
        public void Initialize(Enum initialState)
        {
            if (!States.ContainsKey(initialState))
                throw new Exception($"[PlayerStatemachine] : State {initialState} not registered.");

            currentState = States[initialState];
            currentState.OnEnterState();
        }

        // Creating the necessary states to be used by PlayerStateMachine
        private void CreateStates()
        {
            States.Add(UnitStates.IDLE, new UnitIdleState<PlayerUnitController>());
            States.Add(UnitStates.PROCEED, new UnitProceedState<PlayerUnitController>());
            States.Add(UnitStates.MOVING, new UnitMovingState<PlayerUnitController>());
            States.Add(UnitStates.ARRIVED, new UnitArrivedState<PlayerUnitController>());
        }
    }
}