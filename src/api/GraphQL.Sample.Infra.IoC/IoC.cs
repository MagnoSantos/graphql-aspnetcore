﻿using GraphQL.Sample.Infra.CrossCutting.IoC.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQL.Sample.Infra.CrossCutting.IoC;
public static class IoC
{
    public static IServiceCollection ConfigureContainer(this IServiceCollection services, IConfiguration configuration)
    {
        ApplicationModule.Register(services);
        DomainModule.Register(services);
        InfrastructureModule.Register(services);

        return services;
    }
}
