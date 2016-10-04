namespace Aisino.Fwkp.Wbjk.BLL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.DAL;
    using Aisino.Fwkp.Wbjk.Model;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Xml;

    public class FPCXbll
    {
        private int _currentPage = 1;
        private FPCXdal DAL_FPCX = new FPCXdal();
        private static ILog loger = LogUtil.GetLogger<FPCXbll>();

        public void ExportExel(string filePath, DataTable fpTable, string excelTitle, List<Dictionary<string, string>> listColumnsName)
        {
            new ExportToExcel().DataToExcel(filePath, fpTable, listColumnsName, excelTitle);
        }

        public void ExportXML(string xmlPath, DataTable fpTable)
        {
            string path = xmlPath.Remove(xmlPath.LastIndexOf(@"\"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
            document.AppendChild(newChild);
            XmlElement element = document.CreateElement("发票信息");
            document.AppendChild(element);
            if (fpTable.Rows.Count > 0)
            {
                string str2 = fpTable.Rows[0]["FPZL"].ToString();
                for (int i = 0; i < fpTable.Rows.Count; i++)
                {
                    XmlElement element3;
                    XmlElement element4;
                    XmlElement element5;
                    XmlElement element6;
                    XmlElement element7;
                    XmlElement element8;
                    XmlElement element9;
                    XmlElement element10;
                    XmlElement element2 = document.CreateElement(string.Format("第{0}张发票", i + 1));
                    if ((fpTable.Rows[i]["FPZL"].ToString() == "c") || (fpTable.Rows[i]["FPZL"].ToString() == "s"))
                    {
                        element3 = document.CreateElement("销售单据编号");
                        element3.InnerText = fpTable.Rows[i]["XSDJBH"].ToString();
                        element2.AppendChild(element3);
                        element4 = document.CreateElement("发票种类");
                        element4.InnerText = fpTable.Rows[i]["FPZL"].ToString();
                        element2.AppendChild(element4);
                        element5 = document.CreateElement("发票号码");
                        element5.InnerText = fpTable.Rows[i]["FPHM"].ToString().PadLeft(8, '0');
                        element2.AppendChild(element5);
                        element6 = document.CreateElement("发票代码");
                        element6.InnerText = fpTable.Rows[i]["FPDM"].ToString();
                        element2.AppendChild(element6);
                        element7 = document.CreateElement("购方名称");
                        element7.InnerText = fpTable.Rows[i]["GFMC"].ToString();
                        element2.AppendChild(element7);
                        if (fpTable.Rows[i]["FPZL"].ToString() == "c")
                        {
                            string str3 = fpTable.Rows[i]["GFSH"].ToString();
                            if (((str3.Equals("000000000000000") || str3.Equals("00000000000000000")) || str3.Equals("000000000000000000")) || str3.Equals("00000000000000000000"))
                            {
                                if (((fpTable.Rows[i]["ZFBZ"].ToString() == "True") && (fpTable.Rows[i]["HJJE"].ToString() == "0")) && (fpTable.Rows[i]["HJSE"].ToString() == "0"))
                                {
                                    element8 = document.CreateElement("购方税号");
                                    element8.InnerText = fpTable.Rows[i]["GFSH"].ToString();
                                    element2.AppendChild(element8);
                                }
                                else
                                {
                                    element8 = document.CreateElement("购方税号");
                                    element8.InnerText = "";
                                    element2.AppendChild(element8);
                                }
                            }
                            else
                            {
                                element8 = document.CreateElement("购方税号");
                                element8.InnerText = fpTable.Rows[i]["GFSH"].ToString();
                                element2.AppendChild(element8);
                            }
                        }
                        else
                        {
                            element8 = document.CreateElement("购方税号");
                            element8.InnerText = fpTable.Rows[i]["GFSH"].ToString();
                            element2.AppendChild(element8);
                        }
                        element9 = document.CreateElement("开票日期");
                        element9.InnerText = Convert.ToDateTime(fpTable.Rows[i]["KPRQ"]).Date.ToString("yyyy-MM-dd");
                        element2.AppendChild(element9);
                        element10 = document.CreateElement("作废标志");
                        element10.InnerText = Convert.ToInt16(fpTable.Rows[i]["ZFBZ"]).ToString();
                        element2.AppendChild(element10);
                    }
                    else
                    {
                        XmlElement element18;
                        byte[] buffer;
                        string str5;
                        XmlElement element23;
                        if (fpTable.Rows[i]["FPZL"].ToString() == "f")
                        {
                            element3 = document.CreateElement("销售单据编号");
                            element3.InnerText = fpTable.Rows[i]["XSDJBH"].ToString();
                            element2.AppendChild(element3);
                            element4 = document.CreateElement("发票种类");
                            element4.InnerText = fpTable.Rows[i]["FPZL"].ToString();
                            element2.AppendChild(element4);
                            element5 = document.CreateElement("发票号码");
                            element5.InnerText = fpTable.Rows[i]["FPHM"].ToString().PadLeft(8, '0');
                            element2.AppendChild(element5);
                            element6 = document.CreateElement("发票代码");
                            element6.InnerText = fpTable.Rows[i]["FPDM"].ToString();
                            element2.AppendChild(element6);
                            XmlElement element11 = document.CreateElement("实际受票方名称");
                            element11.InnerText = fpTable.Rows[i]["GFMC"].ToString();
                            element2.AppendChild(element11);
                            XmlElement element12 = document.CreateElement("实际受票方税号");
                            element12.InnerText = fpTable.Rows[i]["GFSH"].ToString();
                            element2.AppendChild(element12);
                            XmlElement element13 = document.CreateElement("收货人名称");
                            element13.InnerText = fpTable.Rows[i]["GFDZDH"].ToString();
                            element2.AppendChild(element13);
                            XmlElement element14 = document.CreateElement("收货人税号");
                            element14.InnerText = fpTable.Rows[i]["CM"].ToString();
                            element2.AppendChild(element14);
                            XmlElement element15 = document.CreateElement("发货人名称");
                            element15.InnerText = fpTable.Rows[i]["XFDZDH"].ToString();
                            element2.AppendChild(element15);
                            XmlElement element16 = document.CreateElement("发货人税号");
                            element16.InnerText = fpTable.Rows[i]["TYDH"].ToString();
                            element2.AppendChild(element16);
                            element9 = document.CreateElement("开票日期");
                            element9.InnerText = Convert.ToDateTime(fpTable.Rows[i]["KPRQ"]).Date.ToString("yyyy-MM-dd");
                            element2.AppendChild(element9);
                            XmlElement element17 = document.CreateElement("合计金额");
                            element17.InnerText = fpTable.Rows[i]["HJJE"].ToString();
                            element2.AppendChild(element17);
                            element18 = document.CreateElement("税率");
                            element18.InnerText = fpTable.Rows[i]["SLV"].ToString();
                            element2.AppendChild(element18);
                            XmlElement element19 = document.CreateElement("起运地经由到达地");
                            element19.InnerText = fpTable.Rows[i]["XFYHZH"].ToString();
                            element2.AppendChild(element19);
                            XmlElement element20 = document.CreateElement("车种车号");
                            element20.InnerText = fpTable.Rows[i]["QYD"].ToString();
                            element2.AppendChild(element20);
                            XmlElement element21 = document.CreateElement("车船吨位");
                            element21.InnerText = fpTable.Rows[i]["YYZZH"].ToString();
                            element2.AppendChild(element21);
                            string s = fpTable.Rows[i]["YSHWXX"].ToString();
                            if ((s == null) || (s == ""))
                            {
                                s = "";
                            }
                            else
                            {
                                buffer = Convert.FromBase64String(s);
                                if ((buffer != null) && (buffer.Length >= 1))
                                {
                                    s = ToolUtil.GetString(buffer);
                                }
                                else
                                {
                                    s = "";
                                }
                            }
                            XmlElement element22 = document.CreateElement("运输货物信息");
                            element22.InnerText = s;
                            element2.AppendChild(element22);
                            str5 = fpTable.Rows[i]["BZ"].ToString();
                            if ((str5 == null) || (str5 == ""))
                            {
                                str5 = "";
                            }
                            else
                            {
                                buffer = Convert.FromBase64String(str5);
                                if ((buffer != null) && (buffer.Length >= 1))
                                {
                                    str5 = ToolUtil.GetString(buffer);
                                }
                                else
                                {
                                    str5 = "";
                                }
                            }
                            element23 = document.CreateElement("备注");
                            element23.InnerText = str5;
                            element2.AppendChild(element23);
                            XmlElement element24 = document.CreateElement("开票人");
                            element24.InnerText = fpTable.Rows[i]["KPR"].ToString();
                            element2.AppendChild(element24);
                            XmlElement element25 = document.CreateElement("收款人");
                            element25.InnerText = fpTable.Rows[i]["SKR"].ToString();
                            element2.AppendChild(element25);
                            XmlElement element26 = document.CreateElement("复核人");
                            element26.InnerText = fpTable.Rows[i]["FHR"].ToString();
                            element2.AppendChild(element26);
                            element10 = document.CreateElement("作废标志");
                            element10.InnerText = Convert.ToInt16(fpTable.Rows[i]["ZFBZ"]).ToString();
                            element2.AppendChild(element10);
                        }
                        else if (fpTable.Rows[i]["FPZL"].ToString() == "j")
                        {
                            element3 = document.CreateElement("销售单据编号");
                            element3.InnerText = fpTable.Rows[i]["XSDJBH"].ToString();
                            element2.AppendChild(element3);
                            element4 = document.CreateElement("发票种类");
                            element4.InnerText = fpTable.Rows[i]["FPZL"].ToString();
                            element2.AppendChild(element4);
                            element5 = document.CreateElement("发票号码");
                            element5.InnerText = fpTable.Rows[i]["FPHM"].ToString().PadLeft(8, '0');
                            element2.AppendChild(element5);
                            element6 = document.CreateElement("发票代码");
                            element6.InnerText = fpTable.Rows[i]["FPDM"].ToString();
                            element2.AppendChild(element6);
                            element7 = document.CreateElement("购货单位");
                            element7.InnerText = fpTable.Rows[i]["GFMC"].ToString();
                            element2.AppendChild(element7);
                            element8 = document.CreateElement("纳税人识别号");
                            element8.InnerText = fpTable.Rows[i]["GFSH"].ToString();
                            element2.AppendChild(element8);
                            XmlElement element27 = document.CreateElement("身份证号码或组织机构代码");
                            element27.InnerText = fpTable.Rows[i]["XSBM"].ToString();
                            element2.AppendChild(element27);
                            element9 = document.CreateElement("开票日期");
                            element9.InnerText = Convert.ToDateTime(fpTable.Rows[i]["KPRQ"]).Date.ToString("yyyy-MM-dd");
                            element2.AppendChild(element9);
                            double num2 = Convert.ToDouble(fpTable.Rows[i]["HJJE"]);
                            double num3 = Convert.ToDouble(fpTable.Rows[i]["HJSE"]);
                            double num4 = num2 + num3;
                            XmlElement element28 = document.CreateElement("价税合计");
                            element28.InnerText = num4.ToString();
                            element2.AppendChild(element28);
                            element18 = document.CreateElement("增值税税率或征收率");
                            element18.InnerText = fpTable.Rows[i]["SLV"].ToString();
                            element2.AppendChild(element18);
                            XmlElement element29 = document.CreateElement("车辆类型");
                            element29.InnerText = fpTable.Rows[i]["GFDZDH"].ToString();
                            element2.AppendChild(element29);
                            XmlElement element30 = document.CreateElement("厂牌型号");
                            element30.InnerText = fpTable.Rows[i]["XFDZ"].ToString();
                            element2.AppendChild(element30);
                            XmlElement element31 = document.CreateElement("产地");
                            element31.InnerText = fpTable.Rows[i]["KHYHMC"].ToString();
                            element2.AppendChild(element31);
                            XmlElement element32 = document.CreateElement("生产企业名称");
                            element32.InnerText = fpTable.Rows[i]["SCCJMC"].ToString();
                            element2.AppendChild(element32);
                            XmlElement element33 = document.CreateElement("合格证号");
                            element33.InnerText = fpTable.Rows[i]["CM"].ToString();
                            element2.AppendChild(element33);
                            XmlElement element34 = document.CreateElement("进口证明书号");
                            element34.InnerText = fpTable.Rows[i]["TYDH"].ToString();
                            element2.AppendChild(element34);
                            XmlElement element35 = document.CreateElement("商检单号");
                            element35.InnerText = fpTable.Rows[i]["QYD"].ToString();
                            element2.AppendChild(element35);
                            XmlElement element36 = document.CreateElement("发动机号码");
                            element36.InnerText = fpTable.Rows[i]["ZHD"].ToString();
                            element2.AppendChild(element36);
                            XmlElement element37 = document.CreateElement("车辆识别代号或车架号码");
                            element37.InnerText = fpTable.Rows[i]["XHD"].ToString();
                            element2.AppendChild(element37);
                            XmlElement element38 = document.CreateElement("电话");
                            element38.InnerText = fpTable.Rows[i]["XFDH"].ToString();
                            element2.AppendChild(element38);
                            XmlElement element39 = document.CreateElement("账号");
                            element39.InnerText = fpTable.Rows[i]["KHYHZH"].ToString();
                            element2.AppendChild(element39);
                            XmlElement element40 = document.CreateElement("地址");
                            element40.InnerText = fpTable.Rows[i]["XFDZDH"].ToString();
                            element2.AppendChild(element40);
                            XmlElement element41 = document.CreateElement("开户银行");
                            element41.InnerText = fpTable.Rows[i]["XFYHZH"].ToString();
                            element2.AppendChild(element41);
                            XmlElement element42 = document.CreateElement("吨位");
                            element42.InnerText = fpTable.Rows[i]["YYZZH"].ToString();
                            element2.AppendChild(element42);
                            XmlElement element43 = document.CreateElement("限乘人数");
                            element43.InnerText = fpTable.Rows[i]["MDD"].ToString();
                            element2.AppendChild(element43);
                            str5 = fpTable.Rows[i]["BZ"].ToString();
                            if ((str5 == null) || (str5 == ""))
                            {
                                str5 = "";
                            }
                            else
                            {
                                buffer = Convert.FromBase64String(str5);
                                if ((buffer != null) && (buffer.Length >= 1))
                                {
                                    str5 = ToolUtil.GetString(buffer);
                                }
                                else
                                {
                                    str5 = "";
                                }
                            }
                            element23 = document.CreateElement("备注");
                            element23.InnerText = str5;
                            element2.AppendChild(element23);
                            element10 = document.CreateElement("作废标志");
                            element10.InnerText = Convert.ToInt16(fpTable.Rows[i]["ZFBZ"]).ToString();
                            element2.AppendChild(element10);
                        }
                    }
                    element.AppendChild(element2);
                }
                document.Save(xmlPath);
            }
        }

        public ArrayList GetAllCustomers()
        {
            return this.DAL_FPCX.GetAllCustomers();
        }

        public ArrayList GetHY_SPFMC()
        {
            return this.DAL_FPCX.GetSPFMC();
        }

        public AisinoDataSet GetInvoiceDetail(InvoiceQueryCondition invoiceQueryCondition, int nPageSize, int nPageNo)
        {
            if (invoiceQueryCondition.m_dtStart > invoiceQueryCondition.m_dtEnd)
            {
                throw new CustomException("截止日期不能小于起始日期");
            }
            return this.DAL_FPCX.GetInvoiceDetail(invoiceQueryCondition, nPageSize, nPageNo);
        }

        public XXFPPaper GetInvPaper(string FPZL, string FPDM, string FPHM, bool GetInvQD)
        {
            return this.DAL_FPCX.GetInvPaper(FPZL, FPDM, FPHM, GetInvQD);
        }

        public ArrayList GetJDC_GHDW()
        {
            return this.DAL_FPCX.GetGHDW();
        }

        public AisinoDataSet QueryFaPiao(FaPiaoQueryArgs QueryArgs, int pagesize, int pageno)
        {
            return this.DAL_FPCX.QueryFaPiao(QueryArgs, pagesize, pageno);
        }

        public DataTable QueryGetFaPiao(InvoiceQueryCondition QueryArgs)
        {
            return this.DAL_FPCX.QueryGetFaPiao(QueryArgs);
        }

        public DataTable QueryGetFaPiao(FaPiaoQueryArgs QueryArgs)
        {
            return this.DAL_FPCX.QueryGetFaPiao(QueryArgs);
        }

        public DataTable QueryGetFaPiaoxml(InvoiceQueryCondition QueryArgs)
        {
            return this.DAL_FPCX.QueryGetFaPiaoxml(QueryArgs);
        }

        public DataTable QueryGetFaPiaoxml(FaPiaoQueryArgs QueryArgs)
        {
            return this.DAL_FPCX.QueryGetFaPiaoxml(QueryArgs);
        }

        public int CurrentPage
        {
            get
            {
                return this._currentPage;
            }
            set
            {
                this._currentPage = value;
            }
        }

        public int Pagesize
        {
            get
            {
                return PropValue.Pagesize;
            }
            set
            {
                PropValue.Pagesize = value;
            }
        }
    }
}

