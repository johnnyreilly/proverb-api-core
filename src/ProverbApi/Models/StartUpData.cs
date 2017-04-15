namespace Proverb.Web.Models
{
    public class StartUpData
    {
        public StartUpData(string appName, string appRoot, bool inDebug, string remoteServiceRoot, string version)
        {
            AppName = appName;
            AppRoot = appRoot;
            InDebug = inDebug;
            RemoteServiceRoot = remoteServiceRoot;
            Version = version;
        }

        public string AppName { get; }
        public string AppRoot { get; }
        public bool InDebug { get; }
        public string RemoteServiceRoot { get; }
        public string Version { get; }
    }
}