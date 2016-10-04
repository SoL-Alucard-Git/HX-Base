namespace Aisino.Fwkp.DataMigrationTool.Common
{
    using Aisino.Framework.Plugin.Core;
    using System;
    using System.Collections.Generic;

    public class DingYiZhiFuChuan
    {
        public static Dictionary<string, Dictionary<int, string>> dictDTableNameC = new Dictionary<string, Dictionary<int, string>>();
        public static Dictionary<string, Dictionary<int, string>> dictDTableNameCB = new Dictionary<string, Dictionary<int, string>>();
        public static string strFphm = "发票号码";
        public static string strFphmFormat = "{0:00000000}";
        public static string strFuZhi = "迁移";
        public static string strFuZhiing = "正在迁移";
        public static string strGfdzdh = "购方地址电话";
        public static string strGfmc = "购方名称";
        public static string strGfsh = "购方税号";
        public static string strGfyhzh = "购方银行帐号";
        public static string strHH_mm_ss_Formart = "HH:mm:ss";
        public static string strHHmmss_Formart = "HHmmss";
        public static string strHXMXMLName = @"\XXFP_TO_XML.XML";
        public static string strLbdm = "类别代码";
        public static string strMW = "密文";
        public static string strOldXXFPName = "销项发票";
        public static string strParadoxPath = string.Empty;
        public static string strParadoxPathText = "旧版开票软件数据库位置：";
        public static string strPathConfigure = @"\Config\DataMigrationTool\Aisino.Fwkp.DataMigrationTool.Configure.XML";
        public static string strPathConfigureKzyf = @"\Config\Common\Kzyf.Configure.XML";
        public static string[] strProgressBar = new string[] { ".", "..", "..." };
        public static string strShowParadoxPath = string.Empty;
        public static string strSsyf = "所属月份";
        public static string strStatement = "数据迁移工具功能：此工具用于旧版开票软件升级为新版开票软件时，旧版开票软件数据库数据迁移到新版开票软件数据库当中";
        public static string[] strTableNameC = new string[] { 
            "XXFP", "XXFP_XHQD", "XXFP_MX", "JXFP", "HZFP_SQD", "HZFP_SQD_MX", "HZFPHY_SQD", "HZFPHY_SQD_MX", "XSDJ", "XSDJ_MX", "XSDJ_MX_HY", "XSDJ_HY", "BM_SPSM", "BM_KH", "BM_SP", "CZY", 
            "FPZLBM", "FPZLDM", "FPLBBM", "BM_XZQY", "系统参数表", "XTSWXX", "BMBM", "BM_SFHR", "BM_FYXM", "BM_GHDW", "BM_CL", "BM_XHDW"
         };
        public static string[] strTableNameCB = new string[] { 
            "销项发票", "销项发票销货清单", "销项发票明细", "进项发票", "红字发票申请单", "红字发票申请单明细", "货运红字发票申请单", "货运红字申请单明细", "销售单据", "销售单据明细", "销售单据明细还原", "销售单据还原", "商品税目编码", "客户编码", "商品编码", "操作员", 
            "发票种类编码", "发票种类代码表", "发票类别编码", "行政区域编码", "系统参数表", "系统税务信息", "部门编码", "发货人编码", "费用项目编码", "购货单位编码", "车辆编码", "销方编码"
         };
        public static string strTableNameCB_BM = "编码";
        public static string strTableNameCB_CLBM = "车辆编码";
        public static string strTableNameCB_FYXMBM = "费用项目编码";
        public static string strTableNameCB_GHDWBM = "购货单位编码";
        public static string strTableNameCB_KHBM = "客户编码";
        public static string strTableNameCB_MC = "名称";
        public static string strTableNameCB_SFHRBM = "发货人编码";
        public static string strTableNameCB_SJMC = "上级码长";
        public static string strTableNameCB_SPBM = "商品编码";
        public static string strTableNameCB_WJ = "文件类型";
        public static string strTableNameCB_XHDWBM = "销方编码";
        public static string strTableNameCB_XJMC = "下级码长";
        public static string[] strTableType = new string[] { "发票信息表-询问覆盖或者跳过", "申请单信息表-询问覆盖或者跳过", "单据信息表-询问覆盖或者跳过", "其他信息表-覆盖" };
        public static string strVerOld = string.Empty;
        public static string strVerOldText = "旧版开票软件版本：";
        public static string strWanCheng = "完成";
        public static string[] strXXFPFieldNameC = new string[] { 
            "FPZL", "FPDM", "FPHM", "KPJH", "XSDJBH", "GFMC", "GFSH", "GFDZDH", "GFYHZH", "XFMC", "XFSH", "XFDZDH", "XFYHZH", "XSBM", "YDXS", "JMBBH", 
            "MW", "KPRQ", "SSYF", "HJJE", "SLV", "HJSE", "ZYSPMC", "SPSM", "JZPZHM", "BZ", "KPR", "FHR", "SKR", "DYBZ", "QDBZ", "GFBH", 
            "BMBH", "SPBH", "ZFBZ", "BSBZ", "DJBZ", "WKBZ", "XFBZ", "JYM", "BSQ", "XZBZ", "PZLB", "PZHM", "PZYWH", "PZZT", "PZRQ", "HXM", 
            "SYH", "SBBZ", "YYSBZ"
         };
        public static string strYear_Month = "yyyyMM";
        public static string strYear_Month_Day_Formart = "yyyy-MM-dd";
        public static string strYear_Month_Day_HHmmss_Formart = "yyyy-MM-dd HH:mm:ss";
        public static string strYearMonthDay_Formart = "yyyyMMdd";
        public static string strYearMonthDayHHmmss_Formart = "yyyyMMddHHmmss";

        public static void Initialize()
        {
            try
            {
                dictDTableNameCB.Clear();
                Dictionary<int, string> dictionary = null;
                int num = 0;
                int num2 = 0;
                dictionary = new Dictionary<int, string>();
                int num3 = 1;
                dictionary.Add(num3++, strTableNameCB[num2++]);
                dictionary.Add(num3++, strTableNameCB[num2++]);
                dictionary.Add(num3++, strTableNameCB[num2++]);
                dictionary.Add(num3++, strTableNameCB[num2++]);
                dictDTableNameCB.Add(strTableType[num++], dictionary);
                dictionary = new Dictionary<int, string>();
                int num4 = 1;
                dictionary.Add(num4++, strTableNameCB[num2++]);
                dictionary.Add(num4++, strTableNameCB[num2++]);
                dictionary.Add(num4++, strTableNameCB[num2++]);
                dictionary.Add(num4++, strTableNameCB[num2++]);
                dictDTableNameCB.Add(strTableType[num++], dictionary);
                dictionary = new Dictionary<int, string>();
                int num5 = 1;
                dictionary.Add(num5++, strTableNameCB[num2++]);
                dictionary.Add(num5++, strTableNameCB[num2++]);
                dictionary.Add(num5++, strTableNameCB[num2++]);
                dictionary.Add(num5++, strTableNameCB[num2++]);
                dictDTableNameCB.Add(strTableType[num++], dictionary);
                dictionary = new Dictionary<int, string>();
                int num6 = 1;
                dictionary.Add(num6++, strTableNameCB[num2++]);
                dictionary.Add(num6++, strTableNameCB[num2++]);
                dictionary.Add(num6++, strTableNameCB[num2++]);
                dictionary.Add(num6++, strTableNameCB[num2++]);
                dictionary.Add(num6++, strTableNameCB[num2++]);
                dictionary.Add(num6++, strTableNameCB[num2++]);
                dictionary.Add(num6++, strTableNameCB[num2++]);
                dictionary.Add(num6++, strTableNameCB[num2++]);
                dictionary.Add(num6++, strTableNameCB[num2++]);
                dictionary.Add(num6++, strTableNameCB[num2++]);
                dictionary.Add(num6++, strTableNameCB[num2++]);
                dictionary.Add(num6++, strTableNameCB[num2++]);
                dictionary.Add(num6++, strTableNameCB[num2++]);
                dictionary.Add(num6++, strTableNameCB[num2++]);
                dictionary.Add(num6++, strTableNameCB[num2++]);
                dictionary.Add(num6++, strTableNameCB[num2++]);
                dictDTableNameCB.Add(strTableType[num++], dictionary);
                dictDTableNameC.Clear();
                Dictionary<int, string> dictionary2 = null;
                int num7 = 0;
                int num8 = 0;
                dictionary2 = new Dictionary<int, string>();
                int num9 = 1;
                dictionary2.Add(num9++, strTableNameC[num8++]);
                dictionary2.Add(num9++, strTableNameC[num8++]);
                dictionary2.Add(num9++, strTableNameC[num8++]);
                dictionary2.Add(num9++, strTableNameC[num8++]);
                dictDTableNameC.Add(strTableType[num7++], dictionary2);
                dictionary2 = new Dictionary<int, string>();
                int num10 = 1;
                dictionary2.Add(num10++, strTableNameC[num8++]);
                dictionary2.Add(num10++, strTableNameC[num8++]);
                dictDTableNameC.Add(strTableType[num7++], dictionary2);
                dictionary2 = new Dictionary<int, string>();
                int num11 = 1;
                dictionary2.Add(num11++, strTableNameC[num8++]);
                dictionary2.Add(num11++, strTableNameC[num8++]);
                dictionary2.Add(num11++, strTableNameC[num8++]);
                dictionary2.Add(num11++, strTableNameC[num8++]);
                dictDTableNameC.Add(strTableType[num7++], dictionary2);
                dictionary2 = new Dictionary<int, string>();
                int num12 = 1;
                dictionary2.Add(num12++, strTableNameC[num8++]);
                dictionary2.Add(num12++, strTableNameC[num8++]);
                dictionary2.Add(num12++, strTableNameC[num8++]);
                dictionary2.Add(num12++, strTableNameC[num8++]);
                dictionary2.Add(num12++, strTableNameC[num8++]);
                dictionary2.Add(num12++, strTableNameC[num8++]);
                dictionary2.Add(num12++, strTableNameC[num8++]);
                dictionary2.Add(num12++, strTableNameC[num8++]);
                dictionary2.Add(num12++, strTableNameC[num8++]);
                dictionary2.Add(num12++, strTableNameC[num8++]);
                dictionary2.Add(num12++, strTableNameC[num8++]);
                dictionary2.Add(num12++, strTableNameC[num8++]);
                dictionary2.Add(num12++, strTableNameC[num8++]);
                dictionary2.Add(num12++, strTableNameC[num8++]);
                dictionary2.Add(num12++, strTableNameC[num8++]);
                dictDTableNameC.Add(strTableType[num7++], dictionary2);
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
    }
}

