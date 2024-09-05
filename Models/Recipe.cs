using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipesManagement.Models;

public class Recipe 
{
    public int Id {get; set;} 

    [Required]
    public string RecipeName {get; set;}

    [Required]
    public string RecipeIngredients {get; set;}

    [Required]
    public string RecipeCategory { get; set; }

    public string? ImagePath {get; set;}

    [NotMapped]
    public IFormFile? RecipePhoto { get; set; }
    
}