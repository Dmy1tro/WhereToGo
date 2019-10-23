using Microsoft.AspNetCore.Mvc;
using WhereToGoWebApi.IDbRepository;

namespace WhereToGoWebApi.Controllers
{
    [Route("api/[controller]")]
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
    }
}
