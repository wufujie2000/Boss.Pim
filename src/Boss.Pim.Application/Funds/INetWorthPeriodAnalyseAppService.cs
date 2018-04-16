﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Boss.Pim.Funds.Dto;

namespace Boss.Pim.Funds
{
    public interface INetWorthPeriodAnalyseAppService : IAsyncCrudAppService<NetWorthPeriodAnalyseDto, Guid>
    {
        Task AsyncDownoad();

        Task DownoadByFundCode(string fundCodes);
    }
}