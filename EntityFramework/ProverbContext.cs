using Microsoft.EntityFrameworkCore;
using Proverb.Api.Core.EntityFramework.Models;

namespace Proverb.Api.Core.EntityFramework
{
    public class ProverbContext : DbContext
    {
        public ProverbContext(DbContextOptions<ProverbContext> options)
            : base(options)
        {
        }

        public DbSet<Sage> Sages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Saying> Sayings { get; set; }
    }
}
