using System.Runtime.InteropServices.JavaScript;
using Azure.Core;
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
        var entity = UserSeeds.EmptyUserEntity with { Name = "Pepa", Surname = "Hlavatý" };

        ICSProjDbContextSUT.Users.Add(entity);
        await ICSProjDbContextSUT.SaveChangesAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Users
            .SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task GetById_User()
    {
        var entity = await ICSProjDbContextSUT.Users
            .SingleAsync(i => i.Id == UserSeeds.UserEntity1.Id);

        var actual = UserSeeds.UserEntity1 with
        {
            Activities = Array.Empty<ActivityEntity>(),
            ProjectAssigns = Array.Empty<ProjectAssignEntity>(),
            Tags = Array.Empty<TagEntity>()
        };

        DeepAssert.Equal(actual ,entity);

    }

    [Fact]
    public async Task GetById_IncludingActivities_User()
    {

        var entity = UserSeeds.UserEntity1 with {Activities = new List<ActivityEntity>(), ProjectAssigns = new List<ProjectAssignEntity>(), Tags = new List<TagEntity>()};
        entity.Activities.Add(ActivitySeeds.ActivityEntity1 with {Creator = entity});

        var actual = await ICSProjDbContextSUT.Users.Include(i => i.Activities)
            .SingleAsync(i => i.Id == entity.Id);

        DeepAssert.Equal(entity, actual);
    }

    [Fact]
    public async Task GetById_IncludingProjectAssign_User()
    {
        var project = ProjectSeeds.ProjectEntity1 with {Creator = UserSeeds.UserEntity1};
        project.ProjectAssigns.Add(ProjectAssignSeeds.ProjectAssignEntity1 with {User = UserSeeds.UserEntity1, Project = project});

        var entity = UserSeeds.UserEntity1;
        entity.ProjectAssigns.Add(ProjectAssignSeeds.ProjectAssignEntity1 with {User = entity, Project = project});



        var actual = await ICSProjDbContextSUT.Users.Include(i => i.ProjectAssigns)
            .ThenInclude(i => i.Project).SingleAsync(i => i.Id == entity.Id);

        DeepAssert.Equal(entity, actual);
    }

    [Fact]
    public async Task GetById_IncludingTag_User()
    {
        var entity = UserSeeds.UserEntity1 with {Activities = new List<ActivityEntity>(), ProjectAssigns = new List<ProjectAssignEntity>(), Tags = new List<TagEntity>()};
        entity.Tags.Add(TagSeeds.TagEntity1 with {Creator = entity});

        var actual = await ICSProjDbContextSUT.Users.Include(i => i.Tags)
            .SingleAsync(i => i.Id == entity.Id);

        DeepAssert.Equal(entity, actual);
    }

    [Fact]
    public async Task Update_User()
    {
        var baseEntity = UserSeeds.UserEntityUpdate;
        var entity = baseEntity with { Name = baseEntity.Name + "Updated", Surname = baseEntity.Surname + "Updated" };

        ICSProjDbContextSUT.Users.Update(entity);
        await ICSProjDbContextSUT.SaveChangesAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Users.SingleAsync(i => i.Id == entity.Id);

        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task Delete_User()
    {
        var entity = UserSeeds.UserEntityDelete;

        ICSProjDbContextSUT.Users.Remove(entity);
        await ICSProjDbContextSUT.SaveChangesAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Users.AnyAsync(i => i.Id == entity.Id);

        Assert.False(actualEntity);
    }

    [Fact]
    public async Task DeleteById_User()
    {
        var entity = UserSeeds.UserEntityDelete;

        ICSProjDbContextSUT.Users.Remove(ICSProjDbContextSUT.Users.Single(i => i.Id == entity.Id));
        await ICSProjDbContextSUT.SaveChangesAsync();

        Assert.False(await ICSProjDbContextSUT.Users.AnyAsync(i => i.Id == entity.Id));
    }
}
