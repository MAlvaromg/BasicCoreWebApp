using BasicCoreWebApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace BasicCoreWebApp.DataAccess
{
    public class BasicCoreWebAppDbContext: DbContext
    {
        public virtual DbSet<Student> Students { get; set; }

        public BasicCoreWebAppDbContext(DbContextOptions<BasicCoreWebAppDbContext> options)
    :       base(options)
        { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=BasicCoreWebApp;Trusted_Connection=true;MultipleActiveResultSets=true");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(Student.NameMaxLength).IsRequired();
            });
        }
    }
}
