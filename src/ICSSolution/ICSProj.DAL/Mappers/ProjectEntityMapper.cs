using ICSProj.DAL.Entities;

namespace ICSProj.DAL.Mappers;

public class ProjectEntityMapper : IEntityMapper<ProjectEntity>
{
    public void MapToExistingEntity(ProjectEntity existingEntity, ProjectEntity newEntity)
    {
        existingEntity.Name = newEntity.Name;
        existingEntity.CreatorId = newEntity.CreatorId;
    }
}
