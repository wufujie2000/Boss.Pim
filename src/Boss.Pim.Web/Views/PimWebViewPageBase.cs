using Abp.Web.Mvc.Views;

namespace Boss.Pim.Web.Views
{
    public abstract class PimWebViewPageBase : PimWebViewPageBase<dynamic>
    {

    }

    public abstract class PimWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected PimWebViewPageBase()
        {
            LocalizationSourceName = PimConsts.LocalizationSourceName;
        }
    }
}