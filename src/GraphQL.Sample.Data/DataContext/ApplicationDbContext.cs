using GraphQL.Sample.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Sample.Data.DataContext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; } = default!;
}