using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQL.Sample.Infra.CrossCutting.IoC.Modules;

public static class ApplicationModule
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
    }
}