using ShoppingSystem.Web.Persistence.Entities;

namespace ShoppingSystem.Web.Models;

public class EditViewModel
{
    public int Id { get; set; }
    public Grocery Grocery { get; set; } = null!;
    public List<Category> Categories { get; set; } = new();
    public List<ShoppingList> ShoppingLists { get; set; } = new();
}