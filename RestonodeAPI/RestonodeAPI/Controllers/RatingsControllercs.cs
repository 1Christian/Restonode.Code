using Microsoft.AspNetCore.Mvc;
using Restonode.API.CustomFilters;
using Restonode.Business.Interfaces;
using System.Threading.Tasks;

namespace Restonode.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HandleExceptionFilter]
    public class RatingsControllercs : ControllerBase
    {
        private readonly IScoreRepository _repository;

        public RatingsControllercs(IScoreRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("Restaurants/ApplyRating")]
        public async Task<IActionResult> ApplyRating(object rating)
        {
            var response = await _repository.ApplyRatingAsync(rating);

            return Ok(response);
        }
    }
}