using Microsoft.AspNetCore.Authentication.Cookies;
using MyProyectTravel.Services;
using MyProyectTravel.Services.Public;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<AccountService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7215/api/Account/");
});

builder.Services.AddHttpClient<PasajesService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7215/api/Pasajes/");
});

builder.Services.AddHttpClient<BusService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7215/api/Bus/");
});

builder.Services.AddHttpClient<ItineraryService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7215/api/Itinerario/");
});

builder.Services.AddHttpClient<StationService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7215/api/Estacion/");
});

builder.Services.AddHttpClient<TicketService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7215/api/Boleto/");
});

builder.Services.AddHttpClient<UserService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7215/api/Usuario/");
});

builder.Services.AddHttpClient<WorkerService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7215/api/Trabajador/");
});

builder.Services.AddHttpClient<SeatingService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7215/api/Seating/");
});

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Pasajes}/{action=SearchForPassage}/{id?}");

    // Redirigir la raíz a la página por defecto
    endpoints.MapGet("/", context =>
    {
        context.Response.Redirect("/Pasajes/SearchForPassage");
        return Task.CompletedTask;
    });
});



app.Run();
