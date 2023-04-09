using System.Collections.ObjectModel;

namespace ICSProj.BL.Models;

public record ProjectDetailModel : ModelBase
{
    public required Guid CreatorId { get; set; }

    public required string Name { get; set; }
    public required string CreatorName { get; set; }
    public ObservableCollection<ActivityListModel> Activities { get; set; } = new();
    public ObservableCollection<ProjectAssignListModel> ProjectAssigns { get; set; } = new();

    public static ProjectDetailModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        CreatorName = string.Empty,
        CreatorId = Guid.Empty
    };
}
