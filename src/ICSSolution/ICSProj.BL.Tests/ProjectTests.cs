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
    private readonly ProjectFacade _projectFacadeSUT;

    public ProjectFacadeTests(ITestOutputHelper output) : base(output)
    {
        _projectFacadeSUT = new ProjectFacade(UnitOfWorkFactory, ProjectModelMapper);
    }

    [Fact]
    public async Task GetAllProjects()
    {
        //Act
        var allprojects = await _projectFacadeSUT.GetAsync();

        //Assert
        var SeedModel = ProjectModelMapper.MapToListModel(ProjectSeeds.ProjectEntity1);
        Assert.Contains(SeedModel, allprojects);
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
        var test = await _projectFacadeSUT.SaveAsync(model);
    }

    [Fact]
    public async Task DeleteProjectCheckDeleted()
    {
        //Arrange
        var DeletedProjectId = ProjectSeeds.ProjectEntity1.Id;

        //Act
        await _projectFacadeSUT.DeleteAsync(DeletedProjectId);

        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();

        Assert.False(await dbxAssert.Projects.AnyAsync(i => i.Id == ProjectSeeds.ProjectEntity1.Id));

    }

    [Fact]
    public async Task DeleteProjectCheckProjectAssigns()
    {
        //Arrange
        var DeletedProjectId = ProjectSeeds.ProjectEntity1.Id;

        //Act
        await _projectFacadeSUT.DeleteAsync(DeletedProjectId);

        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();

        Assert.False(await dbxAssert.Activities.AnyAsync(i => i.Id == ActivitySeeds.ActivityEntityDeletedByProject.Id));
    }

    [Fact]
    public async Task DeleteProjectCheckActivities()
    {
        //Arrange
        var DeletedProjectId = ProjectSeeds.ProjectEntity1.Id;

        //Act
        await _projectFacadeSUT.DeleteAsync(DeletedProjectId);

        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();

        Assert.False(await dbxAssert.Assigns.AnyAsync(i => i.Id == ProjectAssignSeeds.ProjectAssignEntity1.Id));
    }

    [Fact]
    public async Task GetProjectDetail()
    {
        //Arrange
        var projectID = ProjectSeeds.ProjectEntity1.Id;

        var detailedProject = await _projectFacadeSUT.GetAsync(projectID);

        //Assert
        var SeedModel = ProjectModelMapper.MapToListModel(ProjectSeeds.ProjectEntity1);
        //Assert.Contains(SeedModel, allprojects);

        //Act

        //Assert
    }

}
