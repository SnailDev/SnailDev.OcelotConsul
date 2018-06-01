using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API001.Controllers
{
    [Route("api/[controller]/{mallid?}")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public string Get([FromRoute]long mallid)
        {
            return $"{mallid}-->API001:{DateTime.Now.ToString()}  { Environment.MachineName + " OS:" + Environment.OSVersion.VersionString}";
        }

        [HttpGet("/health")]
        public IActionResult Heathle()
        {
            return Ok();
        }
    }
}
