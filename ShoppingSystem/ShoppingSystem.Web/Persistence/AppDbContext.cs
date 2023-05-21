using Microsoft.EntityFrameworkCore;
using ShoppingSystem.Web.Persistence.Entities;

namespace ShoppingSystem.Web.Persistence;

public sealed class AppDbContext : DbContext
{
    public DbSet<Grocery> Groceries { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<ShoppingList> ShoppingLists { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}