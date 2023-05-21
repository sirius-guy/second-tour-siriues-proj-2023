using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingSystem.Web.Models;
using ShoppingSystem.Web.Persistence;
using ShoppingSystem.Web.Persistence.Entities;

namespace ShoppingSystem.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _db;

    public HomeController(ILogger<HomeController> logger, AppDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    [Route("/add")]
    public IActionResult Add()
    {
        var categories = _db.Categories
            .ToList();
        var shoppingLists = _db.ShoppingLists
            .ToList();
        
        return View(model: new AddViewModel
        {
            Categories = categories,
            ShoppingLists = shoppingLists
        });
    }
    
    [Route("/groceryEditPage/{id:int}")]
    public IActionResult GroceryEditPage(int id)
    {
        var categories = _db.Categories
            .ToList();
        var shoppingLists = _db.ShoppingLists
            .ToList();
        var grocery = _db.Groceries.FirstOrDefault(g => g.Id == id)!;
        
        return View(model: new EditViewModel
        {
            Id = id,
            Categories = categories,
            ShoppingLists = shoppingLists,
            Grocery = grocery
        });
    }
    
    [Route("/lists/{id:int}")]
    public IActionResult List(int id)
    {
        var list = _db.ShoppingLists
            .Include(l => l.Groceries)
            .FirstOrDefault(l => l.Id == id)!;
        var categories = _db.Categories
            .ToList();
        var groceries = _db.Groceries
            .Include(g => g.ShoppingList)
            .Include(g => g.Category)
            .Where(g => g.ShoppingList.Id == id)
            .Select(g => new ListViewModelItem
            {
                Id = g.Id,
                State = g.State,
                Name = g.Title,
                CreationDateTime = g.CreationDateTime,
                UpdateDateTime = g.UpdateDateTime,
                Category = g.Category.Text
            })
            .ToList();
        
        categories.Insert(0, new Category
        {
            Text = "Все категории",
            Id = int.MaxValue
        });

        return View(model: new ListViewModel
        {
            Groceries = groceries,
            GroceriesCount = groceries.Count,
            List = list,
            Categories = categories,
            BoughtCount = list.Groceries.Count(g => g.State == "bought")
        });
    }
    
    [Route("/lists/{id:int}/{categoryId:int}")]
    public IActionResult ListFilteredByCategory(int id, int categoryId)
    {
        var list = _db.ShoppingLists
            .Include(l => l.Groceries)
            .FirstOrDefault(l => l.Id == id)!;
        var categories = _db.Categories
            .ToList();
        List<ListViewModelItem> groceries;

        if (categoryId != int.MaxValue)
        {
            groceries = _db.Groceries
                .Include(g => g.ShoppingList)
                .Include(g => g.Category)
                .Where(g => g.ShoppingList.Id == id)
                .Where(g => g.Category.Id == categoryId)
                .Select(g => new ListViewModelItem
                {
                    Id = g.Id,
                    State = g.State,
                    Name = g.Title,
                    CreationDateTime = g.CreationDateTime,
                    UpdateDateTime = g.UpdateDateTime,
                    Category = g.Category.Text
                })
                .ToList();
        }
        else
        {
            groceries = _db.Groceries
                .Include(g => g.ShoppingList)
                .Include(g => g.Category)
                .Where(g => g.ShoppingList.Id == id)
                .Select(g => new ListViewModelItem
                {
                    Id = g.Id,
                    State = g.State,
                    Name = g.Title,
                    CreationDateTime = g.CreationDateTime,
                    UpdateDateTime = g.UpdateDateTime,
                    Category = g.Category.Text
                })
                .ToList();
        }
        
        categories.Insert(0, new Category
        {
            Text = "Все категории",
            Id = int.MaxValue
        });
        
        return View("List", model: new ListViewModel
        {
            Groceries = groceries,
            GroceriesCount = groceries.Count,
            List = list,
            Categories = categories,
            BoughtCount = list.Groceries.Count(g => g.State == "bought")
        });
    }
    
    [Route("/")]
    public IActionResult Lists()
    {
        var lists = _db.ShoppingLists
            .Include(l => l.Groceries)
            .Select(l => new ListsViewModelItem
            {
                Id = l.Id,
                Text = l.Text,
                Bought = l.Groceries.Count(l => l.State == "bought"),
                Scheduled = l.Groceries.Count(l => l.State == "scheduled"),
                InCart = l.Groceries.Count(l => l.State == "inCart")
            })
            .ToList();
        
        return View(model: new ListsViewModel
        {
            Lists = lists
        });
    }
    
    [Route("/categories")]
    public IActionResult Categories()
    {
        var categories = _db.Categories
            .Include(l => l.Groceries)
            .ToList();

        ViewBag.Categories = categories;
        
        return View();
    }

    [Route("/error")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}