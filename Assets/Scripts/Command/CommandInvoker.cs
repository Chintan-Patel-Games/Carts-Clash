using System.Collections.Generic;

namespace CartClash.Command
{
    public class CommandInvoker
    {
        private Stack<ICommand> commandRegistry = new Stack<ICommand>();

        public void ProcessCommand(ICommand commandToProcess)
        {
            if (!commandToProcess.CanExecute()) return;

            commandToProcess.Execute();
            commandRegistry.Push(commandToProcess);
        }

        public void Undo()
        {
            if (!RegistryEmpty())
                commandRegistry.Pop().Undo();
        }

        private bool RegistryEmpty() => commandRegistry.Count == 0;
    }
}