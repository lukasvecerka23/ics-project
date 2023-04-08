using ICSProj.BL.Facades;
using ICSProj.BL.Models;
using ICSProj.Common.Tests;
using ICSProj.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
        var model = new TagDetailModel()
        {
            Name = "Test tag",
            CreatorId = UserSeeds.UserEntity1.Id
        };

        var test = await _tagFacadeSUT.SaveAsync(model);

    }

    [Fact]
    public async Task GetAll_Single_SeededTag()
    {
        var tags = await _tagFacadeSUT.GetAsync();
        var tag = tags.Single(i => i.Id == TagSeeds.TagEntity1.Id);

        DeepAssert.Equal(TagModelMapper.MapToListModel(TagSeeds.TagEntity1), tag);
    }

    [Fact]
    public async Task GetById_SeededTag()
    {
        var tag = await _tagFacadeSUT.GetAsync(TagSeeds.TagEntity2.Id);

        DeepAssert.Equal(TagModelMapper.MapToDetailModel(TagSeeds.TagEntity2), tag);
    }

    [Fact]
    public async Task GetById_NonExistent()
    {
        var tag = await _tagFacadeSUT.GetAsync(TagSeeds.EmptyTagEntity.Id);

        Assert.Null(tag);
    }

    [Fact]
    public async Task SeededWater_DeleteById_Deleted()
    {
        await _tagFacadeSUT.DeleteAsync(TagSeeds.TagEntityDelete.Id);

        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbxAssert.Tags.AnyAsync(i => i.Id == TagSeeds.TagEntityDelete.Id));
    }

    [Fact]
    public async Task NewTag_InsertOrUpdate_TagAdded()
    {
        var tag = new TagDetailModel()
        {
            Id = Guid.Empty,
            Name = "Hokus pokus",
            CreatorId = UserSeeds.UserEntity1.Id
        };

        tag = await _tagFacadeSUT.SaveAsync(tag);

        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var tagFromDb = await dbxAssert.Tags.SingleAsync(i => i.Id == tag.Id);
        DeepAssert.Equal(tag, TagModelMapper.MapToDetailModel(tagFromDb));
    }

    [Fact]
    public async Task SeededTag_InsertOrUpdate_TagUpdated()
    {
        var tag = new TagDetailModel()
        {
            Id = TagSeeds.TagEntityUpdate.Id,
            Name = TagSeeds.TagEntityUpdate.Name,
            CreatorId = TagSeeds.TagEntityUpdate.CreatorId,
        };
        tag.Name += "updated";

        await _tagFacadeSUT.SaveAsync(tag);

        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var tagFromDb = await dbxAssert.Tags.SingleAsync(i => i.Id == tag.Id);
        DeepAssert.Equal(tag, TagModelMapper.MapToDetailModel(tagFromDb));
    }

    [Fact]
    public async Task GetTagsByUser()
    {
        var tags = _tagFacadeSUT.GetTagsByUser(TagSeeds.TagEntity1.CreatorId);
        Assert.Single(tags);
    }

}
