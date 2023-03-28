using System;
using System.Collections.Generic;

namespace ICSProj.BL.Models;
public record ActivityListModel : ModelBase
{
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }

    public string? Description { get; set; }

    public required Guid? CreatorId { get; set; }

    public static ActivityListModel Empty => new()
    {
        Id = Guid.Empty,
        Start = DateTime.Now,
        End = DateTime.Now,
        CreatorId = Guid.Empty
    };
}
