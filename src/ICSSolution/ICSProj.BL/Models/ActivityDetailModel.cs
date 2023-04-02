using System;
using System.Collections.Generic;

namespace ICSProj.BL.Models;

public record ActivityDetailModel : ModelBase
{
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }

    public string? Description { get; set; }

    public required Guid CreatorId { get; set; }
    public Guid? ProjectId { get; set; }
    public required Guid TagId { get; set; }

    public static ActivityDetailModel Empty => new()
    {
        Id = Guid.Empty,
        Start = DateTime.Now,
        End = DateTime.Now,
        Description = string.Empty,
        CreatorId = Guid.Empty,
        TagId = Guid.Empty
    };
}
