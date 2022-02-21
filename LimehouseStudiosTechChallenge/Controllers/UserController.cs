using LimehouseStudios.Models;
using LimehouseStudios.Services;
using LimehouseStudiosTechChallenge.Filters;
using LimehouseStudiosTechChallenge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace LimehouseStudiosTechChallenge.Controllers
{
    [ServiceFilter(typeof(HandleException))]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMemoryCache _memoryCache;

        public UserController(IUserService userService, IMemoryCache memoryCache)
        {
            _userService = userService;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// List of users.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var users = await GetUsers();
            return View(new UsersViewModel() { Users = users});
        }

        /// <summary>
        /// User posts.
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            IEnumerable<Post> posts = new List<Post>();
            IEnumerable<User> users;
            if (!_memoryCache.TryGetValue<IEnumerable<User>>("users", out users))
            {
                users = await GetUsers();
            }
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                return PartialView("_Posts", new UserPostsViewModel() { User = user });
            }
            return NotFound();
        }

        private async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _userService.RetrieveUsersWithPosts().ToListAsync();
            _memoryCache.Set("users", users);
            return users;
        }
    }
}
