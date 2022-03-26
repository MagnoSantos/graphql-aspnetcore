using GraphQL.Sample.Infra.Data.Entitites;
using HotChocolate;
using System.ComponentModel.DataAnnotations;

namespace GraphQL.Sample.Data.Entities;

[GraphQLDescription("Dados de clientes")]
public class Customer : BaseEntity
{
    [GraphQLDescription("Primeiro nome do cliente")]
    [StringLength(200)]
    public string? Name { get; set; }

    [GraphQLDescription("último nome do cliente")]
    [StringLength(200)]
    public string? LastName { get; set; }

    [GraphQLDescription("Número de CPF do cliente")]
    [StringLength(14)]
    public string? Cpf { get; set; }
}