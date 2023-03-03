// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using ICSProj.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICSProj.Common.Tests.Seeds;

public static class UserSeeds
{
    public static readonly UserEntity EmptyUserEntity = new()
    {
        Id = default, Name = default!, Surname = default!, ImageUrl = default!
    };

    public static readonly UserEntity UserEntity1 = new()
    {
        Id = Guid.Parse("9c68b64f-4db0-472d-8863-dc69ffef7f88"),
        Name = "Petr",
        Surname = "Novák",
        ImageUrl = null
    };

    public static readonly UserEntity UserEntity2 = new()
    {
        Id = Guid.Parse("305a8808-e7c9-466e-81f7-f74d5b8f0947"),
        Name = "Martin",
        Surname = "Skočdopole",
        ImageUrl = null
    };

    public static readonly UserEntity UserEntityDelete = UserEntity1 with
    {
        Id = Guid.Parse("1ecd4b98-58f1-4b65-8475-5afa3650adf6"),
        ProjectAssigns = Array.Empty<ProjectAssignEntity>(),
        Tags = Array.Empty<TagEntity>()
    };

    public static readonly UserEntity UserEntityUpdate = UserEntity1 with
    {
        Id = Guid.Parse("076255e4-0b40-4621-bdb5-c3247172ab92"),
        ProjectAssigns = Array.Empty<ProjectAssignEntity>(),
        Tags = Array.Empty<TagEntity>()
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            UserEntity1 with {Activities = Array.Empty<ActivityEntity>(), ProjectAssigns = Array.Empty<ProjectAssignEntity>(), Tags = Array.Empty<TagEntity>() },
            UserEntity2 with {Activities = Array.Empty<ActivityEntity>(), ProjectAssigns = Array.Empty<ProjectAssignEntity>(), Tags = Array.Empty<TagEntity>() },
            UserEntityUpdate,
            UserEntityDelete
        );
    }

}
