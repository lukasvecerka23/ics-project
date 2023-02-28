using System.Runtime.InteropServices.JavaScript;
using ICSProj.DAL.Entities;
using Xunit;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore;
using ICSProj.Common.Tests;
using ICSProj.Common.Tests.Seeds;

namespace ICSProj.DAL.Tests;

public class DbContextUserTests: DbContextTestsBase
{
    public DbContextUserTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task AddNew_User()
    {
        var entity = UserSeeds.EmptyUser with
        {
            Name = "Pepa",
            Surname = "Hlavatý"
        };

        ICSProjDbContextSUT.Users.Add(entity);
        await ICSProjDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Users
            .SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task GetById_User()
    {
        // act
        var entity = await ICSProjDbContextSUT.Users
            .SingleAsync(i => i.Id == UserSeeds.UserEntity1.Id);

        // Assert
        DeepAssert.Equal(UserSeeds.UserEntity1 with
        {Activities = Array.Empty<ActivityEntity>(),
            ProjectAssigns = Array.Empty<ProjectAssignEntity>(), Tags = Array.Empty<TagEntity>()}, entity);

    }
}
