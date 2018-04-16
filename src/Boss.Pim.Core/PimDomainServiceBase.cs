using Abp.Domain.Services;
using Abp.Events.Bus;
using Abp.Linq;

namespace Boss.Pim
{
    public abstract class PimDomainServiceBase : DomainService
    {
        public IAsyncQueryableExecuter AsyncQueryableExecuter { get; set; }

        protected PimDomainServiceBase()
        {
            LocalizationSourceName = PimConsts.LocalizationSourceName;
            AsyncQueryableExecuter = NullAsyncQueryableExecuter.Instance;
        }

    }
}
