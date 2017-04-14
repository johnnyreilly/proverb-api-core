using Microsoft.EntityFrameworkCore;
using Proverb.Data.EntityFramework.Models;

namespace Proverb.Data.EntityFramework
{
    public partial class ProverbContext : DbContext
    {
        public virtual DbSet<Log4Net> Log4Net { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<Saying> Saying { get; set; }
        public virtual DbSet<User> User { get; set; }

        public ProverbContext(DbContextOptions<ProverbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log4Net>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Level).HasMaxLength(10);

                entity.Property(e => e.Logger).HasMaxLength(100);

                entity.Property(e => e.Message).HasMaxLength(255);

                entity.Property(e => e.Thread).HasMaxLength(10);
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Saying>(entity =>
            {
                entity.HasIndex(e => e.SageId)
                    .HasName("IX_SageId");

                entity.Property(e => e.Text).IsRequired();

                entity.HasOne(d => d.Sage)
                    .WithMany(p => p.Saying)
                    .HasForeignKey(d => d.SageId)
                    .HasConstraintName("FK_dbo.Saying_dbo.User_SageId");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Discriminator)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30);
            });
        }
    }
}