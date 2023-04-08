using ICSProj.BL.Facades;
using ICSProj.BL.Models;
using ICSProj.Common.Tests;
using ICSProj.Common.Tests.Seeds;
using ICSProj.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ICSProj.BL.Tests;

public sealed class ActivityFacadeTests : FacadeTestsBase
{
    private readonly ActivityFacade _activityFacadeSUT;

    public ActivityFacadeTests(ITestOutputHelper output) : base(output)
    {
        _activityFacadeSUT = new ActivityFacade(UnitOfWorkFactory, ActivityModelMapper);
    }

    [Fact]
    public async Task Create_WithNonExistingItem_DoesNotThrow()
    {
        var model = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Start = new DateTime(2023, 4, 8, 22, 0, 0),
            End = new DateTime(2023, 4, 8, 23, 0, 0),
            CreatorId = UserSeeds.UserEntity1.Id
        };

        var _ = await _activityFacadeSUT.SaveAsync(model);
    }

    [Fact]
    public async Task GetAll_Single_ActivityEntity1()
    {
        var activities = await _activityFacadeSUT.GetAsync();
        var activity = activities.Single(i => i.Id == ActivitySeeds.ActivityEntity1.Id);

        DeepAssert.Equal(ActivityModelMapper.MapToListModel(ActivitySeeds.ActivityEntity1), activity);
    }

    [Fact]
    public async Task GetById_ActivityEntity1()
    {
        var activity = await _activityFacadeSUT.GetAsync(ActivitySeeds.ActivityEntity1.Id);

        DeepAssert.Equal(ActivityModelMapper.MapToDetailModel(ActivitySeeds.ActivityEntity1), activity);
    }

    [Fact]
    public async Task GetById_NonExistent()
    {
        var activity = await _activityFacadeSUT.GetAsync(ActivitySeeds.EmptyActivityEntity.Id);

        Assert.Null(activity);
    }

    [Fact]
    public async Task ActivityEntity1_DeleteById_Deleted()
    {
        await _activityFacadeSUT.DeleteAsync(ActivitySeeds.ActivityEntity1.Id);

        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbxAssert.Activities.AnyAsync(i => i.Id == ActivitySeeds.ActivityEntity1.Id));
    }

    [Fact]
    public async Task NewActivity_InsertOrUpdate_ActivityAdded()
    {
        //Arrange
        var activity = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Start = new DateTime(2023, 3, 11, 8, 0, 0),
            End = new DateTime(2023, 3, 11, 11, 0, 0),
            CreatorId = UserSeeds.UserEntity2.Id
        };

        //Act
        activity = await _activityFacadeSUT.SaveAsync(activity);

        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var activityFromDb = await dbxAssert.Activities.SingleAsync(i => i.Id == activity.Id);
        DeepAssert.Equal(activity, ActivityModelMapper.MapToDetailModel(activityFromDb));
    }

    [Fact]
    public async Task Activity_InsertOrUpdate_ActivityUpdated()
    {
        //Arrange
        var activity = new ActivityDetailModel()
        {
            Id = ActivitySeeds.ActivityEntity1.Id,
            Start = new DateTime(2023, 3, 11, 8, 0, 0),
            End = new DateTime(2023, 3, 11, 11, 0, 0),
            CreatorId = UserSeeds.UserEntity2.Id,
            ProjectId = ProjectSeeds.ProjectEntity2.Id
        };
        activity.Start = new DateTime(2023, 3, 11, 7, 0, 0);
        

        //Act
        await _activityFacadeSUT.SaveAsync(activity);

        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var activityFromDb = await dbxAssert.Activities.SingleAsync(i => i.Id == activity.Id);
        DeepAssert.Equal(activity, ActivityModelMapper.MapToDetailModel(activityFromDb));
    }
}
