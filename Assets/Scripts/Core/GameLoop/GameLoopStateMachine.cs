using CartClash.Core.GameLoop.States;
using CartClash.Core.StateMachine;
using System;

namespace CartClash.Core.GameLoop
{
    public class GameLoopStateMachine : GenericStateMachine<GameLoopController>
    {
        public GameLoopStateMachine(GameLoopController Owner) : base(Owner)
        {
            CreateStates();
            SetOwner();
        }

        // Initializes the game loop state
        public void Initialize(Enum initialState)
        {
            ChangeState(initialState);
        }

        // Creating the necessary states to be used by GameLoopStateMachine
        private void CreateStates()
        {
            States.Add(GameLoopState.SELECT_PLAYER_SPAWN, new SelectPlayerSpawnState<GameLoopController>());
            States.Add(GameLoopState.SELECT_ENEMY_SPAWN, new SelectEnemySpawnState<GameLoopController>());
            States.Add(GameLoopState.PLAYER_TURN, new PlayerTurnState<GameLoopController>());
            States.Add(GameLoopState.ENEMY_TURN, new EnemyTurnState<GameLoopController>());
            States.Add(GameLoopState.UNDO, new UndoState<GameLoopController>());
        }
    }
}