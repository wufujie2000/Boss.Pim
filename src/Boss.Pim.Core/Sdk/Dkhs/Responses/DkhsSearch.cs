using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boss.Pim.Sdk.Dkhs.Responses
{
    public class DkhsSearch
    {
        public int total_count { get; set; }
        public int current_page { get; set; }
        public DkhsSearchResult[] results { get; set; }
        public int total_page { get; set; }
    }

    public class DkhsSearchResult
    {
        public int id { get; set; }
        public string chi_spell { get; set; }
        public string abbr_name { get; set; }
        public string symbol { get; set; }
        public string code { get; set; }
        public int symbol_type { get; set; }
        public int is_stop { get; set; }
        public int list_status { get; set; }
        public int symbol_stype { get; set; }
        public string chi_spell_all { get; set; }
        public bool followed { get; set; }
    }

}
