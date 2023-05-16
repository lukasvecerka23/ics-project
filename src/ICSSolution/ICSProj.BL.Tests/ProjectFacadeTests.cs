using ICSProj.BL.Facades;
using ICSProj.BL.Models;
using ICSProj.Common.Tests;
using ICSProj.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;
using Xunit;

namespace ICSProj.BL.Tests;

public sealed class ProjectFacadeTests : FacadeTestsBase
{
    private readonly ProjectFacade _projectFacadeSut;

    public ProjectFacadeTests(ITestOutputHelper output) : base(output)
    {
        _projectFacadeSut = new ProjectFacade(UnitOfWorkFactory, ProjectModelMapper, ProjectAssignModelMapper);
    }

    [Fact]
    public async Task GetAll_ContainSeeded()
    {
        //Act
        var allProjects = await _projectFacadeSut.GetAsync();

        //Assert
        var seedModel = ProjectModelMapper.MapToListModel(ProjectSeeds.ProjectEntity1 with { Creator = UserSeeds.UserEntity1 });
        Assert.Contains(seedModel, allProjects);
    }

    [Fact]
    public async Task Create_NewProject_DoesNotThrow()
    {
        //Arrange
        var model = new ProjectDetailModel()
        {
            Id = Guid.NewGuid(),
            Name = "NewProjectTest",
            CreatorId = UserSeeds.UserEntity1.Id,
            CreatorName = UserSeeds.UserEntity1.Name + " " + UserSeeds.UserEntity1.Surname
        };

        //Act
        var test = await _projectFacadeSut.SaveAsync(model);
    }

    [Fact]
    public async Task Delete_SeededEntity_CheckDeleted()
    {
        //Arrange
        var deletedProjectId = ProjectSeeds.ProjectEntity1.Id;

        //Act
        await _projectFacadeSut.DeleteAsync(deletedProjectId);

        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();

        Assert.False(await dbxAssert.Projects.AnyAsync(i => i.Id == ProjectSeeds.ProjectEntity1.Id));

    }

    [Fact]
    public async Task Delete_SeededEntity_CheckProjectAssigns()
    {
        //Arrange
        var deletedProjectId = ProjectSeeds.ProjectEntity1.Id;

        //Act
        await _projectFacadeSut.DeleteAsync(deletedProjectId);

        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();

        Assert.False(await dbxAssert.Activities.AnyAsync(i => i.Id == ActivitySeeds.ActivityEntityDeletedByProject.Id));
    }

    [Fact]
    public async Task Delete_SeededEntity_CheckActivities()
    {
        //Arrange
        var deletedProjectId = ProjectSeeds.ProjectEntity1.Id;

        //Act
        await _projectFacadeSut.DeleteAsync(deletedProjectId);

        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();

        Assert.False(await dbxAssert.Assigns.AnyAsync(i => i.Id == ProjectAssignSeeds.ProjectAssignEntity1.Id));
    }

    [Fact]
    public async Task GetById_SeededProject()
    {
        //Arrange
        var projectId = ProjectSeeds.ProjectEntity1.Id;

        var detailedProject = await _projectFacadeSut.GetAsync(projectId);

        //Assert
        var seedProject = ProjectModelMapper.MapToDetailModel(ProjectSeeds.ProjectEntity1);

        DeepAssert.Equal(seedProject, detailedProject,"ProjectAssigns","Activities", "CreatorName");
    }
}
