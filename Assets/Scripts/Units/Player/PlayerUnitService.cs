using CartClash.AI;
using CartClash.Grid;
using CartClash.PathFinding;
using CartClash.Units.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace CartClash.Units.Player
{
    public class PlayerUnitService : IUnitService
    {
        private GameObject playerPrefab;
        private PlayerUnitController unitController;
        private PlayerUnitAI playerUnitAI;

        // Constructor to get player prefab & initialise player AI
        public PlayerUnitService(GameObject playerPrefab, PathFindingService pathFindingService, GridService gridService)
        {
            this.playerPrefab = playerPrefab;
            playerUnitAI = new PlayerUnitAI(this, pathFindingService, gridService);
        }

        // Generates a new path using BFS pathfinding algorithm
        public List<GridNode> GeneratePath(GridNode targetNode) => playerUnitAI.GeneratePath(targetNode);

        // TickUpdate method to be called in Unity Update lifecycle method
        public void TickUpdate()
        {
            if (unitController != null)
                unitController.TickUpdate();
        }

        // Public mehtod to spawn player unit
        public void SpawnUnit(GridNode spawnNode)
        {
            GameObject player = Object.Instantiate(playerPrefab);  // Spawns the player
            var view = player.GetComponent<PlayerUnitView>();  // Get player view class

            if (view == null)  // Check for player view null references
            {
                Debug.LogError("[PlayerUnitService] : PlayerUnitView missing on Player prefab");
                return;
            }

            PlayerUnitModel model = new PlayerUnitModel(spawnNode, 3f);  // Initialize player model class
            unitController = new PlayerUnitController(model, view);  // Initialize player controller class
        }

        public void DeleteUnit()
        {
            if (unitController == null) return;

            var view = unitController.GetUnitView();

            if (view != null)
                Object.Destroy(view);

            unitController = null;
        }

        // Global method to set player path
        public void SetPath(List<GridNode> path) => unitController.SetPath(path);

        // Global method to get current position of player
        public GridNode GetCurrentPlayerNode() => unitController.GetCurrentPlayerNode();
    }
}