namespace ShoppingSystem.Web.Persistence.Entities;

public class Category
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public List<Grocery> Groceries { get; set; } = new();
}