using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SalesTracker.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] ProductCreate request)
        {
            if (!ModelState.IsValid)
            return BadRequest(ModelState);

            if (await _itemService.CreateItemAsync(request))
            return Ok("Item created successfully.");

            return BadRequest("Item could not be created.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _itemService.GetAllItemsAsync();
            return Ok(items);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item is null)
                return NotFound();
            else
                return Ok(item);
        }
        
        [HttpPut]
        public async Task<IActionResult> EditItemById([FromBody] ProductEdit request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return await _itemService.EditItemAsync(request)
                ? Ok("Game updated successfully.")
                : BadRequest("Game could note be updated.");
        }

        [HttpDelete("{itemId:int}")]
        public async Task<IActionResult> DeleteGame([FromRoute] int itemId)
        {
            return await _itemService.DeleteItemAsync(itemId)
                ? Ok($"Note {itemId} was deleted successfully.")
                : BadRequest($"Note {itemId} could not be deleted.");
        }
    }
}