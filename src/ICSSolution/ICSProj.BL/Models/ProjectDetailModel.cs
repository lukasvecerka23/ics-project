using System;
using System.Collections.Generic;

namespace ICSProj.BL.Models;

public record ProjectDetailModel : ModelBase
{
    public required string Name { get; set; }

    public List<ActivityDetailModel> Activities { get; set; } = new();

    public List<ProjectAssignDetailModel> ProjectAssigns { get; set; } = new();

    public UserListModel? Creator { get; set; }

    public required Guid CreatorId { get; set; }

    public static ProjectDetailModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        CreatorId = Guid.Empty
    };
}
