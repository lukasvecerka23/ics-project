using Xunit;
using Xunit.Abstractions;
using ICSProj.Common.Tests.Factories;
using Microsoft.EntityFrameworkCore;

namespace ICSProj.DAL.Tests;

public class DbContextTestsBase: IAsyncLifetime
{
    protected DbContextTestsBase(ITestOutputHelper output)
    {
        DbContextFactory = new DbContextSQLiteTestingFactory(GetType().FullName!);
        
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