using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class BasicCoreWebAppDbContext: DbContext
    {
        public virtual DbSet<Student> Students { get; set; }

        public BasicCoreWebAppDbContext(DbContextOptions<BasicCoreWebAppDbContext> options)
    :       base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(Student.NameMaxLength).IsRequired();
            });
        }
    }
}
