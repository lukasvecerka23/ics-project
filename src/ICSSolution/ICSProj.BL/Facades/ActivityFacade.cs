using ICSProj.BL.Mappers;
using ICSProj.BL.Models;
using ICSProj.DAL.Entities;
using ICSProj.DAL.Mappers;
using ICSProj.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using ICSProj.DAL.Repositories;

namespace ICSProj.BL.Facades;
public class ActivityFacade :
    FacadeBase<ActivityEntity, ActivityListModel, ActivityDetailModel, ActivityEntityMapper>, IActivityFacade
{
    private readonly IActivityModelMapper _activityModelMapper;
    public ActivityFacade(IUnitOfWorkFactory unitOfWorkFactory,
        IActivityModelMapper modelMapper) : base(unitOfWorkFactory, modelMapper) =>
        _activityModelMapper = modelMapper;

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

    public async Task<ActivityDetailModel> SaveAsync(Guid userId, ActivityDetailModel activity)
    {
        if (await HasMoreActivitiesAtTheSameTime(userId, activity) == true)
        {
            throw new Exception("There are conflicting activities");
        }

        ActivityDetailModel result;

        ActivityEntity entity = ModelMapper.MapToEntity(activity);

        IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<ActivityEntity> repository = uow.GetRepository<ActivityEntity, ActivityEntityMapper>();

        if (await repository.ExistsAsync(entity))
        {
            ActivityEntity updatedEntity = await repository.UpdateAsync(entity);
            result = ModelMapper.MapToDetailModel(updatedEntity);
        }
        else
        {
            entity.Id = Guid.NewGuid();
            ActivityEntity insertedEntity = await repository.InsertAsync(entity);
            result = ModelMapper.MapToDetailModel(insertedEntity);
        }

        await uow.CommitAsync();

        return result;
    }

    public async Task<IEnumerable<ActivityListModel>> FilterActivities(Guid userId, DateTime startDate, DateTime endDate, Guid? projectId, Guid? tagId)
    {
        IRepository<ActivityEntity> activityRepository = UnitOfWorkFactory.Create().GetRepository<ActivityEntity, ActivityEntityMapper>();

        var filteredActivities = activityRepository.Get();

        filteredActivities = filteredActivities.Where(activity => activity.CreatorId == userId);

        if ((startDate != DateTime.MinValue) && (endDate != DateTime.MinValue))
        {
            filteredActivities = filteredActivities.Where(activity =>
                (activity.Start <= startDate && activity.End >= endDate) ||
                 (activity.Start >= startDate && activity.Start <= endDate) ||
                 (activity.End >= startDate && activity.End <= endDate) ||
                 (activity.Start >= startDate && activity.End <= endDate));
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

        return _activityModelMapper.MapToListModel(activities);
    }
}
