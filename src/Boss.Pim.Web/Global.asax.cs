using System;
using System.Configuration;
using Abp.Web;
using Castle.Facilities.Logging;

namespace Boss.Pim.Web
{
    public class MvcApplication : AbpWebApplication<PimWebModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            AbpBootstrapper.IocManager
.IocContainer.AddFacility<LoggingFacility>(f =>
f.UseNLog()
.WithConfig(ConfigurationManager.AppSettings["NLogConfigFilePath"]));

            base.Application_Start(sender, e);
        }
    }
}