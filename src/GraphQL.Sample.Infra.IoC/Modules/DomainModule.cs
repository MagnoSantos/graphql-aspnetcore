using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GraphQL.Sample.Infra.CrossCutting.IoC.Modules;

public static class DomainModule
{
    public static void Register(IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}