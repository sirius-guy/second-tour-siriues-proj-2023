using Microsoft.EntityFrameworkCore;
using ShoppingSystem.Web.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

var serviceScopeFactory = app.Services.GetService<IServiceScopeFactory>();
using (var scope = serviceScopeFactory.CreateScope())
{
    var db = scope.ServiceProvider.GetService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseExceptionHandler("/error");
app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();