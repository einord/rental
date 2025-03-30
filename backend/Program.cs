using Biluthyrning.Endpoints;
using Biluthyrning.Services;

namespace Biluthyrning;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Bind appsettings.json to Settings class
        Settings settings = builder.Configuration.GetSection("Settings").Get<Settings>()
            ?? throw new Exception("Could not read settings properly");
        builder.Services.AddSingleton(settings);

        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services
            .AddOpenApi()
            .AddCors(options => {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            })
            .AddTransient<IRentalService, RentalService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        // Use the CORS policy
        app.UseCors("AllowAllOrigins");

        app.UseHttpsRedirection();

        var routeGroupBuilder = app.MapGroup("/api/v1");
        Rental.MapEndpoints(routeGroupBuilder);

        app.Run();
    }
}