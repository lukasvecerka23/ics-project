using ICSProj.BL.Mappers;
using ICSProj.BL.Models;
using ICSProj.DAL.Entities;
using ICSProj.DAL.Mappers;
using ICSProj.DAL.UnitOfWork;
using ICSProj.BL.Facades.Interfaces;
using Microsoft.EntityFrameworkCore;
using ICSProj.DAL.Repositories;

namespace ICSProj.BL.Facades;
public class ActivityFacade :
    FacadeBase<ActivityEntity, ActivityListModel, ActivityDetailModel, ActivityEntityMapper>, IActivityFacade
{
    public ActivityFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IActivityModelMapper modelMapper)
        : base(unitOfWorkFactory, modelMapper)
    {
    }

    public async Task<bool> HasMoreActivitiesAtTheSameTime(Guid userId, ActivityDetailModel activity)
    {
        var uow = UnitOfWorkFactory.Create();
        var dbSetActivities = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get();
        var activityEntity = ModelMapper.MapToEntity(activity);

        bool conflictingActivity = await dbSetActivities.AnyAsync(
            x => x.CreatorId == userId && (
                 (activityEntity.Start <= x.Start && activityEntity.End >= x.End) ||
                 (activityEntity.Start >= x.Start && activityEntity.Start <= x.End) ||
                 (activityEntity.End >= x.Start && activityEntity.End <= x.End)     ||
                 (activityEntity.Start >= x.Start && activityEntity.End <= x.End)));

        return conflictingActivity;
    }

    public async Task<IEnumerable<ActivityListModel>> FilterActivities(Guid userId, DateTime startDate, DateTime endDate, Guid? projectId, Guid? tagId)
    {
        IRepository<ActivityEntity> activityRepository = UnitOfWorkFactory.Create().GetRepository<ActivityEntity, ActivityEntityMapper>();

        var filteredActivities = activityRepository.Get();

        if ((startDate != DateTime.MinValue) && (endDate != DateTime.MinValue))
        {
            filteredActivities = filteredActivities.Where(activity =>
                activity.CreatorId == userId &&
                ((activity.Start <= startDate && activity.End >= endDate) ||
                (activity.Start >= startDate && activity.Start <= endDate) ||
                (activity.End >= startDate && activity.End <= endDate) ||
                (activity.Start >= startDate && activity.End <= endDate)));
        }
        if (projectId != null)
        {
            filteredActivities = filteredActivities.Where(activity =>
                activity.ProjectId == projectId);
        }
        if (tagId != null)
        {
            filteredActivities = filteredActivities.Where(activity =>
                activity.TagId == tagId);
        }

        List<ActivityEntity> activities = await filteredActivities.ToListAsync();

        return ModelMapper.MapToListModel(activities);
    }

}
