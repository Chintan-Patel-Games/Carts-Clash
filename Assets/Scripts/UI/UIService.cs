using CartClash.UI.GameplayUI;
using UnityEngine;

public class UIService : MonoBehaviour
{
    [Header("Gameplay UI")]
    [SerializeField] private GameplayUIView gameplayUIView;
    private GameplayUIController gameplayUIController;

    private void Awake()
    {
        gameplayUIController = new GameplayUIController(gameplayUIView);
    }

    public void UpdateCurrentTileText(string text) =>
        gameplayUIController.SetCurrentTileText(text);

    public void UpdateCurrentStateText(string text) =>
        gameplayUIController.SetCurrentStateText(text);

    public void ToggleUndoButton(bool value) => gameplayUIController.ToggleUndoButton(value);

    public void ShowPlayerSpawnPanel() => gameplayUIController.ShowPlayerSpawnPanel();
    public void ShowEnemySpawnPanel() => gameplayUIController.ShowEnemySpawnPanel();
    public void ShowWarningPanel() => gameplayUIController.ShowWarningPanel();
    public void HideSpawnPanel() => gameplayUIController.HideSpawnPanel();
}