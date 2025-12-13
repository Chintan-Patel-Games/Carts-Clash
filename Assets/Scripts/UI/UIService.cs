using CartClash.UI.GameplayUI;
using UnityEngine;

public class UIService : MonoBehaviour
{
    [Header("Gameplay UI")]
    [SerializeField] private GameplayUIView gameplayUIView;
    private GameplayUIController gameplayUIController;

    private void Start()
    {
        gameplayUIController = new GameplayUIController(gameplayUIView);
    }

    public void UpdateCurrentTileText(string text) =>
        gameplayUIController.SetCurrentTileText(text);
}