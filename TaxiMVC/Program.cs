using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Models;
using TaxiMVC.Data;
using TaxiMVC.Models;
using Microsoft.AspNetCore.Identity;
using TaxiMVC.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TaxiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TaxiContext") ?? throw new InvalidOperationException("Connection string 'TaxiContext' not found.")));
builder.Services.AddDbContext<ApplicationUserContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationUserContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationUserContextConnection' not found.")));
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationUserContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Orders}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
