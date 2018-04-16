using System.Configuration;
using Abp;
using NLog;
using NLog.Config;

namespace Boss.Pim
{
    public class WinServiceBootstrap
    {
        private static readonly AbpBootstrapper _bs = AbpBootstrapper.Create<WinServiceModule>();

        public void Start()
        {
            LogManager.Configuration = new XmlLoggingConfiguration(ConfigurationManager.AppSettings["NlogConfigFilePath"]);
            _bs.Initialize();
        }

        public void Stop()
        {
            _bs.Dispose();
        }
    }
}
