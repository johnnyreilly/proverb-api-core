using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proverb.Api.Core.Helpers;
using Proverb.Web.Helpers;
using Proverb.Web.Models;

namespace Proverb.Api.Core.Controllers
{
    [Route("[controller]")]
    public class StartupController : Controller
    {
        readonly IAppConfigHelper _appConfigHelper;
        readonly ILogger<StartupController> _logger;

        public StartupController(
            IAppConfigHelper appConfigHelper,
            ILogger<StartupController> logger)
        {
            _appConfigHelper = appConfigHelper;
            _logger = logger;
        }

        public IActionResult Get()
        {
            var appRoot = Url.AbsoluteContent("~/");
            var remoteServiceRoot = Url.AbsoluteContent("~/");

            var startUpData = new StartUpData(
                appName: _appConfigHelper.AppName,
                appRoot: appRoot,
                inDebug: false,
                remoteServiceRoot: remoteServiceRoot,
                version: _appConfigHelper.Version);

            return Ok(startUpData);
        }
    }
}
