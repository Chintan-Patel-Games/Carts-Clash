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

        private void Update() => HandleTileHover();

        private void OnClickStarted(InputAction.CallbackContext context) =>
            inputController.HandleClick(out Vector2Int targetNode);

        private void HandleTileHover()
        {
            if (!inputController.TryGetHoverTile(out Vector2Int currentTile, out string tileState)) return;

            Vector2Int tilePos = new Vector2Int(currentTile.x, currentTile.y);

            if (lastHoveredTile.HasValue && lastHoveredTile.Value == tilePos) return;

            lastHoveredTile = tilePos;

            GameService.Instance.UIService.UpdateCurrentTileText(tilePos.ToString() + " : " + tileState);
        }
    }
}