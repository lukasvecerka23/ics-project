using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ICSProj.BL.Models;

public record ProjectDetailModel : ModelBase
{
    public required string Name { get; set; }

    public ObservableCollection<ProjectAssignListModel> Activities { get; set; } = new();

    public ObservableCollection<ProjectAssignListModel> ProjectAssigns { get; set; } = new();

    public required Guid CreatorId { get; set; }

    public static ProjectDetailModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        CreatorId = Guid.Empty
    };
}
