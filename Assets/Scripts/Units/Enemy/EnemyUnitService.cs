using CartClash.AI;
using CartClash.Grid;
using CartClash.PathFinding;
using CartClash.Units.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace CartClash.Units.Enemy
{
    public class EnemyUnitService : IUnitService
    {
        private GameObject enemyPrefab;
        private EnemyUnitController unitController;
        private EnemyUnitAI enemyUnitAI;

        // Constructor to get enemy prefab & initialise enemy AI
        public EnemyUnitService(GameObject enemyPrefab, PathFindingService pathFindingService)
        {
            this.enemyPrefab = enemyPrefab;
            enemyUnitAI = new EnemyUnitAI(this, pathFindingService);
        }

        // Generates a new path using BFS pathfinding algorithm
        public List<GridNode> GeneratePath(GridNode targetNode) => enemyUnitAI.GeneratePath(targetNode);

        // TickUpdate method to be called in Unity Update lifecycle method
        public void TickUpdate()
        {
            if (unitController != null)
                unitController.TickUpdate();
        }

        // Public mehtod to spawn enemy unit
        public IUnitController SpawnUnit(GridNode spawnNode)
        {
            GameObject enemy = Object.Instantiate(enemyPrefab);  // Spawns the enemy
            var view = enemy.GetComponent<EnemyUnitView>();  // Get enemy view class

            if (view == null)  // Check for enemy view null references
            {
                Debug.LogError("[EnemyUnitService] : EnemyUnitView missing on Enemy prefab");
                return null;
            }

            EnemyUnitModel model = new EnemyUnitModel(spawnNode, 3f);  // Initialize enemy model class
            unitController = new EnemyUnitController(model, view);  // Initialize player controller class
            return unitController;
        }

        // Global method to set enemy path
        public void SetPath(List<GridNode> path) => unitController.SetPath(path);

        // Global method to get current position of enemy
        public GridNode GetCurrentEnemyNode() => unitController.CurrentEnemyNode();
    }
}