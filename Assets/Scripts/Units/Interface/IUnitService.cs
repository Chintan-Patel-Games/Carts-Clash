using CartClash.PathFinding;

namespace CartClash.Units.Interface
{
    // Interface for unit services
    public interface IUnitService
    {
        // Spawns a unit at the specified grid node
        public IUnitController SpawnUnit(GridNode spawnNode);
    }
}