using System.Linq.Expressions;

namespace GraphQL.Sample.Infra.Data.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<List<TEntity>> GetAllAsync();

    Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate);

    Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> predicate);

    Task Add(TEntity entity);

    Task Update(TEntity entity);

    Task Remove(TEntity entity);
}