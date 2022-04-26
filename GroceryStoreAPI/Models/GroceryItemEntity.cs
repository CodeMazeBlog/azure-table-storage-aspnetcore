using Azure;
using Azure.Data.Tables;
using System;

namespace GroceryStoreAPI.Models
{
    public class GroceryItemEntity : ITableEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
