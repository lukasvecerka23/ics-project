using ICSProj.BL.Models;
using ICSProj.DAL.Entities;

namespace ICSProj.BL.Mappers;

public class ProjectModelMapper : ModelMapperBase<ProjectEntity, ProjectListModel, ProjectDetailModel>, IProjectModelMapper
{
    private readonly IActivityModelMapper _activityModelMapper;
    private readonly IProjectAssignModelMapper _projectAssignModelMapper;

    public ProjectModelMapper(IActivityModelMapper activityModelMapper,
        IProjectAssignModelMapper projectAssignModelMapper)
    {
        _activityModelMapper = activityModelMapper;
        _projectAssignModelMapper = projectAssignModelMapper;
    }

    public override ProjectListModel MapToListModel(ProjectEntity? entity)
        => entity is null
            ? ProjectListModel.Empty
            : new ProjectListModel
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatorId = entity.CreatorId
            };

    public override ProjectDetailModel MapToDetailModel(ProjectEntity? entity)
        => entity is null
            ? ProjectDetailModel.Empty
            : new ProjectDetailModel
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatorId = entity.CreatorId,
                Activities = _activityModelMapper.MapToListModel(entity.Activities)
                    .ToObservableCollection(),
                ProjectAssigns = _projectAssignModelMapper.MapToListModel(entity.ProjectAssigns)
                    .ToObservableCollection()
            };

    public override ProjectEntity MapToEntity(ProjectDetailModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name,
            CreatorId = model.CreatorId
        };
}
