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
        }

        public void SetCurrentTileText(string text) => gameplayUIView.SetCurrentTileText(text);

        public void Show() => gameplayUIView.EnableView();

        public void Hide() => gameplayUIView.DisableView();
    }
}