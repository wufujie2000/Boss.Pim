using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Swagger;

namespace Boss.Pim.Api
{
    [DependsOn(typeof(AbpWebApiModule), typeof(PimApplicationModule))]
    public class PimWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.SwaggerApi(
                new Dictionary<Type, string>()
                {
                    [typeof(PimApplicationModule)] = "app"
                }, true);

            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));
        }
    }
}
