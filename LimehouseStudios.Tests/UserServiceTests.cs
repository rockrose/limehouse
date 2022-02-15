using LimehouseStudios.Repositories;
using LimehouseStudios.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;

namespace LimehouseStudios.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private HttpClient _httpClient;

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
            var users = userService.RetrieveUsers().Result;
            Assert.IsTrue(users.Count() > 0, "Expected users but none were returned.");
        }

        [TestMethod]
        public void RetrieveKnownValidUser()
        {
            IUserService userService = new UserService(new UserRepository(_httpClient));
            var user = userService.RetrieveUser(1).Result;
            Assert.IsTrue(user != null && user.Id > 0 && user.Username != null, "Expected a known user but the user wasn't found.");
        }

        [TestMethod]
        public void RetrieveKnownInvalidUser()
        {
            IUserService userService = new UserService(new UserRepository(_httpClient));
            var user = userService.RetrieveUser(99).Result;
            Assert.IsTrue(user == null || (user.Id == 0 && user.Username == null), "A known invalid user not expected to have been returned but a user was found.");
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