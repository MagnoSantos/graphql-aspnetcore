using GraphQL.Sample.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQL.Sample.Infra.CrossCutting.IoC.Modules;

public static class InfrastructureModule
{
    private const string ConnectionString = "DataBase Sample";

    public static void Register(IServiceCollection services)
    {
        services.AddPooledDbContextFactory<ApplicationDbContext>(options => options.UseInMemoryDatabase(ConnectionString));
    }
}