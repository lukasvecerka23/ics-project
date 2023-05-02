using ICSProj.BL.Mappers;
using ICSProj.BL.Models;
using ICSProj.DAL.Entities;
using ICSProj.DAL.Mappers;
using ICSProj.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ICSProj.BL.Facades;

public class UserFacade: FacadeBase<UserEntity, UserListModel, UserDetailModel, UserEntityMapper>, IUserFacade
{
    private readonly IUserModelMapper _userModelMapper;

    public UserFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserModelMapper userModelMapper) : base(unitOfWorkFactory, userModelMapper) =>
        _userModelMapper = userModelMapper;

    public override async Task<UserDetailModel?> GetAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<UserEntity> query = uow.GetRepository<UserEntity, UserEntityMapper>().Get();

        UserEntity? entity = await query
            .Include(e => e.ProjectAssigns)
            .ThenInclude(p => p.Project)
            .SingleOrDefaultAsync(e => e.Id == id);

        return entity == null ? null : _userModelMapper.MapToDetailModel(entity);
    }
}
