using Microsoft.EntityFrameworkCore;
using TestData.Domain;

namespace TestData.DAL;

public class ApplicationDbContext: DbContext
{
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):
        base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<Human> Humans { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Human>().ToTable("humans");
        modelBuilder.Entity<Human>().Property(p => p.Id).IsRequired(true).HasColumnName("id");
        modelBuilder.Entity<Human>().Property(p => p.Name).IsRequired(true).HasColumnName("name");
        modelBuilder.Entity<Human>().Property(p => p.Age).IsRequired(true).HasColumnName("age");
        modelBuilder.Entity<Human>().Property(p => p.Sex).IsRequired(true).HasColumnName("sex");
        modelBuilder.Entity<Human>().HasKey(k => k.Id);
    }
}