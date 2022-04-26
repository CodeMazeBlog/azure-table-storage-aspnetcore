using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using GroceryStoreAPI.Models;
using GroceryStoreAPI.Services;

namespace GroceryStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ITableStorageService _storageService;

        public ItemsController(ITableStorageService storageService)
        {
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }

        [HttpGet]
        [ActionName(nameof(GetAsync))]
        public async Task<IActionResult> GetAsync([FromQuery] string category, string id)
        {
            return Ok(await _storageService.GetEntityAsync(category, id));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] GroceryItemEntity entity)
        {
            entity.PartitionKey = entity.Category;

            string Id = Guid.NewGuid().ToString();
            entity.Id = Id;
            entity.RowKey = Id;

            var createdEntity = await _storageService.UpsertEntityAsync(entity);
            return CreatedAtAction(nameof(GetAsync), createdEntity);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] GroceryItemEntity entity)
        {
            entity.PartitionKey = entity.Category;
            entity.RowKey = entity.Id;

            await _storageService.UpsertEntityAsync(entity);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery] string category, string id)
        {
            await _storageService.DeleteEntityAsync(category, id);
            return NoContent();
        }
    }
}
