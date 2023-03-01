using System.Collections.Generic;

namespace ICSProj.DAL.Entities;

public record TagEntity : IEntity
{
    public required string Name { get; set; }

    public ICollection<ActivityEntity> Activities { get; init; }
    public UserEntity? Creator { get; init; }
    public required Guid CreatorId { get; set; }
    public Guid Id { get; set; }
}
