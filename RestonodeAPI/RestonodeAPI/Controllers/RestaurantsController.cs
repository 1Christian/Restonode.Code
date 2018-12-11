using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restonode.API.CustomFilters;
using Restonode.Business.Interfaces;

namespace RestonodeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HandleExceptionFilter]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantRepository _bRestaurant;

        public RestaurantsController(IRestaurantRepository bRestaurant)
        {
            _bRestaurant = bRestaurant;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
           var response = await _bRestaurant.GetRestaurantsAsync();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _bRestaurant.GetRestaurantByIdAsync(id);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(object restaurant)
        {
            var response = await _bRestaurant.CreateRestaurantAsync(restaurant);

            return Ok(HttpStatusCode.OK);            
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpPost]
        [Route("Restaurants/FilterByRating")]
        public async Task<IActionResult> FilterByRating(double rating)
        {
            var response = await _bRestaurant.FilterByRaitingAsync(rating);

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }        
    }
}