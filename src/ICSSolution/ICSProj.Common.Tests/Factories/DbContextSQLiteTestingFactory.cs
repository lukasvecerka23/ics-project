using ICSProj.DAL;
using Microsoft.EntityFrameworkCore;

namespace ICSProj.Common.Tests.Factories;

public class DbContextSQLiteTestingFactory: IDbContextFactory<ICSProjDbContext>
{
    private readonly string _databaseName;
    private readonly bool _seedTestingData;

    public DbContextSQLiteTestingFactory(string databaseName, bool seedTestingData = false)
    {
        _databaseName = databaseName;
        _seedTestingData = seedTestingData;
    }
    public ICSProjDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<ICSProjDbContext> builder = new();
        builder.UseSqlite($"Data Source={_databaseName};Cache=Shared");

        // contextOptionsBuilder.LogTo(System.Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
        // builder.EnableSensitiveDataLogging();

        return new ICSProjTestingDbContext(builder.Options, _seedTestingData);
    }
}
