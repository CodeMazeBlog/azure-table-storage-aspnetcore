using GroceryStoreAPI.Models;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Services
{
public interface ITableStorageService
{
    Task<GroceryItemEntity> GetEntityAsync(string category, string id);
    Task<GroceryItemEntity> UpsertEntityAsync(GroceryItemEntity entity);
    Task DeleteEntityAsync(string category, string id);
}
}
