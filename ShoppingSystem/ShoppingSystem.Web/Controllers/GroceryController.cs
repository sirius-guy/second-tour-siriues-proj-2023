using Microsoft.AspNetCore.Mvc;
using ShoppingSystem.Web.Models;
using ShoppingSystem.Web.Persistence;
using ShoppingSystem.Web.Persistence.Entities;

namespace ShoppingSystem.Web.Controllers;

[Route("groceries")]
public class GroceryController : Controller
{
    private readonly AppDbContext _db;

    public GroceryController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("add")]
    public IActionResult Add([FromForm] AddGroceryModel model)
    {
        var category = _db.Categories.FirstOrDefault(c => c.Id == model.Category);
        var list = _db.ShoppingLists.FirstOrDefault(c => c.Id == model.List);
        
        _db.Groceries.Add(new Grocery
        {
            Category = category!,
            ShoppingList = list!,
            Title = model.Name,
            State = "scheduled",
            CreationDateTime = DateTime.Now,
            UpdateDateTime = DateTime.Now
        });

        _db.SaveChanges();
        
        return View("MessagePage", model: new MessageViewModel
        {
            Title = "Успех",
            Text = $"Добавлен продукт «{model.Name}»"
        });
    }
    
    [HttpPost("edit/{id:int}")]
    public IActionResult Edit(int id, [FromForm] EditGroceryModel model)
    {
        var category = _db.Categories.FirstOrDefault(c => c.Id == model.Category)!;
        var list = _db.ShoppingLists.FirstOrDefault(c => c.Id == model.List)!;

        var grocery = _db.Groceries.FirstOrDefault(g => g.Id == id)!;

        grocery.Category = category;
        grocery.ShoppingList = list;
        grocery.Title = model.Name;
        grocery.UpdateDateTime = DateTime.Now;

        _db.SaveChanges();
        
        return View("MessagePage", model: new MessageViewModel
        {
            Title = "Успех",
            Text = $"Продукт «{model.Name}» изменён."
        });
    }
    
    [Route("delete/{id:int}")]
    public IActionResult Delete(int id)
    {
        var grocery = _db.Groceries.FirstOrDefault(g => g.Id == id);
        _db.Groceries.Remove(grocery!);
        _db.SaveChanges();
        
        return View("MessagePage", model: new MessageViewModel
        {
            Title = "Успех",
            Text = $"Продукт «{grocery!.Title}» удалён."
        });
    }
    
    [Route("updateStatus/{id:int}/{state}")]
    public IActionResult UpdateStatus(int id, string state)
    {
        var grocery = _db.Groceries.FirstOrDefault(g => g.Id == id)!;

        grocery.State = state;
        
        _db.SaveChanges();
        
        return Ok("updated status");
    }
}