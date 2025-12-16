using CartClash.Grid;
using CartClash.Units.Enemy;
using System.Collections.Generic;

namespace CartClash.Command
{
    public class EnemyChaseCommand : ICommand
    {
        private EnemyUnitService unitService;
        private GridNode startNode;
        private GridNode endNode;
        private List<GridNode> path;

        public EnemyChaseCommand(EnemyUnitService unitService, GridNode targetNode)
        {
            this.unitService = unitService;
            startNode = unitService.GetCurrentEnemyNode();
            endNode = targetNode;
        }

        // Check to execute command or not
        public bool CanExecute()
        {
            path = unitService.GeneratePath(endNode);
            return path != null && path.Count > 0;
        }

        // Executes the command
        public void Execute() => unitService.SetPath(path);

        // Undo the command
        public void Undo()
        {
            var undoPath = unitService.GeneratePath(startNode);

            if (undoPath != null)
                unitService.SetPath(undoPath);
        }
    }
}