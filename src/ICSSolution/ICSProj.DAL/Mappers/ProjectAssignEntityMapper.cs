using ICSProj.DAL.Entities;

namespace ICSProj.DAL.Mappers;

public class ProjectAssignEntityMapper : IEntityMapper<ProjectAssignEntity>
{
    public void MapToExistingEntity(ProjectAssignEntity existingEntity, ProjectAssignEntity newEntity)
    {
        existingEntity.UserId = newEntity.UserId;
        existingEntity.ProjectId = newEntity.ProjectId;
    }
}
