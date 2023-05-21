using ShoppingSystem.Web.Persistence.Entities;

namespace ShoppingSystem.Web.Models;

public class AddViewModel
{
    public List<Category> Categories { get; set; } = new();
    public List<ShoppingList> ShoppingLists { get; set; } = new();
}