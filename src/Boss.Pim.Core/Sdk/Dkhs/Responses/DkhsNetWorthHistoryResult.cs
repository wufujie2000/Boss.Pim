using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boss.Pim.Sdk.Dkhs.Responses
{
    public class DkhsNetWorthHistoryResult
    {
        public int id { get; set; }
        public string tradedate { get; set; }
        public float net_value { get; set; }
        public float net_cumulative { get; set; }
        public float fac_unit_net { get; set; }
        public string percent { get; set; }
    }
}
