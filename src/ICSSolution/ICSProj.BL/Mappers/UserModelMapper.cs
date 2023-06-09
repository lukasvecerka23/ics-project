﻿using ICSProj.BL.Models;
using ICSProj.DAL.Entities;

namespace ICSProj.BL.Mappers;

public class UserModelMapper : ModelMapperBase<UserEntity, UserListModel, UserDetailModel>, IUserModelMapper
{
    private readonly IProjectAssignModelMapper _projectAssignModelMapper;
    private readonly ITagModelMapper _tagModelMapper;

    public UserModelMapper(IProjectAssignModelMapper projectAssignModelMapper, ITagModelMapper tagModelMapper)
    {
        _projectAssignModelMapper = projectAssignModelMapper;
        _tagModelMapper = tagModelMapper;
    }

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
                ProjectAssigns = _projectAssignModelMapper.MapToListModel(entity.ProjectAssigns)
                    .ToObservableCollection(),
                Tags = _tagModelMapper.MapToListModel(entity.Tags)
                    .ToObservableCollection()
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
