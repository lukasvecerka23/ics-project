using System.Collections.Generic;

namespace ICSProj.DAL.Entities;

public record TagEntity : IEntity
{
    public required string Name { get; set; }

    public ICollection<ActivityEntity> Activities { get; init; }
    public ICollection<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();

    public Guid Id { get; set; }
}