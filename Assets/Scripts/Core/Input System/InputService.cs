using CartClash.Grid;
using CartClash.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CartClash.Core.InputSystem
{
    public class InputService : GenericMonoSingleton<InputService>
    {
        [Header("Raycast Settings")]
        [SerializeField] private LayerMask tileLayer;

        private InputController inputController;
        private Vector2Int? lastHoveredTile;

        private PlayerInput playerInput;
        private InputAction clickAction;

        private bool inputEnabled = true;

        protected override void Awake()
        {
            base.Awake();

            Camera cam = Camera.main;
            inputController = new InputController(cam, tileLayer);

            playerInput = GetComponent<PlayerInput>();
            clickAction = playerInput.actions["UI/Click"];
        }

        private void OnEnable()
        {
            playerInput.actions.Enable();
            clickAction.performed += OnClickStarted;
        }

        private void OnDisable()
        {
            clickAction.performed -= OnClickStarted;
            playerInput.actions.Disable();
        }

        private void Update()
        {
            if (!inputEnabled) return;
            HandleTileHover();
        }

        public void ToggleInput(bool value) => inputEnabled = value;

        // Handles the logic for mouse click
        private void OnClickStarted(InputAction.CallbackContext context)
        {
            if (!inputEnabled) return;
            inputController.HandleClick(out GridNode targetNode);
        }

        // Handles the logic for cursor when hovered over a tile
        private void HandleTileHover()
        {
            if (!inputController.TryGetHoverTile(out GridNode currentTile, out string tileState)) return;

            Vector2Int tilePos = new Vector2Int(currentTile.x, currentTile.y);

            if (lastHoveredTile.HasValue && lastHoveredTile.Value == tilePos) return;

            lastHoveredTile = tilePos;

            GameService.Instance.UIService.UpdateCurrentTileText(tilePos.ToString() + " : " + tileState);
        }
    }
}