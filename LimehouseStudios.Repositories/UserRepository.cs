using LimehouseStudios.Models;
using System.Net.Http.Formatting;

namespace LimehouseStudios.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int userId);
    }

    public class UserRepository : IUserRepository
    {
        private readonly HttpClient _httpClient;

        public UserRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var response = await _httpClient.GetAsync("users");
            var users = await response.Content.ReadAsAsync<IEnumerable<User>>();
            users.ToList().ForEach(u => u.Posts.ToList().ForEach(p => p.UserId = u.Id));
            return users;
        }

        public async Task<User> GetUser(int userId)
        {
            var response = await _httpClient.GetAsync($"users/{userId}");
            var user = await response.Content.ReadAsAsync<User>();
            return user;
        }
    }
}