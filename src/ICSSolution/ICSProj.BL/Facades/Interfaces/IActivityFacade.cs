using ICSProj.BL.Models;
using ICSProj.DAL.Entities;

namespace ICSProj.BL.Facades;

public interface IActivityFacade: IFacade<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
    public Task<IEnumerable<ActivityListModel>> FilterActivities(Guid userId, DateTime startDate,
        DateTime endDate, Guid? projectId, Guid? tagId);
}
