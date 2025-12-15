using UnityEngine;

namespace CartClash.Core.Events
{
    public class EventService
    {
        // Event for OnMouseClick to send tilePos to PathfindingService
        public EventController<Vector2Int> OnTileSelected { get; private set; }

        public EventService()
        {
            OnTileSelected = new EventController<Vector2Int>();
        }
    }
}