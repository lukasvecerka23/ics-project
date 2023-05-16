using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using ICSProj.BL.Models;
using ICSProj.DAL.Entities;
using ICSProj.DAL.UnitOfWork;
using ICSProj.BL.Mappers;
using ICSProj.DAL.Mappers;
using ICSProj.DAL.Repositories;

namespace ICSProj.BL.Facades;

public class ProjectFacade : FacadeBase<ProjectEntity, ProjectListModel, ProjectDetailModel, ProjectEntityMapper>, IProjectFacade
{

    private readonly IProjectModelMapper _projectModelMapper;
    private readonly IProjectAssignModelMapper _projectAssignModelMapper;

    public ProjectFacade(IUnitOfWorkFactory unitOfWorkFactory,
        IProjectModelMapper projectModelMapper, IProjectAssignModelMapper projectAssignModelMapper) : base(unitOfWorkFactory, projectModelMapper)
    {
        _projectModelMapper = projectModelMapper;
        _projectAssignModelMapper = projectAssignModelMapper;
    }

    public async Task<bool> RegisterProject(Guid UserId, Guid ProjectId)
    {

        ProjectAssignDetailModel newRegistration = ProjectAssignDetailModel.Empty;

        newRegistration.ProjectId = ProjectId;
        newRegistration.UserId = UserId;

        var entity = _projectAssignModelMapper.MapToEntity(newRegistration);

        var uow = UnitOfWorkFactory.Create();

        IRepository<ProjectAssignEntity> projectAssignRepository = uow.GetRepository<ProjectAssignEntity, ProjectAssignEntityMapper>();

        entity.Id = Guid.NewGuid();

        await projectAssignRepository.InsertAsync(entity);

        await uow.CommitAsync();

        return true;

    }

    public async Task<IEnumerable<ProjectListModel>> GetProjectsAssignedToUser(Guid CurrentUserId)
    {
        await using IUnitOfWork unitOfWork = UnitOfWorkFactory.Create();

        IRepository<ProjectEntity> projectRepository =
            UnitOfWorkFactory.Create().GetRepository<ProjectEntity, ProjectEntityMapper>();

        var entities = await projectRepository.Get()
            .Include(e => e.ProjectAssigns)
            .Include(e => e.Creator)
            .Where(e => e.CreatorId == CurrentUserId || e.ProjectAssigns.Any(p => p.UserId == CurrentUserId))
            .ToListAsync();

        var projects = _projectModelMapper.MapToListModel(entities);
        return projects;
    }

    public async Task<bool> LeaveProject(Guid userId, Guid ProjectId)
    {

        var uow = UnitOfWorkFactory.Create();

        IRepository<ProjectAssignEntity> projectAssignRepository = uow.GetRepository<ProjectAssignEntity, ProjectAssignEntityMapper>();

        var entityId = projectAssignRepository.Get()
            .SingleOrDefault(e => e.UserId == userId && e.ProjectId == ProjectId)!.Id;

        projectAssignRepository.Delete(entityId);

        await uow.CommitAsync().ConfigureAwait(false);

        return true;
    }

    protected override List<string> IncludesNavigationPathDetail =>
        new()
        {
            $"{nameof(ProjectEntity.Creator)}",
            $"{nameof(ProjectEntity.Activities)}",
            $"{nameof(ProjectEntity.ProjectAssigns)}",
            $"{nameof(ProjectEntity.ProjectAssigns)}.{nameof(ProjectAssignEntity.User)}"
        };
}
