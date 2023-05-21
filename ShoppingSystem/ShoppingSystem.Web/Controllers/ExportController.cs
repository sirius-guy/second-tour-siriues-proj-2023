using System.Text;
using Microsoft.AspNetCore.Mvc;
using ShoppingSystem.Web.Persistence;

namespace ShoppingSystem.Web.Controllers;

[Route("export")]
public class ExportController
{
    private readonly AppDbContext _db;

    public ExportController(AppDbContext db)
    {
        _db = db;
    }

    [Route("csv")]
    public IActionResult ExportToTsv()
    {
        var groceries = _db.Groceries
            .Select(g => new []
            {
                g.Id.ToString(),
                g.Title.ToString(),
                g.Category.Text.ToString(),
                g.ShoppingList.Text.ToString(),
                g.State.ToString()
            })
            .ToList();
        
        groceries.Insert(0, new [] { "ID", "Name", "Category", "List", "State" });
 
        var sb = new StringBuilder();
        
        foreach (var customer in groceries)
        {
            foreach (var t in customer) sb.Append(t + ',');

            sb.Append("\r\n");
        }

        
        return new FileContentResult(
            Encoding.UTF8.GetBytes(sb.ToString()),
            "text/csv")
        {
            FileDownloadName = "Продукты.csv"
        };
    }
}