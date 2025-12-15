using CartClash.Grid;

namespace CartClash.Core.Events
{
    public class EventService
    {
        // Event for OnMouseClick to send tilePos to PathfindingService
        public EventController<GridNode> OnTileSelected { get; private set; }
        // Event for enemy to start chasing player when player enters arrived state
        public EventController<GridNode> StartChasingPlayer { get; private set; }

        public EventService()
        {
            OnTileSelected = new EventController<GridNode>();
            StartChasingPlayer = new EventController<GridNode>();
        }
    }
}