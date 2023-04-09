namespace ICSProj.DAL.Entities;

public record TagEntity : IEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public required Guid CreatorId { get; set; }

    public ICollection<ActivityEntity> Activities { get; init; } = new List<ActivityEntity>();
    public UserEntity? Creator { get; init; }
}
