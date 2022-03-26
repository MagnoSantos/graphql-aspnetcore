using GraphQL.Sample.Data.DataContext;
using System.Runtime.CompilerServices;

namespace GraphQL.Sample.Infra.Data.DataContext;

public class UseApplicationDbContextAttribute : UseDbContextAttribute
{
    public UseApplicationDbContextAttribute([CallerLineNumber] int order = 0) : base(typeof(ApplicationDbContext))
    {
        Order = order;
    }
}