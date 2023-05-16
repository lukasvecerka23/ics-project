// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using ICSProj.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICSProj.DAL.Seeds;

public static class TagSeeds
{
    public static readonly TagEntity EmptyTagEntity = new()
    {
        Id = default, CreatorId = default, Name = default!
    };

    public static readonly TagEntity TagEntity1 = new()
    {
        Id = Guid.Parse("b599fe5d-67e0-4848-a61d-f772acabbe59"),
        CreatorId = UserSeeds.UserEntity1.Id,
        Creator = UserSeeds.UserEntity1,
        Color = "#cab7fc",
        Name = "Backend"
    };

    public static readonly TagEntity TagEntity2 = new()
    {
        Id = Guid.Parse("924f20e8-9866-4771-8b45-a47a79a81705"),
        CreatorId = UserSeeds.UserEntity2.Id,
        Creator = UserSeeds.UserEntity2,
        Color = "#f6cc57",
        Name = "Frontend"
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TagEntity>().HasData(
            TagEntity1 with {Creator = null, Activities = Array.Empty<ActivityEntity>()},
            TagEntity2 with {Creator = null, Activities = Array.Empty<ActivityEntity>()}
        );
    }
}
