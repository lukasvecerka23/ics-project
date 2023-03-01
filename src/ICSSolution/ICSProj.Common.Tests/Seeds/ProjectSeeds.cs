// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using ICSProj.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICSProj.Common.Tests.Seeds;

public static class ProjectSeeds
{
    public static readonly ProjectEntity EmptyProjectEntity = new()
    {
        Id = default, Name = default!, CreatorId = default
    };

    public static readonly ProjectEntity ProjectEntity1 = new()
    {
        Id = Guid.Parse("a5ac05fd-daae-4109-b5dc-09793b82ab61"),
        Name = "Project 1",
        Creator = UserSeeds.UserEntity1,
        CreatorId = UserSeeds.UserEntity1.Id
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEntity>().HasData(
            ProjectEntity1 with {Creator = null}
        );
    }
}
