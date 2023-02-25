using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ICSProj.DAL.Factories;

/// <summary>
///     EF Core CLI migration generation uses this DbContext to create model and migration
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ICSProjDbContext>
{
    public ICSProjDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ICSProjDbContext> builder = new();

        builder.UseSqlite($"Data Source=ICSProj;Cache=Shared");

        return new ICSProjDbContext(builder.Options);
    }
}
