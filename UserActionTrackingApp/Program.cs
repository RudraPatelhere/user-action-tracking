var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// ADD SESSION SUPPORT
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5); // SO, THIS WILL SET SESSION TIMEOUT
    options.Cookie.HttpOnly = true; // THIS WILL MAKE COOKIE MORE SECURE
    options.Cookie.IsEssential = true; // THIS WILL ENSURE COOKIE ALWAYS WORKS
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

app.UseSession(); // ENABLE SESSION MIDDLEWARE

app.UseAuthorization();

app.MapControllerRoute(
    name: "DEFAULT_ROUTE", // SO, THIS WILL BE THE DEFAULT ROUTE
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
