using System.Collections.Generic;

namespace Boss.Pim.Sdk.Eastmoney.Responses
{

    public class EastmoneyFundRateInfo
    {
        public FundRateInfoData Datas { get; set; }
        public int ErrCode { get; set; }
        public object ErrMsg { get; set; }
        public int TotalCount { get; set; }
        public object Expansion { get; set; }
    }


    /// <summary>
    /// 基金费率信息
    /// </summary>
    public class FundRateInfoData
    {
        //public string FEATURE { get; set; }
        //public string SGZT { get; set; }
        //public string SHZT { get; set; }
        //public string DTZT { get; set; }
        //public string MINSG { get; set; }
        //public string MINDT { get; set; }
        //public string MAXSG { get; set; }
        //public string MINSSG { get; set; }
        //public string MINSBSG { get; set; }
        //public string SSBCFMDATA { get; set; }
        //public string RDMCFMDATA { get; set; }
        //public string DRAWCFMDATA { get; set; }
        //public string STKTOCASH { get; set; }
        //public string IPESTART1 { get; set; }
        //public string IPEEND1 { get; set; }
        //public string SHIPESTART1 { get; set; }
        //public string SHIPEEND1 { get; set; }
        //public string KFR { get; set; }
        /// <summary>
        /// 管理费率
        /// </summary>
        public string MGREXP { get; set; }
        /// <summary>
        /// 托管费率
        /// </summary>
        public string TRUSTEXP { get; set; }
        /// <summary>
        /// 销售服务费
        /// </summary>
        public string SALESEXP { get; set; }

        //public string OPENTYPE { get; set; }
        //public string PRSVPERIOD { get; set; }
        //public string PRSVDATE { get; set; }
        //public string CYCLE { get; set; }
        //public string DUEDATE { get; set; }
        //public string OPESTART { get; set; }
        //public string OPEEND { get; set; }
        //public string ISNEW { get; set; }
        //public string ispublish { get; set; }
        //public object[] rghd { get; set; }
        //public object[] sghd { get; set; }
        //public object[] shhd { get; set; }
        /// <summary>
        /// 认购费率
        /// </summary>
        public List<FundRateData> rg { get; set; }

        /// <summary>
        /// 申购费率
        /// </summary>
        public List<FundRateData> sg { get; set; }

        /// <summary>
        /// 赎回费率
        /// </summary>
        public List<FundRateData> sh { get; set; }
    }

    public class FundRateData
    {
        public string money { get; set; }
        public string time { get; set; }
        public string source { get; set; }
        public string rate { get; set; }
    }
}