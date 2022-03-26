using GraphQL.Sample.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GraphQL.Sample.Infra.Data.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public BaseRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task Add(TEntity entity)
    {
        using ApplicationDbContext dbContext = await CrateDbContext();

        dbContext.Add(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        using ApplicationDbContext dbContext = await CrateDbContext();

        return await dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        using ApplicationDbContext dbContext = await CrateDbContext();

        return await dbContext.Set<TEntity>().Where(predicate)
                              .AsNoTracking()
                              .ToListAsync();
    }

    public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        using ApplicationDbContext dbContext = await CrateDbContext();

        return await dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public async Task Remove(TEntity entity)
    {
        using ApplicationDbContext dbContext = await CrateDbContext();

        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task Update(TEntity entity)
    {
        using ApplicationDbContext dbContext = await CrateDbContext();

        dbContext.Entry(entity).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }

    private async Task<ApplicationDbContext> CrateDbContext()
        => await _dbContextFactory.CreateDbContextAsync();
}