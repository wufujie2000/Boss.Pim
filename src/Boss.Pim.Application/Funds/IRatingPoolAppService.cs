using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Boss.Pim.Funds.Dto;

namespace Boss.Pim.Funds
{
    public interface IRatingPoolAppService : IAsyncCrudAppService<RatingPoolDto>
    {
        Task DownloadStockstar();
    }
}