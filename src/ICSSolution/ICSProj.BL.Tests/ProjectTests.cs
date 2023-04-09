// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using ICSProj.BL.Facades;
using ICSProj.BL.Facades.Interfaces;
using ICSProj.BL.Models;
using ICSProj.Common.Tests;
using ICSProj.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit;
using ICSProj.DAL.Entities;

namespace ICSProj.BL.Tests;


public sealed class ProjectFacadeTests : FacadeTestsBase
{
    private readonly ProjectFacade _projectFacadeSut;

    public ProjectFacadeTests(ITestOutputHelper output) : base(output)
    {
        _projectFacadeSut = new ProjectFacade(UnitOfWorkFactory, ProjectModelMapper);
    }

    [Fact]
    public async Task GetAllProjects()
    {
        //Act
        var allProjects = await _projectFacadeSut.GetAsync();

        //Assert
        var seedModel = ProjectModelMapper.MapToListModel(ProjectSeeds.ProjectEntity1);
        Assert.Contains(seedModel, allProjects);
    }

    [Fact]
    public async Task CreateNewProject()
    {
        //Arrange
        var model = new ProjectDetailModel()
        {
            Id = Guid.NewGuid(),
            Name = "NewProjectTest",
            CreatorId = UserSeeds.UserEntity1.Id,
        };

        //Act
        var test = await _projectFacadeSut.SaveAsync(model);
    }

    [Fact]
    public async Task DeleteProjectCheckDeleted()
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
    public async Task DeleteProjectCheckProjectAssigns()
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
    public async Task DeleteProjectCheckActivities()
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
    public async Task GetProjectDetail()
    {
        //Arrange
        var projectId = ProjectSeeds.ProjectEntity1.Id;

        var detailedProject = await _projectFacadeSut.GetAsync(projectId);

        //Assert
        var seedProject = ProjectModelMapper.MapToDetailModel(ProjectSeeds.ProjectEntity1);

        DeepAssert.Equal(seedProject, detailedProject,"ProjectAssigns","Activities");
    }

}
