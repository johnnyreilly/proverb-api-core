using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proverb.Api.Core.EntityFramework;
using Proverb.Api.Core.EntityFramework.Models;
using Proverb.Api.Core.Helpers;

namespace Proverb.Api.Core.Controllers
{
    [Route("[controller]")]
    public class SageController : Controller
    {
        ProverbContext _proverbContext;
        ILogger<SageController> _logger;
        public SageController(ProverbContext proverbContext, ILogger<SageController> logger)
        {
            _proverbContext = proverbContext;
            _logger = logger;
        }

        // GET sage
        [HttpGet]
        public IActionResult Get()
        {
            var sages = _proverbContext.User.Take(100).ToList();
            return Ok(sages);
        }

        // GET sage/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var sage = _proverbContext.User.Find(id);
            if (sage == null)
                return NotFound("No sage with id " + id.ToString());

            return Ok(sage);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]User sage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ToValidationMessages());
            }

            // await _sageService.UpdateAsync(sage);

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
        public IActionResult Delete(int id)
        {
            // TODO: Delete

            return Ok();
        }
    }
}
