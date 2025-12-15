using CartClash.Grid;
using CartClash.Units.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace CartClash.Units.Player
{
    public class PlayerUnitService : IUnitService
    {
        private GameObject playerPrefab;
        private PlayerUnitController unitController;

        public PlayerUnitService(GameObject playerPrefab) => this.playerPrefab = playerPrefab;

        public void TickUpdate() => unitController.TickUpdate();

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
            unitController = new PlayerUnitController(model, view);
            return unitController;
        }

        public void SetPath(List<GridNode> path) => unitController.SetPath(path);

        public GridNode GetCurrentPlayerNode() => unitController.CurrentPlayerNode();
    }
}