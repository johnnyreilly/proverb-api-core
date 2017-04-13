using System;
using System.Collections.Generic;

namespace Proverb.Api.Core.EntityFramework.Models
{
    public partial class User
    {
        public User()
        {
            Saying = new HashSet<Saying>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Discriminator { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Sagacity { get; set; }

        public virtual ICollection<Saying> Saying { get; set; }
    }
}
