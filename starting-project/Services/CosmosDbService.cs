using Microsoft.Azure.Cosmos;
using starting_project.Models;
using starting_project.Services;

namespace starting_project.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private readonly Container _container;

        public CosmosDbService(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync<T>(T item) where T : QuestionDto
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.Id));
        }

        public async Task UpdateItemAsync<T>(string id, T item) where T : QuestionDto
        {
            await _container.UpsertItemAsync(item, new PartitionKey(id));
        }

        public async Task<IEnumerable<T>> GetItemsAsync<T>(string query) where T : QuestionDto
        {
            var iterator = _container.GetItemQueryIterator<T>(new QueryDefinition(query));
            List<T> results = new List<T>();
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
    }


}
