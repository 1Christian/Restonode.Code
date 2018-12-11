using Microsoft.AspNetCore.Mvc;
using Restonode.API.CustomFilters;
using Restonode.Business.Interfaces;
using System.Threading.Tasks;

namespace Restonode.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HandleExceptionFilter]
    public class MealsController : ControllerBase
    {
        private readonly IMealRepository _repository;

        public MealsController(IMealRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Post(object meal)
        {
            var response = await _repository.AddMealAsync(meal);

            return Ok(response);
        }
    }
}