using Microsoft.AspNetCore.Mvc;
using ShoppingSystem.Web.Models;
using ShoppingSystem.Web.Persistence;
using ShoppingSystem.Web.Persistence.Entities;

namespace ShoppingSystem.Web.Controllers;

[Route("categories")]
public class CategoryController : Controller
{
    private readonly AppDbContext _db;

    public CategoryController(AppDbContext db)
    {
        _db = db;
    }

    [Route("add")]
    public IActionResult Add([FromForm] string text)
    {
        _db.Categories.Add(new Category
        {
            Text = text
        });
        _db.SaveChanges();
        return View("MessagePage", new MessageViewModel
        {
            Title = "Успех",
            Text = $"Успешно добавлена категория «{text}»"
        });
    }
    
    [Route("delete/{id:int}")]
    public IActionResult Delete(int id)
    {
        _db.Remove(_db.Categories.FirstOrDefault(c => c.Id == id)!);
        _db.SaveChanges();
        
        return View("MessagePage", new MessageViewModel
        {
            Title = "Успех",
            Text = $"Успешно удалена категория."
        });
    }
}