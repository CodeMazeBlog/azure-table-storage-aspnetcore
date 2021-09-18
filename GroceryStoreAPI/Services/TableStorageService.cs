using GroceryStoreAPI.Models;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Services
{
	public class TableStorageService : ITableStorageService
	{
		private const string TableName = "Item";
		private readonly IConfiguration _configuration;

		public TableStorageService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task<GroceryItemEntity> RetrieveAsync(string category, string id)
		{
			var retrieveOperation = TableOperation.Retrieve<GroceryItemEntity>(category, id);
			return await ExecuteTableOperation(retrieveOperation) as GroceryItemEntity;
		}

		public async Task<GroceryItemEntity> InsertOrMergeAsync(GroceryItemEntity entity)
		{
			var insertOrMergeOperation = TableOperation.InsertOrMerge(entity);
			return await ExecuteTableOperation(insertOrMergeOperation) as GroceryItemEntity;
		}

		public async Task<GroceryItemEntity> DeleteAsync(GroceryItemEntity entity)
		{
			var deleteOperation = TableOperation.Delete(entity);
			return await ExecuteTableOperation(deleteOperation) as GroceryItemEntity;
		}

		private async Task<object> ExecuteTableOperation(TableOperation tableOperation)
		{
			var table = await GetCloudTable();
			var tableResult = await table.ExecuteAsync(tableOperation);
			return tableResult.Result;
		}

		private async Task<CloudTable> GetCloudTable()
		{
			var storageAccount = CloudStorageAccount.Parse(_configuration["StorageConnectionString"]);
			var tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
			var table = tableClient.GetTableReference(TableName);
			await table.CreateIfNotExistsAsync();
			return table;
		}
	}
}
