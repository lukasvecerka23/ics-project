// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using ICSProj.BL.Mappers;
using ICSProj.BL.Models;
using ICSProj.DAL.Entities;
using ICSProj.DAL.Mappers;
using ICSProj.DAL.UnitOfWork;

namespace ICSProj.BL.Facades;

public class UserFacade: FacadeBase<UserEntity, UserListModel, UserDetailModel, UserEntityMapper>
{
    private readonly IUserModelMapper _userModelMapper;

    public UserFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserModelMapper userModelMapper) : base(unitOfWorkFactory, userModelMapper) =>
        _userModelMapper = userModelMapper;


}
