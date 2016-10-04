namespace Aisino.Fwkp.Sjbf.Common
{
    using Framework.Plugin.Core;
    using System;

    internal class ZhiFuChuan
    {
        public static readonly string strBeginningMonthCopy = "每月月初备份数据";
        public static readonly string strDateBaseName = "cc3268.dll";
        public static readonly string strDateBaseZipName = "FWSK";
        public static readonly string strDateTimeFormat = "yyyyMMddHHmmss";
        public static readonly string strDateTimeFormat1 = "yyyy-MM-dd HH-mm-ss";
        public static string strDefaultPathCopy = string.Concat(new object[] { @"D:\开票软件数据备份\", TaxCardFactory.CreateTaxCard().TaxCode, "-", TaxCardFactory.CreateTaxCard().Machine });
        public static readonly string strEndRunCopy = "程序每次运行结束备份数据";
        public static readonly string strIntervalTimeFormat = "yyyy-MM-dd";
        public static readonly string strIntervalTimeNum = "间隔一定时间备份-数目";
        public static readonly string strIntervalTimeType = "间隔一定时间备份类型-天或者月";
        public static readonly string[] strIntervalTimeTypeValue = new string[] { "天", "月" };
        public static readonly string strPathCopy = "数据备份路径";
        public static readonly string strPreIntervalTimeCopy = "上一次间隔时间备份日期";
        public static readonly string strRight = "1";
        public static readonly string strWrong = "0";
    }
}

