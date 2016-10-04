namespace Aisino.Fwkp.Fpzpz.Common
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    internal class DingYiZhiFuChuan
    {
        public static byte[] _byteDefault = new byte[] { 210, 0xd3, 0xd0, 0xd9, 0xd7, 0xde, 0xd6, 0xd1, 0xd4, 0xd5, 0xdb, 0xd8, 0xda };
        public static byte[] _bytePos = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        public static byte[] _byteTransfer = new byte[0x10];
        public static string[] _strChuanChuType = new string[] { "已传出", "未传出", "全部传出" };
        public static string[] _strFPZL = new string[] { "专用发票", "普通发票" };
        public static string[] _strFPZL_s_c = new string[] { "s", "c" };
        public static string[] _strHead = new string[] { "FPZL", "FPDM", "FPHM", "KPRQ", "XFSH", "GFSH", "JE", "SE", "MW", "JMBBH", "JSRQ", "CCZT", "HIDE" };
        public static string[] _strHeadName = new string[] { "发票种类", "发票代码", "发票号码", "开票日期", "销方税号", "购方税号", "金额", "税额", "密文", "加密版本号", "接收日期", "传出状态", "发票种类(s,c)" };
        public static readonly string A6GuidUtil = "A6用户Guid";
        public static readonly string A6KHMsgWrite = "A6客户地区信息写入";
        public static readonly string A6LinkZhiFuChuan_KeMuSelect = "/gl/pz/refer_account.jsp?";
        public static readonly string A6LinkZhiFuChuan_PingZheng = "/gl/pz/voucher.jsp?";
        public static string A6ServerLink = string.Empty;
        public static readonly string A6ServerLinkUtil = "A6服务器Link";
        public static string A6SuitGuid = string.Empty;
        public static readonly string A6SuitUtil = "A6账套Suit";
        public static string A6UserGuid = string.Empty;
        public static Color colorReadOnly = Color.Yellow;
        public static Color colorReadOnlyNot = Color.White;
        public static readonly string[] CpkmItem = new string[] { "按存货分类", "按存货" };
        public static readonly string CpkmItemValue = "U3产品销售科目依据-产品科目选项";
        public static DateTime dataTimeCCRQ = new DateTime(0x76b, 12, 30, 0x17, 0x3b, 0x3b);
        public static Dictionary<string, object> dict = new Dictionary<string, object>();
        public static EmailOutFilePromptType emailOutFilePromptType = EmailOutFilePromptType.FpEmailSend_Enum;
        public static readonly string[] FPZPZ_Fpzl = new string[] { "全部发票", "专用发票", "普通发票" };
        public static readonly string[] FPZPZ_FpzlData = new string[] { "", "s", "c" };
        public static Color GroupBoxBackColor = Color.FromArgb(0xf2, 0xf4, 0xf5);
        public static int iByteLength = 0x100000;
        public static bool isA6PzVersion = true;
        public static byte[] key = new byte[] { 
            1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 
            7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2
         };
        public static readonly string[] KHBMCulmnDataName = new string[] { 
            "BM", "MC", "JM", "SJBM", "KJM", "SH", "DZDH", "JWBZ", "YHZH", "YJDZ", "BZ", "YSKM", "DQBM", "DQMC", "DQKM", "SFZJY", 
            "WJ"
         };
        public static readonly string[] KHBMCulmnHeaderText = new string[] { 
            "编码", "名称", "简码", "上级编码", "快捷码", "税号", "地址电话", "境外标志", "银行帐号", "邮件地址", "备注", "应收科目", "地区编码", "地区名称", "地区科目", "身份证校验", 
            "文件类型"
         };
        public static readonly int KMMaxInputLength = 20;
        public static readonly DateTime MinDate = new DateTime(0x76c, 1, 1);
        public static readonly string[] NSKSubValue_Qtkm_ZdfsItem = new string[] { "明细到客户", "明细到发票", "汇总" };
        public static readonly string NSKSubValue_Qtkm_ZdfsItemValue = "非受控科目制单方式-其他科目制单方式";
        public static readonly string PathZhiDanLog = @"c:\temp";
        public static readonly string[] PosiInvOpType_Zfsfp_ZdsItem = new string[] { "借贷方向相同，金额正负相反", "借贷方向相反，金额正负相同" };
        public static readonly string PosiInvOpType_Zfsfp_ZdsItemValue = "A6负数发票处理方式-正负数发票制单时";
        public static readonly string[] PZCulmnDataName = new string[] { "PZRQ", "PZLB", "PZHM", "PZYWH", "PZZT", "KPRQ", "FPZL", "FPDM", "FPHM", "GFMC", "GFSH", "ZFBZ", "PZWLYWH" };
        public static readonly string[] PZCulmnHeaderText = new string[] { "凭证日期", "凭证类型", "凭证号码", "凭证业务号", "凭证状态", "开票日期", "发票种类", "发票代码", "发票号码", "购方名称", "购方税号", "作废标志", "往来业务号" };
        public static readonly string[] PZFLBCulmnDataName = new string[] { "ZH", "DJXX", "KHBH", "JE", "KM", "JDBZ", "SPBH", "SL", "DJ", "HSBZ", "KPRQ", "JLDW" };
        public static readonly string[] PZFLBCulmnHeaderText = new string[] { "组号", "单据信息", "用户编码", "发票金额", "科目", "借贷标志", "商品编码", "数量", "单价", "核算标志", "开票日期", "计量单位" };
        public static readonly string ServerEmpty = "FPZPZ-000001";
        public static readonly string[] SKSubValue_Yskm_ZdfsItem = new string[] { "明细到客户", "明细到发票" };
        public static readonly string SKSubValue_Yskm_ZdfsItemValue = "受控科目制单方式-应收科目制单方式";
        public static readonly string[] SPBMCulmnDataName = new string[] { 
            "BM", "MC", "JM", "SJBM", "KJM", "SLV", "SPSM", "GGXH", "JLDW", "DJ", "HSJBZ", "XSSRKM", "YJZZSKM", "XSTHKM", "HYSY", "SPZL", 
            "SPSX", "WJ"
         };
        public static readonly string[] SPBMCulmnHeaderText = new string[] { 
            "编码", "名称", "简码", "上级编码", "快捷码", "税率", "商品税目", "规格型号", "计量单位", "单价", "含税价标志", "销售收入科目", "应缴增值税科目", "销售退回科目", "海洋石油", "商品种类", 
            "商品属性", "文件类型"
         };
        public static readonly string StartStopPfzpzJieKou = "启用停用发票转凭证接口";
        public static string strBianMaFangShiSelect = "编码方式选择";
        public static string[] strBianMaFangShiValue = new string[] { "meMIME", "meUU" };
        public static string strCCRQ = "1899-12-30 23:59:59";
        public static string strCS = "CS";
        public static string[] strDaYinDefault = new string[] { "成功", "不成功" };
        public static string strDklfpcc = "抵扣联发票传出-文件传出路径";
        public static string strDklfpjs_JieZhi = "抵扣联发票接收-从介质读入-文件传出路径";
        public static string strDR = "DR";
        public static string strDyHouZhiJieSend = "发票填开打印后直接进行发送";
        public static string strEmailGeShi = ".zip";
        public static string strEmailTitle = "HTXXFWSKKP_FPRZJK_XML";
        public static readonly string strErrLinkFailTip = "FPZPZ-000004";
        public static string strErrorAdressMoRen = "http://www.cnnsr.com.cn/support/yjdp";
        public static string strErrorChaKanAddress = "错误信息查看地址";
        public static string strErrorEmailReceRen = "错误信息收件人地址";
        public static string strErrorEmailTitle = "错误信息邮件主题";
        public static string strErrorLinkRen = "错误信息联系人地址";
        public static string strFenGeFu = ",";
        public static string strFphmFormat = "{0:00000000}";
        public static string strFPTKSendEmailFangShi = "发票填开邮件发送方式--邮件或者介质";
        public static string[] strFPZL = new string[] { "专用发票", "普通发票", "机动车发票", "货运发票" };
        public static string[] strFPZL_s_c = new string[] { "s", "c", "j", "f" };
        public static string strHH_mm_ss_Formart = "HH:mm:ss";
        public static string strHHmmss_Formart = "HHmmss";
        public static string strKaiZhangMonth = "开帐月份";
        public static string strMonth_Day_Year_Formart = "MM/dd/yyyy";
        public static string strPathConfigureKzyf = @"\Config\Common\Kzyf.Configure.XML";
        public static string strReceEmailPOP3 = "接收邮件-POP3-";
        public static string strReceEmailPOP3_DuanKouHao = "接收邮件-POP3-端口号";
        public static string strReceEmailServerPassWord = "邮件接收服务器-密码";
        public static string strReceEmailServerRemberPassWord = "邮件接收服务器-记住密码";
        public static string strReceEmailServerUser = "邮件接收服务器-账户名";
        public static string strRecePersonAdressQueRen = "发送时进行收件人地址确认";
        public static string[] strRefaultJieZhiIn = new string[] { "导入成功", "导入失败" };
        public static string[] strRefaultJieZhiOut = new string[] { "导出成功", "导出失败" };
        public static string[] strRefaultReceEmail = new string[] { "接收成功", "接收失败" };
        public static string[] strRefaultSendEmail = new string[] { "发送成功", "发送失败" };
        public static readonly string[] strRight_Wrong = new string[] { "0", "1" };
        public static string strSendDiskFPCX = "发票查询发票传出为介质";
        public static string strSendEmailJianGe = "发送邮件时间间隔";
        public static string strSendEmailNum = "一次连接连续发送邮件数";
        public static string strSendEmailPersonAddress = "发件人邮件地址";
        public static string strSendEmailServerPassWord = "邮件发送服务器-密码";
        public static string strSendEmailServerRemberPassWord = "邮件发送服务器-记住密码";
        public static string strSendEmailServerShenFenYanZheng = "邮件发送服务器-我的服务器要求身份验证";
        public static string strSendEmailServerUser = "邮件发送服务器-账户名";
        public static string strSendEmailSMTP = "发送邮件-SMTP-";
        public static string strSendEmailSMTP_DuanKouHao = "发送邮件-SMTP-端口号";
        public static string strSendItemQueDing = "每次发送时都进行发送选项设置";
        public static string strServerDeleteEmail = "成功接收邮件后-从邮件服务器删除邮件";
        public static string[] strSQLID = new string[] { "aisino.fwkp.Fpzpz.CreateTempTable", "aisino.fwkp.Fpzpz.DropTempTable", "aisino.fwkp.Fpzpz.EmptyTempTable", "aisino.fwkp.Fpzpz.ExistTempTable" };
        public static string[] strTableNameTemp = new string[] { "DQKMB", "KHKMB", "PZFLB", "SPKMB" };
        public static string strTempPathEmail = @"\EmailFile\邮件";
        public static string strTempPathEmailError = @"\EmailFile\ErrInfo";
        public static string strTitleEmailSend = "邮件发送结果";
        public static string strTitleJieZhiOut = "介质导出结果";
        public static string strYear_Month_Day_Formart = "yyyy-MM-dd";
        public static string strYear_Month_Day_HHmmss_Formart = "yyyy-MM-dd HH:mm:ss";
        public static string strYearMonthDay_Formart = "yyyyMMdd";
        public static string strYearMonthDayHHmmss_Formart = "yyyyMMddHHmmss";
        public static string[] strZhiDuanEmailSend = new string[] { "FromEmail", "ToEmail", "ContentEmail", "FileEmail", "RefaultSend", "BeiZhuSend" };
        public static string[] strZhiDuanJieZhiOut = new string[] { "ReceTaxJieZhi", "FilePathJieZhi", "OutFileJieZhi", "RefaultJieZhi", "BeiZhuJieZhi" };
        public static readonly string UserEmpty = "FPZPZ-000002";
        public static readonly string UserSuitEmpty = "FPZPZ-000003";
        public static byte[] vector = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6 };
        public static Dictionary<string, List<string>> XmlFieldInfo = new Dictionary<string, List<string>>();
        public static readonly string Xssrkm = "U3销售收入科目-销售收入科目";
        public static readonly string Xsthkm = "U3销售退回科目-销售退回科目";
        public static readonly string[] Xtsp_ZdsItem = new string[] { "单价不同不合并", "单价不同合并,计算平均单价" };
        public static readonly string Xtsp_ZdsItemValue = "A6相同商品制单时-相同商品制单时";
        public static readonly string[] XXFPCulmnDataName = new string[] { 
            "FPZL", "FPDM", "FPHM", "KPJH", "XSDJBH", "GFMC", "GFSH", "GFDZDH", "GFYHZH", "XFMC", "XFSH", "XFDZDH", "XFYHZH", "XSBM", "YDXS", "JMBBH", 
            "MW", "KPRQ", "SSYF", "HJJE", "SLV", "HJSE", "ZYSPMC", "SPSM", "JZPZHM", "BZ", "KPR", "FHR", "SKR", "DYBZ", "QDBZ", "GFBH", 
            "HZTZDH", "SPBH", "ZFBZ", "BSBZ", "DJBZ", "WKBZ", "XFBZ", "JYM", "BSQ", "XZBZ", "PZLB", "PZHM", "PZYWH", "PZZT", "PZRQ", "HXM", 
            "SYH", "SBBZ", "YYSBZ", "XZBZ", "JSHJ", "PZWLYWH"
         };
        public static readonly string[] XXFPCulmnHeaderText = new string[] { 
            "发票种类", "发票代码", "发票号码", "开票机号", "销售单据编号", "购方名称", "购方税号", "购方地址电话", "购方银行帐号", "销方名称", "销方税号", "销方地址电话", "销方银行帐号", "销售部门", "异地销售", "加密版本号", 
            "密文", "开票日期", "所属月份", "合计金额", "税率", "合计税额", "主要商品名称", "商品税目", "记帐凭证号码", "备注", "开票人", "复核人", "收款人", "打印标志", "清单标志", "购方编号", 
            "红字通知单号", "商品编号", "作废标志", "报税标志", "登记标志", "外开标志", "修复标志", "校验码", "报税期", "选择标志", "凭证类型", "凭证号码", "凭证业务号", "凭证状态", "凭证日期", "汉信码", 
            "索引号", "设置标志", "营业税标志", "选择标志", "价税合计"
         };
        public static readonly string[] XZQYBMCulmnDataName = new string[] { "BM", "MC" };
        public static readonly string[] XZQYBMCulmnHeaderText = new string[] { "编码", "名称" };
        public static readonly string Yjzzskm = "U3应缴增值税科目-应交增值税科目";
        public static readonly string[] YskmItem = new string[] { "按客户分类", "按客户", "按地区分类" };
        public static readonly string YskmItemValue = "U3控制科目依据-应收科目选项";
        public static readonly string Ysrkm = "U3应收科目-应收入科目";

        public static void GetLinkUserSuit()
        {
            try
            {
                string str = PropertyUtil.GetValue(A6ServerLinkUtil);
                string str2 = PropertyUtil.GetValue(A6SuitUtil);
                string str3 = PropertyUtil.GetValue(A6GuidUtil);
                A6SuitGuid = str2.Trim();
                A6UserGuid = str3.Trim();
                A6ServerLink = str.Trim();
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
        }

        public static void GetXsth_Yjzzs_Xssr_YsrKm()
        {
        }

        public static void Initialize()
        {
            try
            {
                if (string.IsNullOrEmpty(PropertyUtil.GetValue(YskmItemValue)))
                {
                    PropertyUtil.SetValue(YskmItemValue, YskmItem[0]);
                }
                if (string.IsNullOrEmpty(PropertyUtil.GetValue(CpkmItemValue)))
                {
                    PropertyUtil.SetValue(CpkmItemValue, CpkmItem[0]);
                }
                if (string.IsNullOrEmpty(PropertyUtil.GetValue(SKSubValue_Yskm_ZdfsItemValue)))
                {
                    PropertyUtil.SetValue(SKSubValue_Yskm_ZdfsItemValue, SKSubValue_Yskm_ZdfsItem[0]);
                }
                if (string.IsNullOrEmpty(PropertyUtil.GetValue(NSKSubValue_Qtkm_ZdfsItemValue)))
                {
                    PropertyUtil.SetValue(NSKSubValue_Qtkm_ZdfsItemValue, NSKSubValue_Qtkm_ZdfsItem[0]);
                }
                if (string.IsNullOrEmpty(PropertyUtil.GetValue(PosiInvOpType_Zfsfp_ZdsItemValue)))
                {
                    PropertyUtil.SetValue(PosiInvOpType_Zfsfp_ZdsItemValue, PosiInvOpType_Zfsfp_ZdsItem[0]);
                }
                if (string.IsNullOrEmpty(PropertyUtil.GetValue(Xtsp_ZdsItemValue)))
                {
                    PropertyUtil.SetValue(Xtsp_ZdsItemValue, Xtsp_ZdsItem[0]);
                }
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
        }

        public enum EmailOutFilePromptType
        {
            FpEmailSend_Enum,
            FpJieZhiOut_Enum
        }
    }
}

