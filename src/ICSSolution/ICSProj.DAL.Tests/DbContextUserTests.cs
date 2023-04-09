using ICSProj.DAL.Entities;
using Xunit;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore;
using ICSProj.Common.Tests;
using ICSProj.Common.Tests.Seeds;

namespace ICSProj.DAL.Tests;

public class DbContextUserTests : DbContextTestsBase
{
    public DbContextUserTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task AddNew_User()
    {
        // Arrange
        var entity = UserSeeds.EmptyUserEntity with { Name = "Pepa", Surname = "Hlavatý" };

        // Act
        ICSProjDbContextSUT.Users.Add(entity);
        await ICSProjDbContextSUT.SaveChangesAsync();

        // Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Users
            .SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task GetById_User()
    {
        // Act
        var entity = await ICSProjDbContextSUT.Users
            .SingleAsync(i => i.Id == UserSeeds.UserEntity1.Id);

        var actual = UserSeeds.UserEntity1 with
        {
            Activities = Array.Empty<ActivityEntity>(),
            ProjectAssigns = Array.Empty<ProjectAssignEntity>(),
            Tags = Array.Empty<TagEntity>()
        };

        // Assert
        DeepAssert.Equal(actual ,entity);

    }

    [Fact]
    public async Task GetById_IncludingActivities_User()
    {
        // Arrange
        var entity = UserSeeds.UserEntity1 with {Activities = new List<ActivityEntity>(), ProjectAssigns = new List<ProjectAssignEntity>(), Tags = new List<TagEntity>()};
        entity.Activities.Add(ActivitySeeds.ActivityEntity1 with {Creator = entity});

        // Act
        var actual = await ICSProjDbContextSUT.Users.Include(i => i.Activities)
            .SingleAsync(i => i.Id == entity.Id);

        // Assert
        DeepAssert.Equal(entity, actual);
    }

    [Fact]
    public async Task GetById_IncludingProjectAssign_User()
    {
        // Arrange
        var project = ProjectSeeds.ProjectEntity1 with {Creator = UserSeeds.UserEntity1};
        project.ProjectAssigns.Add(ProjectAssignSeeds.ProjectAssignEntity1 with {User = UserSeeds.UserEntity1, Project = project});
        var project2 = ProjectSeeds.ProjectEntity2 with {Creator = UserSeeds.UserEntity2};
        project2.ProjectAssigns.Add(ProjectAssignSeeds.ProjectAssignEntity3 with {User = UserSeeds.UserEntity1, Project = project2});

        var entity = UserSeeds.UserEntity1;
        entity.ProjectAssigns.Add(ProjectAssignSeeds.ProjectAssignEntity1 with {User = entity, Project = project});
        entity.ProjectAssigns.Add(ProjectAssignSeeds.ProjectAssignEntity3 with {User = entity, Project = project2});

        // Act
        var actual = await ICSProjDbContextSUT.Users.Include(i => i.ProjectAssigns)
            .ThenInclude(i => i.Project).ThenInclude(i => i!.Creator).SingleAsync(i => i.Id == entity.Id);

        // Assert
        DeepAssert.Equal(entity, actual);
    }

    [Fact]
    public async Task GetById_IncludingTag_User()
    {
        // Arrange
        var entity = UserSeeds.UserEntity1 with {Activities = new List<ActivityEntity>(), ProjectAssigns = new List<ProjectAssignEntity>(), Tags = new List<TagEntity>()};
        entity.Tags.Add(TagSeeds.TagEntity1 with {Creator = entity});

        // Act
        var actual = await ICSProjDbContextSUT.Users.Include(i => i.Tags)
            .SingleAsync(i => i.Id == entity.Id);

        // Assert
        DeepAssert.Equal(entity, actual);
    }

    [Fact]
    public async Task Update_User()
    {
        // Arrange
        var baseEntity = UserSeeds.UserEntityUpdate;
        var entity = baseEntity with { Name = baseEntity.Name + "Updated", Surname = baseEntity.Surname + "Updated" };

        // Act
        ICSProjDbContextSUT.Users.Update(entity);
        await ICSProjDbContextSUT.SaveChangesAsync();

        // Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Users.SingleAsync(i => i.Id == entity.Id);

        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task Delete_User()
    {
        // Arrange
        var entity = UserSeeds.UserEntityDelete;

        // Act
        ICSProjDbContextSUT.Users.Remove(entity);
        await ICSProjDbContextSUT.SaveChangesAsync();

        // Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Users.AnyAsync(i => i.Id == entity.Id);

        Assert.False(actualEntity);
    }

    [Fact]
    public async Task DeleteById_User()
    {
        // Arrange
        var entity = UserSeeds.UserEntityDelete;

        // Act
        ICSProjDbContextSUT.Users.Remove(ICSProjDbContextSUT.Users.Single(i => i.Id == entity.Id));
        await ICSProjDbContextSUT.SaveChangesAsync();

        // Assert
        Assert.False(await ICSProjDbContextSUT.Users.AnyAsync(i => i.Id == entity.Id));
    }
}
