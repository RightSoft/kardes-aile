using Microsoft.EntityFrameworkCore;

namespace KardesAile.Database;

public class KardesAileDbContext : DbContext
{
    public const string SchemaName = "kardesaile";
    public const string MigrationHistoryTablename = "migrations_history";
    
    public KardesAileDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(SchemaName);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}