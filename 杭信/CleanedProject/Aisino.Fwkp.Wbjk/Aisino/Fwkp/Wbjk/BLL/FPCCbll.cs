namespace Aisino.Fwkp.Wbjk.BLL
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.DAL;
    using Aisino.Fwkp.Wbjk.Model;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Threading;

    public class FPCCbll
    {
        private int _currentPage = 1;
        private DateTime _EndDate;
        private string _FpType;
        private DateTime _StartDate;
        private FPCCdal fpccDAL = new FPCCdal();
        private static ILog loger = LogUtil.GetLogger<FPCCbll>();

        public int ExportInvoiceToTxt(List<InvoiceData> InvoiceDataList, bool ExportAll)
        {
            int count = 0;
            try
            {
                int num3;
                XXFPPaper paper;
                StreamWriter writer;
                MessageHelper.MsgWait("正在导出发票，请稍候...");
                string invExportTxtPath = PropValue.InvExportTxtPath;
                if (invExportTxtPath.Length <= 0)
                {
                    throw new FileNotFoundException("没有设置传出文件路径");
                }
                int startIndex = invExportTxtPath.LastIndexOf(@"\");
                if (startIndex == -1)
                {
                    throw new FileNotFoundException("路径格式错误");
                }
                string path = invExportTxtPath.Remove(startIndex);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                bool checkBoxKqd = PropValue.CheckBoxKqd;
                List<XXFPPaper> list = new List<XXFPPaper>();
                if (ExportAll)
                {
                    InvoiceDataList.Clear();
                    DataTable faPiao = this.GetFaPiao();
                    for (num3 = 0; num3 < faPiao.Rows.Count; num3++)
                    {
                        InvoiceData item = new InvoiceData {
                            m_strInvType = faPiao.Rows[num3]["FPZL"].ToString(),
                            m_strInvCode = faPiao.Rows[num3]["FPDM"].ToString(),
                            m_strInvNum = faPiao.Rows[num3]["FPHM"].ToString()
                        };
                        InvoiceDataList.Add(item);
                    }
                }
                if (InvoiceDataList.Count > 0)
                {
                    FPCXbll xbll = new FPCXbll();
                    for (num3 = 0; num3 < InvoiceDataList.Count; num3++)
                    {
                        paper = xbll.GetInvPaper(InvoiceDataList[num3].m_strInvType, InvoiceDataList[num3].m_strInvCode, InvoiceDataList[num3].m_strInvNum, checkBoxKqd);
                        list.Add(paper);
                    }
                }
                else
                {
                    return 0;
                }
                string str3 = "~~";
                StringBuilder builder = new StringBuilder();
                builder.Append(list.Count.ToString());
                builder.Append(str3);
                builder.Append(this._StartDate.ToString("yyyyMMdd"));
                builder.Append(str3);
                builder.Append(this._EndDate.ToString("yyyyMMdd"));
                using (writer = new StreamWriter(invExportTxtPath, false, ToolUtil.GetEncoding()))
                {
                    writer.WriteLine("SJJK0201" + str3 + "已开发票传出");
                    writer.WriteLine(builder.ToString());
                }
                count = list.Count;
                for (num3 = 0; num3 < list.Count; num3++)
                {
                    int num5;
                    int invType;
                    string str6;
                    XXFP_MXModel model;
                    int num10;
                    StreamWriter writer2;
                    paper = list[num3];
                    string str4 = paper.FPHM.ToString();
                    string str5 = string.Format("发票号码: {0} ", str4);
                    if ((paper.FPZL == "c") || (paper.FPZL == "s"))
                    {
                        int num4 = 0;
                        if (paper.ListInvWare.Count > 0)
                        {
                            num5 = 0;
                            while (num5 < paper.ListInvWare.Count)
                            {
                                if (!checkBoxKqd || ((paper.ListInvWare[num5].FPHXZ != 1) && (paper.ListInvWare[num5].FPHXZ != 5)))
                                {
                                    num4++;
                                }
                                num5++;
                            }
                        }
                        builder.Remove(0, builder.Length);
                        builder.Append(Convert.ToInt32(paper.ZFBZ));
                        builder.Append(str3);
                        builder.Append(Convert.ToInt32(paper.QDBZ));
                        builder.Append(str3);
                        invType = (int) CommonTool.GetInvType(paper.FPZL);
                        builder.Append(invType);
                        builder.Append(str3);
                        builder.Append(paper.FPDM);
                        builder.Append(str3);
                        builder.Append(paper.FPHM.ToString().PadLeft(8, '0'));
                        builder.Append(str3);
                        builder.Append(num4);
                        builder.Append(str3);
                        builder.Append(paper.KPRQ.ToString("yyyyMMdd"));
                        builder.Append(str3);
                        builder.Append(Convert.ToDateTime(paper.KPRQ).Month.ToString().PadLeft(2, '0'));
                        builder.Append(str3);
                        builder.Append(paper.XSDJBH);
                        builder.Append(str3);
                        str6 = paper.HJJE.ToString("0.00");
                        builder.Append(str6);
                        builder.Append(str3);
                        if (paper.MULSLV)
                        {
                            builder.Append("");
                        }
                        else if (paper.SLV == 0.0)
                        {
                            builder.Append(paper.SLV);
                        }
                        else
                        {
                            builder.Append(paper.SLV);
                        }
                        builder.Append(str3);
                        str6 = paper.HJSE.ToString("0.00");
                        builder.Append(str6);
                        builder.Append(str3);
                        builder.Append(paper.GFMC);
                        builder.Append(str3);
                        if (paper.FPZL == "c")
                        {
                            if (((paper.GFSH.Equals("000000000000000") || paper.GFSH.Equals("00000000000000000")) || paper.GFSH.Equals("000000000000000000")) || paper.GFSH.Equals("00000000000000000000"))
                            {
                                if ((paper.ZFBZ && (paper.HJJE == 0.0)) && (paper.HJSE == 0.0))
                                {
                                    builder.Append(paper.GFSH);
                                }
                                else
                                {
                                    builder.Append("");
                                }
                            }
                            else
                            {
                                builder.Append(paper.GFSH);
                            }
                        }
                        else
                        {
                            builder.Append(paper.GFSH);
                        }
                        builder.Append(str3);
                        builder.Append(paper.GFDZDH);
                        builder.Append(str3);
                        builder.Append(paper.GFYHZH);
                        builder.Append(str3);
                        builder.Append(paper.XFMC);
                        builder.Append(str3);
                        builder.Append(paper.XFSH);
                        builder.Append(str3);
                        builder.Append(paper.XFDZDH);
                        builder.Append(str3);
                        builder.Append(paper.XFYHZH);
                        builder.Append(str3);
                        if (paper.BZ.IndexOf("\r\n") == -1)
                        {
                            builder.Append(paper.BZ);
                        }
                        else
                        {
                            str6 = paper.BZ.Replace("\r\n", @"\n");
                            builder.Append(str6);
                        }
                        builder.Append(str3);
                        builder.Append(paper.KPR);
                        builder.Append(str3);
                        builder.Append(paper.FHR);
                        builder.Append(str3);
                        builder.Append(paper.SKR);
                        writer = new StreamWriter(invExportTxtPath, true, ToolUtil.GetEncoding());
                        using (writer2 = writer)
                        {
                            num10 = num3 + 1;
                            writer.WriteLine("//发票" + num10.ToString());
                            writer.WriteLine(builder.ToString());
                            if (paper.ListInvWare.Count > 0)
                            {
                                num5 = 0;
                                while (num5 < paper.ListInvWare.Count)
                                {
                                    model = paper.ListInvWare[num5];
                                    if (!checkBoxKqd || ((model.FPHXZ != 1) && (model.FPHXZ != 5)))
                                    {
                                        builder.Remove(0, builder.Length);
                                        if (model.FPHXZ == 4)
                                        {
                                            builder.Append("1");
                                        }
                                        else
                                        {
                                            builder.Append("0");
                                        }
                                        builder.Append(str3);
                                        builder.Append(model.SPMC);
                                        builder.Append(str3);
                                        builder.Append(model.GGXH);
                                        builder.Append(str3);
                                        builder.Append(model.JLDW);
                                        builder.Append(str3);
                                        if (Convert.ToDouble(model.SL) == 0.0)
                                        {
                                            builder.Append("");
                                        }
                                        else
                                        {
                                            builder.Append(Convert.ToDouble(model.SL));
                                        }
                                        builder.Append(str3);
                                        str6 = model.JE.ToString("0.00");
                                        builder.Append(str6);
                                        builder.Append(str3);
                                        if (((model.FPHXZ == 1) || (model.FPHXZ == 5)) && paper.MULSLV)
                                        {
                                            builder.Append("");
                                        }
                                        else
                                        {
                                            double sLV = model.SLV;
                                            builder.Append(model.SLV);
                                        }
                                        builder.Append(str3);
                                        str6 = model.SE.ToString("0.00");
                                        builder.Append(str6);
                                        builder.Append(str3);
                                        if (Convert.ToDouble(model.DJ) == 0.0)
                                        {
                                            builder.Append("");
                                        }
                                        else
                                        {
                                            builder.Append(Convert.ToDouble(model.DJ));
                                        }
                                        builder.Append(str3);
                                        builder.Append(Convert.ToInt32(model.HSJBZ));
                                        builder.Append(str3);
                                        builder.Append(model.SPSM);
                                        writer.WriteLine(builder.ToString());
                                    }
                                    num5++;
                                }
                            }
                        }
                    }
                    else if (paper.FPZL == "f")
                    {
                        builder.Remove(0, builder.Length);
                        builder.Append(Convert.ToInt32(paper.ZFBZ));
                        builder.Append(str3);
                        invType = (int) CommonTool.GetInvType(paper.FPZL);
                        builder.Append(invType);
                        builder.Append(str3);
                        builder.Append(paper.FPDM);
                        builder.Append(str3);
                        builder.Append(paper.FPHM.ToString().PadLeft(8, '0'));
                        builder.Append(str3);
                        builder.Append(paper.ListInvWare.Count);
                        builder.Append(str3);
                        builder.Append(paper.KPRQ.ToString("yyyyMMdd"));
                        builder.Append(str3);
                        builder.Append(Convert.ToDateTime(paper.KPRQ).Month);
                        builder.Append(str3);
                        builder.Append(paper.XSDJBH);
                        builder.Append(str3);
                        str6 = paper.HJJE.ToString("0.00");
                        builder.Append(str6);
                        builder.Append(str3);
                        if (paper.SLV == 0.0)
                        {
                            builder.Append(paper.SLV);
                        }
                        else
                        {
                            builder.Append(paper.SLV);
                        }
                        builder.Append(str3);
                        str6 = paper.HJSE.ToString("0.00");
                        builder.Append(str6);
                        builder.Append(str3);
                        builder.Append(paper.GFMC);
                        builder.Append(str3);
                        builder.Append(paper.GFSH);
                        builder.Append(str3);
                        builder.Append(paper.SHRMC);
                        builder.Append(str3);
                        builder.Append(paper.SHRSH);
                        builder.Append(str3);
                        builder.Append(paper.FHRMC);
                        builder.Append(str3);
                        builder.Append(paper.FHRSH);
                        builder.Append(str3);
                        builder.Append(paper.XFMC);
                        builder.Append(str3);
                        builder.Append(paper.XFSH);
                        builder.Append(str3);
                        builder.Append(paper.QYJYMD);
                        builder.Append(str3);
                        builder.Append(paper.CZCH);
                        builder.Append(str3);
                        builder.Append(paper.CCDW);
                        builder.Append(str3);
                        builder.Append(paper.YSHWXX);
                        builder.Append(str3);
                        if (paper.BZ.IndexOf("\r\n") == -1)
                        {
                            builder.Append(paper.BZ);
                        }
                        else
                        {
                            str6 = paper.BZ.Replace("\r\n", @"\n");
                            builder.Append(str6);
                        }
                        builder.Append(str3);
                        builder.Append(paper.KPR);
                        builder.Append(str3);
                        builder.Append(paper.FHR);
                        builder.Append(str3);
                        builder.Append(paper.SKR);
                        builder.Append(str3);
                        builder.Append(paper.ZGSWMC);
                        builder.Append(str3);
                        builder.Append(paper.ZGSWDM);
                        builder.Append(str3);
                        builder.Append(paper.JQBH);
                        writer = new StreamWriter(invExportTxtPath, true, ToolUtil.GetEncoding());
                        using (writer2 = writer)
                        {
                            num10 = num3 + 1;
                            writer.WriteLine("//发票" + num10.ToString());
                            writer.WriteLine(builder.ToString());
                            if (paper.ListInvWare.Count > 0)
                            {
                                for (num5 = 0; num5 < paper.ListInvWare.Count; num5++)
                                {
                                    model = paper.ListInvWare[num5];
                                    builder.Remove(0, builder.Length);
                                    builder.Append(model.SPMC);
                                    builder.Append(str3);
                                    str6 = model.JE.ToString("0.00");
                                    builder.Append(str6);
                                    writer.WriteLine(builder.ToString());
                                }
                            }
                        }
                    }
                    else if (paper.FPZL == "j")
                    {
                        builder.Remove(0, builder.Length);
                        builder.Append(Convert.ToInt32(paper.ZFBZ));
                        builder.Append(str3);
                        invType = (int) CommonTool.GetInvType(paper.FPZL);
                        builder.Append(invType);
                        builder.Append(str3);
                        builder.Append(paper.FPDM);
                        builder.Append(str3);
                        builder.Append(paper.FPHM.ToString().PadLeft(8, '0'));
                        builder.Append(str3);
                        builder.Append(paper.KPRQ.ToString("yyyyMMdd"));
                        builder.Append(str3);
                        builder.Append(Convert.ToDateTime(paper.KPRQ).Month);
                        builder.Append(str3);
                        builder.Append(paper.XSDJBH);
                        builder.Append(str3);
                        str6 = SaleBillCtrl.GetRound((double) (paper.HJJE + paper.HJSE), 2).ToString("0.00");
                        builder.Append(str6);
                        builder.Append(str3);
                        str6 = paper.HJJE.ToString("0.00");
                        builder.Append(str6);
                        builder.Append(str3);
                        if (paper.SLV == 0.0)
                        {
                            builder.Append(paper.SLV);
                        }
                        else
                        {
                            builder.Append(paper.SLV);
                        }
                        builder.Append(str3);
                        str6 = paper.HJSE.ToString("0.00");
                        builder.Append(str6);
                        builder.Append(str3);
                        builder.Append(paper.GFMC);
                        builder.Append(str3);
                        builder.Append(paper.GFSH);
                        builder.Append(str3);
                        builder.Append(paper.SFZHM);
                        builder.Append(str3);
                        builder.Append(paper.CLLX);
                        builder.Append(str3);
                        builder.Append(paper.CPXH);
                        builder.Append(str3);
                        builder.Append(paper.CD);
                        builder.Append(str3);
                        builder.Append(paper.SCCJMC);
                        builder.Append(str3);
                        builder.Append(paper.HGZH);
                        builder.Append(str3);
                        builder.Append(paper.JKZMSH);
                        builder.Append(str3);
                        builder.Append(paper.SJDH);
                        builder.Append(str3);
                        builder.Append(paper.FDJH);
                        builder.Append(str3);
                        builder.Append(paper.CLSBDH);
                        builder.Append(str3);
                        builder.Append(paper.XFMC);
                        builder.Append(str3);
                        builder.Append(paper.XFSH);
                        builder.Append(str3);
                        builder.Append(paper.DH);
                        builder.Append(str3);
                        builder.Append(paper.ZH);
                        builder.Append(str3);
                        builder.Append(paper.DZ);
                        builder.Append(str3);
                        builder.Append(paper.KHYH);
                        builder.Append(str3);
                        builder.Append(paper.DW);
                        builder.Append(str3);
                        builder.Append(paper.XCRS);
                        builder.Append(str3);
                        builder.Append(paper.KPR);
                        builder.Append(str3);
                        builder.Append(paper.ZGSWMC);
                        builder.Append(str3);
                        builder.Append(paper.ZGSWDM);
                        builder.Append(str3);
                        builder.Append(paper.JQBH);
                        builder.Append(str3);
                        string zYSPMC = paper.ZYSPMC;
                        string str8 = "";
                        string str9 = "";
                        string[] strArray = paper.SPSM.Split(new char[] { '#', '%' });
                        if (strArray.Length == 3)
                        {
                            str8 = strArray[0].Trim();
                            str9 = strArray[2].Trim();
                        }
                        string str11 = "0";
                        string str12 = "";
                        string[] strArray2 = paper.SKR.Split(new char[] { '#', '%' });
                        if (strArray2.Length == 3)
                        {
                            str11 = strArray2[0].Trim();
                            str12 = strArray2[3].Trim();
                        }
                        if (paper.BZ.IndexOf("\r\n") == -1)
                        {
                            builder.Append(paper.BZ);
                        }
                        else
                        {
                            str6 = paper.BZ.Replace("\r\n", @"\n");
                            builder.Append(str6);
                        }
                        writer = new StreamWriter(invExportTxtPath, true, ToolUtil.GetEncoding());
                        using (writer2 = writer)
                        {
                            writer.WriteLine("//发票" + ((num3 + 1)).ToString());
                            writer.WriteLine(builder.ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Thread.Sleep(100);
                MessageHelper.MsgWait();
            }
            return count;
        }

        public DataTable GetFaPiao()
        {
            return this.fpccDAL.GetFaPiao(this._StartDate, this._EndDate, this._FpType);
        }

        public DateTime GetLastRepDate()
        {
            return TaxCardFactory.CreateTaxCard().get_LastRepDate();
        }

        public DateTime GetTaxCardDate()
        {
            return TaxCardFactory.CreateTaxCard().GetCardClock();
        }

        public AisinoDataSet QueryFaPiao(DateTime StartDate, DateTime EndDate, string FpType, int pagesize, int pageno)
        {
            this._StartDate = StartDate;
            this._EndDate = EndDate;
            this._FpType = FpType;
            return this.fpccDAL.QueryFaPiao(StartDate, EndDate, FpType, pagesize, pageno);
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

