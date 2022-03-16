using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQL.Sample.Domain.GraphQL.Extensions;

public static class ObjectFieldDescriptorExtensions
{
    public static IObjectFieldDescriptor UseDbContext<TDbContext>(this IObjectFieldDescriptor descriptor) 
        where TDbContext : DbContext
        => descriptor.UseScopedService(create: service => service.GetRequiredService<IDbContextFactory<TDbContext>>().CreateDbContext(),
                                       disposeAsync: (service, collection) => collection.DisposeAsync());

    public static IObjectFieldDescriptor UseUpperCase(this IObjectFieldDescriptor descriptor)
    {
        return descriptor.Use(next => async context =>
        {
            await next(context);

            if (context.Result is string s)
            {
                context.Result = s.ToUpperInvariant();
            }
        });
    }
}