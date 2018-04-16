namespace Boss.Pim.Sdk.Eastmoney.Responses
{
    public class DiagnoseDatas
    {
        public string FCODE { get; set; }
        public string SHORTNAME { get; set; }
        public string FTYPE { get; set; }
        public string FUNDTYPE { get; set; }
        public string FEATURE { get; set; }
        public string DWJZ { get; set; }
        public string RZDF { get; set; }
        public string SYI { get; set; }
        public string FTYI { get; set; }
        public string TEYI { get; set; }
        public string FSRQ { get; set; }
        public string SYL_Y { get; set; }
        public string SYL_3Y { get; set; }
        public string SE1 { get; set; }
        public string SRT1 { get; set; }
        /// <summary>
        /// 抗风险
        /// </summary>
        public string SDR1 { get; set; }
        public string SSP1 { get; set; }
        /// <summary>
        /// 稳定性
        /// </summary>
        public string SSTD1 { get; set; }
        /// <summary>
        /// 跟踪误差
        /// </summary>
        public string STRK1 { get; set; }
        public string SPTRK1 { get; set; }
        /// <summary>
        /// 超额收益
        /// </summary>
        public string SINFO1 { get; set; }
        /// <summary>
        /// 选证能力
        /// </summary>
        public string SCS1 { get; set; }
        /// <summary>
        /// 择时能力
        /// </summary>
        public string SCT1 { get; set; }
        /// <summary>
        /// 管理规模
        /// </summary>
        public string SNAV1 { get; set; }
        /// <summary>
        /// 收益率
        /// </summary>
        public string SY1 { get; set; }
        /// <summary>
        /// 综合评分（10分制）
        /// </summary>
        public string FGOLD { get; set; }
        /// <summary>
        /// 打败同类百分比
        /// </summary>
        public float PROWIN { get; set; }
    }
}