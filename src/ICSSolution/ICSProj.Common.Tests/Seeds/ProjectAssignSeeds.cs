// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using ICSProj.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICSProj.Common.Tests.Seeds;

public static class ProjectAssignSeeds
{
    public static readonly ProjectAssignEntity EmptyProjectAssignEntity = new()
    {
        Id = default,
        ProjectId = default,
        UserId = default
    };

    public static readonly ProjectAssignEntity ProjectAssignEntity1 = new()
    {
        Id = Guid.Parse("59bd31de-35d2-43d8-b975-45881a69184c"),
        ProjectId = ProjectSeeds.ProjectEntity1.Id,
        UserId = UserSeeds.UserEntity1.Id
    };

    public static readonly ProjectAssignEntity ProjectAssignEntity2 = new()
    {
        Id = Guid.Parse("74425785-f74e-4437-a854-c6f664ef2273"),
        ProjectId = ProjectSeeds.ProjectEntity1.Id,
        UserId = UserSeeds.UserEntity2.Id
    };

    public static readonly ProjectAssignEntity ProjectAssignEntity3 = new()
    {
        Id = Guid.Parse("307efe6b-a6e8-4fc8-b74d-169b03ca88b4"),
        ProjectId = ProjectSeeds.ProjectEntity2.Id,
        UserId = UserSeeds.UserEntity1.Id
    };

    public static readonly ProjectAssignEntity ProjectAssignEntityDelete = ProjectAssignEntity2 with { Id = Guid.Parse("e70ce064-0bf7-4c57-a39b-1e07097eb087") };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectAssignEntity>().HasData(
            ProjectAssignEntity1 with {Project = null, User = null},
            ProjectAssignEntity2 with {Project = null, User = null},
            ProjectAssignEntity3 with { Project = null, User = null},
            ProjectAssignEntityDelete with { Project = null, User = null}
        );
    }
}
