using System;
using System.Linq;
using System.Net.Http;
using LimehouseStudios.Repositories;
using LimehouseStudios.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LimehouseStudios.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private HttpClient? _httpClient;

        [TestInitialize]
        public void Initialise()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _httpClient = HttpClientFactory.Create();
            _httpClient.BaseAddress = new Uri(configuration.GetSection("Services").GetSection("UserPostDataService")["UserPostDataBaseUri"]);
        }

        [TestMethod]
        public void RetrieveUsers()
        {
            IUserService userService = new UserService(new UserRepository(_httpClient));
            var users = userService.RetrieveUsersWithPosts().ToListAsync().Result;
            Assert.IsTrue(users.Count() > 0, "Expected users but none were returned.");
        }

        [TestMethod]
        public void RetrievePostsForKnownUser()
        {
            IUserService userService = new UserService(new UserRepository(_httpClient));
            var users = userService.RetrieveUsersWithPosts().ToListAsync().Result;
            var user = users.FirstOrDefault(u => u.Id == 1);
            Assert.IsTrue(user != null && user.PostCount > 0, "Expected posts for a known user but no posts were found.");
        }

        [TestMethod]
        public void RetrievePostsForUnknownUser()
        {
            IUserService userService = new UserService(new UserRepository(_httpClient));
            var users = userService.RetrieveUsersWithPosts().ToListAsync().Result;
            var user = users.FirstOrDefault(u => u.Id == 99);
            Assert.IsTrue(user == null, "Posts for a unknown user not expected to have been returned but a posts for this user was found.");
        }

        [TestCleanup]
        public void Deinitialise()
        {
            if (_httpClient != null)
            {
                _httpClient.Dispose();
            }
        }
    }
}