using ICSProj.Common.Tests.Seeds;
using ICSProj.DAL;
using Microsoft.EntityFrameworkCore;


namespace ICSProj.Common.Tests;

public class ICSProjTestingDbContext : ICSProjDbContext
{
    private readonly bool _seedTestingData;
    public ICSProjTestingDbContext(DbContextOptions contextOptions, bool seedTestingData = false)
        : base(contextOptions, seedDemoData:false)
    {
        _seedTestingData = seedTestingData;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (_seedTestingData)
        {
            UserSeeds.Seed(modelBuilder);
            ProjectSeeds.Seed(modelBuilder);
            ProjectAssignSeeds.Seed(modelBuilder);
            ActivitySeeds.Seed(modelBuilder);
            TagSeeds.Seed(modelBuilder);
        }
    }
}
