using starting_project.Models;

namespace starting_project.Services
{
    public interface ICosmosDbService
    {
        Task AddItemAsync<T>(T item) where T : QuestionDto;
        Task UpdateItemAsync<T>(string id, T item) where T : QuestionDto;
        Task<IEnumerable<T>> GetItemsAsync<T>(string query) where T : QuestionDto;
    }

}
