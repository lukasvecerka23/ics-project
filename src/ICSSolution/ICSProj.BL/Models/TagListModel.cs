namespace ICSProj.BL.Models;

public record TagListModel : ModelBase
{
    public required Guid CreatorId { get; set; }

    public required string Name { get; set; }

    public static TagListModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        CreatorId = Guid.Empty
    };
}
