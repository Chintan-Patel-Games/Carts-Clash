namespace CartClash.Command
{
    public interface ICommand
    {
        public bool CanExecute();
        public void Execute();
        public void Undo();
    }
}