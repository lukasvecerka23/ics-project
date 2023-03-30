using ICSProj.DAL.Entities;

namespace ICSProj.DAL.Mappers;

public class TagEntityMapper : IEntityMapper<TagEntity>
{
    public void MapToExistingEntity(TagEntity existingEntity, TagEntity newEntity)
    {
        existingEntity.Name = newEntity.Name;
        existingEntity.CreatorId = newEntity.CreatorId;
    }
}
