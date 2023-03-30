using ICSProj.DAL.Entities;

namespace ICSProj.DAL.Mappers;

public class ActivityEntityMapper : IEntityMapper<ActivityEntity>
{
    public void MapToExistingEntity(ActivityEntity existingEntity, ActivityEntity newEntity)
    {
        existingEntity.Start = newEntity.Start;
        existingEntity.End = newEntity.End;
        existingEntity.Description = newEntity.Description;
        existingEntity.CreatorId = newEntity.CreatorId;
        existingEntity.ProjectId = newEntity.ProjectId;
        existingEntity.TagId = newEntity.TagId;
    }
}
