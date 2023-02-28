using System.Collections.Generic;

namespace ICSProj.DAL.Entities;

public record UserEntity : IEntity
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? ImageUrl { get; set; }

    public ICollection<ActivityEntity> Activities { get; set; } = new List<ActivityEntity>();
    public ICollection<ProjectAssignEntity> ProjectAssigns { get; set; } = new List<ProjectAssignEntity>();
    public ICollection<TagEntity> Tags { get; set; } = new List<TagEntity>();
    //public ProjectEntity? Project { get; init; }

    public required Guid Id { get; set; }
}
