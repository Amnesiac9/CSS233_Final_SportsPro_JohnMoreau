/*
* John Moreau
* CSS233
* 12/9/2023
*
*/

using CSS233_Final_SportsPro_JohnMoreau.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Enable Session State
builder.Services.AddMemoryCache();
builder.Services.AddSession();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRouting(options => {
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;

});

// Add EF Core DI
builder.Services.AddDbContext<SportsContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SportsContext")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Enable Session State
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}"
);

app.Run();
