using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MitsTest.Models
{
    //[Table("CultureCode",Schema ="tests")]
    public class CultureCodeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; } = "";

        [Required]
        public string CultureCode { get; set; } = "";

        [Required]
        public bool Active { get; set; } = false;

    }
}
