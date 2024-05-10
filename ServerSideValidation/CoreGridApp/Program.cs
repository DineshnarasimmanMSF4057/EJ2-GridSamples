var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Add SignalR service
builder.Services.AddSignalR();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}"
);

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<GridUpdateHub>("/gridUpdateHub"); // Map SignalR hub endpoint
    endpoints.MapControllers(); // Add this line if you want to use controller endpoints
});

app.Run();
