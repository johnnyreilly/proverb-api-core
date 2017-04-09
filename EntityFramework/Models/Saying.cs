using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proverb.Api.Core.EntityFramework.Models
{
    public class Saying
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SageId { get; set; }
        public Sage Sage { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
