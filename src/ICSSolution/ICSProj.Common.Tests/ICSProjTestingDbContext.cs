using ICSProj.DAL;
using Microsoft.EntityFrameworkCore;


namespace ICSProj.Common.Tests;

public class ICSProjTestingDbContext : ICSProjDbContext
{

    public ICSProjTestingDbContext(DbContextOptions contextOptions, bool seedTestingData = false)
        : base(contextOptions, seedDemoData:false)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}