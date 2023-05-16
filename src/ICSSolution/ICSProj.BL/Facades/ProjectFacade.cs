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
        

    public override async Task<ProjectDetailModel?> GetAsync(Guid id)
    {
        await using IUnitOfWork unitOfWork = UnitOfWorkFactory.Create();

        IRepository<ProjectEntity> projectRepository =
            UnitOfWorkFactory.Create().GetRepository<ProjectEntity, ProjectEntityMapper>();

        ProjectEntity? entity = await projectRepository.Get()
            .Include(e => e.Activities)
            .Include(e => e.ProjectAssigns)
            .ThenInclude(p => p.User)
            .SingleOrDefaultAsync(e => e.Id == id);

        return entity == null ? null : _projectModelMapper.MapToDetailModel(entity);
    }

    public async Task<bool> RegisterProject(Guid UserId, Guid ProjectId)
    {

        ProjectAssignDetailModel NewRegistration = ProjectAssignDetailModel.Empty;

        NewRegistration.Id = new Guid();
        NewRegistration.ProjectId = ProjectId;
        NewRegistration.UserId = UserId;

        var entity = _projectAssignModelMapper.MapToEntity(NewRegistration);


        await using IUnitOfWork unitOfWork = UnitOfWorkFactory.Create();

        IRepository<ProjectAssignEntity> projectRepository =
            UnitOfWorkFactory.Create().GetRepository<ProjectAssignEntity, ProjectAssignEntityMapper>();

        await projectRepository.InsertAsync(entity);

        return true;

    }

    public async Task<bool> LeaveProject(Guid id)
    {
        await using IUnitOfWork unitofWork = UnitOfWorkFactory.Create();

        IRepository<ProjectEntity> projectRepository =
            UnitOfWorkFactory.Create().GetRepository<ProjectEntity, ProjectEntityMapper>();

        return true;
    }

}
