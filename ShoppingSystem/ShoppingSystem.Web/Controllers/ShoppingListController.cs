using Microsoft.AspNetCore.Mvc;
using ShoppingSystem.Web.Models;
using ShoppingSystem.Web.Persistence;
using ShoppingSystem.Web.Persistence.Entities;

namespace ShoppingSystem.Web.Controllers;

[Route("shoppingLists")]
public class ShoppingListController : Controller
{
    private readonly AppDbContext _db;

    public ShoppingListController(AppDbContext db)
    {
        _db = db;
    }

    [Route("add")]
    public IActionResult Add([FromForm] string text)
    {
        _db.ShoppingLists.Add(new ShoppingList
        {
            Text = text
        });
        _db.SaveChanges();
        return View("MessagePage", new MessageViewModel
        {
            Title = "Успех",
            Text = $"Успешно добавлен список «{text}»"
        });
    }
    
    [Route("delete/{id:int}")]
    public IActionResult Delete(int id)
    {
        _db.Remove(_db.ShoppingLists.FirstOrDefault(c => c.Id == id)!);
        _db.SaveChanges();
        
        return View("MessagePage", new MessageViewModel
        {
            Title = "Успех",
            Text = $"Успешно удалён список."
        });
    }
}