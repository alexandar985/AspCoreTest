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

        // GET api/values
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<string>>> Get()
        //{
        //    var input = new InputDto
        //    {
        //        Dates = new List<string> { "2018-02-01", "2018-02-15", "2018-03-01" },
        //        BaseCurrency = "SEK",
        //        TargetCurrency = "NOK"
        //    };

        //    var service = new Service();
        //    var model = service.GetDatesForApi(input);
        //    var json = await service.GetJsonFromExternalApi(model.StarDate, model.EndDate, input.BaseCurrency, input.TargetCurrency);

        //    var result = service.ReturnResultForThisTask(json);
        //    return Ok(result);
        //}


        // POST api/values
        [HttpPost]
        public async Task<ActionResult<IEnumerable<string>>> Post([FromBody] InputDto data)
        {
            
            var model = _service.GetDatesForApi(data);
            var json = await _service.GetJsonFromExternalApi(model.StarDate, model.EndDate, data.BaseCurrency, data.TargetCurrency);

            var result = _service.ReturnResultForThisTask(json);
            return Ok(result);
        }

    }
}
