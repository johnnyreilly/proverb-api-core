using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proverb.Api.Core.Helpers;
using Proverb.Data.EntityFramework.CommandQuery;
using Proverb.Data.EntityFramework.Models;
using Proverb.Web.Models;

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

            return Ok(sayings.Select(saying => new SayingVM(saying)));
        }

        // GET saying/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var saying = await _sayingQuery.GetByIdAsync(id);

            if (saying == null)
                return NotFound("No saying with id " + id.ToString());

            return Ok(new SayingVM(saying));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SayingVM saying)
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
                var sayingToAdd = new Saying();
                saying.UpdateSaying(sayingToAdd);
                var addedId = await _sayingCommand.CreateAsync(sayingToAdd);
                return Ok(addedId);
            }

            var sayingToUpdate = await _sayingQuery.GetByIdAsync(saying.Id);
            var sayingId = await _sayingCommand.CreateAsync(sayingToUpdate);
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
