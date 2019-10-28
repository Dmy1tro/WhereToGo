using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhereToGoWebApi.IDbRepository;

namespace WhereToGoWebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IEventDbRepository dbRepository;

        public ValuesController(IEventDbRepository dbRepository)
        {
            this.dbRepository = dbRepository;
        }

        [HttpGet]
        public ActionResult Get() =>
            Ok();

        [HttpGet("getUser")]
        [Authorize(Roles = "User")]
        public ActionResult GetUser() =>
            Ok("Welcome User");

        [HttpGet("getAdmin")]
        [Authorize(Roles = "Admin")]
        public ActionResult GetAdmin() =>
             Ok("Welcome Admin");

    }
}
