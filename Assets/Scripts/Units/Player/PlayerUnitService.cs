using CartClash.AI;
using CartClash.Grid;
using CartClash.PathFinding;
using CartClash.Units.Base.Service;
using System.Collections.Generic;
using UnityEngine;

namespace CartClash.Units.Player
{
    /// <summary>
    /// Provides player-specific unit management functionality, including path generation and position tracking, for
    /// player-controlled units within the game.
    /// </summary>
    public class PlayerUnitService : UnitService<PlayerUnitController, PlayerUnitModel, PlayerUnitView>
    {
        private PlayerUnitAI playerUnitAI;

        /// <summary>
        /// Constructor to get player prefab & initialise player AI
        /// </summary>
        public PlayerUnitService(GameObject playerPrefab, PathFindingService pathFindingService) : base(playerPrefab) =>
            playerUnitAI = new PlayerUnitAI(this, pathFindingService);

        /// <summary>
        /// Generates a new path using BFS pathfinding algorithm
        /// </summary>
        public List<GridNode> GeneratePath(GridNode targetNode) => playerUnitAI.GeneratePath(targetNode);
        
        protected override PlayerUnitModel CreateModel(GridNode spawnNode) => new PlayerUnitModel(spawnNode, 3f);

        protected override PlayerUnitController CreateController(PlayerUnitModel model, PlayerUnitView view)
            => new PlayerUnitController(model, view);

        /// <summary>
        /// Global method to get current position of player
        /// </summary>
        public GridNode GetCurrentPlayerNode() => unitController.GetCurrentPlayerNode();
    }
}