using GraphQL.Sample.Data.DataContext;
using GraphQL.Sample.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQL.Sample.Infra.CrossCutting.IoC.Modules;

public static class InfrastructureModule
{
    private const string ConnectionString = "DataBase Sample";

    public static void Register(IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>()
                .AddPooledDbContextFactory<ApplicationDbContext>(options => options.UseInMemoryDatabase(ConnectionString))
                .AddScoped<ICustomerRepository, CustomerRepository>();
    }
}