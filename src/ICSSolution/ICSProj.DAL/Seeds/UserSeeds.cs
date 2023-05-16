// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using ICSProj.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICSProj.DAL.Seeds;

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
        ImageUrl = "https://www.sablonynazed.cz/576-large_default/medvidek-pu-01.jpg"
    };

    public static readonly UserEntity UserEntity2 = new()
    {
        Id = Guid.Parse("305a8808-e7c9-466e-81f7-f74d5b8f0947"),
        Name = "Martin",
        Surname = "Skočdopole",
        ImageUrl = null
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            UserEntity1 with {Activities = Array.Empty<ActivityEntity>(), ProjectAssigns = Array.Empty<ProjectAssignEntity>(), Tags = Array.Empty<TagEntity>() },
            UserEntity2 with {Activities = Array.Empty<ActivityEntity>(), ProjectAssigns = Array.Empty<ProjectAssignEntity>(), Tags = Array.Empty<TagEntity>() }
        );
    }

}
