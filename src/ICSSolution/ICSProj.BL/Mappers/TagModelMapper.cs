using ICSProj.BL.Models;
using ICSProj.DAL.Entities;

namespace ICSProj.BL.Mappers;

public class TagModelMapper : ModelMapperBase<TagEntity, TagListModel,TagDetailModel>,
    ITagModelMapper
{
    private readonly IActivityModelMapper _activityModelMapper;

    public TagModelMapper(IActivityModelMapper activityModelMapper) =>
        _activityModelMapper = activityModelMapper;

    public override TagListModel MapToListModel(TagEntity? entity)
        => entity is null
            ? TagListModel.Empty
            : new TagListModel
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatorId = entity.CreatorId
            };

    public override TagDetailModel MapToDetailModel(TagEntity? entity)
        => entity is null
            ? TagDetailModel.Empty
            : new TagDetailModel
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatorId = entity.CreatorId,
                Activities = _activityModelMapper.MapToListModel(entity.Activities)
                    .ToObservableCollection()
            };

    public override TagEntity MapToEntity(TagDetailModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name,
            CreatorId = model.CreatorId
        };
}
