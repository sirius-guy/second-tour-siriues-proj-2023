namespace ShoppingSystem.Web.Models;

public class GroceryCsvModel
{
    public int Id { get; set; }
    public string Category { get; set; } = null!;
    public string ShoppingList { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string State { get; set; } = null!;
}