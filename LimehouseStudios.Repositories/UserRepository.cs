using LimehouseStudios.Models;

namespace LimehouseStudios.Repositories
{
    public interface IUserRepository
    {
        IAsyncEnumerable<User> GetUsers();
        Task<IEnumerable<Post>> GetUserPosts(int userId);
    }

    public class UserRepository : IUserRepository
    {
        private readonly HttpClient _httpClient;

        public UserRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async IAsyncEnumerable<User> GetUsers()
        {
            var response = await _httpClient.GetAsync("users");
            var users = await response.Content.ReadAsAsync<IEnumerable<User>>();
            foreach (var user in users)
            {
                yield return user;
            }
        }

        public async Task<IEnumerable<Post>> GetUserPosts(int userId)
        {
            var response = await _httpClient.GetAsync($"users/{userId}/posts");
            var posts = await response.Content.ReadAsAsync<IEnumerable<Post>>();
            return posts;
        }
    }
}