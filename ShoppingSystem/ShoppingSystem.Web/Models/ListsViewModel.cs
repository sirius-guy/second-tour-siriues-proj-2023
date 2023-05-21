namespace ShoppingSystem.Web.Models;

public class ListsViewModelItem
{
    public int Id { get; set; }
    public string Text { get; set; } = "";
    public int Scheduled { get; set; }
    public int InCart { get; set; }
    public int Bought { get; set; }
}

public class ListsViewModel
{
    public List<ListsViewModelItem> Lists { get; set; } = new();
}