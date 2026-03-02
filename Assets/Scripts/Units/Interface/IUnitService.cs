using CartClash.Grid;

namespace CartClash.Units.Interface.Service
{
    /// <summary>
    /// Defines a service for spawning units at specified locations within a grid.
    /// </summary>
    public interface IUnitService
    {
        public void SpawnUnit(GridNode spawnNode);
        public void DeleteUnit();
    }
}