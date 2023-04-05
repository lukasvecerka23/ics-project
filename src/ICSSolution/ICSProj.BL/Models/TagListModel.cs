using System;
using System.Collections.Generic;

namespace ICSProj.BL.Models;

public record TagListModel : ModelBase
{
    public required string Name { get; set; }

    public required Guid CreatorId { get; set; }

    //TODO: Pridat barvu k tagu
    //public string Color { get;}

    public static TagListModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        CreatorId = Guid.Empty
    };
}
