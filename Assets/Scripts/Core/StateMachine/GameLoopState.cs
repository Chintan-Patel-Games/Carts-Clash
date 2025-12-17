namespace CartClash.Core.StateMachine
{
    public enum GameLoopState
    {
        NONE,
        SELECT_PLAYER_SPAWN,
        SELECT_ENEMY_SPAWN,
        PLAYER_TURN,
        ENEMY_TURN,
        UNDO
    }
}