using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Business.Interfaces;
using WebApplication1.Business.Services;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IService _service;

        public ValuesController(IService service)
        {
            _service = service;
        }
               

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<IEnumerable<string>>> Post([FromBody] InputDto data)
        {
            
            var model = _service.GetDatesForApi(data);
            var json = await _service.GetJsonFromExternalApi(model.StarDate, model.EndDate, data.BaseCurrency, data.TargetCurrency);

            var result =await _service.ReturnResultForThisTask(json);
            return Ok(result);
        }

    }
}
