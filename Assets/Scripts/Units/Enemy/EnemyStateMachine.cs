using CartClash.Core.StateMachine;
using CartClash.Units.States;
using System;

namespace CartClash.Units.Enemy
{
    public class EnemyStateMachine : GenericStateMachine<EnemyUnitController>
    {
        public EnemyStateMachine(EnemyUnitController Owner) : base(Owner)
        {
            CreateStates();
            SetOwner();
            Initialize(UnitStates.IDLE);
        }

        // Initializes the enemy state
        public void Initialize(Enum initialState)
        {
            if (!States.ContainsKey(initialState))
                throw new Exception($"[EnemyStatemachine] : State {initialState} not registered.");

            currentState = States[initialState];
            currentState.OnEnterState();
        }

        // Creating the necessary states to be used by EnemyStateMachine
        private void CreateStates()
        {
            States.Add(UnitStates.IDLE, new UnitIdleState<EnemyUnitController>());
            States.Add(UnitStates.PROCEED, new UnitProceedState<EnemyUnitController>());
            States.Add(UnitStates.MOVING, new UnitMovingState<EnemyUnitController>());
            States.Add(UnitStates.ARRIVED, new UnitArrivedState<EnemyUnitController>());
        }
    }
}