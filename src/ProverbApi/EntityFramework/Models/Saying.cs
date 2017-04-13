
namespace Proverb.Api.Core.EntityFramework.Models
{
    public partial class Saying
    {
        public int Id { get; set; }
        public int SageId { get; set; }
        public string Text { get; set; }

        public virtual User Sage { get; set; }
    }
}
