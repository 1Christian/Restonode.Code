using Microsoft.AspNetCore.Mvc;
using Restonode.API.CustomFilters;

namespace RestonodeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HandleExceptionFilter]
    public class UsersController : ControllerBase
    {
    }
}