using ICSProj.BL.Facades;
using ICSProj.BL.Models;
using ICSProj.Common.Tests;
using ICSProj.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;
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
        // Arrange
        var model = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Start = new DateTime(2023, 4, 8, 22, 0, 0),
            End = new DateTime(2023, 4, 8, 23, 0, 0),
            CreatorId = UserSeeds.UserEntity1.Id
        };

        // Act
        var _ = await _activityFacadeSUT.SaveAsync(model);
    }

    [Fact]
    public async Task GetAll_Single_ActivityEntity1()
    {
        //Act
        var activities = await _activityFacadeSUT.GetAsync();
        var activity = activities.Single(i => i.Id == ActivitySeeds.ActivityEntity1.Id);

        // Assert
        DeepAssert.Equal(ActivityModelMapper.MapToListModel(ActivitySeeds.ActivityEntity1), activity, "ProjectName", "CreatorName");
    }

    [Fact]
    public async Task GetById_ActivityEntity1()
    {
        // Act
        var activity = await _activityFacadeSUT.GetAsync(ActivitySeeds.ActivityEntity1.Id);

        // Assert
        DeepAssert.Equal(ActivityModelMapper.MapToDetailModel(ActivitySeeds.ActivityEntity1), activity, "ProjectName", "CreatorName");
    }

    [Fact]
    public async Task GetById_NonExistent()
    {
        // Act
        var activity = await _activityFacadeSUT.GetAsync(ActivitySeeds.EmptyActivityEntity.Id);

        // Assert
        Assert.Null(activity);
    }

    [Fact]
    public async Task ActivityEntity1_DeleteById_Deleted()
    {
        // Act
        await _activityFacadeSUT.DeleteAsync(ActivitySeeds.ActivityEntity1.Id);

        // Assert
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
        DeepAssert.Equal(activity, ActivityModelMapper.MapToDetailModel(activityFromDb), "CreatorName");
    }

    [Fact]
    public async Task CheckForConflictingActivity_True()
    {
        // Arrange
        var activity = new ActivityDetailModel()
        {
            Description = "Activity 1",
            Start = new DateTime(2023, 2, 10, 10, 20, 0),
            End = new DateTime(2023, 2, 10, 11, 0, 0),
            CreatorId = UserSeeds.UserEntity1.Id
        };

        // Assert

        await Assert.ThrowsAsync<Exception>(async () =>
        await _activityFacadeSUT.SaveAsync(UserSeeds.UserEntity1.Id, activity));
    }

    [Fact]
    public async Task CheckForConflictingActivity_False()
    {
        // Arrange
        var activity = new ActivityDetailModel()
        {
            Description = "Activity 1",
            Start = new DateTime(2023, 2, 10, 9, 0, 0),
            End = new DateTime(2023, 2, 10, 9, 50, 0),
            CreatorId = UserSeeds.UserEntity1.Id
        };

        // Act
        var conflictingActivity = await _activityFacadeSUT.SaveAsync(UserSeeds.UserEntity1.Id, activity);

        // Assert
        DeepAssert.Equals(conflictingActivity, activity);
    }

    [Fact]
    public async Task FilterByUserIdDateFromDateTo_ActivityEntity2()
    {
        // Arrange
        DateTime from = new DateTime(2023, 2, 12, 8, 0, 0);
        DateTime to = new DateTime(2023, 2, 12, 23, 0, 0);

        // Act
        var activities = await _activityFacadeSUT.FilterActivities(UserSeeds.UserEntity2.Id, from, to, null, null);

        // Assert
        Assert.Contains(ActivityModelMapper.MapToListModel(ActivitySeeds.ActivityEntity2 with {Creator = UserSeeds.UserEntity2}), activities);
    }

    [Fact]
    public async Task FilterByUserIdDateFromDateToProjectId_ActivityEntity1()
    {
        // Arrange
        DateTime from = new DateTime(2023, 2, 10, 8, 0, 0);
        DateTime to = new DateTime(2023, 2, 10, 23, 0, 0);

        // Act
        var activities = await _activityFacadeSUT.FilterActivities(UserSeeds.UserEntity1.Id, from, to, ProjectSeeds.ProjectEntity1.Id, null);

        var testActivity = ActivityModelMapper.MapToListModel(
            ActivitySeeds.ActivityEntity1 with
            {
                Project = ProjectSeeds.ProjectEntity1,
                Creator = UserSeeds.UserEntity1
            });
        // Assert
        Assert.Contains(testActivity, activities);
    }

    [Fact]
    public async Task FilterByUserIdDateFromDateToTagId_ActivityEntity2()
    {
        // Arrange
        DateTime from = new DateTime(2023, 2, 12, 8, 0, 0);
        DateTime to = new DateTime(2023, 2, 12, 23, 0, 0);

        // Act
        var activities = await _activityFacadeSUT.FilterActivities(UserSeeds.UserEntity2.Id, from, to, null, TagSeeds.TagEntity2.Id);

        var testActivity = ActivityModelMapper.MapToListModel(
            ActivitySeeds.ActivityEntity2 with
            {
                Creator = UserSeeds.UserEntity2
            });
        // Assert
        Assert.Contains(testActivity, activities);
    }
}
