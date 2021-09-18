using Microsoft.Azure.Cosmos.Table;

namespace GroceryStoreAPI.Models
{
    public class GroceryItemEntity : TableEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
    }
}
