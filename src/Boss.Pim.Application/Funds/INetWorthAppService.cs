using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Boss.Pim.Funds.Dto;

namespace Boss.Pim.Funds
{
    public interface INetWorthAppService : IAsyncCrudAppService<NetWorthDto, Guid>
    {
        Task Download3Days();

        Task DownloadByDays(string fundCode, int page, int size);

        Task DownloadCheck();

    }
}