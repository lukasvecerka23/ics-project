using System.Runtime.InteropServices.JavaScript;
using ICSProj.DAL.Entities;
using Xunit;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore;
using ICSProj.Common.Tests;
using ICSProj.Common.Tests.Seeds;

namespace ICSProj.DAL.Tests;

public class DbContextProjectAssignTests : DbContextTestsBase
{
    protected DbContextProjectAssignTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task GetAll_ProjectAssigns_ForProject()
    {
        //Act
        var projectAssigns = await ICSProjDbContextSUT.Assigns
            .Where(i => i.ProjectId == ProjectSeeds.ProjectEntity1.Id)
            .ToArrayAsync();

        //Assert
        Assert.Contains(ProjectAssignSeeds.ProjectAssignEntity1 with { Project = null, User = null }, projectAssigns);
        Assert.Contains(ProjectAssignSeeds.ProjectAssignEntity2 with { Project = null, User = null }, projectAssigns);
    }

    [Fact]

    public async Task GetAll_ProjectAssigns_ForUser()
    {
        //Act
        var projectAssigns = await ICSProjDbContextSUT.Assigns
            .Where(i => i.UserId == UserSeeds.UserEntity1.Id)
            .ToArrayAsync();

        //Assert
        Assert.Contains(ProjectAssignSeeds.ProjectAssignEntity1 with { Project = null, User = null }, projectAssigns);
        Assert.Contains(ProjectAssignSeeds.ProjectAssignEntity3 with { Project = null, User = null }, projectAssigns);
    }

    [Fact]
    public async Task GetById_ProjectAssign()
    {
        //Act
        var projectAssign = await ICSProjDbContextSUT.Assigns
            .SingleAsync(i => i.Id == ProjectAssignSeeds.ProjectAssignEntity1.Id);

        //Assert
        DeepAssert.Equal(ProjectAssignSeeds.ProjectAssignEntity1 with { Project = null, User = null }, projectAssign);
    }

    [Fact]
    public async Task Delete_ProjectAssign()
    {
        //Arrange
        var baseEntity = ProjectAssignSeeds.ProjectAssignEntityDelete;

        //Act
        ICSProjDbContextSUT.Assigns.Remove(baseEntity);
        await ICSProjDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await ICSProjDbContextSUT.Assigns.AnyAsync(i => i.Id == baseEntity.Id));
    }

    [Fact]
    public async Task DeleteById_ProjectAssign()
    {
        //Arrange
        var baseEntity = ProjectAssignSeeds.ProjectAssignEntityDelete;

        //Act
        ICSProjDbContextSUT.Assigns.Remove(
            ICSProjDbContextSUT.Assigns.Single(i => i.Id == baseEntity.Id));
        await ICSProjDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await ICSProjDbContextSUT.Assigns.AnyAsync(i => i.Id == baseEntity.Id));
    }

    [Fact]

    public async Task AddNew_ProjectAssign()
    {
        //Arrange
        var entity = ProjectAssignSeeds.EmptyProjectAssignEntity with
        {
            ProjectId = ProjectSeeds.ProjectEntity3.Id,
            UserId = UserSeeds.UserEntity2.Id
        };

        //Act
        ICSProjDbContextSUT.Assigns.Add(entity);
        await ICSProjDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Assigns
            .SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity with { Project = null, User = null }, actualEntity);
    }

}
