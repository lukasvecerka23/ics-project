using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ICSProj.BL.Models;

public record UserDetailModel : ModelBase
{
    public required string Name { get; set; }

    public required string Surname { get; set; }

    public string? ImageUrl { get; set; }

    public ObservableCollection<ProjectAssignListModel> ProjectAssigns { get; init; } = new();

    public static UserDetailModel Empty => new()
    {
        Id      = Guid.Empty,
        Name    = string.Empty,
        Surname = string.Empty,
    };
}
