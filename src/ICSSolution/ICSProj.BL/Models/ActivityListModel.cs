using System;
using System.Collections.Generic;

namespace ICSProj.BL.Models;
public record ActivityListModel : ModelBase
{
    public required Guid? CreatorId { get; set; }

    public required Guid? TagId { get; set; }

    public required Guid? ProjectId { get; set; }

    public required DateTime Start { get; set; }

    public string? Description { get; set; }

    public required TimeSpan Duration { get; set; }

    public required string ProjectName { get; set; }

    // public required string TagColor { get; set; }

    public static ActivityListModel Empty => new()
    {
        Id = Guid.Empty,
        CreatorId = Guid.Empty,
        TagId = Guid.Empty,
        ProjectId = Guid.Empty,
        Start = DateTime.Now,
        Description = string.Empty,
        Duration = TimeSpan.Zero,
        ProjectName = string.Empty
        // TagColor = string.Empty
    };
}
