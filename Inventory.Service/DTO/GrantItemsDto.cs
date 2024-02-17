using System;

namespace Inventory.Service.DTO
{
    public record GrantItemsDto(Guid userId, Guid CatalogItemId, int Quantity);
    public record InventoryItemsDto(Guid CatalogItemId, int Quantity,DateTimeOffset AcquiredDate);

}
