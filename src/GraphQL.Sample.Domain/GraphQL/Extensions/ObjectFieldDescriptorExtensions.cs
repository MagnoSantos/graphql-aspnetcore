using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQL.Sample.Domain.GraphQL.Extensions;

public static class ObjectFieldDescriptorExtensions
{
    public static IObjectFieldDescriptor UseDbContext<TDbContext>(this IObjectFieldDescriptor descriptor) 
        where TDbContext : DbContext
        => descriptor.UseScopedService(create: service => service.GetRequiredService<IDbContextFactory<TDbContext>>().CreateDbContext(),
                                       disposeAsync: (service, collection) => collection.DisposeAsync());
}