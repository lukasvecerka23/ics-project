using System;
using ICSProj.BL.Models;
using ICSProj.DAL.Entities;

namespace ICSProj.BL.Mappers;

public class ProjectAssignModelMapper :
    ModelMapperBase<ProjectAssignEntity, ProjectAssignListModel, ProjectAssignDetailModel>,
    IProjectAssignModelMapper
{
    public override ProjectAssignListModel MapToListModel(ProjectAssignEntity? entity)
        => entity is null
            ? ProjectAssignListModel.Empty
            : new ProjectAssignListModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                ProjectId = entity.ProjectId,
                UserName = entity.User!.Name,
                UserSurname = entity.User!.Surname,
                ProjectName = entity.Project!.Name
            };

    public override ProjectAssignDetailModel MapToDetailModel(ProjectAssignEntity? entity)
        => entity is null
            ? ProjectAssignDetailModel.Empty
            : new ProjectAssignDetailModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                ProjectId = entity.ProjectId,
                UserName = entity.User!.Name,
                UserSurname = entity.User.Surname,
                UserImageUrl = entity.User.ImageUrl,
                ProjectName = entity.Project!.Name
            };

    public override ProjectAssignEntity MapToEntity(ProjectAssignDetailModel model)
        => new()
        {
            Id = model.Id,
            UserId = model.UserId,
            ProjectId = model.ProjectId
        };
}
