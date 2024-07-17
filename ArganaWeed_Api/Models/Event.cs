namespace ArganaWeed_Api.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public DateTime EventDatetime { get; set; }
        public string EventSource { get; set; }
        public string EventType { get; set; }
        public int PlantuleId { get; set; }
        public string EventNature { get; set; }
        public string EventValeur { get; set; }
        public string EventUserName { get; set; }

        // Navigation property
        public Plantule Plantule { get; set; }
    }
}
