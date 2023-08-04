using SignalRHubECS;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddHostedService<MessageIngestionService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

var origin = Environment.GetEnvironmentVariable("ORIGIN") ?? "https://localhost:3000";
app.UseCors(builder => 
{
    builder
        .WithOrigins(origin)
        // .SetIsOriginAllowed((host) => true)
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
