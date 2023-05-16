namespace ICSProj.BL.Models;

public record ActivityCreationModel : ModelBase
{
    public TimeSpan TimeFrom { get; set; }
    public DateTime DateFrom { get; set; }
    public TimeSpan TimeTo { get; set; }
    public DateTime DateTo { get; set; }
    public TagListModel? Tag { get; set; }
    public ProjectAssignListModel? Project { get; set; }

    public static ActivityCreationModel Empty => new()
    {
        TimeFrom = TimeSpan.Zero,
        DateFrom = DateTime.Today,
        TimeTo = TimeSpan.Zero,
        DateTo = DateTime.Today,
        Tag = null,
        Project = null
    };
}
