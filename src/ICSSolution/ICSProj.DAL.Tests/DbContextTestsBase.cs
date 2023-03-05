using Xunit;
using Xunit.Abstractions;
using ICSProj.Common.Tests.Factories;
using ICSProj.Common.Tests;
using Microsoft.EntityFrameworkCore;

namespace ICSProj.DAL.Tests;

public class DbContextTestsBase: IAsyncLifetime
{
    protected DbContextTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        DbContextFactory = new DbContextSQLiteTestingFactory(GetType().FullName!, seedTestingData: true);

        ICSProjDbContextSUT = DbContextFactory.CreateDbContext();
    }

    protected IDbContextFactory<ICSProjDbContext> DbContextFactory { get; }
    protected ICSProjDbContext ICSProjDbContextSUT { get; }


    public async Task InitializeAsync()
    {
        await ICSProjDbContextSUT.Database.EnsureDeletedAsync();
        await ICSProjDbContextSUT.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await ICSProjDbContextSUT.Database.EnsureDeletedAsync();
        await ICSProjDbContextSUT.DisposeAsync();
    }
}
