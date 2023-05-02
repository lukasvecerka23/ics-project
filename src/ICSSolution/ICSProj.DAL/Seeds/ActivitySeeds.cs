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
        Description = "Working on important thing...",
        Start = new DateTime(2023, 2, 10, 10,0,0),
        End = new DateTime(2023, 2,10,11,0,0),
        ProjectId = ProjectSeeds.ProjectEntity1.Id,
        TagId = default
    };

    public static readonly ActivityEntity ActivityEntity2 = new()
    {
        Id = Guid.Parse("79168d67-711c-48f2-bbec-a89cf4b831ae"),
        CreatorId = UserSeeds.UserEntity2.Id,
        Description = "Working on second important thing...",
        Start = new DateTime(2023, 2, 12, 20,0,0),
        End = new DateTime(2023, 2,12,22,0,0),
        ProjectId = default,
        TagId = TagSeeds.TagEntity2.Id
    };
    public static readonly ActivityEntity ActivityEntityDeletedByProject = new()
    {
        Id = Guid.Parse("79168d67-711c-48f2-bbec-a89cf4b831aa"),
        CreatorId = UserSeeds.UserEntity2.Id,
        Description = "Working on something important",
        Start = new DateTime(2023, 4, 9, 16, 20, 0, 0),
        End = new DateTime(2023, 4, 11, 10, 10, 0, 0),
        ProjectId = ProjectSeeds.ProjectEntity1.Id,
        TagId = default
    };

    public static readonly ActivityEntity ActivityUpdate = ActivityEntity2 with {Id = Guid.Parse("A61EF52A-F678-4C6D-AD1D-692640278753")};
    public static readonly ActivityEntity ActivityDelete = ActivityEntity2 with {Id = Guid.Parse("EE694005-F18C-46BF-A098-776147447FA9")};


    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityEntity>().HasData(
            ActivityEntity1 with {Creator = null, Project = null, Tag = null},
            ActivityEntity2 with {Creator = null, Project = null, Tag = null},
            ActivityUpdate with { Creator = null, Project = null, Tag = null },
            ActivityDelete with { Creator = null, Project = null, Tag = null },
            ActivityEntityDeletedByProject with {Creator = null, Project = null, Tag = null}
        );
    }
}

