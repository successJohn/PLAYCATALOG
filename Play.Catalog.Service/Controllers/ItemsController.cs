using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Play.Catalog.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private static readonly List<ItemDto> _items = new()
        {
            new ItemDto(Guid.NewGuid(),"Potion", "Restores a small amount",5,DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(),"Antidote", "Restores a small amount",6,DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(),"Bronzer", "Restores a small amount",7,DateTimeOffset.UtcNow),
        };

        [HttpGet]
        public IEnumerable<ItemDto> Get()
        {
            return _items;
        }
         

        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetById(Guid id)
        {
            var item=  _items.Where(x => x.Id == id).FirstOrDefault();
            if(item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public ActionResult<ItemDto> CreatePost([FromBody] CreateItemDto item) {
            var newitem = new ItemDto(Guid.NewGuid(), item.name, item.Description, item.Price, DateTimeOffset.UtcNow);

            _items.Add(newitem);

            return CreatedAtAction(nameof(GetById), new { id = newitem.Id }, newitem);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id,UpdateItemDto item)
        {
            var existingItem = _items.Where(x => x.Id ==id).FirstOrDefault();

            if (existingItem != null) {
                var updateItem = existingItem with
                {
                    Name = item.name,
                    Description = item.Description,
                    Price = item.Price

                };

                var index = _items.FindIndex(existingItem => existingItem.Id == id);
                _items[index] = updateItem;
                return NoContent();
                      
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var index = _items.FindIndex(existingItem => existingItem.Id == id);
            if (index < 0) { 
                return NotFound();
            }
            _items.RemoveAt(index);

            return NoContent();
        }
    }

}
 