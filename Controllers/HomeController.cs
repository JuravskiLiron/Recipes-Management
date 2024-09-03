using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipesManagement.Models;

namespace RecipesManagement.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext context;
    private readonly ILogger<HomeController> loger;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        this.context = context;
        this.loger = logger;
    }

    [HttpGet]
    public IActionResult Home(int page = 1)
    {
        var recipes = context.Recipes.ToList();
        var recipesPerPage = 9;
        var totalRecipes = recipes.Count();
        var totalPages = (int)Math.Ceiling(totalRecipes / (double)recipesPerPage);
        
        var pagedRecipes = recipes.Skip((page - 1) * recipesPerPage).Take(recipesPerPage);
    
        ViewData["CurrentPage"] = page;
        ViewData["TotalPages"] = totalPages;
    
        return View(pagedRecipes);
    }
    
    public IActionResult RecipeDetail()
    {
        
        return View();
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(Recipe recipe)
    {
        if (ModelState.IsValid)
        {
            if (!string.IsNullOrEmpty(recipe.RecipePhoto))
            {
                
            }

            context.Recipes.Add(recipe);
            await context.SaveChangesAsync();
            
            return RedirectToAction("Home");
        }
    
        return View(recipe);
    }
    
    
    

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
