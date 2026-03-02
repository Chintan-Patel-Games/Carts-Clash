using CartClash.Core;
using CartClash.Grid;
using CartClash.Units.Enemy;

namespace CartClash.Command
{
    public class SpawnEnemyCommand : ICommand
    {
        private EnemyUnitService unitService;
        private GridNode spawnNode;
        private GridNode targetNode;

        public SpawnEnemyCommand(EnemyUnitService unitService, GridNode spawnNode, GridNode targetNode)
        {
            this.unitService = unitService;
            this.spawnNode = spawnNode;
            this.targetNode = targetNode;
        }

        // Check to execute command or not
        public bool CanExecute()
        {
            if (!unitService.CanGeneratePath(spawnNode, targetNode))
            {
                GameService.Instance.UIService.ShowWarningPanel();
                return false;
            }
            return true;
        }

        // Executes the command
        public void Execute() => unitService.SpawnUnit(spawnNode);

        // Undo the command
        public void Undo() => unitService.DeleteUnit();
    }
}