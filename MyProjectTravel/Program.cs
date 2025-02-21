using Microsoft.AspNetCore.Authentication.Cookies;
using MyProyectTravel.Data.Public;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<AccountService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7215/api/Account/");
});

builder.Services.AddHttpClient<PasajesService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7215/api/Account/");
});


//builder.Services.AddHttpClient<AccountApiClient>(client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7215/api/");  // Cambia esto por la URL real de tu API
//});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
