using System.ComponentModel.DataAnnotations;

namespace GraphQL.Sample.Data.Entities;

public class Customer
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(200)]
    public string? Name { get; set; }

    [StringLength(200)]
    public string? LastName { get; set; }

    [StringLength(14)]
    public string? Cpf { get; set; }
}