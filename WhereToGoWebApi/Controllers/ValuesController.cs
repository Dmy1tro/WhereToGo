using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WhereToGoWebApi.DataBaseContext;
using WhereToGoWebApi.Models;

namespace WhereToGoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get() =>
            Ok();
    }
}
