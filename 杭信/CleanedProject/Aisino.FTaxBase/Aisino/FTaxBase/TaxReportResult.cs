namespace Aisino.FTaxBase
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct TaxReportResult
    {
        public string ReportDate;
        public string NewOldFlag;
        public string CurPeriod;
        public string Period;
    }
}

