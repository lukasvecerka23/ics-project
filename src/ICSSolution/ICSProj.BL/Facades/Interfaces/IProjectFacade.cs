using ICSProj.BL.Models;
using ICSProj.DAL.Entities;

namespace ICSProj.BL.Facades;

public interface IProjectFacade: IFacade<ProjectEntity, ProjectListModel, ProjectDetailModel>
{
    public Task<bool> RegisterProject(Guid UserId, Guid ProjectId);
    public Task<bool> LeaveProject(Guid userId, Guid ProjectId);

    public Task<IEnumerable<ProjectListModel>> GetProjectsAssignedToUser(Guid CurrentUserId);

}
