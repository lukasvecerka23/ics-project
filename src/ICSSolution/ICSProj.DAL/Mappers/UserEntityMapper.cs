using ICSProj.DAL.Entities;

namespace ICSProj.DAL.Mappers;

public class UserEntityMapper : IEntityMapper<UserEntity>
{
    public void MapToExistingEntity(UserEntity existingEntity, UserEntity newEntity)
    {
        existingEntity.Name = newEntity.Name;
        existingEntity.Surname = newEntity.Surname;
        existingEntity.ImageUrl = newEntity.ImageUrl;
    }
}
