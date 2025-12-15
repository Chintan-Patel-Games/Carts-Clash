using CartClash.Grid;
using CartClash.Units.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace CartClash.Units.Player
{
    public class PlayerUnitService : IUnitService
    {
        private GameObject playerPrefab;
        private PlayerUnitController playerUnitController;

        public PlayerUnitService(GameObject playerPrefab) => this.playerPrefab = playerPrefab;

        public void TickUpdate() => playerUnitController.TickUpdate();

        public IUnitController SpawnUnit(GridNode spawnNode)
        {
            GameObject player = Object.Instantiate(playerPrefab);
            var view = player.GetComponent<PlayerUnitView>();

            if (view == null)
            {
                Debug.LogError("PlayerUnitView missing on Player prefab");
                return null;
            }

            PlayerUnitModel model = new PlayerUnitModel(spawnNode, 3f);
            playerUnitController = new PlayerUnitController(model, view);
            return playerUnitController;
        }

        public void SetPath(List<GridNode> path) => playerUnitController.SetPath(path);

        public GridNode GetCurrentPlayerNode() => playerUnitController.CurrentPlayerNode();
    }
}