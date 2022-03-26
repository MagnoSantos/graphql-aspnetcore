using GraphQL.Sample.Data.DataContext;
using GraphQL.Sample.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Sample.Infra.Data.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}