using CartClash.Utilities;
using UnityEngine;

namespace CartClash.Core.InputSystem
{
    public class InputService : GenericMonoSingleton<InputService>
    {
        [Header("Raycast Settings")]
        [SerializeField] private LayerMask tileLayer;

        private InputController inputController;
        private Vector2Int? lastHoveredTile;

        protected override void Awake()
        {
            base.Awake();

            Camera cam = Camera.main;
            inputController = new InputController(cam, tileLayer);
        }

        private void Update() => HandleTileHover();

        private void HandleTileHover()
        {
            if (!inputController.TryGetHoverTile(out Vector2Int currentTile, out string tileState)) return;

            if (lastHoveredTile.HasValue && lastHoveredTile.Value == currentTile) return;

            lastHoveredTile = currentTile;

            GameService.Instance.UIService.UpdateCurrentTileText(currentTile.ToString() + " : " + tileState);
        }
    }
}