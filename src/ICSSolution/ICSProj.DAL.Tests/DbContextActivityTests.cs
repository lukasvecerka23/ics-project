using System.Runtime.InteropServices.JavaScript;
using ICSProj.DAL.Entities;
using Xunit;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore;
using ICSProj.Common.Tests;
using ICSProj.Common.Tests.Seeds;
using System.Globalization;

namespace ICSProj.DAL.Tests;

public class DbContextActivityTests : DbContextTestsBase
{
    public DbContextActivityTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task AddNew_Activity()
    {
        var entity = ActivitySeeds.EmptyActivityEntity with
        {
            CreatorId = UserSeeds.UserEntity1.Id,
            Start = DateTime.Parse("02/27/2023 18:22:16", CultureInfo.InvariantCulture),
            End = DateTime.Parse("02/27/2023 19:20:55", CultureInfo.InvariantCulture),
            ProjectId = ProjectSeeds.ProjectEntity1.Id,
            TagId = TagSeeds.TagEntity2.Id

        };

        ICSProjDbContextSUT.Activities.Add(entity);
        await ICSProjDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Activities
            .SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task GetById_Activity()
    {
        //Act
        var entity = await ICSProjDbContextSUT.Activities.SingleAsync(i => i.Id == ActivitySeeds.ActivityEntity2.Id);

        //Assert
        Assert.Equal(ActivitySeeds.ActivityEntity2, entity);
    }
}
