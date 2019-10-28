using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace FunctionApp1.Data
{
    public class TransactionDocumentStore
    {
        private readonly CosmosClient _cosmosClient;

        public TransactionDocumentStore(
            CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public async Task<TransactionDocument> AddAsync(
               TransactionDocument document)
        {
            var cosmosDatabase =
                _cosmosClient.GetDatabase(
                    Environment.GetEnvironmentVariable("CosmosDBDatabase"));

            var cosmosContainer =
               cosmosDatabase.GetContainer(
                   "transactions");

            var cosmosItemResponse =
                await cosmosContainer.CreateItemAsync(
                    document,
                    new PartitionKey(document.JobId.ToString()));

            return cosmosItemResponse.Resource;
        }
    }
}
