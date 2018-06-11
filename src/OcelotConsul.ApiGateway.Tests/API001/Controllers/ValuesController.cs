using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace API001.Controllers
{
    [Route("api/[controller]/{mallid?}")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public async Task<string> Get([FromRoute]long mallid, [FromServices]HttpClient httpClient)
        {
            var result = await httpClient.GetStringAsync("http://localhost:5000/api002/values");
            return $"{mallid}-->API001:{DateTime.Now.ToString()}  { Environment.MachineName + " OS:" + Environment.OSVersion.VersionString} <br />{result}";
        }

        [HttpPost]
        public async Task<string> Post([FromRoute]long mallid, [FromServices]HttpClient httpClient)
        {
            var result = await httpClient.GetStringAsync("http://localhost:5000/api002/values");
            return $"{mallid}-->API001:{DateTime.Now.ToString()}  { Environment.MachineName + " OS:" + Environment.OSVersion.VersionString} <br />{result}";
        }

        [HttpGet("/health")]
        public IActionResult Heathle()
        {
            return Ok();
        }
    }
}
