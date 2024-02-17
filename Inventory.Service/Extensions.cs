using Inventory.Service.DTO;
using Inventory.Service.Entities;
using System.Runtime.CompilerServices;

namespace Inventory.Service
{
    public static class Extensions
    {
        public static InventoryItemsDto AsDto(this InventoryItem item)
        {
            return new InventoryItemsDto(item.CatalogItemId, item.Quantity, item.AcquiredDate);
        }
    }
}
