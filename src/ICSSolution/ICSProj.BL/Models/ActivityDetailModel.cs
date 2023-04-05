using System;
using System.Collections.Generic;

namespace ICSProj.BL.Models;

public record ActivityDetailModel : ModelBase
{
    public required Guid CreatorId { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid? TagId { get; set; }

    public required DateTime Start { get; set; }

    public required DateTime End { get; set; }

    public string? ProjectName { get; set; }

    // public string TagColor { get; set; }

    public string? TagName { get; set; }

    public string? CreatorName { get; set; }

    public static ActivityDetailModel Empty => new()
    {
        Id = Guid.Empty,
        CreatorId = Guid.Empty,
        TagId = Guid.Empty,
        ProjectId = Guid.Empty,
        Start = DateTime.Now,
        End = DateTime.Now,
        ProjectName = string.Empty,
        // TagColor = string.Empty,
        TagName = string.Empty,
        CreatorName = string.Empty
    };
}
