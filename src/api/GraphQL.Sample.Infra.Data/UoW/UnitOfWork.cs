using GraphQL.Sample.Data.DataContext;

namespace GraphQL.Sample.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext
                ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public async Task CommitAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _applicationDbContext.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}