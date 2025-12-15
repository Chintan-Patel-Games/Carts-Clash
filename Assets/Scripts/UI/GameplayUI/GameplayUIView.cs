using CartClash.UI.Interface;
using TMPro;
using UnityEngine;

namespace CartClash.UI.GameplayUI
{
    // Controller for managing gameplay UI elements
    public class GameplayUIView : MonoBehaviour, IUIView
    {
        [SerializeField] private TextMeshProUGUI currentTileText;
        private GameplayUIController controller;

        public void SetController(IUIController controllerToSet) =>
            controller = controllerToSet as GameplayUIController;

        public void SetCurrentTileText(string text) => currentTileText.SetText(text);

        public void EnableView() => gameObject.SetActive(true);

        public void DisableView() => gameObject.SetActive(false);
    }
}