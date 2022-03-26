using GraphQL.Sample.Data.DataContext;
using GraphQL.Sample.Infra.Data.Repositories;
using GraphQL.Sample.Infra.Data.UoW;
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
                .AddTransient<ICustomerRepository, CustomerRepository>()
                .AddTransient<IUnitOfWork, UnitOfWork>();
    }
}