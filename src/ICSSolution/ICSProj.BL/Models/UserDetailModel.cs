using System;
using System.Collections.Generic;

namespace ICSProj.BL.Models;

public record UserDetailModel : ModelBase
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? ImageUrl { get; set; }

    public List<ActivityListModel> Activities { get; init; } = new();
    public List<ProjectAssignListModel> ProjectAssigns { get; init; } = new();
    public List<TagListModel> Tags { get; init; } = new();


    public static UserDetailModel Empty => new()
    {
        Id      = Guid.Empty,
        Name    = string.Empty,
        Surname = string.Empty,
    };
}
