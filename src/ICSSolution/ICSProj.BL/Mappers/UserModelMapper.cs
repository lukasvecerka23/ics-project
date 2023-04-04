using ICSProj.BL.Models;
using ICSProj.DAL.Entities;

namespace ICSProj.BL.Mappers;

public class UserModelMapper : ModelMapperBase<UserEntity, UserListModel, UserDetailModel>, IUserModelMapper
{
    private readonly IActivityModelMapper _activityModelMapper;
    private readonly IProjectAssignModelMapper _projectAssignModelMapper;
    private readonly ITagModelMapper _tagModelMapper;

    public UserModelMapper(IActivityModelMapper activityModelMapper) =>
        _activityModelMapper = activityModelMapper;

    public UserModelMapper(IProjectAssignModelMapper projectAssignModelMapper) =>
        _projectAssignModelMapper = projectAssignModelMapper;

    public UserModelMapper(ITagModelMapper tagModelMapper) =>
        _tagModelMapper = tagModelMapper;


    public override UserListModel MapToListModel(UserEntity? entity)
        => entity is null
            ? UserListModel.Empty
            : new UserListModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                ImageUrl = entity.ImageUrl
            };

    public override UserDetailModel MapToDetailModel(UserEntity? entity)
        => entity is null
            ? UserDetailModel.Empty
            : new UserDetailModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                ImageUrl = entity.ImageUrl,
                Activities = _activityModelMapper.MapToListModel(entity.Activities)
                    .ToObservableCollection(),
                ProjectAssigns = _projectAssignModelMapper.MapToListModel(entity.ProjectAssigns)
                    .ToObservableCollection(),
                Tags = _tagModelMapper.MapToListModel(entity.Tags)
                    .ToObservableCollection(),

            };

    public override UserEntity MapToEntity(UserDetailModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name,
            Surname = model.Surname,
            ImageUrl = model.ImageUrl
        };
}
