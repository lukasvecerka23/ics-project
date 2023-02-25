using ICSProj.DAL;
using Microsoft.EntityFrameworkCore;

namespace ICSProj.Common.Tests.Factories;

public class DbContextSQLiteTestingFactory: IDbContextFactory<ICSProjDbContext>
{
    private readonly string _databaseName;

    public DbContextSQLiteTestingFactory(string databaseName)
    {
        _databaseName = databaseName;
    }
    public ICSProjDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<ICSProjDbContext> builder = new();
        builder.UseSqlite($"Data Source={_databaseName};Cache=Shared");
        
        // contextOptionsBuilder.LogTo(System.Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
        // builder.EnableSensitiveDataLogging();
        
        return new ICSProjTestingDbContext(builder.Options);
    }
}