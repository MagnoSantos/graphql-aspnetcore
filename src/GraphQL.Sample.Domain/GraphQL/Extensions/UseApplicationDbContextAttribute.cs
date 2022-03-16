using GraphQL.Sample.Data.DataContext;
using HotChocolate.Types.Descriptors;
using System.Reflection;

namespace GraphQL.Sample.Domain.GraphQL.Extensions;

public class UseApplicationDbContextAttribute : ObjectFieldDescriptorAttribute
{
    public override void OnConfigure(IDescriptorContext context, IObjectFieldDescriptor descriptor, MemberInfo member)
        => descriptor.UseDbContext<ApplicationDbContext>();
}