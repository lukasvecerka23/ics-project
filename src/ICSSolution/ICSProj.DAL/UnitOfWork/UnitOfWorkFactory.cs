using Microsoft.EntityFrameworkCore;

namespace ICSProj.DAL.UnitOfWork;

public class UnitOfWorkFactory: IUnitOfWorkFactory
{
    private readonly IDbContextFactory<ICSProjDbContext> _dbContextFactory;

    public UnitOfWorkFactory(IDbContextFactory<ICSProjDbContext> dbContextFactory) =>
        _dbContextFactory = dbContextFactory;

    public IUnitOfWork Create() => new UnitOfWork(_dbContextFactory.CreateDbContext());
}
