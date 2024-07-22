using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPlantule.Models
{
    public class User
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        [Column("user_name")]
        public string UserName { get; set; }

        [Required, MaxLength(255)]
        [Column("hashed_password")]
        public string HashedPassword { get; set; }

        [Required, MaxLength(200)]
        [Column("user_email")]
        public string UserEmail { get; set; }

        [MaxLength(50)]
        [Column("salt")]
        public string Salt { get; set; }

        [MaxLength(26)]
        [Column("statut")]
        public string Statut { get; set; }
    }
}
