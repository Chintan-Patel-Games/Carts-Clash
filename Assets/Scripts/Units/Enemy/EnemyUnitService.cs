using CartClash.AI;
using CartClash.Grid;
using CartClash.PathFinding;
using CartClash.Units.Base.Service;
using System.Collections.Generic;
using UnityEngine;

namespace CartClash.Units.Enemy
{
    /// <summary>
    /// Provides services for managing enemy units, including path generation, movement, and access to enemy unit state
    /// within the game.
    /// </summary>
    public class EnemyUnitService : UnitService<EnemyUnitController, EnemyUnitModel, EnemyUnitView>
    {
        private EnemyUnitAI enemyUnitAI;

        /// <summary>
        /// Constructor to get enemy prefab & initialise enemy AI
        /// </summary>
        public EnemyUnitService(GameObject enemyPrefab, PathFindingService pathFindingService) : base(enemyPrefab) =>
            enemyUnitAI = new EnemyUnitAI(this, pathFindingService);

        public bool CanGeneratePath(GridNode spawnNode, GridNode targetNode)
        {
            var path = enemyUnitAI.GeneratePathFrom(spawnNode, targetNode);
            return path != null && path.Count > 0;
        }

        /// <summary>
        /// Generates a new path using BFS pathfinding algorithm
        /// </summary>
        public List<GridNode> GeneratePath(GridNode targetNode) => enemyUnitAI.GeneratePath(targetNode);

        protected override EnemyUnitModel CreateModel(GridNode spawnNode) => new EnemyUnitModel(spawnNode, 3f);

        protected override EnemyUnitController CreateController(EnemyUnitModel model, EnemyUnitView view)
            => new EnemyUnitController(model, view);

        /// <summary>
        /// Global method to get current position of enemy
        /// </summary>
        public GridNode GetCurrentEnemyNode() => unitController.GetCurrentEnemyNode();
    }
}