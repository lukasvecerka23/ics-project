using ICSProj.DAL.Repositories;
using ICSProj.DAL.Entities;
using ICSProj.DAL.Mappers;

namespace ICSProj.DAL.UnitOfWork;

public interface IUnitOfWork: IAsyncDisposable
{
    IRepository<TEntity> GetRepository<TEntity, TEntityMapper>()
        where TEntity : class, IEntity
        where TEntityMapper : IEntityMapper<TEntity>, new();

    Task CommitAsync();
}
