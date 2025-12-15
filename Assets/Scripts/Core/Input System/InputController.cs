using CartClash.Grid.Tile;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CartClash.Core.InputSystem
{
    // Controller for handling player input
    public class InputController
    {
        private Camera camera;
        private LayerMask tileLayer;

        public InputController(Camera camera, LayerMask tileLayer)
        {
            this.camera = camera;
            this.tileLayer = tileLayer;
        }

        // Tries to get the grid position of the tile currently under the mouse cursor
        public bool TryGetHoverTile(out Vector2Int tilePos)
        {
            tilePos = default;

            if (Mouse.current == null || camera == null) return false;

            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = camera.ScreenPointToRay(mousePos);

            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, tileLayer)) return false;

            TileView tileView = hit.collider.GetComponent<TileView>();
            if (tileView == null) return false;

            tilePos = tileView.gridPosition;
            return true;
        }
    }
}