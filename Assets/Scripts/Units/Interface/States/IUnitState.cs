namespace CartClash.Units.Interface.States
{
    // Interface for unit states
    public interface IUnitState
    {
        public void Start();
        public void Update();
        public void Exit();
    }
}