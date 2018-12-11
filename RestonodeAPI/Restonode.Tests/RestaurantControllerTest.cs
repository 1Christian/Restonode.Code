using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Restonode.Business.Interfaces;
using Restonode.DAL.Entities;
using RestonodeAPI.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restonode.Tests
{
    [TestClass]
    public class RestaurantControllerTest
    {
        RestaurantsController _mockRestaurantController;

        IRestaurantRepository _mockRestaurantRepository;

        [TestInitialize]
        public void Setup()
        {
            var mockRestaurantRepository = new Mock<IRestaurantRepository>();

            var restaurants = new List<Restaurant>
            {
                new Restaurant { RestaurantID = 1, Name = "My Restaurant 2", Address = "Av Rivadavia 1200", Phone = "1134567890", Latitude = -34.5947550, Longitude = -58.428840,  },

                new Restaurant { RestaurantID = 2, Name = "My Restaurant 3", Address = "Av Rivadavia 7800", Phone = "1134567890", Latitude = -14.5947550, Longitude = -45.428840,  }
            };

            mockRestaurantRepository.Setup(m => m.GetRestaurantByIdAsync(It.IsAny<int>())).Returns(async (int id) =>
            {
                return await Task.Run(() =>
                {
                    return restaurants.Where(r => r.RestaurantID == id).SingleOrDefault();
                });
            });

            mockRestaurantRepository.Setup(m => m.GetRestaurantsAsync()).Returns(async () => {
                return await Task.Run(() => {
                    return restaurants;
                });
            });

            _mockRestaurantRepository = mockRestaurantRepository.Object;

            _mockRestaurantController = new RestaurantsController(_mockRestaurantRepository);
        }

        [TestMethod]
        public async Task GetRestaurantByIdAsyncTest()
        {
            IActionResult result = await _mockRestaurantController.Get(1);

            Assert.IsNotNull(((Restaurant)((OkObjectResult)result).Value));

            Assert.AreEqual(1, ((Restaurant)((OkObjectResult)result).Value).RestaurantID);
        }

        [TestMethod]
        public async Task GetRestaurantsAsyncTest()
        {
            IActionResult result = await _mockRestaurantController.Get();

            Assert.IsNotNull((OkObjectResult)result);

            Assert.AreEqual(2, ((IEnumerable<Restaurant>)((OkObjectResult)result).Value).Count());
        }
    }
}