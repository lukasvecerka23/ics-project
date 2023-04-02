﻿using ICSProj.BL.Models;
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
                End = entity.End,
                CreatorId = entity.CreatorId,
                Description = entity.Description
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
                TagId = entity.TagId,
                ProjectId = entity.ProjectId,
                Description = entity.Description
            };

    public override ActivityEntity MapToEntity(ActivityDetailModel model)
        => new()
        {
            Id = model.Id,
            Start = model.Start,
            End = model.End,
            Description = model.Description,
            CreatorId = model.CreatorId,
            TagId = model.TagId,
            ProjectId = model.ProjectId
        };
}
