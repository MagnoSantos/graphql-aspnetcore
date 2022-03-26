using GraphQL.Sample.Data.DataContext;
using GraphQL.Sample.Infra.Data.Entitites;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Sample.Infra.Loaders
{
    public class EntityByIdBatchDataLoader<TEntity> : BatchDataLoader<Guid, TEntity> where TEntity : BaseEntity
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public EntityByIdBatchDataLoader(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                                         IBatchScheduler batchSchedule)
            : base(batchSchedule)
        {
            _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<Guid, TEntity>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            using ApplicationDbContext dbContext = await _dbContextFactory.CreateDbContextAsync();

            return await dbContext.Set<TEntity>()
                                  .Where(entity => keys.Contains(entity.Id))
                                  .ToDictionaryAsync(entity => entity.Id, cancellationToken);
        }
    }
}