using ICSProj.DAL.Entities;
using Xunit;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore;
using ICSProj.Common.Tests;

namespace ICSProj.DAL.Tests;

public class DbContextUserTests: DbContextTestsBase
{
    public DbContextUserTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task AddNew_User()
    {
        UserEntity entity = new()
        {
            Id = default,
            Name = "Pepa",
            Surname = "Novák"
        };

        ICSProjDbContextSUT.Users.Add(entity);
        await ICSProjDbContextSUT.SaveChangesAsync();
        
        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Users
            .SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);
    }
}