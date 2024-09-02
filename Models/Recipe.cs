namespace RecipesManagement.Models;

public class Recipe 
{
    public int Id {get; set;}
    public string RecipeName {get; set;}
    public string RecipeIngredients {get; set;}
    public string RecipeCategory { get; set; }
    public string RecipePhoto { get; set; }
    
}