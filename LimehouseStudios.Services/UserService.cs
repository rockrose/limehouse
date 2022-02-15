using LimehouseStudios.Repositories;
using LimehouseStudios.Models;

namespace LimehouseStudios.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> RetrieveUsers();
        Task<User> RetrieveUser(int userId);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> RetrieveUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<User> RetrieveUser(int userId)
        {
            return await _userRepository.GetUser(userId);
        }
    }
}