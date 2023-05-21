namespace ShoppingSystem.Web.Persistence.Entities;

public class Grocery
{
    public int Id { get; set; }
    public Category Category { get; set; } = null!;
    public ShoppingList ShoppingList { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string State { get; set; } = null!;
    public DateTime CreationDateTime { get; set; }
    public DateTime UpdateDateTime { get; set; }
}