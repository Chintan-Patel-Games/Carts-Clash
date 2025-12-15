using CartClash.PathFinding;

namespace CartClash.Core.Events
{
    public class EventService
    {
        // Event for OnMouseClick to send tilePos to PathfindingService
        public EventController<GridNode> OnTileSelected { get; private set; }

        public EventService()
        {
            OnTileSelected = new EventController<GridNode>();
        }
    }
}