using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ICSProj.DAL.Factories;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ICSProjDbContext>
{
    public ICSProjDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ICSProjDbContext> builder = new();

        builder.UseSqlite($"Data Source=ICSProj;Cache=Shared");

        return new ICSProjDbContext(builder.Options);
    }
}
