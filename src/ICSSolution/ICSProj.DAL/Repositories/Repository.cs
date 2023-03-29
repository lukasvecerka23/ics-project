using System;
using System.Linq;
using System.Threading.Tasks;
using ICSProj.DAL.Entities;
using ICSProj.DAL.Repositories;
// using ICSProj.DAL.Mappers; uncomment after creating the mapper
using Microsoft.EntityFrameworkCore;

namespace ICS.DAL.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
where TEntity : class, IEntity
{
    // protected readonly IEntityMapper<TEntity> _entityMapper; //uncomment after creating the mapper
    protected readonly DbSet<TEntity> _dbSet;


// replace Repository args with the following after implementing the mapper:
// (
//         DbContext dbContext,
//         IEntityMapper<TEntity> entityMapper)
    public Repository(
        DbContext dbContext)
    {
        _dbSet = dbContext.Set<TEntity>();
        // _entityMapper = entityMapper; //uncomment after creating the mapper
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
        // _entityMapper.Map(entity, existingEntity); //uncomment after creating the mapper
        return existingEntity;
    }


    public void Delete(Guid entityId) =>_dbSet.Remove(_dbSet.Single(i => i.Id == entityId));

}