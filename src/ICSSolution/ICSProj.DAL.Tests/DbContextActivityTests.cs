using System.Globalization;
using ICSProj.DAL.Entities;
using Xunit;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore;
using ICSProj.Common.Tests;
using ICSProj.Common.Tests.Seeds;

namespace ICSProj.DAL.Tests;

public class DbContextActivityTests: DbContextTestsBase
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
            Start = DateTime.Parse("02/27/2023 19:20:55", CultureInfo.InvariantCulture),
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
    public async Task GetById_User()
    {
        // act
        var entity = await ICSProjDbContextSUT.Users
            .SingleAsync(i => i.Id == UserSeeds.UserEntity1.Id);

        // Assert
        DeepAssert.Equal(UserSeeds.UserEntity1 with
        {Activities = Array.Empty<ActivityEntity>(),
            ProjectAssigns = Array.Empty<ProjectAssignEntity>(), Tags = Array.Empty<TagEntity>()}, entity);

    }
}
