using Microsoft.EntityFrameworkCore;
using RecipesManagement.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Recipe> Recipes {get; set;}
}