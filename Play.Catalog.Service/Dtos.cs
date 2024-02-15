using System;
using System.ComponentModel.DataAnnotations;

namespace Play.Catalog.Service
{
    public class Dto 
    {
    }

    public record ItemDto(Guid Id, string Name, string Description, decimal Price, DateTimeOffset createdDate);

    public record CreateItemDto([Required]string name, string Description, [Range(0,1000)] decimal Price);

    public record UpdateItemDto ([Required]string name, string Description, [Required] decimal Price);
}
