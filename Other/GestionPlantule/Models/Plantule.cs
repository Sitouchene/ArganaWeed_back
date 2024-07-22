using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPlantule.Models
{
    public class Plantule
    {
        [Key]
        [Column("plantule_id")]
        public int PlantuleId { get; set; }

        [Required]
        [Column("variete_id")]
        public int VarieteId { get; set; }

        [Required, MaxLength(9)]
        [Column("plantule_variete")]
        public string PlantuleVariete { get; set; }

        [MaxLength(255)]
        [Column("plantule_description")]
        public string PlantuleDescription { get; set; }

        [Required]
        [Column("date_reception")]
        public DateTime DateReception { get; set; }

        [Required]
        [Column("provenance_id")]
        public int ProvenanceId { get; set; }

        [MaxLength(50)]
        [Column("stade")]
        public string Stade { get; set; }

        [MaxLength(50)]
        [Column("sante")]
        public string Sante { get; set; }

        [Required]
        [Column("emplacement_id")]
        public int EmplacementId { get; set; }

        [Column("statut")]
        public bool Statut { get; set; }

        [MaxLength(10)]
        [Column("qrbase")]
        public string Qrbase { get; set; }

        [Column("sortie_date")]
        public DateTime? SortieDate { get; set; }

        [Column("ArchiveStatut")]
        public bool ArchiveStatut { get; set; }

        [ForeignKey(nameof(VarieteId))]
        public Variete Variete { get; set; }

        [ForeignKey(nameof(ProvenanceId))]
        public Provenance Provenance { get; set; }

        [ForeignKey(nameof(EmplacementId))]
        public Emplacement Emplacement { get; set; }

        [NotMapped]
        public string SanteColor { get; set; }
    }
}
