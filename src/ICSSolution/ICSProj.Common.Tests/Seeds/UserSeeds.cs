// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using ICSProj.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICSProj.Common.Tests.Seeds;

public static class UserSeeds
{
    public static readonly UserEntity EmptyUser = new()
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

    public static readonly UserEntity UserEntityWithNoActivitiesProjectAssignsTags = UserEntity1 with {Id = Guid.Parse("0e6dca36-3874-40d5-a44d-252a1fad1b85"), Activities = Array.Empty<ActivityEntity>(), ProjectAssigns = Array.Empty<ProjectAssignEntity>(), Tags = Array.Empty<TagEntity>()};

    static UserSeeds()
    {
        UserEntity1.ProjectAssigns.Add(ProjectAssignSeeds.ProjectAssignEntity1);
        UserEntity2.ProjectAssigns.Add(ProjectAssignSeeds.ProjectAssignEntity2);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            UserEntity1 with {Activities = Array.Empty<ActivityEntity>(), ProjectAssigns = Array.Empty<ProjectAssignEntity>(), Tags = Array.Empty<TagEntity>() },
            UserEntity2 with {Activities = Array.Empty<ActivityEntity>(), ProjectAssigns = Array.Empty<ProjectAssignEntity>(), Tags = Array.Empty<TagEntity>() },
            UserEntityWithNoActivitiesProjectAssignsTags
        );
    }

}
