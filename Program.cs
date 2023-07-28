using SignalRHubECS;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(builder => 
{
    builder
        .WithOrigins("https://localhost:5088")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
});

app.UseEndpoints(routes =>
{
    routes.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );

    routes.MapHub<MessageHub>("/MessageHub");
    routes.MapHealthChecks("/health");
});

app.Run();
