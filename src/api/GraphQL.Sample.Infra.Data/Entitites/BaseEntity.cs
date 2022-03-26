using HotChocolate;
using HotChocolate.Types.Relay;

namespace GraphQL.Sample.Infra.Data.Entitites
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreateAt = DateTime.Now;
        }

        [ID]
        [GraphQLDescription("ID do cliente")]
        public Guid Id { get; set; }

        [GraphQLDescription("Momento de criação de dado do cliente")]
        public DateTime? CreateAt { get; set; }
    }
}