using CartClash.UI.Interface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CartClash.UI.GameplayUI
{
    // Controller for managing gameplay UI elements
    public class GameplayUIView : MonoBehaviour, IUIView
    {
        [SerializeField] private TextMeshProUGUI currentTileText;
        [SerializeField] private TextMeshProUGUI currentStateText;
        [SerializeField] private CanvasGroup spawnPlayerCanvas;
        [SerializeField] private CanvasGroup spawnEnemyCanvas;
        [SerializeField] private CanvasGroup warningCanvas;
        [SerializeField] private Button undoBtn;

        private GameplayUIController controller;

        private float warningTimer;
        private bool warningActive;

        private void Update()
        {
            if (!warningActive) return; ;

            warningTimer -= Time.deltaTime;

            if (warningTimer <= 0f)
            {
                warningActive = false;
                warningCanvas.alpha = 0;
            }
        }

        public void SetController(IUIController controllerToSet)
        {
            controller = controllerToSet as GameplayUIController;
            undoBtn.onClick.AddListener(controller.OnUndoButtonClicked);
        }

        public void ToggleUndoButton(bool toggle) => undoBtn.enabled = toggle;

        public void SetCurrentTileText(string text) => currentTileText.SetText(text);

        public void SetCurrentStateText(string text) => currentStateText.SetText(text);

        public void ShowPlayerSpawnPanel()
        {
            spawnPlayerCanvas.alpha = 1;
            spawnEnemyCanvas.alpha = 0;
        }

        public void ShowEnemySpawnPanel()
        {
            spawnEnemyCanvas.alpha = 1;
            spawnPlayerCanvas.alpha = 0;
        }

        public void ShowWarningPanel(float duration = 3f)
        {
            warningTimer = duration;
            warningActive = true;

            warningCanvas.alpha = 1;
        }

        public void HideSpawnPanel()
        {
            spawnEnemyCanvas.alpha = 0;
            spawnPlayerCanvas.alpha = 0;
            warningCanvas.alpha = 0;
        }

        public void EnableView() => gameObject.SetActive(true);

        public void DisableView() => gameObject.SetActive(false);
    }
}