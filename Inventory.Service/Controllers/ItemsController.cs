using Inventory.Service.DTO;
using Inventory.Service.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<InventoryItem> _itemsRepository;

        public ItemsController(IRepository<InventoryItem> itemsRepository)
        {
            _itemsRepository = itemsRepository;   
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemsDto>>> GetAsync(Guid userId)
        {
            if(userId == Guid.Empty)
            {
                return BadRequest();
            }

            var items = (await _itemsRepository.GetAllAsync(item => item.UserId == userId))
                .Select(item => item.AsDto());

            return Ok(items);
        
        }



        [HttpPost]
        public async Task<ActionResult> PostAsync(GrantItemsDto grantItemsDto)
        {
            var inventoryItem = await _itemsRepository.GetAsync(item => item.UserId == grantItemsDto.userId
            && item.CatalogItemId == grantItemsDto.CatalogItemId);

            if(inventoryItem == null)
            {
                var item = new InventoryItem
                {
                  
                    CatalogItemId = grantItemsDto.CatalogItemId,
                    UserId = grantItemsDto.userId,
                    Quantity = grantItemsDto.Quantity,
                    AcquiredDate = DateTimeOffset.UtcNow

                };

                await _itemsRepository.CreateAsync(item);
            }
            else
            {
                inventoryItem.Quantity = grantItemsDto.Quantity;
                await _itemsRepository.UpdateAsync(inventoryItem);
            }

            return Ok();
          
        }
    }
}
