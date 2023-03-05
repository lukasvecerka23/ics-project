using System.Runtime.InteropServices.JavaScript;
using ICSProj.DAL.Entities;
using Xunit;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore;
using ICSProj.Common.Tests;
using ICSProj.Common.Tests.Seeds;

namespace ICSProj.DAL.Tests;

public class DbContextTagTests : DbContextTestsBase
{
    public DbContextTagTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task AddNew_Tag()
    {
        var entity = TagSeeds.EmptyTagEntity with
        {
            Name = "Sport",
            CreatorId = UserSeeds.UserEntity1.Id
        };

        ICSProjDbContextSUT.Tags.Add(entity);
        await ICSProjDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Tags
            .SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);
    }
    
    [Fact]
    public async Task GetById_Tag()
    {
        // Act
        var entity = await ICSProjDbContextSUT.Tags
            .SingleAsync(i => i.Id == TagSeeds.TagEntity2.Id);

        // Assert
        DeepAssert.Equal(TagSeeds.TagEntity2 with
        {
            Activities = Array.Empty<ActivityEntity>()
        }, entity);
    }

    [Fact]
    public async Task AddNew_TagWithActivities()
    {
        var entity = TagSeeds.EmptyTagEntity with
        {
            Name = "Aktivita",
            CreatorId = UserSeeds.UserEntity2.Id,
            Activities = new List<ActivityEntity>
            {
                ActivitySeeds.EmptyActivityEntity with
                {
                    Id = Guid.Parse("d524977c-a212-4289-bb2f-431b6e9c5725"),
                    CreatorId = UserSeeds.UserEntity1.Id,
                    Description = "First activity",
                    Start = new DateTime(2023, 2, 10, 12, 0, 0),
                    End = new DateTime(2023, 2, 10, 13, 0, 0),
                    ProjectId = default,
                    TagId = default
                },
                ActivitySeeds.EmptyActivityEntity with
                {
                    Id = Guid.Parse("00b23592-cdf5-4c0f-ba87-0da92152e5c5"),
                    CreatorId = UserSeeds.UserEntity2.Id,
                    Description = "Second activity",
                    Start = new DateTime(2023, 2, 10, 10, 0, 0),
                    End = new DateTime(2023, 2, 10, 11, 0, 0),
                    ProjectId = default,
                    TagId = default
                }
            }
        };

        ICSProjDbContextSUT.Tags.Add(entity);
        await ICSProjDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Tags
            .Include(i => i.Activities)
            .SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]

    public async Task GetById_IncludingActivities_Tag()
    {
        var entity = await ICSProjDbContextSUT.Tags
            .Include(i => i.Activities)
            .SingleAsync(i => i.Id == TagSeeds.TagEntity1.Id);

        //Assert
        DeepAssert.Equal(TagSeeds.TagEntity1, entity);
    }

    [Fact]
    public async Task Update_Tag()
    {
        var baseEntity = TagSeeds.TagEntityUpdate;
        var entity = baseEntity with { Name = "Updated name" };

        ICSProjDbContextSUT.Tags.Update(entity);
        await ICSProjDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Tags
            .SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task Delete_Tag()
    {
        //Arrange
        var baseEntity = TagSeeds.TagEntityDelete;

        //Act
        ICSProjDbContextSUT.Tags.Remove(baseEntity);
        await ICSProjDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await ICSProjDbContextSUT.Tags.AnyAsync(i => i.Id == baseEntity.Id));
    }

    [Fact]
    public async Task DeleteById_Tag()
    {
        //Arrange
        var baseEntity = TagSeeds.TagEntityDelete;

        //Act
        ICSProjDbContextSUT.Remove(
            ICSProjDbContextSUT.Tags.Single(i => i.Id == baseEntity.Id));
        await ICSProjDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await ICSProjDbContextSUT.Tags.AnyAsync(i => i.Id == baseEntity.Id));
    }
}
