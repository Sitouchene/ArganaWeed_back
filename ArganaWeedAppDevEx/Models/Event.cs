using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ArganaWeedAppDevEx.Models
{
    public class Event:BaseModel
    {
        //public int EventId { get; set; }
        public DateTime EventDatetime { get; set; }
        public string EventSource { get; set; }
        public string EventType { get; set; }
        public int PlantuleId { get; set; }
        public string EventNature { get; set; }
        public string EventValeur { get; set; }
        public string EventObservation { get; set; }
        public string EventUserName { get; set; }
    }
}
