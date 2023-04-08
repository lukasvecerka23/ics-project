using ICSProj.BL.Mappers;
using ICSProj.BL.Models;
using ICSProj.DAL.Entities;
using ICSProj.DAL.Mappers;
using ICSProj.DAL.UnitOfWork;
using ICSProj.BL.Facades.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

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

    public virtual async Task<List<ActivityListModel>> GetActivitiesInInterval(Guid userId, DateTime startDate, DateTime endDate)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        var activities = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get().Where(activity =>
                        activity.CreatorId == userId &&
                        ((activity.Start <= startDate && activity.End >= endDate) ||
                        (activity.Start >= startDate && activity.Start <= endDate) ||
                        (activity.End >= startDate && activity.End <= endDate) ||
                        (activity.Start >= startDate && activity.End <= endDate))).ToListAsync().Result;

        return (List<ActivityListModel>)ModelMapper.MapToListModel(activities);
        
    }
    
}
