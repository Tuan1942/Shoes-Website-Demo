using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoesMVC.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ShoesMVCDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ShoesMVCDbContextConnection' not found.");

builder.Services.AddDbContext<ShoesMVCDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ShoesMVCUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ShoesMVCDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();   
app.Run();
