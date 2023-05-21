using ShoppingSystem.Web.Persistence.Entities;

namespace ShoppingSystem.Web.Models;

public class IndexViewModel
{
    public List<Grocery> Groceries { get; set; } = new();
}