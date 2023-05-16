namespace ICSProj.BL.Models;

public record ActivityListModel : ModelBase
{
    public required Guid CreatorId { get; set; }
    public Guid? TagId { get; set; }
    public Guid? ProjectId { get; set; }

    public required DateTime Start { get; set; }
    public string? Description { get; set; }
    public required TimeSpan Duration { get; set; }
    public string? ProjectName { get; set; }
    public string? TagColor { get; set; }


    public static ActivityListModel Empty => new()
    {
        Id = Guid.Empty,
        CreatorId = Guid.Empty,
        TagId = null,
        ProjectId = null,
        Start = DateTime.Now,
        Duration = TimeSpan.Zero,
        ProjectName = string.Empty,
        Description = string.Empty,
        TagColor = string.Empty
    };
}
