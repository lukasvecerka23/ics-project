using System;
using System.Collections.Generic;

namespace ICSProj.BL.Models;

public record ProjectAssignDetailModel : ModelBase
{
    public required Guid UserId { get; set; }
    public required Guid ProjectId { get; set; }

    public static ProjectAssignDetailModel Empty => new()
    {
        Id = Guid.Empty,
        UserId = Guid.Empty,
        ProjectId = Guid.Empty
    };
}
