// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using ICSProj.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICSProj.DAL.Seeds;

public static class ProjectSeeds
{
    public static readonly ProjectEntity EmptyProjectEntity = new()
    {
        Id = default, Name = default!, CreatorId = default
    };

    public static readonly ProjectEntity ProjectEntity1 = new()
    {
        Id = Guid.Parse("2a6901cd-7519-43ee-a43c-f7e017498910"),
        Name = "Project 1",
        CreatorId = UserSeeds.UserEntity1.Id,
        Creator = UserSeeds.UserEntity1
    };

    public static readonly ProjectEntity ProjectEntity2 = new()
    {
        Id = Guid.Parse("a78a1eaa-df80-4c4a-9cd1-2e9e4fd40653"),
        Name = "Project 2",
        CreatorId = UserSeeds.UserEntity2.Id,
        Creator = UserSeeds.UserEntity2
    };

    public static readonly ProjectEntity ProjectEntity3 = new()
    {
        Id = Guid.Parse("1cf02870-a793-4368-ac23-8cfbca36dfe2"),
        Name = "Project 3",
        CreatorId = UserSeeds.UserEntity1.Id,
        Creator = UserSeeds.UserEntity1
    };

    public static readonly ProjectEntity ProjectDelete =
        ProjectEntity1 with { Id = Guid.Parse("1a0b54ce-4182-439a-851a-cfb0173c679f") };
    public static readonly ProjectEntity ProjectUpdate =
        ProjectEntity1 with { Id = Guid.Parse("34d552e4-1b52-4288-afe6-26a448964801") };


    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEntity>().HasData(
            ProjectEntity1 with {Creator = null, ProjectAssigns = Array.Empty<ProjectAssignEntity>(), Activities = Array.Empty<ActivityEntity>()},
            ProjectDelete with { Creator = null, ProjectAssigns = Array.Empty<ProjectAssignEntity>(), Activities = Array.Empty<ActivityEntity>() },
            ProjectUpdate with { Creator = null, ProjectAssigns = Array.Empty<ProjectAssignEntity>(), Activities = Array.Empty<ActivityEntity>() },
            ProjectEntity2 with { Creator = null, ProjectAssigns = Array.Empty<ProjectAssignEntity>()},
            ProjectEntity3 with { Creator = null, ProjectAssigns = Array.Empty<ProjectAssignEntity>()}
                );
    }
}
