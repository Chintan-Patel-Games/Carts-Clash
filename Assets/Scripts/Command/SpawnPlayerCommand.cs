using CartClash.Grid;
using CartClash.Units.Player;

namespace CartClash.Command
{
    public class SpawnPlayerCommand : ICommand
    {
        private PlayerUnitService unitService;
        private GridNode spawnNode;

        public SpawnPlayerCommand(PlayerUnitService unitService, GridNode spawnNode)
        {
            this.unitService = unitService;
            this.spawnNode = spawnNode;
        }

        // Check to execute command or not
        public bool CanExecute() => true;

        // Executes the command
        public void Execute() => unitService.SpawnUnit(spawnNode);

        // Undo the command
        public void Undo() => unitService.DeleteUnit();
    }
}