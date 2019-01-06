using System;
using System.Configuration;
using System.Threading;
using System.Web;
using System.Web.Http;
using BusinessLayer;
using NLog;

namespace Task5
{
    public class WebApiApplication : HttpApplication
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            try
            {
                var watcher = new CsvFileWatcher(GetWatchingDir());
                var thread = new Thread(watcher.StartWatch);
                thread.Start();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private static string GetWatchingDir()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                if (appSettings.Count > 0) return appSettings["watchingDir"] ?? "";

                Logger.Warn("App settings is empty");
            }
            catch (Exception e)
            {
                Logger.Error($"Error in app settings: {e}");
            }

            return null;
        }
    }
}