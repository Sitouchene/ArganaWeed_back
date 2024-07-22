using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPlantule.Models
{
    public class Variete
    {
        [Key]
        [Column("variete_id")]
        public int VarieteId { get; set; }

        [Required, MaxLength(5)]
        [Column("variete_code")]
        public string VarieteCode { get; set; }

        [MaxLength(100)]
        [Column("variete_nom")]
        public string VarieteNom { get; set; }

        [MaxLength(255)]
        [Column("variete_description")]
        public string VarieteDescription { get; set; }
    }
}
