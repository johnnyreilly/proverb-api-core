using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proverb.Api.Core.Helpers;
using Proverb.Data.EntityFramework.CommandQuery;
using Proverb.Data.EntityFramework.Models;

namespace Proverb.Api.Core.Controllers
{
    [Route("[controller]")]
    public class SayingController : Controller
    {
        readonly ISayingCommand _sayingCommand;
        readonly ISayingQuery _sayingQuery;
        readonly ILogger<SayingController> _logger;
        public SayingController(ISayingCommand sayingCommand, ISayingQuery sayingService, ILogger<SayingController> logger)
        {
            _sayingCommand = sayingCommand;
            _sayingQuery = sayingService;
            _logger = logger;
        }

        // GET saying
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sayings = await _sayingQuery.GetAllAsync();

            return Ok(sayings);
        }

        // GET saying/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var saying = await _sayingQuery.GetByIdAsync(id);

            if (saying == null)
                return NotFound("No saying with id " + id.ToString());

            return Ok(saying);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Saying saying)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ToValidationMessages());
            }

            if (saying.SageId == 0)
            {
                return BadRequest(
                    new ValidationMessages(
                        new ValidationMessage(
                            name: ValidationHelpers.GetFieldName(saying, x => x.SageId), // eg "saying.sageId"
                            message: "Please select a sage."
                        )
                    )
                );
            }

            if (saying.Id > 0)
            {
                await _sayingCommand.UpdateAsync(saying);
                return Ok();
            }

            var sayingId = await _sayingCommand.CreateAsync(saying);
            return Ok(sayingId);
        }

        /*
                // PUT api/values/5
                [HttpPut("{id}")]
                public void Put(int id, [FromBody]string value)
                {
                }
         */
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sayingCommand.DeleteAsync(id);

            return Ok();
        }
    }
}
