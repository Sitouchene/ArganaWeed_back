using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPlantule.Models
{
    public class Provenance
    {
        [Key]
        [Column("provenance_id")]
        public int ProvenanceId { get; set; }

        [Required, MaxLength(100)]
        [Column("provenance_nom")]
        public string ProvenanceNom { get; set; }

        [MaxLength(255)]
        [Column("provenance_description")]
        public string ProvenanceDescription { get; set; }
    }
}
