using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ICSProj.BL.Models;

public record TagDetailModel : ModelBase
{
    public required string Name { get; set; }

    public ObservableCollection<ActivityListModel> Activities { get; init; } = new();

    public required Guid CreatorId { get; set; }

    //TODO: Pridat barvu k tagu
    //public string Color { get;}

    public static TagDetailModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        CreatorId = Guid.Empty
    };
}
