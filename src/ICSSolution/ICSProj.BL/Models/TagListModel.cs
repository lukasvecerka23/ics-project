using System;
using System.Collections.Generic;

namespace ICSProj.BL.Models;

public record TagListModel : ModelBase
{
    public required string Name { get; set; }

    public required Guid CreatorId { get; set; }

    public static TagListModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        CreatorId = Guid.Empty
    };
}
