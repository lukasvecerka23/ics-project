using System.Collections.Generic;

namespace ICSProj.DAL.Entities;

public record TagEntity : IEntity
{
    public required string Name { get; set; }

    public ICollection<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();

    public Guid Id { get; set; }
}