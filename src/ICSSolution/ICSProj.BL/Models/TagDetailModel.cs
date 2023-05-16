using System.Collections.ObjectModel;

namespace ICSProj.BL.Models;

public record TagDetailModel : ModelBase
{
    public required Guid CreatorId { get; set; }

    public required string Name { get; set; }

    public string? Color { get; set; }

    public ObservableCollection<ActivityListModel> Activities { get; init; } = new();

    public static TagDetailModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        CreatorId = Guid.Empty
    };
}
