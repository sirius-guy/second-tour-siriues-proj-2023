using ShoppingSystem.Web.Persistence.Entities;

namespace ShoppingSystem.Web.Models;

public class ListViewModelItem
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string State { get; set; } = "";
    public string Category { get; set; } = "";
    public DateTime CreationDateTime { get; set; }
    public DateTime UpdateDateTime { get; set; }
}

public class ListViewModel
{
    public List<Category> Categories { get; set; } = new();
    public List<ListViewModelItem> Groceries { get; set; } = new();
    public int GroceriesCount { get; set; }
    public ShoppingList List { get; set; } = null!;
    public int BoughtCount { get; set; }
}