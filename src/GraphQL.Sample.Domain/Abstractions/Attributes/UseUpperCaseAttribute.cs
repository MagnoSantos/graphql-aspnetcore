using GraphQL.Sample.Domain.GraphQL.Extensions;
using HotChocolate.Types.Descriptors;
using System.Reflection;

namespace GraphQL.Sample.Domain.GraphQL.Attributes;

public class UseUpperCaseAttribute : ObjectFieldDescriptorAttribute
{
    public override void OnConfigure(IDescriptorContext context, IObjectFieldDescriptor descriptor, MemberInfo member)
        => descriptor.UseUpperCase();
}