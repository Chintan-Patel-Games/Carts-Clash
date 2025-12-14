using CartClash.PathFinding;
using CartClash.Units.Interface;
using UnityEngine;

namespace CartClash.Units.Player
{
    public class PlayerUnitService : IUnitService
    {
        private GameObject playerPrefab;

        public PlayerUnitService(GameObject playerPrefab) => this.playerPrefab = playerPrefab;

        public IUnitController SpawnUnit(GridNode spawnNode)
        {
            GameObject player = Object.Instantiate(playerPrefab);
            var view = player.GetComponent<PlayerUnitView>();

            IUnitModel model = new PlayerUnitModel(spawnNode, 3f);
            return new PlayerUnitController(model, view);
        }
    }
}