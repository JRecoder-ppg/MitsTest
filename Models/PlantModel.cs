using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MitsTest.Models
{
    public class PlantModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; } = "";

        [Required]
        public bool Active { get; set; } = false;

        [ForeignKey("CultureCode")]
        public int CultureCodeId { get; set; }
        public CultureCodeModel CultureCode { get; set; } = new CultureCodeModel();
    }
}
