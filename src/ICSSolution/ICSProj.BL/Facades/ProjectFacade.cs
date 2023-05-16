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
    public ProjectFacade(IUnitOfWorkFactory unitOfWorkFactory,
        IProjectModelMapper projectModelMapper) : base(unitOfWorkFactory, projectModelMapper) =>
        _projectModelMapper = projectModelMapper;

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

    protected override List<string> IncludesNavigationPathDetail =>
        new()
        {
            $"{nameof(ActivityEntity.Creator)}"
        };
}
