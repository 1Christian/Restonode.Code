using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restonode.API.CustomFilters;
using Restonode.API.Interfaces;
using Restonode.Business.Interfaces;

namespace RestonodeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HandleExceptionFilter]
    public class OrdersController : ControllerBase
    {
        private readonly IPublisher _publisher;
        private readonly IOrderRepository _repository;

        
        public OrdersController(IPublisher publisher, IOrderRepository repository)
        {
            _publisher = publisher;
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Post(object order)
        {
            var response = await _repository.CreateOrderAsync(order);
            
            var message = _repository.GetOrderMessageAsync(response);

            await _publisher.PublishAsync(message.Result);

            return Ok(response);
        }
    }
}