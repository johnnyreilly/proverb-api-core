using System.Reflection;

namespace Proverb.Web.Helpers
{
    public interface IAppConfigHelper
    {
        string AppName { get; }
        string Version { get; }
    }
    public class AppConfigHelper : IAppConfigHelper
    {
        Assembly _assembly;
        Assembly Assembly
        {
            get 
            {
                if (_assembly == null)
                    _assembly = typeof(AppConfigHelper).GetTypeInfo().Assembly;

                return _assembly;
            }
        }

        public string AppName
        {
            get { return Assembly.GetName().Name; }
        }

        public string Version
        {
            get { return Assembly.GetName().Version.ToString(); }
        }
    }
}