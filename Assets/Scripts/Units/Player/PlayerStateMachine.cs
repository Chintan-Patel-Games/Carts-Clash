using CartClash.Core.StateMachine;
using CartClash.Units.States;
using System;

namespace CartClash.Units.Player
{
    /// <summary>
    /// Represents a state machine for managing the states of a player unit within the game.
    /// </summary>
    public class PlayerStateMachine : GenericStateMachine<PlayerUnitController>
    {
        public PlayerStateMachine(PlayerUnitController Owner) : base(Owner)
        {
            CreateStates();
            SetOwner();
            Initialize(UnitStates.IDLE);
        }

        public void Initialize(Enum initialState)
        {
            currentState = States[initialState];
            currentState.OnEnterState();
        }

        private void CreateStates()
        {
            States.Add(UnitStates.IDLE, new UnitIdleState<PlayerUnitController>());
            States.Add(UnitStates.PROCEED, new UnitProceedState<PlayerUnitController>());
            States.Add(UnitStates.MOVING, new UnitMovingState<PlayerUnitController>());
            States.Add(UnitStates.ARRIVED, new UnitArrivedState<PlayerUnitController>());
        }
    }
}