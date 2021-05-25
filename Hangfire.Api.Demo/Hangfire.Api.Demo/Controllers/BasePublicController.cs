using Microsoft.AspNetCore.Mvc;

namespace Hangfire.Api.Demo.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasePublicController : ControllerBase
    {

    }
}
