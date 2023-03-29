using System.Collections.Generic;

namespace ICSProj.DAL.Entities;

public record ProjectEntity : IEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public required Guid CreatorId { get; set; }

    public UserEntity? Creator { get; init; }
    public ICollection<ActivityEntity> Activities { get; set; } = new List<ActivityEntity>();
    public ICollection<ProjectAssignEntity> ProjectAssigns { get; set; } = new List<ProjectAssignEntity>();
}
