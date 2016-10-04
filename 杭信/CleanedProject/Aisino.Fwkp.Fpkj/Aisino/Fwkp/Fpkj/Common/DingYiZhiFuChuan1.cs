namespace Aisino.Fwkp.Fpkj.Common
{
    using Aisino.Fwkp.Fpkj.Model;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    internal class DingYiZhiFuChuan1
    {
        public static byte[] _byteDefault = new byte[] { 210, 0xd3, 0xd0, 0xd9, 0xd7, 0xde, 0xd6, 0xd1, 0xd4, 0xd5, 0xdb, 0xd8, 0xda };
        public static byte[] _bytePos = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        public static byte[] _byteTransfer = new byte[0x10];
        public static string[] _strChuanChuType = new string[] { "已传出", "未传出", "全部传出" };
        public static string[] _strFPZL = new string[] { "专用发票", "普通发票" };
        public static string[] _strFPZL_s_c = new string[] { "s", "c" };
        public static string[] _strHead = new string[] { "FPZL", "FPDM", "FPHM", "KPRQ", "XFSH", "GFSH", "JE", "SE", "MW", "JMBBH", "JSRQ", "CCZT", "HIDE" };
        public static string[] _strHeadName = new string[] { "发票种类", "类别代码", "发票号码", "开票日期", "销方税号", "购方税号", "金额", "税额", "密文", "加密版本号", "接收日期", "传出状态", "发票种类(s,c)" };
        public static UserMsg _UserMsg = new UserMsg();
        public static Color colorReadOnly = Color.Yellow;
        public static Color colorReadOnlyNot = Color.White;
        public static DateTime dataTimeCCRQ = new DateTime(0x76b, 12, 30, 0x17, 0x3b, 0x3b);
        public static Dictionary<string, object> dict = new Dictionary<string, object>();
        public static EmailOutFilePromptType emailOutFilePromptType = EmailOutFilePromptType.FpEmailSend_Enum;
        public static Color GroupBoxBackColor = Color.FromArgb(0xf2, 0xf4, 0xf5);
        public static int iByteLength = 0x100000;
        public static byte[] key = new byte[] { 
            1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 
            7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2
         };
        public static string strCCRQ = "1899-12-30 23:59:59";
        public static string strCS = "CS";
        public static string[] strDaYinDefault = new string[] { "成功", "不成功" };
        public static string strDklfpcc = "抵扣联发票传出-文件传出路径";
        public static string strDklfpjs_JieZhi = "抵扣联发票接收-从介质读入-文件传出路径";
        public static string strDR = "DR";
        public static string strDyHouZhiJieSend = "发票填开打印后直接进行发送";
        public static string strEmailGeShi = ".zip";
        public static string strFphmFormat = "{0:00000000}";
        public static string strFPTKSendEmailFangShi = "发票填开邮件发送方式--邮件或者介质";
        public static string strHH_mm_ss_Formart = "HH:mm:ss";
        public static string strHHmmss_Formart = "HHmmss";
        public static string strKaiZhangMonth = "开帐月份";
        public static string strMonth_Day_Year_Formart = "MM/dd/yyyy";
        public static string strPathConfigureKzyf = @"\Config\Common\Kzyf.Configure.XML";
        public static string[] strRefaultJieZhiIn = new string[] { "导入成功", "导入失败" };
        public static string[] strRefaultJieZhiOut = new string[] { "导出成功", "导出失败" };
        public static string[] strRefaultReceEmail = new string[] { "接收成功", "接收失败" };
        public static string[] strRefaultSendEmail = new string[] { "发送成功", "发送失败" };
        public static string strSendItemQueDing = "每次发送时都进行发送选项设置";
        public static string strServerDeleteEmail = "成功接收邮件后-从邮件服务器删除邮件";
        public static string strTitleEmailSend = "邮件发送结果";
        public static string strTitleJieZhiOut = "介质导出结果";
        public static string strYear_Month_Day_Formart = "yyyy-MM-dd";
        public static string strYear_Month_Day_HHmmss_Formart = "yyyy-MM-dd HH:mm:ss";
        public static string strYearMonthDay_Formart = "yyyyMMdd";
        public static string strYearMonthDayHHmmss_Formart = "yyyyMMddHHmmss";
        public static string[] strZhiDuanEmailSend = new string[] { "FromEmail", "ToEmail", "ContentEmail", "FileEmail", "RefaultSend", "BeiZhuSend" };
        public static string[] strZhiDuanJieZhiOut = new string[] { "ReceTaxJieZhi", "FilePathJieZhi", "OutFileJieZhi", "RefaultJieZhi", "BeiZhuJieZhi" };
        public static byte[] vector = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6 };
        public static Dictionary<string, List<string>> XmlFieldInfo = new Dictionary<string, List<string>>();

        public static void Initialize()
        {
        }

        public enum EmailOutFilePromptType
        {
            FpEmailSend_Enum,
            FpJieZhiOut_Enum
        }
    }
}

