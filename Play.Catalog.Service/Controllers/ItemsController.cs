using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Play.Catalog.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<Item> _itemsRepository;

        public ItemsController(IRepository<Item> itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetAsync()
        {
            var items = (await _itemsRepository.GetAllAsync())
                 .Select(item => item.AsDto());
            return items;
        }
         

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {
            var item= await _itemsRepository.GetAsync(id);
            if(item == null)
            {
                return NotFound();
            }

            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreatePostAsync([FromBody] CreateItemDto item) {
            var newItem = new Item
            {
                Name = item.name,
                Description = item.Description,
                Price = item.Price,
                CreatedDate = DateTimeOffset.Now
            };

            await _itemsRepository.CreateAsync(newItem);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = newItem.Id }, newItem);
        }

        [HttpPut("{id}")]
        public  async Task<IActionResult> PutAsync(Guid id,UpdateItemDto item)
        {
            var existingItem = await _itemsRepository.GetAsync(id);
          
            if (existingItem != null) {

                existingItem.Name = item.name;
                existingItem.Description = item.Description;
                existingItem.Price = item.Price;

                await _itemsRepository.UpdateAsync(existingItem);
                return NoContent();
                      
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingItem = await _itemsRepository.GetAsync(id);

            if (existingItem != null)
            {
                await _itemsRepository.RemoveAsync(existingItem.Id);
            }
            return NotFound();
        }
    }

}
 