using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPlantule.Models
{
    public class Note
    {
        [Key]
        [Column("note_id")]
        public int NoteId { get; set; }

        [Required, MaxLength(255)]
        [Column("note_texte")]
        public string NoteTexte { get; set; }

        [Column("note_date")]
        public DateTime NoteDate { get; set; }

        [Required]
        [Column("plantule_id")]
        public int PlantuleId { get; set; }

        [MaxLength(100)]
        [Column("note_user_name")]
        public string NoteUserName { get; set; }

        [ForeignKey(nameof(PlantuleId))]
        public Plantule Plantule { get; set; }
    }
}
