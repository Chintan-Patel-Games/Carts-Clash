using CartClash.Grid;
using CartClash.Units.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace CartClash.Units.Enemy
{
    public class EnemyUnitService : IUnitService
    {
        private GameObject enemyPrefab;
        private EnemyUnitController unitController;

        public EnemyUnitService(GameObject enemyPrefab) => this.enemyPrefab = enemyPrefab;

        public void TickUpdate() => unitController.TickUpdate();

        public IUnitController SpawnUnit(GridNode spawnNode)
        {
            GameObject enemy = Object.Instantiate(enemyPrefab);
            var view = enemy.GetComponent<EnemyUnitView>();

            if (view == null)
            {
                Debug.LogError("EnemyUnitView missing on Enemy prefab");
                return null;
            }

            EnemyUnitModel model = new EnemyUnitModel(spawnNode, 3f);
            unitController = new EnemyUnitController(model, view);
            return unitController;
        }

        public void SetPath(List<GridNode> path) => unitController.SetPath(path);

        public GridNode GetCurrentEnemyNode() => unitController.CurrentEnemyNode();
    }
}