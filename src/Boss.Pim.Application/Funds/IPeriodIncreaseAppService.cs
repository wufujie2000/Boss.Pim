using System.Threading.Tasks;
using Abp.Application.Services;
using Boss.Pim.Funds.Dto;

namespace Boss.Pim.Funds
{
    public interface IPeriodIncreaseAppService : IAsyncCrudAppService<PeriodIncreaseDto>
    {
        Task AsyncDownoad();

        Task DownoadByFundCode(string fundCodes);

        Task DownloadRank();
    }
}