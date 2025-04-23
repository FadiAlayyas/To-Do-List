
using TodoApi.Data;

public static class ApplicationBuilderExtensions
{
    public static async Task SeedDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        await SeedData.Initialize(services);
    }
}
