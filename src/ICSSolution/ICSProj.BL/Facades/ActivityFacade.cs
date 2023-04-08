using ICSProj.BL.Mappers;
using ICSProj.BL.Models;
using ICSProj.DAL.Entities;
using ICSProj.DAL.Mappers;
using ICSProj.DAL.UnitOfWork;
using ICSProj.BL.Facades.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
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

    public async Task<bool> HasMoreActivitiesAtTheSameTime(Guid userId, Guid activityId)
    {
        var uow = UnitOfWorkFactory.Create();
        var dbSetActivities = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get();

        var activity = await dbSetActivities.SingleOrDefaultAsync(x => x.Id == activityId);
        if (activity is null)
        {
            throw new ArgumentException("Activity with given ID doesnt exist");
        }

        bool conflictingActivity = await dbSetActivities.AnyAsync(
            x => x.CreatorId == userId && (
                 (activity.Start <= x.Start && activity.End >= x.End) ||
                 (activity.Start >= x.Start && activity.Start <= x.End) ||
                 (activity.End >= x.Start && activity.End <= x.End)     ||
                 (activity.Start >= x.Start && activity.End <= x.End)));

        return conflictingActivity;
    }

    public IEnumerable<ActivityListModel> FilterActivities(Guid userId, DateTime startDate, DateTime endDate, Guid projectId, Guid tagId)
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
        if (projectId != Guid.Empty)
        {
            filteredActivities = filteredActivities.Where(activity =>
                activity.ProjectId == projectId);
        }
        if (tagId != Guid.Empty)
        {
            filteredActivities = filteredActivities.Where(activity =>
                activity.TagId == tagId);
        }

        List<ActivityEntity> activities = filteredActivities.ToList();

        return ModelMapper.MapToListModel(activities);
    }

}
