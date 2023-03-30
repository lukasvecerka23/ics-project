using System;
using System.Linq;
using System.Threading.Tasks;
using ICSProj.DAL.Entities;
using ICSProj.DAL.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ICSProj.DAL.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
where TEntity : class, IEntity
{
    protected readonly IEntityMapper<TEntity> _entityMapper;
    protected readonly DbSet<TEntity> _dbSet;


    public Repository(
        DbContext dbContext,
        IEntityMapper<TEntity> entityMapper)
    {
        _dbSet = dbContext.Set<TEntity>();
        _entityMapper = entityMapper;
    }

    public IQueryable<TEntity> Get()
    {
        return _dbSet;
    }

    public async ValueTask<bool> ExistsAsync(TEntity entity)
    {
        return await _dbSet.AnyAsync(e => e.Id == entity.Id) && entity.Id != Guid.Empty;
    }

    // check if its ok (now different than in cookbook)
    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        TEntity existingEntity = await _dbSet.SingleAsync(e => e.Id == entity.Id);
        _entityMapper.MapToExistingEntity(entity, existingEntity);
        return existingEntity;
    }


    public void Delete(Guid entityId) =>_dbSet.Remove(_dbSet.Single(i => i.Id == entityId));

}
