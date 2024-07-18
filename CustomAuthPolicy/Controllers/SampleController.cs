using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomAuthPolicy.Controllers
{
    [Authorize(Policy = "PermissionPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("You are authorized with the required permission!");
        }
    }

}
