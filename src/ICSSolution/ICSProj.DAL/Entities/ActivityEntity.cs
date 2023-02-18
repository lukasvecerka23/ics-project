using System.Runtime.InteropServices.JavaScript;

namespace ICSProj.DAL.Entities;

public record ActivityEntity : IEntity
{
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    
    // Add tag

    public string? Description { get; set; }

    public Guid Id { get; set; }
}