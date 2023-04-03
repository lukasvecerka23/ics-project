using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ICSProj.BL.Models;

public record TagDetailModel : ModelBase
{
    public required string Name { get; set; }

    public ObservableCollection<ProjectAssignListModel> Activities { get; init; } = new();

    public required Guid CreatorId { get; set; }

    public static TagDetailModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        CreatorId = Guid.Empty
    };
}
