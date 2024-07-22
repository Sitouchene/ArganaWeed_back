using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPlantule.Models
{
    public class Event
    {
        [Key]
        [Column("event_id")]
        public int EventId { get; set; }

        [Required]
        [Column("plantule_id")]
        public int PlantuleId { get; set; }

        [Required]
        [Column("event_datetime")]
        public DateTime EventDatetime { get; set; }

        [MaxLength(100)]
        [Column("event_user_name")]
        public string EventUserName { get; set; }

        [Required, MaxLength(50)]
        [Column("event_source")]
        public string EventSource { get; set; }

        [MaxLength(255)]
        [Column("event_log")]
        public string EventLog { get; set; }

        [ForeignKey(nameof(PlantuleId))]
        public Plantule Plantule { get; set; }
    }
}
