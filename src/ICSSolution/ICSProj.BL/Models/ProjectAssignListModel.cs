namespace ICSProj.BL.Models;

public record ProjectAssignListModel : ModelBase
{
    public required Guid UserId { get; set; }
    public required Guid ProjectId { get; set; }

    public required string UserName { get; set; }
    public required string UserSurname { get; set; }
    public required string ProjectName { get; set; }

    public static ProjectAssignListModel Empty => new()
    {
        Id = Guid.Empty,
        UserId = Guid.Empty,
        ProjectId = Guid.Empty,
        UserName = string.Empty,
        UserSurname = string.Empty,
        ProjectName = string.Empty
    };
}
