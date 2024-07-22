using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPlantule.Models
{
    public class Emplacement
    {
        [Key]
        [Column("emplacement_id")]
        public int EmplacementId { get; set; }

        [Required, MaxLength(20)]
        [Column("emplacement_code")]
        public string EmplacementCode { get; set; }

        [MaxLength(255)]
        [Column("emplacement_description")]
        public string EmplacementDescription { get; set; }

        [Column("StorageMax")]
        public int StorageMax { get; set; }
    }
}
