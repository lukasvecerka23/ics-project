using System;
using System.Collections.Generic;

namespace ICSProj.BL.Models;

public record ProjectListModel : ModelBase
{
    public required string Name { get; set; }

    public required Guid CreatorId { get; set; }

    public static ProjectListModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        CreatorId = Guid.Empty
    };
}
