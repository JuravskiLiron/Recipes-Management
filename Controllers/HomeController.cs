using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RecipesManagement.Models;

namespace RecipesManagement.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext context;
    private readonly ILogger<HomeController> loger;
    private readonly IWebHostEnvironment _environment;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IWebHostEnvironment environment)
    {
        this.context = context;
        this.loger = logger;
        _environment = environment;
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

    private bool IsAllowedFileType(string fileType)
    {
        return fileType == ".png" || fileType == ".jpg" || fileType == ".jpeg";
    }

    private bool IsFileSizeAllowed(long fileSize)
    {
        return fileSize <= 10485760; // 10MB
    }

    private string GetUniqueFileName(string fileName)
    {
        return $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
    }

    [BindProperty]
    public Models.Recipe Recipes { get; set; } = default!;
    
    [HttpPost]
    public async Task<IActionResult> Create(IFormFile RecipePhoto)
    {
        if (ModelState.IsValid)
        {
            if (RecipePhoto != null && RecipePhoto.Length > 0)
            {
                var fileType = Path.GetExtension(RecipePhoto.FileName).ToLowerInvariant();
                if (IsAllowedFileType(fileType) && IsFileSizeAllowed(RecipePhoto.Length))
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = GetUniqueFileName(RecipePhoto.FileName);
                    var fileSavePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(fileSavePath, FileMode.Create))
                    {
                        await RecipePhoto.CopyToAsync(stream);
                    }

                    Recipes.ImagePath = "/uploads/" + uniqueFileName;
                }
            }

            context.Recipes.Add(Recipes);
            await context.SaveChangesAsync();

            return RedirectToAction("Home");
        }
    
        // Возвращаем View с текущей моделью, если ModelState не валидна
        return View(Recipes);
    }
    
    
    public IActionResult Terms()
    {
        return View();
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
