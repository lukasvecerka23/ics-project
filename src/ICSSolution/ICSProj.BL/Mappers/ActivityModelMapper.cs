using ICSProj.BL.Models;
using ICSProj.DAL.Entities;

namespace ICSProj.BL.Mappers;

public class ActivityModelMapper : ModelMapperBase<ActivityEntity, ActivityListModel, ActivityDetailModel>, IActivityModelMapper
{

    public override ActivityListModel MapToListModel(ActivityEntity? entity)
        => entity is null
            ? ActivityListModel.Empty
            : new ActivityListModel
            {
                Id = entity.Id,
                Start = entity.Start,
                CreatorId = entity.CreatorId,
                CreatorName = ConcateNameSurname(entity),
                Description = entity.Description,
                ProjectId = entity.ProjectId,
                TagId = entity.TagId,
                TagColor = entity.Tag?.Color,
                ProjectName = entity.Project?.Name,
                Duration = entity.End - entity.Start
            };

    public override ActivityDetailModel MapToDetailModel(ActivityEntity? entity)
        => entity is null
            ? ActivityDetailModel.Empty
            : new ActivityDetailModel
            {
                Id = entity.Id,
                Start = entity.Start,
                End = entity.End,
                CreatorId = entity.CreatorId,
                ProjectId = entity.ProjectId,
                TagId = entity.TagId,
                ProjectName = entity.Project?.Name,
                TagName = entity.Tag?.Name,
                CreatorName = ConcateNameSurname(entity),
                Description = entity.Description
            };

    public override ActivityEntity MapToEntity(ActivityDetailModel model)
        => new()
        {
            Id = model.Id,
            Start = model.Start,
            End = model.End,
            CreatorId = model.CreatorId,
            TagId = model.TagId,
            ProjectId = model.ProjectId,
            Description = model.Description
        };
    public string ConcateNameSurname(ActivityEntity entity)
        => $"{entity.Creator?.Name} {entity.Creator?.Surname}";
}
