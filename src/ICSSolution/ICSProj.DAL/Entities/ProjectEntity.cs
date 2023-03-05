using System.Collections.Generic;

namespace ICSProj.DAL.Entities;

public record ProjectEntity : IEntity
{
    public required string Name { get; set; }

    public ICollection<ActivityEntity> Activities { get; set; } = new List<ActivityEntity>();

    public ICollection<ProjectAssignEntity> ProjectAssigns { get; set; } = new List<ProjectAssignEntity>();

    public UserEntity? Creator { get; init; }

    public required Guid CreatorId { get; set; }

    public Guid Id { get; set; }
}
