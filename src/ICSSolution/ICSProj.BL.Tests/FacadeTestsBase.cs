using ICSProj.BL.Mappers;
using ICSProj.Common.Tests;
using ICSProj.Common.Tests.Factories;
using Microsoft.EntityFrameworkCore;
using ICSProj.DAL;
using ICSProj.DAL.Mappers;
using ICSProj.DAL.UnitOfWork;
using Xunit;
using Xunit.Abstractions;

namespace ICSProj.BL.Tests;

public class FacadeTestsBase: IAsyncLifetime
{
    protected FacadeTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        DbContextFactory = new DbContextSQLiteTestingFactory(GetType().FullName!, seedTestingData: true);

        UserEntityMapper = new UserEntityMapper();
        TagEntityMapper = new TagEntityMapper();
        ActivityEntityMapper = new ActivityEntityMapper();
        ProjectEntityMapper = new ProjectEntityMapper();
        ProjectAssignEntityMapper = new ProjectAssignEntityMapper();



        ActivityModelMapper = new ActivityModelMapper();
        ProjectAssignModelMapper = new ProjectAssignModelMapper();
        TagModelMapper = new TagModelMapper(ActivityModelMapper);
        ProjectModelMapper = new ProjectModelMapper(ActivityModelMapper, ProjectAssignModelMapper);
        UserModelMapper = new UserModelMapper(ProjectAssignModelMapper);

        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);
    }

    protected IDbContextFactory<ICSProjDbContext> DbContextFactory { get; }

    protected UserEntityMapper UserEntityMapper { get; }
    protected TagEntityMapper TagEntityMapper { get; }
    protected ActivityEntityMapper ActivityEntityMapper { get; }
    protected ProjectEntityMapper ProjectEntityMapper { get; }
    protected ProjectAssignEntityMapper ProjectAssignEntityMapper { get; }

    protected IUserModelMapper UserModelMapper { get; }
    protected ITagModelMapper TagModelMapper { get; }
    protected IActivityModelMapper ActivityModelMapper { get; }
    protected IProjectModelMapper ProjectModelMapper { get; }
    protected IProjectAssignModelMapper ProjectAssignModelMapper { get; }

    protected UnitOfWorkFactory UnitOfWorkFactory { get; }

    public async Task InitializeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureCreatedAsync();
        await dbx.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
    }
}
