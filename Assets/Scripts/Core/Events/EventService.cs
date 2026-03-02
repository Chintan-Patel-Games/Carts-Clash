using CartClash.Grid;
using CartClash.Core.Events.Controller;
using CartClash.Core.Events.Controllers;

namespace CartClash.Core.Events.Service
{
    public class EventService
    {
        // Event for OnMouseClick to send tilePos to PathfindingService
        public GenericEventController<GridNode> OnTileSelected { get; private set; }
        // Event for enemy to start chasing player when player enters arrived state
        public EventController StartChasingPlayer { get; private set; }
        // Event for switching to player turn
        public EventController SwitchToPlayerTurn { get; private set; }

        public EventService()
        {
            OnTileSelected = new GenericEventController<GridNode>();
            StartChasingPlayer = new EventController();
            SwitchToPlayerTurn = new EventController();
        }
    }
}