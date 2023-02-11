using KardesAile.Database;
using Microsoft.EntityFrameworkCore;

namespace KardesAile.AspNetCoreHost.Middlewares;

public static class MigrationsMiddleware
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<KardesAileDbContext>();
        db.Database.Migrate();
    }
}