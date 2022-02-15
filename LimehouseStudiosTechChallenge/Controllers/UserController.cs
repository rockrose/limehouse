using LimehouseStudios.Services;
using LimehouseStudiosTechChallenge.Filters;
using LimehouseStudiosTechChallenge.Models;
using Microsoft.AspNetCore.Mvc;

namespace LimehouseStudiosTechChallenge.Controllers
{
    [ServiceFilter(typeof(HandleException))]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// List of users.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(new UsersViewModel() { Users = _userService.RetrieveUsers().Result });
        }

        /// <summary>
        /// User including posts.
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            var user = _userService.RetrieveUser(id).Result;

            if (user == null || user.Id != id)
            {
                return NotFound();
            }

            return PartialView("_Posts", new UserPostsViewModel() { User = user });
        }
    }
}
