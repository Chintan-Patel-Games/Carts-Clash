using CartClash.Core;
using CartClash.UI.Interface;

namespace CartClash.UI.GameplayUI
{
    // Controller for managing gameplay UI elements
    public class GameplayUIController : IUIController
    {
        private GameplayUIView gameplayUIView;

        public GameplayUIController(GameplayUIView gameplayUIView)
        {
            this.gameplayUIView = gameplayUIView;
            gameplayUIView.SetController(this);
            gameplayUIView.HideSpawnPanel();
        }

        public void SetCurrentTileText(string text) => gameplayUIView.SetCurrentTileText(text);
        public void SetCurrentStateText(string text) => gameplayUIView.SetCurrentStateText(text);

        public void ShowPlayerSpawnPanel() => gameplayUIView.ShowPlayerSpawnPanel();
        public void ShowEnemySpawnPanel() => gameplayUIView.ShowEnemySpawnPanel();
        public void ShowWarningPanel() => gameplayUIView.ShowWarningPanel();
        public void HideSpawnPanel() => gameplayUIView.HideSpawnPanel();

        public void OnUndoButtonClicked() => GameService.Instance.GameLoopService.OnUndo();

        public void Show() => gameplayUIView.EnableView();
        public void Hide() => gameplayUIView.DisableView();
    }
}