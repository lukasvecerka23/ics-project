using ICSProj.BL.Mappers;
using ICSProj.BL.Models;
using ICSProj.DAL.Entities;
using ICSProj.DAL.Mappers;
using ICSProj.DAL.UnitOfWork;

namespace ICSProj.BL.Facades;

public class UserFacade: FacadeBase<UserEntity, UserListModel, UserDetailModel, UserEntityMapper>, IUserFacade
{

    public UserFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserModelMapper userModelMapper) : base(unitOfWorkFactory, userModelMapper){ }

    protected override List<string> IncludesNavigationPathDetail =>
        new()
        {
            $"{nameof(UserEntity.ProjectAssigns)}.{nameof(ProjectAssignEntity.Project)}",
            $"{nameof(UserEntity.Tags)}"
        };
}
