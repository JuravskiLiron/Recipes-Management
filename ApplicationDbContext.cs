using Microsoft.EntityFrameworkCore;
using RecipesManagement.Models;

public class ApplicationDbContext : DbContext
{
    public DbSet<Recipe> Recipes { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recipe>().HasKey(x => x.Id);
        base.OnModelCreating(modelBuilder);
    }

}
