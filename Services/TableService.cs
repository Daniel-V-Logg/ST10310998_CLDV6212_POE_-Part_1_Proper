using Azure.Data.Tables;
using ST10310998_CLDV6212_POE__Part_1.Models;
using System.Threading.Tasks;

namespace ST10310998_CLDV6212_POE__Part_1.Services
{
    public class TableService
    {
        private readonly TableClient _tableClient;

        public TableService(TableServiceClient tableServiceClient)
        {
            _tableClient = tableServiceClient.GetTableClient("CustomerProfiles");
            _tableClient.CreateIfNotExists();
        }

        public async Task AddEntityAsync(CustomerProfile profile)
        {
            await _tableClient.AddEntityAsync(profile);
        }

        public async Task<CustomerProfile> GetEntityAsync(string partitionKey, string rowKey)
        {
            return await _tableClient.GetEntityAsync<CustomerProfile>(partitionKey, rowKey);
        }

        public async Task DeleteEntityAsync(string partitionKey, string rowKey)
        {
            await _tableClient.DeleteEntityAsync(partitionKey, rowKey);
        }
    }
}
