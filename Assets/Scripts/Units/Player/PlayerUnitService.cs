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

        // Constructor to get player prefab
        public PlayerUnitService(GameObject playerPrefab, PathFindingService pathFindingService)
        {
            this.playerPrefab = playerPrefab;
            playerUnitAI = new PlayerUnitAI(this, pathFindingService);
        }

        public void OnEnable() => playerUnitAI.OnEnable();

        public void OnDisable() => playerUnitAI.OnDisable();

        // TickUpdate method to be called in Unity Update lifecycle method
        public void TickUpdate() => unitController.TickUpdate();

        // Public mehtod to spawn player unit
        public IUnitController SpawnUnit(GridNode spawnNode)
        {
            GameObject player = Object.Instantiate(playerPrefab);  // Spawns the player
            var view = player.GetComponent<PlayerUnitView>();  // Get player view class

            if (view == null)  // Check for player view null references
            {
                Debug.LogError("[PlayerUnitService] : PlayerUnitView missing on Player prefab");
                return null;
            }

            PlayerUnitModel model = new PlayerUnitModel(spawnNode, 3f);  // Initialize player model class
            unitController = new PlayerUnitController(model, view);  // Initialize player controller class
            return unitController;
        }

        // Global method to set player path
        public void SetPath(List<GridNode> path) => unitController.SetPath(path);

        // Global method to get current position of player
        public GridNode GetCurrentPlayerNode() => unitController.GetCurrentPlayerNode();
    }
}