using ICSProj.BL.Facades;
using ICSProj.BL.Models;
using ICSProj.Common.Tests;
using ICSProj.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;
using Xunit;

namespace ICSProj.BL.Tests;

public sealed class UserFacadeTests: FacadeTestsBase
{
    private readonly UserFacade _userFacadeSUT;

    public UserFacadeTests(ITestOutputHelper output) : base(output)
    {
        _userFacadeSUT = new UserFacade(UnitOfWorkFactory, UserModelMapper);
    }

    [Fact]
    public async Task Create_WithNonExistingItem_DoesNotTrow()
    {
        // Arrange
        var model = new UserDetailModel()
        {
            Name = "UserNameTest1",
            Surname = "UserSurnameTest1",
        };

        // Act
        var _ = await _userFacadeSUT.SaveAsync(model);
    }

    [Fact]
    public async Task GetAll_Single_SeededUser1()
    {
        // Act
        var users = await _userFacadeSUT.GetAsync();
        var user = users.Single(i => i.Id == UserSeeds.UserEntity1.Id);

        // Assert
        DeepAssert.Equal(UserModelMapper.MapToListModel(UserSeeds.UserEntity1), user);
    }

    [Fact]
    public async Task GetById_UserEntity1()
    {
        // Act
        var user = await _userFacadeSUT.GetAsync(UserSeeds.UserEntity1.Id);

        // Assert
        DeepAssert.Equal(UserModelMapper.MapToDetailModel(UserSeeds.UserEntity1), user, "ProjectAssigns", "Tags");
    }

    [Fact]
    public async Task GetById_NonExistingUser()
    {
        // Act
        var user = await _userFacadeSUT.GetAsync(UserSeeds.EmptyUserEntity.Id);

        // Assert
        Assert.Null(user);
    }

    [Fact]
    public async Task UserEntity1_DeleteById_Deleted()
    {
        // Act
        await _userFacadeSUT.DeleteAsync(UserSeeds.UserEntity1.Id);

        // Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbxAssert.Users.AnyAsync(i => i.Id == UserSeeds.UserEntity1.Id));
    }

    [Fact]
    public async Task GetAll_FromSeeded_ContainsSeeded()
    {
        // Arrange
        var listModel = UserModelMapper.MapToListModel(UserSeeds.UserEntity2);

        // Act
        var returnedModel = await _userFacadeSUT.GetAsync();

        // Assert
        Assert.Contains(listModel, returnedModel);
    }

    [Fact]
    public async Task Update_Name_FromSeeded_CheckUpdated()
    {
        // Arrange
        var detailModel = UserModelMapper.MapToDetailModel(UserSeeds.UserEntity1);
        detailModel.Name = "New user name";

        // Act
        await _userFacadeSUT.SaveAsync(detailModel);
        var returnedModel = await _userFacadeSUT.GetAsync(detailModel.Id);

        // Assert
        DeepAssert.Equal(detailModel, returnedModel, "ProjectAssigns", "Tags");
    }

    [Fact]
    public async Task SeededUserEntity1_Delete_CheckIfAllRelatedTagsDeleted()
    {
        // Act
        await _userFacadeSUT.DeleteAsync(UserSeeds.UserEntity1.Id);

        // Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbxAssert.Tags.AnyAsync(i => i.CreatorId == UserSeeds.UserEntity1.Id));
    }

    [Fact]
    public async Task SeededUserEntity1_Delete_CheckIfAllRelatedActivitiesDeleted()
    {
        // Act
        await _userFacadeSUT.DeleteAsync(UserSeeds.UserEntity1.Id);

        // Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbxAssert.Activities.AnyAsync(i => i.CreatorId == UserSeeds.UserEntity1.Id));
    }

    [Fact]
    public async Task SeededUserEntity1_Delete_CheckIfAllRelatedProjectAssignsDeleted()
    {
        // Act
        await _userFacadeSUT.DeleteAsync(UserSeeds.UserEntity1.Id);

        // Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbxAssert.Assigns.AnyAsync(i => i.UserId == UserSeeds.UserEntity1.Id));
    }
}
