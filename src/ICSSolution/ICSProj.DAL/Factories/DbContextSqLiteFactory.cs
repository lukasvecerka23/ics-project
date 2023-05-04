using Microsoft.EntityFrameworkCore;

namespace ICSProj.DAL.Factories;
public class DbContextSqLiteFactory: IDbContextFactory<ICSProjDbContext>
{
    private readonly bool _seedTestingData;
    private readonly DbContextOptionsBuilder<ICSProjDbContext> _contextOptionsBuilder = new();

    public DbContextSqLiteFactory(string databaseName, bool seedTestingData = false)
    {
        _seedTestingData = seedTestingData;

        _contextOptionsBuilder.UseSqlite($"Data Source={databaseName};Cache=Shared");
    }

    public ICSProjDbContext CreateDbContext() => new(_contextOptionsBuilder.Options, _seedTestingData);
}
