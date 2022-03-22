namespace GraphQL.Sample.Infra.Data.UoW
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task CommitAsync();
    }
}