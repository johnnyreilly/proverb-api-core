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
    public class SageController : Controller
    {
        readonly ISageCommand _sageCommand;
        readonly ISageQuery _sageQuery;
        readonly ILogger<SageController> _logger;
        public SageController(ISageCommand sageCommand, ISageQuery sageService, ILogger<SageController> logger)
        {
            _sageCommand = sageCommand;
            _sageQuery = sageService;
            _logger = logger;
        }

        // GET sage
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sages = await _sageQuery.GetAllAsync();

            return Ok(sages);
        }

        // GET sage/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var sage = await _sageQuery.GetByIdAsync(id);

            if (sage == null)
                return NotFound("No sage with id " + id.ToString());

            return Ok(sage);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Sage sage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ToValidationMessages());
            }

            var user = await _sageQuery.GetByIdAsync(sage.Id);
            sage.UpdateUser(user);

            await _sageCommand.UpdateAsync(user);

            _logger.LogInformation("Sage " + sage.Name + " [id: " + sage.Id + "] updated by "/* + _userHelper.UserName*/);

            return Ok();
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
            await _sageCommand.DeleteAsync(id);

            return Ok();
        }
    }
}
