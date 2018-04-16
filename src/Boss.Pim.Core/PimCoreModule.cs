using System.Reflection;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Zero;
using Abp.Zero.Configuration;
using Boss.Pim.Authorization;
using Boss.Pim.Authorization.Roles;
using Boss.Pim.Authorization.Users;
using Boss.Pim.Configuration;
using Boss.Pim.MultiTenancy;

namespace Boss.Pim
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class PimCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            //Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            //Remove the following line to disable multi-tenancy.
            Configuration.MultiTenancy.IsEnabled = PimConsts.MultiTenancyEnabled;

            //Add/remove localization sources here
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    PimConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "Boss.Pim.Localization.Source"
                        )
                    )
                );

            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Authorization.Providers.Add<PimAuthorizationProvider>();

            Configuration.Settings.Providers.Add<AppSettingProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
