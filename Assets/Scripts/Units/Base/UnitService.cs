using CartClash.Grid;
using CartClash.Units.Base.Controller;
using CartClash.Units.Base.Model;
using CartClash.Units.Base.View;
using CartClash.Units.Interface.Service;
using System.Collections.Generic;
using UnityEngine;

namespace CartClash.Units.Base.Service
{
    public abstract class UnitService<TController, TModel, TView> : IUnitService
        where TController : UnitController<TModel, TView>
        where TModel : UnitModel
        where TView : UnitView
    {
        protected GameObject prefab;
        protected TController unitController;

        protected UnitService(GameObject prefab) => this.prefab = prefab;

        public virtual void TickUpdate() => unitController?.TickUpdate();

        public virtual void SpawnUnit(GridNode spawnNode)
        {
            GameObject unitObject = Object.Instantiate(prefab);
            var view = unitObject.GetComponent<TView>();

            if (view == null)
            {
                Debug.LogError($"[{GetType().Name}] : {typeof(TView).Name} missing on prefab");
                return;
            }

            TModel model = CreateModel(spawnNode);
            unitController = CreateController(model, view);
        }

        protected abstract TModel CreateModel(GridNode spawnNode);
        protected abstract TController CreateController(TModel model, TView view);

        public virtual void DeleteUnit()
        {
            if (unitController == null) return;

            var view = unitController.GetUnitView();
            if (view != null) Object.Destroy(view.gameObject);

            unitController = null;
        }

        public virtual void SetPath(List<GridNode> path) => unitController?.SetPath(path);

        public virtual GridNode GetCurrentNode() => unitController.GetCurrentNode();

        public TController GetUnitController() => unitController;
    }
}