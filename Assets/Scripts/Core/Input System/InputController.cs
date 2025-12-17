using CartClash.Core.StateMachine;
using CartClash.Grid;
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

        // Handles the Mouse Click event
        public void HandleClick(out GridNode targetNode)
        {
            targetNode = default;
            if (!Mouse.current.leftButton.isPressed) return;

            TileView tileView = GetTileFromMousePos();
            targetNode = default;
            if (tileView == null) return;

            targetNode = tileView.gridPosition;

            GameLoopState currentState = GameService.Instance.GameLoopService.GetCurrentState();
            GameService.Instance.GameLoopService.OnTileSelected(currentState, targetNode);
            GameService.Instance.EventService.OnTileSelected.InvokeEvent(targetNode);
        }

        // Tries to get the grid position of the tile currently under the mouse cursor
        public bool TryGetHoverTile(out GridNode tilePos, out string tileState)
        {
            tilePos = default;
            tileState = string.Empty;

            if (Mouse.current == null || camera == null) return false;

            TileView tileView = GetTileFromMousePos();
            if (tileView == null) return false;

            tilePos = tileView.gridPosition;
            tileState = tileView.GetTileState().ToString();
            return true;
        }

        // Getter method to get TileView from mouse click
        private TileView GetTileFromMousePos()
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = camera.ScreenPointToRay(mousePos);

            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, tileLayer)) return null;

            return hit.collider.GetComponent<TileView>();
        }
    }
}