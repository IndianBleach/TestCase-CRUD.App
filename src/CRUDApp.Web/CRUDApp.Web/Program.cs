using CRUDApp.Infrastructure.Data;
using CRUDApp.Web.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddCoreServices();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLConnectionString")));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await ApplicationDbContextSeed
        .SeedDatabaseAsync(scope.ServiceProvider.GetRequiredService<ApplicationDbContext>());
}


if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
