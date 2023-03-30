using Microsoft.EntityFrameworkCore;
using ICSProj.DAL.Repositories;
using ICSProj.DAL.Entities;
using ICSProj.DAL.Mappers;

namespace ICSProj.DAL.UnitOfWork;

public class UnitOfWork: IUnitOfWork
{
    private readonly DbContext _dbContext;

    public UnitOfWork(DbContext dbContext) =>
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public IRepository<TEntity> GetRepository<TEntity, TEntityMapper>()
        where TEntity : class, IEntity
        where TEntityMapper : class, IEntityMapper<TEntity>, new()
        // TODO: It will work after repository will be implemented with mappers
        => new Repository<TEntity>(_dbContext, new TEntityMapper());

    public async Task CommitAsync() => await _dbContext.SaveChangesAsync();

    public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();
}
