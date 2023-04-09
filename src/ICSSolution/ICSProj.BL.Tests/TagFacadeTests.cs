using ICSProj.BL.Facades;
using ICSProj.BL.Models;
using ICSProj.Common.Tests;
using ICSProj.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace ICSProj.BL.Tests;

public sealed class TagFacadeTests : FacadeTestsBase
{
    private readonly TagFacade _tagFacadeSUT;

    public TagFacadeTests(ITestOutputHelper output) : base(output)
    {
        _tagFacadeSUT = new TagFacade(UnitOfWorkFactory, TagModelMapper);
    }

    [Fact]
    public async Task Create_WithNonExistingItem_DoesNotThrow()
    {
        // Arrange
        var model = new TagDetailModel()
        {
            Name = "Test tag",
            CreatorId = UserSeeds.UserEntity1.Id
        };

        // Act
        var test = await _tagFacadeSUT.SaveAsync(model);
    }

    [Fact]
    public async Task GetAll_Single_SeededTag()
    {
        // Act
        var tags = await _tagFacadeSUT.GetAsync();
        var tag = tags.Single(i => i.Id == TagSeeds.TagEntity1.Id);

        // Assert
        DeepAssert.Equal(TagModelMapper.MapToListModel(TagSeeds.TagEntity1), tag);
    }

    [Fact]
    public async Task GetById_SeededTag()
    {
        // Act
        var tag = await _tagFacadeSUT.GetAsync(TagSeeds.TagEntity2.Id);

        // Assert
        DeepAssert.Equal(TagModelMapper.MapToDetailModel(TagSeeds.TagEntity2), tag);
    }

    [Fact]
    public async Task GetById_NonExistent()
    {
        // Act
        var tag = await _tagFacadeSUT.GetAsync(TagSeeds.EmptyTagEntity.Id);

        // Assert
        Assert.Null(tag);
    }

    [Fact]
    public async Task SeededWater_DeleteById_Deleted()
    {
        // Act
        await _tagFacadeSUT.DeleteAsync(TagSeeds.TagEntityDelete.Id);

        // Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbxAssert.Tags.AnyAsync(i => i.Id == TagSeeds.TagEntityDelete.Id));
    }

    [Fact]
    public async Task NewTag_InsertOrUpdate_TagAdded()
    {
        // Arrange
        var tag = new TagDetailModel()
        {
            Id = Guid.Empty,
            Name = "Hokus pokus",
            CreatorId = UserSeeds.UserEntity1.Id
        };

        // Act
        tag = await _tagFacadeSUT.SaveAsync(tag);

        // Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var tagFromDb = await dbxAssert.Tags.SingleAsync(i => i.Id == tag.Id);
        DeepAssert.Equal(tag, TagModelMapper.MapToDetailModel(tagFromDb));
    }

    [Fact]
    public async Task SeededTag_InsertOrUpdate_TagUpdated()
    {
        // Arrange
        var tag = new TagDetailModel()
        {
            Id = TagSeeds.TagEntityUpdate.Id,
            Name = TagSeeds.TagEntityUpdate.Name,
            CreatorId = TagSeeds.TagEntityUpdate.CreatorId,
        };
        tag.Name += "updated";

        // Act
        await _tagFacadeSUT.SaveAsync(tag);

        // Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var tagFromDb = await dbxAssert.Tags.SingleAsync(i => i.Id == tag.Id);
        DeepAssert.Equal(tag, TagModelMapper.MapToDetailModel(tagFromDb));
    }

    [Fact]
    public async Task GetTagsByUser()
    {
        // Act
        var tags = await _tagFacadeSUT.GetTagsByUser(TagSeeds.TagEntity1.CreatorId);

        // Assert
        Assert.Single(tags!);
    }

    [Fact]
    public async Task GetAllSeededTags_ContainsSeededTag()
    {
        // Arrange
        var listModel = TagModelMapper.MapToListModel(TagSeeds.TagEntity2);

        // Act
        var returnedModel = await _tagFacadeSUT.GetTagsByUser(TagSeeds.TagEntity2.CreatorId);

        // Assert
        Assert.Contains(listModel, returnedModel!);
    }

    [Fact]
    public async Task GetTagByUser_NoTag()
    {
        // Act
        var listModel = await _tagFacadeSUT.GetTagsByUser(UserSeeds.UserEntity3.Id);

        // Assert
        Assert.Null(listModel);
    }
}
