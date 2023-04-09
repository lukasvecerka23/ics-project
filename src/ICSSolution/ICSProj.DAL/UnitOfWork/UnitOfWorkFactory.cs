// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.EntityFrameworkCore;

namespace ICSProj.DAL.UnitOfWork;

public class UnitOfWorkFactory: IUnitOfWorkFactory
{
    private readonly IDbContextFactory<ICSProjDbContext> _dbContextFactory;

    public UnitOfWorkFactory(IDbContextFactory<ICSProjDbContext> dbContextFactory) =>
        _dbContextFactory = dbContextFactory;

    public IUnitOfWork Create() => new UnitOfWork(_dbContextFactory.CreateDbContext());
}
