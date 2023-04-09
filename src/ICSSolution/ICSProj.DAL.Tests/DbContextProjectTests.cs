using ICSProj.DAL.Entities;
using Xunit;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore;
using ICSProj.Common.Tests;
using ICSProj.Common.Tests.Seeds;

namespace ICSProj.DAL.Tests;

public class DbContextProjectTests : DbContextTestsBase
{
    public DbContextProjectTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task Add_Project()
    {
        var entity = ProjectSeeds.EmptyProjectEntity with
        {
            CreatorId = UserSeeds.UserEntity1.Id,
            Activities = Array.Empty<ActivityEntity>(),
            ProjectAssigns = Array.Empty<ProjectAssignEntity>(),
            Name = ProjectSeeds.ProjectEntity1.Name,

        };

        ICSProjDbContextSUT.Projects.Add(entity);
        await ICSProjDbContextSUT.SaveChangesAsync();


        //Asert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Projects
            .SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);

    }

    [Fact]
    public async Task GetById_Project()
    {
        //Act
        var entity = await ICSProjDbContextSUT.Projects.SingleAsync(i => i.Id == ProjectSeeds.ProjectEntity1.Id);

        //Assert
        DeepAssert.Equal(ProjectSeeds.ProjectEntity1, entity);
    }

    [Fact]
    public async Task Update_Project()
    {
        //Arrange
        var baseEntity = ProjectSeeds.ProjectUpdate;
        var entity = baseEntity with {Name = baseEntity.Name + "Updated"};

        //Act
        ICSProjDbContextSUT.Projects.Update(entity);
        await ICSProjDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Projects.SingleAsync(i => i.Id == entity.Id);

        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task Delete_Project()
    {
        var entityBase = ProjectSeeds.ProjectDelete;


        //Act
        ICSProjDbContextSUT.Projects.Remove(entityBase);
        await ICSProjDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await ICSProjDbContextSUT.Projects.AnyAsync(i => i.Id == entityBase.Id));
    }

    [Fact]
    public async Task DeleteById_Project()
    {
        //Arrange
        var entityBase = ProjectSeeds.ProjectDelete;

        //Act
        ICSProjDbContextSUT.Remove(
            ICSProjDbContextSUT.Projects.Single(i => i.Id == entityBase.Id));
        await ICSProjDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await ICSProjDbContextSUT.Projects.AnyAsync(i => i.Id == entityBase.Id));
    }
}
