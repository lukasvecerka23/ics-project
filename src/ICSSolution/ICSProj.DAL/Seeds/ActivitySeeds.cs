// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Security.Principal;
using ICSProj.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICSProj.DAL.Seeds;

public static class ActivitySeeds
{
    public static readonly ActivityEntity EmptyActivityEntity = new()
    {
        Id = default, CreatorId = default, Description = default,
        Start = default, End = default, ProjectId = default, TagId = default
    };

    public static readonly ActivityEntity ActivityEntity1 = new()
    {
        Id = Guid.Parse("42390eec-637b-4cf0-a33d-380a418b7915"),
        CreatorId = UserSeeds.UserEntity1.Id,
        Creator = UserSeeds.UserEntity1,
        Description = "Working on important thing...",
        Start = new DateTime(2023, 2, 10, 10,0,0),
        End = new DateTime(2023, 2,10,11,0,0),
        ProjectId = ProjectSeeds.ProjectEntity1.Id,
        Project = ProjectSeeds.ProjectEntity1,
        TagId = default,
        Tag = default
    };

    public static readonly ActivityEntity ActivityEntity2 = new()
    {
        Id = Guid.Parse("79168d67-711c-48f2-bbec-a89cf4b831ae"),
        CreatorId = UserSeeds.UserEntity2.Id,
        Creator = UserSeeds.UserEntity2,
        Description = "Delani na projektu ICS",
        Start = new DateTime(2023, 2, 12, 20,0,0),
        End = new DateTime(2023, 2,12,22,0,0),
        ProjectId = default,
        Project = default,
        TagId = TagSeeds.TagEntity2.Id,
        Tag = TagSeeds.TagEntity2
    };

    public static readonly ActivityEntity ActivityEntity3 = new()
    {
        Id = Guid.Parse("5eac6bd3-d915-4d2f-a8a1-9cf872b586c5"),
        CreatorId = UserSeeds.UserEntity2.Id,
        Creator = UserSeeds.UserEntity2,
        Description = "Uceni na zkousky :void:",
        Start = new DateTime(2023, 4, 12, 20,0,0),
        End = new DateTime(2023, 4,12,22,0,0),
        ProjectId = default,
        Project = default,
        TagId = TagSeeds.TagEntity2.Id,
        Tag = TagSeeds.TagEntity2
    };

    public static readonly ActivityEntity ActivityEntity4 = new()
    {
        Id = Guid.Parse("1eda4cdb-c52c-422c-ad6c-c374169ef7bb"),
        CreatorId = UserSeeds.UserEntity1.Id,
        Creator = UserSeeds.UserEntity1,
        Description = "Cetba knihy",
        Start = new DateTime(2023, 4, 10, 20,0,0),
        End = new DateTime(2023, 4,10,22,0,0),
        ProjectId = default,
        Project = default,
        TagId = default,
        Tag = default
    };



    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityEntity>().HasData(
            ActivityEntity1 with {Creator = null, Project = null, Tag = null},
            ActivityEntity2 with {Creator = null, Project = null, Tag = null},
            ActivityEntity3 with {Creator = null, Project = null, Tag = null},
            ActivityEntity4 with {Creator = null, Project = null, Tag = null}
        );
    }
}

