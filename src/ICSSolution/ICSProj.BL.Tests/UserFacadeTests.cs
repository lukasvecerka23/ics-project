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
        var model = new UserDetailModel()
        {
            Name = "UserNameTest1",
            Surname = "UserSurnameTest1",
        };

        var _ = await _userFacadeSUT.SaveAsync(model);
    }

    [Fact]
    public async Task GetAll_Single_SeededUser1()
    {
        var users = await _userFacadeSUT.GetAsync();
        var user = users.Single(i => i.Id == UserSeeds.UserEntity1.Id);

        DeepAssert.Equal(UserModelMapper.MapToListModel(UserSeeds.UserEntity1), user);
    }

    [Fact]
    public async Task GetById_UserEntity1()
    {
        var user = await _userFacadeSUT.GetAsync(UserSeeds.UserEntity1.Id);

        DeepAssert.Equal(UserModelMapper.MapToDetailModel(UserSeeds.UserEntity1), user);
    }

    [Fact]
    public async Task GetById_NonExistingUser()
    {
        var user = await _userFacadeSUT.GetAsync(UserSeeds.EmptyUserEntity.Id);

        Assert.Null(user);
    }

    [Fact]
    public async Task UserEntity1_DeleteById_Deleted()
    {
        await _userFacadeSUT.DeleteAsync(UserSeeds.UserEntity1.Id);

        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();

        Assert.False(await dbxAssert.Users.AnyAsync(i => i.Id == UserSeeds.UserEntity1.Id));
    }

    [Fact]
    public async Task GetAll_FromSeeded_ContainsSeeded()
    {
        var listModel = UserModelMapper.MapToListModel(UserSeeds.UserEntity2);

        var returnedModel = await _userFacadeSUT.GetAsync();

        Assert.Contains(listModel, returnedModel);
    }

    [Fact]
    public async Task Update_Name_FromSeeded_CheckUpdated()
    {
        var detailModel = UserModelMapper.MapToDetailModel(UserSeeds.UserEntity1);
        detailModel.Name = "New user name";

        await _userFacadeSUT.SaveAsync(detailModel);

        var returnedModel = await _userFacadeSUT.GetAsync(detailModel.Id);

        DeepAssert.Equal(detailModel, returnedModel);
    }

    [Fact]
    public async Task SeededUserEntity1_Delete_CheckIfAllRelatedTagsDeleted()
    {
        await _userFacadeSUT.DeleteAsync(UserSeeds.UserEntity1.Id);

        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();

        Assert.False(await dbxAssert.Tags.AnyAsync(i => i.CreatorId == UserSeeds.UserEntity1.Id));
    }

    [Fact]
    public async Task SeededUserEntity1_Delete_CheckIfAllRelatedActivitiesDeleted()
    {
        await _userFacadeSUT.DeleteAsync(UserSeeds.UserEntity1.Id);

        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();

        Assert.False(await dbxAssert.Activities.AnyAsync(i => i.CreatorId == UserSeeds.UserEntity1.Id));
    }

    [Fact]
    public async Task SeededUserEntity1_Delete_CheckIfAllRelatedProjectAssignsDeleted()
    {
        await _userFacadeSUT.DeleteAsync(UserSeeds.UserEntity1.Id);

        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();

        Assert.False(await dbxAssert.Assigns.AnyAsync(i => i.UserId == UserSeeds.UserEntity1.Id));
    }
}
