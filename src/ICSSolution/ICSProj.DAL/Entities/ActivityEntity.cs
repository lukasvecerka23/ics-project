namespace ICSProj.DAL.Entities;

public record ActivityEntity : IEntity
{

    public Guid Id { get; set; }
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    public string? Description { get; set; }

    public required Guid CreatorId { get; set; }
    public required Guid? ProjectId { get; set; }
    public required Guid? TagId { get; set; }

    public UserEntity? Creator { get; init; }
    public ProjectEntity? Project { get; init; }
    public TagEntity? Tag { get; init; }
}
