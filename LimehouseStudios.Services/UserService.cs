using LimehouseStudios.Repositories;
using LimehouseStudios.Models;

namespace LimehouseStudios.Services
{
    public interface IUserService
    {
        IAsyncEnumerable<User> RetrieveUsersWithPosts();
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private async IAsyncEnumerable<User> RetrieveUsers()
        {
            await foreach (var user in _userRepository.GetUsers())
            {
                yield return user;
            }
        }

        private async Task<IEnumerable<Post>> RetrieveUserPosts(int userId)
        {
            return await _userRepository.GetUserPosts(userId);
        }

        public async IAsyncEnumerable<User> RetrieveUsersWithPosts()
        {
            await foreach (var user in RetrieveUsers())
            {
                var posts = await RetrieveUserPosts(user.Id);
                user.Posts = posts;
                yield return user;
            }
        }
    }
}