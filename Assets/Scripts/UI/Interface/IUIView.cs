namespace CartClash.UI.Interface
{
    // Interface for UI Views
    public interface IUIView
    {
        public void SetController(IUIController controllerToSet);
        public void EnableView();
        public void DisableView();
    }
}