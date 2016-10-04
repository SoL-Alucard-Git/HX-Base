namespace Aisino.Fwkp.Fpkj.Common
{
    using AESLib;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fpkj.DAL;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Xml;

    internal class XMLOperation : DockForm
    {
        private string _strFilePath = string.Empty;
        public static int iByteLength = 0x100000;
        public static byte[] key = new byte[] { 
            1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 
            7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2
         };
        private ILog loger = LogUtil.GetLogger<XMLOperation>();
        public static string strTempPathEmail = @"\Bin";
        public static byte[] vector = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6 };
        private XXFP xxfpChaXunBll = new XXFP(false);

        public void EncryptXML(List<Fpxx> fpList, string Path)
        {
            try
            {
                if ((fpList != null) && (fpList.Count != 0))
                {
                    XmlDocument document = this.FpzjDaoChuCreateXml(fpList);
                    if (document == null)
                    {
                        this.loger.Error("[EncryptXML异常]构造的XMLDocument对象为空");
                    }
                    else
                    {
                        Encryptor encryptor = (Encryptor) Activator.CreateInstance(System.Type.GetTypeFromCLSID(new Guid("C0A6B38B-16A7-4374-BB51-0084087910CD")));
                        string outerXml = document.OuterXml;
                        outerXml = encryptor.CryptEncrypt(ref outerXml);
                        using (StreamWriter writer = new StreamWriter(Path, false, ToolUtil.GetEncoding()))
                        {
                            writer.Write(outerXml);
                        }
                        fpList = null;
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        public XmlDocument FpzjDaoChuCreateXml(List<Fpxx> fpList)
        {
            if ((fpList == null) || (fpList.Count == 0))
            {
                return null;
            }
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration newChild = xmlDoc.CreateXmlDeclaration("1.0", "GBK", "");
                xmlDoc.AppendChild(newChild);
                System.Xml.XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, "kp", "");
                xmlDoc.AppendChild(node);
                System.Xml.XmlNode node2 = xmlDoc.CreateNode(XmlNodeType.Element, "errorinfo", "");
                node.AppendChild(node2);
                System.Xml.XmlNode node3 = xmlDoc.CreateNode(XmlNodeType.Element, "Code", "");
                node3.InnerText = "7011";
                node2.AppendChild(node3);
                System.Xml.XmlNode node4 = xmlDoc.CreateNode(XmlNodeType.Element, "Message", "");
                node4.InnerText = "发票明细查询成功";
                node2.AppendChild(node4);
                System.Xml.XmlNode node5 = xmlDoc.CreateNode(XmlNodeType.Element, "Invoices", "");
                node.AppendChild(node5);
                System.Xml.XmlNode node6 = xmlDoc.CreateNode(XmlNodeType.Element, "TotalCount", "");
                node6.InnerText = fpList.Count.ToString();
                node5.AppendChild(node6);
                double num = 0.0;
                System.Xml.XmlNode node7 = xmlDoc.CreateNode(XmlNodeType.Element, "TotalJe", "");
                node5.AppendChild(node7);
                double num2 = 0.0;
                System.Xml.XmlNode node8 = xmlDoc.CreateNode(XmlNodeType.Element, "TotalSe", "");
                node5.AppendChild(node8);
                foreach (Fpxx fpxx in fpList)
                {
                    try
                    {
                        if ((fpxx.fphm == "00097347") || (fpxx.fphm == "00097348"))
                        {
                            int num3 = 2;
                            num3++;
                        }
                        switch (fpxx.fplx)
                        {
                            case (FPLX)0:
                            case (FPLX)2:
                            {
                                num += Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(fpxx.je);
                                num2 += Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(fpxx.se);
                                System.Xml.XmlNode node9 = xmlDoc.CreateNode(XmlNodeType.Element, "Fpxx", "");
                                node5.AppendChild(node9);
                                System.Xml.XmlNode node10 = xmlDoc.CreateNode(XmlNodeType.Element, "Invoice", "");
                                node9.AppendChild(node10);
                                this.FpzjDaoChuFpxxToXML(fpxx, ref node10, ref xmlDoc);
                                continue;
                            }
                            case (FPLX)1:
                            {
                                continue;
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        this.loger.Error(exception.Message);
                    }
                }
                node7.InnerText = num.ToString("f2");
                node8.InnerText = num2.ToString("f2");
                fpList = null;
                return xmlDoc;
            }
            catch (Exception exception2)
            {
                this.loger.Error("税务代开发票遍历构造XML异常：" + exception2.Message);
                return null;
            }
        }

        private void FpzjDaoChuFpxxToXML(Fpxx fp, ref System.Xml.XmlNode InvoiceNode, ref XmlDocument xmlDoc)
        {
            if (fp != null)
            {
                try
                {
                    System.Xml.XmlNode newChild = xmlDoc.CreateElement("InvKind");
                    newChild.InnerText = Aisino.Fwkp.Fpkj.Common.Tool.PareFpType(fp.fplx).ToString();
                    InvoiceNode.AppendChild(newChild);
                    System.Xml.XmlNode node2 = xmlDoc.CreateElement("TypeCode");
                    node2.InnerText = fp.fpdm;
                    InvoiceNode.AppendChild(node2);
                    System.Xml.XmlNode node3 = xmlDoc.CreateElement("InvNo");
                    node3.InnerText = fp.fphm;
                    InvoiceNode.AppendChild(node3);
                    System.Xml.XmlNode node4 = xmlDoc.CreateElement("Machine");
                    node4.InnerText = fp.kpjh.ToString();
                    InvoiceNode.AppendChild(node4);
                    System.Xml.XmlNode node5 = xmlDoc.CreateElement("ClientName");
                    node5.InnerText = fp.gfmc;
                    InvoiceNode.AppendChild(node5);
                    System.Xml.XmlNode node6 = xmlDoc.CreateElement("ClientTaxCode");
                    node6.InnerText = fp.gfsh;
                    InvoiceNode.AppendChild(node6);
                    System.Xml.XmlNode node7 = xmlDoc.CreateElement("ClientBankAccount");
                    node7.InnerText = fp.gfyhzh;
                    InvoiceNode.AppendChild(node7);
                    System.Xml.XmlNode node8 = xmlDoc.CreateElement("ClientAddressPhone");
                    node8.InnerText = fp.gfdzdh;
                    InvoiceNode.AppendChild(node8);
                    System.Xml.XmlNode node9 = xmlDoc.CreateElement("SellerTaxCode");
                    node9.InnerText = fp.xfsh;
                    InvoiceNode.AppendChild(node9);
                    System.Xml.XmlNode node10 = xmlDoc.CreateElement("SellerName");
                    node10.InnerText = fp.xfmc;
                    InvoiceNode.AppendChild(node10);
                    System.Xml.XmlNode node11 = xmlDoc.CreateElement("SellerBankAccount");
                    node11.InnerText = fp.xfyhzh;
                    InvoiceNode.AppendChild(node11);
                    System.Xml.XmlNode node12 = xmlDoc.CreateElement("SellerAddressPhone");
                    node12.InnerText = fp.xfdzdh;
                    InvoiceNode.AppendChild(node12);
                    System.Xml.XmlNode node13 = xmlDoc.CreateElement("Date");
                    node13.InnerText = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDateTime(fp.kprq).ToString("yyyy-MM-dd");
                    InvoiceNode.AppendChild(node13);
                    System.Xml.XmlNode node14 = xmlDoc.CreateElement("Amount");
                    node14.InnerText = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(fp.je).ToString("f2");
                    InvoiceNode.AppendChild(node14);
                    System.Xml.XmlNode node15 = xmlDoc.CreateElement("Tax");
                    node15.InnerText = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(fp.se).ToString("f2");
                    InvoiceNode.AppendChild(node15);
                    System.Xml.XmlNode node16 = xmlDoc.CreateElement("Rate");
                    node16.InnerText = fp.sLv;
                    InvoiceNode.AppendChild(node16);
                    System.Xml.XmlNode node17 = xmlDoc.CreateElement("Note");
                    node17.InnerText = ToolUtil.GetString(Convert.FromBase64String(fp.bz));
                    InvoiceNode.AppendChild(node17);
                    System.Xml.XmlNode node18 = xmlDoc.CreateElement("Invoicer");
                    node18.InnerText = fp.kpr;
                    InvoiceNode.AppendChild(node18);
                    System.Xml.XmlNode node19 = xmlDoc.CreateElement("Checker");
                    node19.InnerText = fp.fhr;
                    InvoiceNode.AppendChild(node19);
                    System.Xml.XmlNode node20 = xmlDoc.CreateElement("Cashier");
                    node20.InnerText = fp.skr;
                    InvoiceNode.AppendChild(node20);
                    System.Xml.XmlNode node21 = xmlDoc.CreateElement("WasteFlag");
                    node21.InnerText = fp.zfbz ? "1" : "0";
                    InvoiceNode.AppendChild(node21);
                    System.Xml.XmlNode node22 = xmlDoc.CreateElement("ListFlag");
                    node22.InnerText = ((fp.Qdxx != null) && (fp.Qdxx.Count > 0)) ? "1" : "0";
                    InvoiceNode.AppendChild(node22);
                    System.Xml.XmlNode node23 = xmlDoc.CreateElement("PrintFlag");
                    InvoiceNode.AppendChild(node23);
                    System.Xml.XmlNode node24 = xmlDoc.CreateElement("Data");
                    InvoiceNode.AppendChild(node24);
                    if ((fp.Qdxx != null) && (fp.Qdxx.Count > 0))
                    {
                        int num = 1;
                        foreach (Dictionary<SPXX, string> dictionary in fp.Qdxx)
                        {
                            if (dictionary[(SPXX)10] != "5")
                            {
                                System.Xml.XmlNode node25 = xmlDoc.CreateElement("Mxxx");
                                System.Xml.XmlNode node26 = xmlDoc.CreateElement("ID");
                                node26.InnerText = num.ToString();
                                num++;
                                node25.AppendChild(node26);
                                System.Xml.XmlNode node27 = xmlDoc.CreateElement("FPHXZ");
                                node27.InnerText = dictionary[(SPXX)10];
                                node25.AppendChild(node27);
                                System.Xml.XmlNode node28 = xmlDoc.CreateElement("Spmc");
                                node28.InnerText = dictionary[(SPXX)0];
                                node25.AppendChild(node28);
                                System.Xml.XmlNode node29 = xmlDoc.CreateElement("Ggxh");
                                node29.InnerText = dictionary[(SPXX)3];
                                node25.AppendChild(node29);
                                System.Xml.XmlNode node30 = xmlDoc.CreateElement("Jldw");
                                node30.InnerText = dictionary[(SPXX)4];
                                node25.AppendChild(node30);
                                System.Xml.XmlNode node31 = xmlDoc.CreateElement("Dj");
                                node31.InnerText = dictionary[(SPXX)5];
                                node25.AppendChild(node31);
                                System.Xml.XmlNode node32 = xmlDoc.CreateElement("Sl");
                                node32.InnerText = dictionary[(SPXX)6];
                                node25.AppendChild(node32);
                                System.Xml.XmlNode node33 = xmlDoc.CreateElement("Je");
                                node33.InnerText = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(dictionary[(SPXX)7]).ToString("f2");
                                node25.AppendChild(node33);
                                System.Xml.XmlNode node34 = xmlDoc.CreateElement("Slv");
                                node34.InnerText = dictionary[(SPXX)8];
                                node25.AppendChild(node34);
                                System.Xml.XmlNode node35 = xmlDoc.CreateElement("Se");
                                node35.InnerText = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(dictionary[(SPXX)9]).ToString("f2");
                                node25.AppendChild(node35);
                                System.Xml.XmlNode node36 = xmlDoc.CreateElement("Hsjbz");
                                node36.InnerText = dictionary[(SPXX)11];
                                node25.AppendChild(node36);
                                node24.AppendChild(node25);
                            }
                        }
                    }
                    else if ((fp.Mxxx != null) && (fp.Mxxx.Count > 0))
                    {
                        int num2 = 1;
                        foreach (Dictionary<SPXX, string> dictionary2 in fp.Mxxx)
                        {
                            if (dictionary2[(SPXX)10] != "5")
                            {
                                System.Xml.XmlNode node37 = xmlDoc.CreateElement("Mxxx");
                                System.Xml.XmlNode node38 = xmlDoc.CreateElement("ID");
                                node38.InnerText = num2.ToString();
                                num2++;
                                node37.AppendChild(node38);
                                System.Xml.XmlNode node39 = xmlDoc.CreateElement("FPHXZ");
                                node39.InnerText = dictionary2[(SPXX)10];
                                node37.AppendChild(node39);
                                System.Xml.XmlNode node40 = xmlDoc.CreateElement("Spmc");
                                node40.InnerText = dictionary2[(SPXX)0];
                                node37.AppendChild(node40);
                                System.Xml.XmlNode node41 = xmlDoc.CreateElement("Ggxh");
                                node41.InnerText = dictionary2[(SPXX)3];
                                node37.AppendChild(node41);
                                System.Xml.XmlNode node42 = xmlDoc.CreateElement("Jldw");
                                node42.InnerText = dictionary2[(SPXX)4];
                                node37.AppendChild(node42);
                                System.Xml.XmlNode node43 = xmlDoc.CreateElement("Dj");
                                node43.InnerText = dictionary2[(SPXX)5];
                                node37.AppendChild(node43);
                                System.Xml.XmlNode node44 = xmlDoc.CreateElement("Sl");
                                node44.InnerText = dictionary2[(SPXX)6];
                                node37.AppendChild(node44);
                                System.Xml.XmlNode node45 = xmlDoc.CreateElement("Je");
                                node45.InnerText = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(dictionary2[(SPXX)7]).ToString("f2");
                                node37.AppendChild(node45);
                                System.Xml.XmlNode node46 = xmlDoc.CreateElement("Slv");
                                node46.InnerText = dictionary2[(SPXX)8];
                                node37.AppendChild(node46);
                                System.Xml.XmlNode node47 = xmlDoc.CreateElement("Se");
                                node47.InnerText = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(dictionary2[(SPXX)9]).ToString("f2");
                                node37.AppendChild(node47);
                                System.Xml.XmlNode node48 = xmlDoc.CreateElement("Hsjbz");
                                node48.InnerText = dictionary2[(SPXX)11];
                                node37.AppendChild(node48);
                                node24.AppendChild(node37);
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.loger.Error("组件接口发票构造XML异常：" + exception.Message);
                }
            }
        }

        private string GetInvoiceType(FPLX type)
        {
            switch (type)
            {
                case (FPLX)0:
                    return "s";

                case (FPLX)2:
                    return "c";

                case (FPLX)11:
                    return "f";

                case (FPLX)12:
                    return "j";
            }
            return "";
        }

        public bool OutXmlToDiskCard(List<Fpxx> listModel, out string FilePathZip)
        {
            try
            {
                FilePathZip = string.Empty;
                if (listModel == null)
                {
                    MessageManager.ShowMsgBox("FPCX-000016");
                    return false;
                }
                if (0 >= listModel.Count)
                {
                    MessageManager.ShowMsgBox("FPCX-000016");
                    return false;
                }
                string str = listModel[0].gfsh.Trim();
                string filename = this.FILE_PATH + @"\";
                string str3 = "FPRZJK_" + str.Trim() + "_" + base.TaxCardInstance.GetCardClock().ToString("yyyyMMdd_hh_mm_ss") + "_KP";
                if (!Directory.Exists(this.FILE_PATH))
                {
                    Directory.CreateDirectory(this.FILE_PATH);
                }
                FilePathZip = filename + str3 + ".zip";
                string str4 = base.TaxCardInstance.GetCardClock().ToString();
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", "");
                document.AppendChild(newChild);
                System.Xml.XmlNode node = document.CreateElement("FPRZJK");
                XmlComment comment = document.CreateComment("防伪税控开票子系统与认证子系统的接口");
                document.AppendChild(comment);
                System.Xml.XmlNode node2 = document.CreateElement("JK_FWSKKP");
                node.AppendChild(node2);
                System.Xml.XmlNode node3 = document.CreateElement("JK_FWSKKP_BTXX");
                comment = document.CreateComment("购货方纳税人识别号\r\n文件生成日期(格式:YYYYMMDD)");
                node3.AppendChild(comment);
                System.Xml.XmlNode node4 = document.CreateElement("GFSBH");
                node4.InnerText = str;
                node3.AppendChild(node4);
                System.Xml.XmlNode node5 = document.CreateElement("WJRQ");
                node5.InnerText = str4;
                node3.AppendChild(node5);
                node2.AppendChild(node3);
                System.Xml.XmlNode node6 = document.CreateElement("JK_FWSKKP_FPXX");
                comment = document.CreateComment("发票种类\r\n发票代码\r\n发票号码\r\n开票日期(格式:YYYYMMDD)\r\n金额\r\n税额\r\n销货方纳税人识别号\r\n购货方纳税人识别号\r\n密文\r\n加密版本号");
                node6.AppendChild(comment);
                node2.AppendChild(node6);
                foreach (Fpxx fpxx in listModel)
                {
                    System.Xml.XmlNode node7 = document.CreateElement("JK_FWSKKP_MXXX");
                    System.Xml.XmlNode node8 = document.CreateElement("FPZL");
                    node8.InnerText = this.GetInvoiceType(fpxx.fplx);
                    node7.AppendChild(node8);
                    System.Xml.XmlNode node9 = document.CreateElement("FPDM");
                    node9.InnerText = fpxx.fpdm.Trim();
                    node7.AppendChild(node9);
                    System.Xml.XmlNode node10 = document.CreateElement("FPHM");
                    node10.InnerText = fpxx.fphm.Trim();
                    node7.AppendChild(node10);
                    System.Xml.XmlNode node11 = document.CreateElement("KPRQ");
                    node11.InnerText = DateTime.ParseExact(fpxx.kprq.Trim(), "yyyy-MM-dd", CultureInfo.CurrentCulture).ToString();
                    node7.AppendChild(node11);
                    System.Xml.XmlNode node12 = document.CreateElement("JE");
                    node12.InnerText = fpxx.je.Trim();
                    node7.AppendChild(node12);
                    System.Xml.XmlNode node13 = document.CreateElement("SE");
                    node13.InnerText = fpxx.se.Trim();
                    node7.AppendChild(node13);
                    System.Xml.XmlNode node14 = document.CreateElement("XFSBH");
                    node14.InnerText = fpxx.xfsh.Trim();
                    node7.AppendChild(node14);
                    System.Xml.XmlNode node15 = document.CreateElement("GFSBH");
                    node15.InnerText = fpxx.gfsh.Trim();
                    node7.AppendChild(node15);
                    System.Xml.XmlNode node16 = document.CreateElement("MW");
                    node16.InnerText = fpxx.mw.Trim();
                    node7.AppendChild(node16);
                    System.Xml.XmlNode node17 = document.CreateElement("JMBBH");
                    node17.InnerText = fpxx.jmbbh.Trim();
                    node7.AppendChild(node17);
                    node6.AppendChild(node7);
                }
                document.AppendChild(node);
                DirectoryInfo info = new DirectoryInfo(Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf('\\')) + strTempPathEmail);
                if (!info.Exists)
                {
                    info.Create();
                }
                if (filename == "")
                {
                    filename = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf('\\')) + strTempPathEmail + @"\" + str3 + ".xml";
                }
                else
                {
                    filename = this.FILE_PATH + @"\" + str3 + ".xml";
                }
                document.Save(filename);
                FilePathZip = filename;
                if (!File.Exists(filename))
                {
                    MessageManager.ShowMsgBox("FPCX-000017");
                    return false;
                }
                return true;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                FilePathZip = string.Empty;
                return false;
            }
        }

        public void SaveAllFPxxToTxt(List<Fpxx> fpList, string Path, DateTime StartDt, DateTime EndDt)
        {
            try
            {
                if ((fpList == null) || (fpList.Count == 0))
                {
                    MessageManager.ShowMsgBox("FPCX-000039");
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(Path, false, ToolUtil.GetEncoding()))
                    {
                        writer.WriteLine("//发票主体明细信息");
                        writer.WriteLine("//" + StartDt.ToString("yyyyMMdd") + "~~" + EndDt.ToString("yyyyMMdd"));
                        int num = 0;
                        foreach (Fpxx fpxx in fpList)
                        {
                            num++;
                            writer.WriteLine("//发票" + num.ToString());
                            string str = fpxx.fpdm + "~~" + ShareMethods.FPHMTo8Wei(fpxx.fphm) + "~~" + fpxx.kpjh.ToString() + "~~" + fpxx.gfmc + "~~" + fpxx.gfsh + "~~" + fpxx.xfsh + "~~" + Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDateTime(fpxx.kprq).ToString("yyyy-MM-dd") + "~~" + fpxx.je + "~~" + fpxx.se + "~~" + ToolUtil.GetString(Convert.FromBase64String(fpxx.bz)) + "~~" + (fpxx.zfbz ? "1" : "0");
                            writer.WriteLine(str);
                        }
                    }
                    fpList = null;
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        public void SaveSelectToTxt(DataGridViewSelectedRowCollection selectRows, string Path, DateTime StartDt, DateTime EndDt, bool IsWenjian)
        {
            try
            {
                if ((selectRows == null) || (selectRows.Count == 0))
                {
                    MessageManager.ShowMsgBox("FPCX-000039");
                    return;
                }
                List<Fpxx> fpList = new List<Fpxx>();
                if (IsWenjian)
                {
                    using (StreamWriter writer = new StreamWriter(Path + ".txt", false, ToolUtil.GetEncoding()))
                    {
                        writer.WriteLine("//发票主体明细信息");
                        writer.WriteLine("//" + StartDt.ToString("yyyyMMdd") + "~~" + EndDt.ToString("yyyyMMdd"));
                        int num = 0;
                        foreach (DataGridViewRow row in selectRows)
                        {
                            num++;
                            if (row.Cells["FPDM"].Value != null)
                            {
                                string fPZL = row.Cells["FPZL"].Value.ToString();
                                string fPDM = row.Cells["FPDM"].Value.ToString();
                                string data = row.Cells["FPHM"].Value.ToString();
                                Fpxx item = this.xxfpChaXunBll.GetModel(fPZL, fPDM, Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(data), "");
                                if (item != null)
                                {
                                    writer.WriteLine("//发票" + num.ToString());
                                    string str4 = item.fpdm + "~~" + ShareMethods.FPHMTo8Wei(item.fphm) + "~~" + item.kpjh.ToString() + "~~" + item.gfmc + "~~" + item.gfsh + "~~" + item.xfsh + "~~" + Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDateTime(item.kprq).ToString("yyyy-MM-dd") + "~~" + item.je + "~~" + item.se + "~~" + ToolUtil.GetString(Convert.FromBase64String(item.bz)) + "~~" + (item.zfbz ? "1" : "0");
                                    writer.WriteLine(str4);
                                    fpList.Add(item);
                                }
                                else
                                {
                                    this.loger.Error("[SaveToTxt函数]：代码：" + fPDM + "， 发票号码：" + data + "在数据库库没有查到");
                                }
                            }
                        }
                        goto Label_03EC;
                    }
                }
                foreach (DataGridViewRow row2 in selectRows)
                {
                    string str5 = row2.Cells["FPZL"].Value.ToString();
                    string str6 = row2.Cells["FPDM"].Value.ToString();
                    string str7 = row2.Cells["FPHM"].Value.ToString();
                    Fpxx fpxx2 = this.xxfpChaXunBll.GetModel(str5, str6, Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(str7), "");
                    if (fpxx2 != null)
                    {
                        fpList.Add(fpxx2);
                    }
                    else
                    {
                        this.loger.Error("[SaveToTxt函数]：代码：" + str6 + "， 发票号码：" + str7 + "在数据库库没有查到");
                    }
                }
            Label_03EC:
                if (fpList.Count > 0)
                {
                    this.EncryptXML(fpList, Path + ".Dat");
                }
                fpList = null;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        public string FILE_PATH
        {
            get
            {
                return this._strFilePath.Trim();
            }
            set
            {
                this._strFilePath = value.Trim();
            }
        }
    }
}

