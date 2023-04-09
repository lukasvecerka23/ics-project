namespace ICSProj.BL.Models;

public record ProjectListModel : ModelBase
{
    public required Guid CreatorId { get; set; }

    public required string Name { get; set; }
    public required string CreatorName { get; set; }

    public static ProjectListModel Empty => new()
    {
        Id = Guid.Empty,
        CreatorId = Guid.Empty,
        Name = string.Empty,
        CreatorName = string.Empty
    };
}
