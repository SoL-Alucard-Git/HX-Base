namespace Aisino.Fwkp.Fpkj.Form.DKFP
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Const;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fpkj.Common;
    using Aisino.Fwkp.Fpkj.DAL;
    using Aisino.Fwkp.Fpkj.Form.FPXF;
    using Aisino.Fwkp.Fpkj.Model;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;

    public class DiKouFaPiao : DockForm
    {
        private AisinoDataGrid aisinoGrid;
        private IContainer components;
        private ContextMenuStrip contextMenuStrip;
        private Aisino.Fwkp.Fpkj.DAL.DKFP dkfpDal = new Aisino.Fwkp.Fpkj.DAL.DKFP();
        private ILog loger = LogUtil.GetLogger<DiKouFaPiao>();
        private int MAX_DKFP = 0x1388;
        private FPProgressBar progressBar = new FPProgressBar();
        private int step = 0x7d0;
        private ToolStripButton tool_chazhao;
        private ToolStripButton tool_daochu;
        private ToolStripButton tool_geshi;
        private ToolStripButton tool_shanchu;
        private ToolStripButton tool_time_daochu;
        private ToolStripButton tool_tuichu;
        private ToolStripButton tool_xiazai;
        private ToolStrip toolStrip1;
        private ToolStripMenuItem ToolStripMenuItemLb;
        private ToolStripMenuItem ToolStripMenuItemSqd;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private XmlComponentLoader xmlComponentLoader1;

        public DiKouFaPiao()
        {
            this.Initialize();
            this.toolStripSeparator1 = this.xmlComponentLoader1.GetControlByName<ToolStripSeparator>("toolStripSeparator1");
            this.toolStripSeparator2 = this.xmlComponentLoader1.GetControlByName<ToolStripSeparator>("toolStripSeparator2");
            this.aisinoGrid.ReadOnly=true;
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "发票类型");
            item.Add("Property", "FPLX");
            item.Add("Width", "200");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "发票代码");
            item.Add("Property", "FPDM");
            item.Add("Width", "120");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "发票号码");
            item.Add("Property", "FPHM");
            item.Add("Width", "100");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "认证类型");
            item.Add("Property", "RZLX");
            item.Add("Width", "130");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "销方识别号");
            item.Add("Property", "XFSBH");
            item.Add("Width", "160");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "开票日期");
            item.Add("Property", "KPRQ");
            item.Add("Width", "100");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "认证日期");
            item.Add("Property", "RZRQ");
            item.Add("Width", "120");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "金额");
            item.Add("Property", "JE");
            item.Add("Width", "140");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "税额");
            item.Add("Property", "SE");
            item.Add("Width", "120");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "下载时间");
            item.Add("Property", "XZSJ");
            item.Add("Width", "150");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            this.aisinoGrid.ColumeHead = list;
            DataGridViewColumn column = this.aisinoGrid.Columns["FPHM"];
            if (column != null)
            {
                column.DefaultCellStyle.Format = new string('0', 8);
            }
            DataGridViewColumn column2 = this.aisinoGrid.Columns["JE"];
            if (column2 != null)
            {
                column2.DefaultCellStyle.Format = "0.00";
            }
            DataGridViewColumn column3 = this.aisinoGrid.Columns["SE"];
            if (column3 != null)
            {
                column3.DefaultCellStyle.Format = "0.00";
            }
            ControlStyleUtil.SetToolStripStyle(this.toolStrip1);
            this.BindGridData();
        }

        private void aisinoGrid_DataGridRowClickEvent(object sender, DataGridRowEventArgs e)
        {
        }

        private void aisinoGrid_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            this.dkfpDal.CurrentPage = e.PageNO;
            this.aisinoGrid.DataSource = this.dkfpDal.SelectList(e.PageNO, e.PageSize);
        }

        private void BindGridData()
        {
            this.dkfpDal.CurrentPage = 1;
            this.aisinoGrid.DataSource = this.dkfpDal.SelectList(this.dkfpDal.CurrentPage, this.dkfpDal.Pagesize);
        }

        private void but_quxiao_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public XmlDocument genCRCXml(string fileName, string crcValue)
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
            document.PreserveWhitespace = false;
            document.AppendChild(newChild);
            System.Xml.XmlNode node = document.CreateElement("CRC");
            System.Xml.XmlNode node2 = document.CreateElement("FILE");
            System.Xml.XmlAttribute attribute = document.CreateAttribute("NAME");
            System.Xml.XmlAttribute attribute2 = document.CreateAttribute("CRCVALUE");
            document.AppendChild(node);
            node.AppendChild(node2);
            node2.Attributes.Append(attribute);
            node2.Attributes.Append(attribute2);
            attribute.InnerText = fileName;
            attribute2.InnerText = crcValue;
            return document;
        }

        private XmlDocument genDKFPXml(int sNum, int eNum, SelectSSQ selectSsq)
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
            document.PreserveWhitespace = false;
            document.AppendChild(newChild);
            System.Xml.XmlNode node = document.CreateElement("taxML");
            System.Xml.XmlAttribute attribute = document.CreateAttribute("cnName");
            attribute.InnerText = "进项税额申报抵扣明细";
            node.Attributes.Append(attribute);
            System.Xml.XmlAttribute attribute2 = document.CreateAttribute("xmlns");
            attribute2.InnerText = "http://www.chinatax.gov.cn/dataspec/";
            node.Attributes.Append(attribute2);
            System.Xml.XmlAttribute attribute3 = document.CreateAttribute("name");
            attribute3.InnerText = "slSbbtjZzsFB6Request";
            node.Attributes.Append(attribute3);
            System.Xml.XmlAttribute attribute4 = document.CreateAttribute("version");
            attribute4.InnerText = "SW5001-2006";
            node.Attributes.Append(attribute4);
            System.Xml.XmlAttribute attribute5 = document.CreateAttribute("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance");
            attribute5.InnerText = "slSbbtjZzsFB6Request";
            node.Attributes.Append(attribute5);
            document.AppendChild(node);
            System.Xml.XmlNode node2 = document.CreateElement("sbbZzsFB6");
            System.Xml.XmlNode node3 = document.CreateElement("head");
            System.Xml.XmlNode node4 = document.CreateElement("publicHead");
            System.Xml.XmlNode node5 = document.CreateElement("nsrsbh");
            System.Xml.XmlNode node6 = document.CreateElement("nsrmc");
            System.Xml.XmlNode node7 = document.CreateElement("tbrq");
            System.Xml.XmlNode node8 = document.CreateElement("sssq");
            System.Xml.XmlNode node9 = document.CreateElement("rqQ");
            System.Xml.XmlNode node10 = document.CreateElement("rqZ");
            System.Xml.XmlNode node11 = document.CreateElement("body");
            node9.InnerText = selectSsq.ssqQ;
            node10.InnerText = selectSsq.ssqZ;
            System.Xml.XmlNode node12 = document.CreateElement("skzyfpRzxx");
            System.Xml.XmlNode node13 = document.CreateElement("qqrzZzsZyfpmx");
            System.Xml.XmlNode node14 = document.CreateElement("qqrzZzsZyfphjxx");
            System.Xml.XmlNode node15 = document.CreateElement("qqrzZzsfphjs");
            System.Xml.XmlNode node16 = document.CreateElement("qqrzZzsZyfpJe");
            System.Xml.XmlNode node18 = document.CreateElement("qqrzHyZyfpmx");
            System.Xml.XmlNode node19 = document.CreateElement("qqrzHyZyfphjxx");
            System.Xml.XmlNode node20 = document.CreateElement("qqrzHyfphjs");
            System.Xml.XmlNode node17 = document.CreateElement("qqrzZzsZyfpSe");
            System.Xml.XmlNode node21 = document.CreateElement("qqrzHyZyfpJe");
            System.Xml.XmlNode node22 = document.CreateElement("qqrzHyZyfpSe");
            System.Xml.XmlNode node23 = document.CreateElement("bqrzZzsZyfpmx");
            System.Xml.XmlNode node24 = document.CreateElement("bqrzZzsZyfphjxx");
            System.Xml.XmlNode node25 = document.CreateElement("bqrzZzsfphjs");
            System.Xml.XmlNode node26 = document.CreateElement("bqrzZzsZyfpJe");
            System.Xml.XmlNode node27 = document.CreateElement("bqrzZzsZyfpSe");
            System.Xml.XmlNode node28 = document.CreateElement("bqrzHyZyfpmx");
            System.Xml.XmlNode node29 = document.CreateElement("bqrzHyZyfphjxx");
            System.Xml.XmlNode node30 = document.CreateElement("bqrzHyfphjs");
            System.Xml.XmlNode node31 = document.CreateElement("bqrzHyZyfpJe");
            System.Xml.XmlNode node32 = document.CreateElement("bqrzHyZyfpSe");
            System.Xml.XmlNode node33 = document.CreateElement("hgzyjksRzxx");
            System.Xml.XmlNode node34 = document.CreateElement("hgzyjksRzmx");
            System.Xml.XmlNode node35 = document.CreateElement("hgzyjkshjxx");
            System.Xml.XmlNode node36 = document.CreateElement("hgzyjkshjs");
            System.Xml.XmlNode node37 = document.CreateElement("hgzyjkshjSe");
            node.AppendChild(node2);
            node2.AppendChild(node3);
            node2.AppendChild(node11);
            node3.AppendChild(node4);
            node4.AppendChild(node5);
            node4.AppendChild(node6);
            node4.AppendChild(node7);
            node4.AppendChild(node8);
            node8.AppendChild(node9);
            node8.AppendChild(node10);
            node11.AppendChild(node12);
            node11.AppendChild(node33);
            node12.AppendChild(node13);
            node12.AppendChild(node14);
            node12.AppendChild(node18);
            node12.AppendChild(node19);
            node12.AppendChild(node23);
            node12.AppendChild(node24);
            node12.AppendChild(node28);
            node12.AppendChild(node29);
            node33.AppendChild(node34);
            node33.AppendChild(node35);
            node14.AppendChild(node15);
            node14.AppendChild(node16);
            node14.AppendChild(node17);
            node19.AppendChild(node20);
            node19.AppendChild(node21);
            node19.AppendChild(node22);
            node24.AppendChild(node25);
            node24.AppendChild(node26);
            node24.AppendChild(node27);
            node29.AppendChild(node30);
            node29.AppendChild(node31);
            node29.AppendChild(node32);
            node33.AppendChild(node34);
            node33.AppendChild(node35);
            node35.AppendChild(node36);
            node35.AppendChild(node37);
            node6.InnerText = base.TaxCardInstance.Corporation;
            node5.InnerText = base.TaxCardInstance.TaxCode;
            node7.InnerText = selectSsq.tbrq;
            Aisino.Fwkp.Fpkj.Model.DKFP model = null;
            string fPDM = "";
            int fPHM = 0;
            int num2 = 1;
            double num3 = 0.0;
            double num4 = 0.0;
            int num5 = 1;
            double num6 = 0.0;
            double num7 = 0.0;
            int num8 = 1;
            double num9 = 0.0;
            double num10 = 0.0;
            int num11 = 1;
            double num12 = 0.0;
            double num13 = 0.0;
            for (int i = sNum; i <= eNum; i++)
            {
                try
                {
                    int num15;
                    fPDM = this.aisinoGrid.SelectedRows[i].Cells["FPDM"].Value.ToString();
                    fPHM = int.Parse(this.aisinoGrid.SelectedRows[i].Cells["FPHM"].Value.ToString());
                    model = this.dkfpDal.GetModel(fPDM, fPHM);
                    System.Xml.XmlNode node38 = document.CreateElement("mxxx");
                    System.Xml.XmlNode node39 = document.CreateElement("xh");
                    System.Xml.XmlNode node40 = document.CreateElement("fpdm");
                    System.Xml.XmlNode node41 = document.CreateElement("fphm");
                    System.Xml.XmlNode node42 = document.CreateElement("rzlx");
                    System.Xml.XmlNode node43 = document.CreateElement("fplx");
                    System.Xml.XmlNode node44 = document.CreateElement("kprq");
                    System.Xml.XmlNode node45 = document.CreateElement("xsfnsrsbh");
                    System.Xml.XmlNode node46 = document.CreateElement("je");
                    System.Xml.XmlNode node47 = document.CreateElement("se");
                    System.Xml.XmlNode node48 = document.CreateElement("rzrq");
                    System.Xml.XmlNode node49 = document.CreateElement("bz");
                    node40.InnerText = model.FPDM;
                    node41.InnerText = ShareMethods.FPHMTo8Wei(model.FPHM.ToString());
                    node42.InnerText = model.RZLX;
                    node43.InnerText = model.FPLX;
                    node44.InnerText = model.KPRQ.ToString("yyyyMMdd");
                    node45.InnerText = model.XFSBH;
                    node46.InnerText = model.JE.ToString();
                    node47.InnerText = model.SE.ToString();
                    node48.InnerText = model.RZRQ.ToString("yyyyMMdd");
                    node49.InnerText = "";
                    node38.AppendChild(node39);
                    node38.AppendChild(node40);
                    node38.AppendChild(node41);
                    node38.AppendChild(node44);
                    node38.AppendChild(node45);
                    node38.AppendChild(node46);
                    node38.AppendChild(node47);
                    node38.AppendChild(node48);
                    node38.AppendChild(node49);
                    if ((model.RZLX == "0") && (model.FPLX == "01"))
                    {
                        num15 = num2++;
                        node39.InnerText = num15.ToString();
                        num3 += model.JE;
                        num4 += model.SE;
                        node23.AppendChild(node38);
                    }
                    else if ((model.RZLX == "0") && (model.FPLX == "02"))
                    {
                        num15 = num5++;
                        node39.InnerText = num15.ToString();
                        num6 += model.JE;
                        num7 += model.SE;
                        node28.AppendChild(node38);
                    }
                    else if ((model.RZLX == "1") && (model.FPLX == "01"))
                    {
                        num15 = num8++;
                        node39.InnerText = num15.ToString();
                        num9 += model.JE;
                        num10 += model.SE;
                        node13.AppendChild(node38);
                    }
                    else if ((model.RZLX == "1") && (model.FPLX == "02"))
                    {
                        node39.InnerText = num11++.ToString();
                        num12 += model.JE;
                        num13 += model.SE;
                        node18.AppendChild(node38);
                    }
                }
                catch (Exception exception)
                {
                    this.loger.Error(exception.Message);
                }
            }
            node15.InnerText = (num8 - 1).ToString();
            node16.InnerText = num9.ToString();
            node17.InnerText = num10.ToString();
            node21.InnerText = num12.ToString();
            node20.InnerText = (num11 - 1).ToString();
            node22.InnerText = num13.ToString();
            node25.InnerText = (num2 - 1).ToString();
            node26.InnerText = num3.ToString();
            node27.InnerText = num4.ToString();
            node31.InnerText = num6.ToString();
            node30.InnerText = (num5 - 1).ToString();
            node32.InnerText = num7.ToString();
            document.AppendChild(node);
            document.PreserveWhitespace = true;
            return document;
        }

        private XmlDocument genDKFPXml(int sNum, int eNum, SelectSSQ selectSsq, List<Aisino.Fwkp.Fpkj.Model.DKFP> dkfpList)
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
            document.PreserveWhitespace = false;
            document.AppendChild(newChild);
            System.Xml.XmlNode node = document.CreateElement("taxML");
            System.Xml.XmlAttribute attribute = document.CreateAttribute("cnName");
            attribute.InnerText = "进项税额申报抵扣明细";
            node.Attributes.Append(attribute);
            System.Xml.XmlAttribute attribute2 = document.CreateAttribute("xmlns");
            attribute2.InnerText = "http://www.chinatax.gov.cn/dataspec/";
            node.Attributes.Append(attribute2);
            System.Xml.XmlAttribute attribute3 = document.CreateAttribute("name");
            attribute3.InnerText = "slSbbtjZzsFB6Request";
            node.Attributes.Append(attribute3);
            System.Xml.XmlAttribute attribute4 = document.CreateAttribute("version");
            attribute4.InnerText = "SW5001-2006";
            node.Attributes.Append(attribute4);
            System.Xml.XmlAttribute attribute5 = document.CreateAttribute("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance");
            attribute5.InnerText = "slSbbtjZzsFB6Request";
            node.Attributes.Append(attribute5);
            document.AppendChild(node);
            System.Xml.XmlNode node2 = document.CreateElement("sbbZzsFB6");
            System.Xml.XmlNode node3 = document.CreateElement("head");
            System.Xml.XmlNode node4 = document.CreateElement("publicHead");
            System.Xml.XmlNode node5 = document.CreateElement("nsrsbh");
            System.Xml.XmlNode node6 = document.CreateElement("nsrmc");
            System.Xml.XmlNode node7 = document.CreateElement("tbrq");
            System.Xml.XmlNode node8 = document.CreateElement("sssq");
            System.Xml.XmlNode node9 = document.CreateElement("rqQ");
            System.Xml.XmlNode node10 = document.CreateElement("rqZ");
            System.Xml.XmlNode node11 = document.CreateElement("body");
            node9.InnerText = selectSsq.ssqQ;
            node10.InnerText = selectSsq.ssqZ;
            System.Xml.XmlNode node12 = document.CreateElement("skzyfpRzxx");
            System.Xml.XmlNode node13 = document.CreateElement("qqrzZzsZyfpmx");
            System.Xml.XmlNode node14 = document.CreateElement("qqrzZzsZyfphjxx");
            System.Xml.XmlNode node15 = document.CreateElement("qqrzZzsfphjs");
            System.Xml.XmlNode node16 = document.CreateElement("qqrzZzsZyfpJe");
            System.Xml.XmlNode node18 = document.CreateElement("qqrzHyZyfpmx");
            System.Xml.XmlNode node19 = document.CreateElement("qqrzHyZyfphjxx");
            System.Xml.XmlNode node20 = document.CreateElement("qqrzHyfphjs");
            System.Xml.XmlNode node17 = document.CreateElement("qqrzZzsZyfpSe");
            System.Xml.XmlNode node21 = document.CreateElement("qqrzHyZyfpJe");
            System.Xml.XmlNode node22 = document.CreateElement("qqrzHyZyfpSe");
            System.Xml.XmlNode node23 = document.CreateElement("bqrzZzsZyfpmx");
            System.Xml.XmlNode node24 = document.CreateElement("bqrzZzsZyfphjxx");
            System.Xml.XmlNode node25 = document.CreateElement("bqrzZzsfphjs");
            System.Xml.XmlNode node26 = document.CreateElement("bqrzZzsZyfpJe");
            System.Xml.XmlNode node27 = document.CreateElement("bqrzZzsZyfpSe");
            System.Xml.XmlNode node28 = document.CreateElement("bqrzHyZyfpmx");
            System.Xml.XmlNode node29 = document.CreateElement("bqrzHyZyfphjxx");
            System.Xml.XmlNode node30 = document.CreateElement("bqrzHyfphjs");
            System.Xml.XmlNode node31 = document.CreateElement("bqrzHyZyfpJe");
            System.Xml.XmlNode node32 = document.CreateElement("bqrzHyZyfpSe");
            System.Xml.XmlNode node33 = document.CreateElement("hgzyjksRzxx");
            System.Xml.XmlNode node34 = document.CreateElement("hgzyjksRzmx");
            System.Xml.XmlNode node35 = document.CreateElement("hgzyjkshjxx");
            System.Xml.XmlNode node36 = document.CreateElement("hgzyjkshjs");
            System.Xml.XmlNode node37 = document.CreateElement("hgzyjkshjSe");
            node.AppendChild(node2);
            node2.AppendChild(node3);
            node2.AppendChild(node11);
            node3.AppendChild(node4);
            node4.AppendChild(node5);
            node4.AppendChild(node6);
            node4.AppendChild(node7);
            node4.AppendChild(node8);
            node8.AppendChild(node9);
            node8.AppendChild(node10);
            node11.AppendChild(node12);
            node11.AppendChild(node33);
            node12.AppendChild(node13);
            node12.AppendChild(node14);
            node12.AppendChild(node18);
            node12.AppendChild(node19);
            node12.AppendChild(node23);
            node12.AppendChild(node24);
            node12.AppendChild(node28);
            node12.AppendChild(node29);
            node33.AppendChild(node34);
            node33.AppendChild(node35);
            node14.AppendChild(node15);
            node14.AppendChild(node16);
            node14.AppendChild(node17);
            node19.AppendChild(node20);
            node19.AppendChild(node21);
            node19.AppendChild(node22);
            node24.AppendChild(node25);
            node24.AppendChild(node26);
            node24.AppendChild(node27);
            node29.AppendChild(node30);
            node29.AppendChild(node31);
            node29.AppendChild(node32);
            node33.AppendChild(node34);
            node33.AppendChild(node35);
            node35.AppendChild(node36);
            node35.AppendChild(node37);
            node6.InnerText = base.TaxCardInstance.Corporation;
            node5.InnerText = base.TaxCardInstance.TaxCode;
            node7.InnerText = selectSsq.tbrq;
            Aisino.Fwkp.Fpkj.Model.DKFP model = null;
            string fPDM = "";
            int fPHM = 0;
            int num2 = 1;
            double num3 = 0.0;
            double num4 = 0.0;
            int num5 = 1;
            double num6 = 0.0;
            double num7 = 0.0;
            int num8 = 1;
            double num9 = 0.0;
            double num10 = 0.0;
            int num11 = 1;
            double num12 = 0.0;
            double num13 = 0.0;
            for (int i = sNum; i <= eNum; i++)
            {
                try
                {
                    int num15;
                    fPDM = dkfpList[i].FPDM;
                    fPHM = dkfpList[i].FPHM;
                    model = this.dkfpDal.GetModel(fPDM, fPHM);
                    System.Xml.XmlNode node38 = document.CreateElement("mxxx");
                    System.Xml.XmlNode node39 = document.CreateElement("xh");
                    System.Xml.XmlNode node40 = document.CreateElement("fpdm");
                    System.Xml.XmlNode node41 = document.CreateElement("fphm");
                    System.Xml.XmlNode node42 = document.CreateElement("rzlx");
                    System.Xml.XmlNode node43 = document.CreateElement("fplx");
                    System.Xml.XmlNode node44 = document.CreateElement("kprq");
                    System.Xml.XmlNode node45 = document.CreateElement("xsfnsrsbh");
                    System.Xml.XmlNode node46 = document.CreateElement("je");
                    System.Xml.XmlNode node47 = document.CreateElement("se");
                    System.Xml.XmlNode node48 = document.CreateElement("rzrq");
                    System.Xml.XmlNode node49 = document.CreateElement("bz");
                    node40.InnerText = model.FPDM;
                    node41.InnerText = ShareMethods.FPHMTo8Wei(model.FPHM.ToString());
                    node42.InnerText = model.RZLX;
                    node43.InnerText = model.FPLX;
                    node44.InnerText = model.KPRQ.ToString("yyyyMMdd");
                    node45.InnerText = model.XFSBH;
                    node46.InnerText = model.JE.ToString();
                    node47.InnerText = model.SE.ToString();
                    node48.InnerText = model.RZRQ.ToString("yyyyMMdd");
                    node49.InnerText = "";
                    node38.AppendChild(node39);
                    node38.AppendChild(node40);
                    node38.AppendChild(node41);
                    node38.AppendChild(node44);
                    node38.AppendChild(node45);
                    node38.AppendChild(node46);
                    node38.AppendChild(node47);
                    node38.AppendChild(node48);
                    node38.AppendChild(node49);
                    if ((model.RZLX == "0") && (model.FPLX == "01"))
                    {
                        num15 = num2++;
                        node39.InnerText = num15.ToString();
                        num3 += model.JE;
                        num4 += model.SE;
                        node23.AppendChild(node38);
                    }
                    else if ((model.RZLX == "0") && (model.FPLX == "02"))
                    {
                        num15 = num5++;
                        node39.InnerText = num15.ToString();
                        num6 += model.JE;
                        num7 += model.SE;
                        node28.AppendChild(node38);
                    }
                    else if ((model.RZLX == "1") && (model.FPLX == "01"))
                    {
                        num15 = num8++;
                        node39.InnerText = num15.ToString();
                        num9 += model.JE;
                        num10 += model.SE;
                        node13.AppendChild(node38);
                    }
                    else if ((model.RZLX == "1") && (model.FPLX == "02"))
                    {
                        node39.InnerText = num11++.ToString();
                        num12 += model.JE;
                        num13 += model.SE;
                        node18.AppendChild(node38);
                    }
                }
                catch (Exception exception)
                {
                    this.loger.Error(exception.Message);
                }
            }
            node15.InnerText = (num8 - 1).ToString();
            node16.InnerText = num9.ToString();
            node17.InnerText = num10.ToString();
            node21.InnerText = num12.ToString();
            node20.InnerText = (num11 - 1).ToString();
            node22.InnerText = num13.ToString();
            node25.InnerText = (num2 - 1).ToString();
            node26.InnerText = num3.ToString();
            node27.InnerText = num4.ToString();
            node31.InnerText = num6.ToString();
            node30.InnerText = (num5 - 1).ToString();
            node32.InnerText = num7.ToString();
            document.AppendChild(node);
            document.PreserveWhitespace = true;
            return document;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.tool_tuichu = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_tuichu");
            this.tool_chazhao = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_chazhao");
            this.tool_shanchu = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_shanchu");
            this.aisinoGrid = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoGrid");
            this.tool_daochu = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_daochu");
            this.tool_time_daochu = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_time_daochu");
            this.tool_xiazai = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_xiazai");
            this.tool_geshi = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_geshi");
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.toolStripSeparator1 = this.xmlComponentLoader1.GetControlByName<ToolStripSeparator>("toolStripSeparator1");
            this.toolStripSeparator2 = this.xmlComponentLoader1.GetControlByName<ToolStripSeparator>("toolStripSeparator2");
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Height = SystemColor.TOOLSCRIPT_CONTROL_HEIGHT;
            this.toolStripSeparator1.Font = SystemColor.TOOLSCRIPT_BOLD_FONT;
            this.toolStripSeparator1.RightToLeft = RightToLeft.No;
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Height = SystemColor.TOOLSCRIPT_CONTROL_HEIGHT;
            this.toolStripSeparator2.Font = SystemColor.TOOLSCRIPT_BOLD_FONT;
            this.toolStripSeparator2.RightToLeft = RightToLeft.No;
            this.tool_tuichu.Click += new EventHandler(this.tool_tuichu_Click);
            this.tool_chazhao.Click += new EventHandler(this.tool_chazhao_Click);
            this.tool_shanchu.Click += new EventHandler(this.tool_shanchu_Click);
            this.tool_daochu.Click += new EventHandler(this.tool_daochu_Click);
            this.tool_xiazai.Click += new EventHandler(this.tool_xiazai_Click);
            this.tool_geshi.Click += new EventHandler(this.tool_geshi_Click);
            this.tool_time_daochu.Click += new EventHandler(this.tool_time_daochu_Click);
            this.contextMenuStrip = new ContextMenuStrip();
            this.ToolStripMenuItemSqd = new ToolStripMenuItem("抵扣发票");
            this.ToolStripMenuItemLb = new ToolStripMenuItem("抵扣发票列表");
            this.contextMenuStrip.Items.Add(this.ToolStripMenuItemSqd);
            this.contextMenuStrip.Items.Add(this.ToolStripMenuItemLb);
            if (base.TaxCardInstance.QYLX.ISTDQY)
            {
                this.tool_xiazai.Visible = false;
            }
            this.aisinoGrid.GoToPageEvent +=  this.aisinoGrid_GoToPageEvent;
            this.tool_tuichu.Margin = new Padding(20, 1, 0, 2);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(DiKouFaPiao));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x2c2, 0x21a);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.XMLPath=@"..\Config\Components\Aisino.Fwkp.Fpkj.Form.DKFP.DiKouFaPiao\Aisino.Fwkp.Fpkj.Form.DKFP.DiKouFaPiao.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2c2, 0x21a);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "DkfpChaXun";
            this.Text = "抵扣发票查询";
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "抵扣发票查询";
            base.ResumeLayout(false);
        }

        private void PerformStep(int step)
        {
            if (this.progressBar.fpxf_progressBar.InvokeRequired)
            {
                PerformStepHandle method = new PerformStepHandle(this.PerformStep);
                this.progressBar.Invoke(method, new object[] { step });
            }
            else
            {
                for (int i = 0; i < step; i++)
                {
                    this.progressBar.fpxf_progressBar.Value++;
                }
                this.Refresh();
            }
        }

        private void ProccessBarShow(object obj)
        {
            try
            {
                int step = (int) obj;
                this.PerformStep(step);
            }
            catch (Exception exception)
            {
                this.loger.Info("[ThreadFun]" + exception.Message);
            }
        }

        public void ProcessStartThread(int value)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(this.ProccessBarShow));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(value);
        }

        private void tool_chazhao_Click(object sender, EventArgs e)
        {
            ChaXunTiaoJian jian = new ChaXunTiaoJian {
                AisinoGrid = this.aisinoGrid
            };
            if (jian.ShowDialog(this) == DialogResult.OK)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("FPDM", jian.fpdm);
                dictionary.Add("FPHM", jian.fphm);
                dictionary.Add("XFSBH", jian.xfsh);
                dictionary.Add("FPZL", jian.fpzl);
                dictionary.Add("KPRQ_S", jian.kprq_s);
                dictionary.Add("KPRQ_E", jian.kprq_e);
                dictionary.Add("RZRQ_S", jian.rzrq_s);
                dictionary.Add("RZRQ_E", jian.rzrq_e);
                dictionary.Add("KPRQ_S_CK", jian.kprq_s_ck);
                dictionary.Add("KPRQ_E_CK", jian.kprq_e_ck);
                dictionary.Add("RZRQ_S_CK", jian.rzrq_s_ck);
                dictionary.Add("RZRQ_E_CK", jian.rzrq_e_ck);
                this.dkfpDal.CurrentPage = 1;
                this.aisinoGrid.DataSource=this.dkfpDal.SelectDkfplist_ChaXunTiaoJian(this.dkfpDal.CurrentPage, this.dkfpDal.Pagesize, dictionary);
            }
        }

        private void tool_daochu_Click(object sender, EventArgs e)
        {
            try
            {
                int count = this.aisinoGrid.SelectedRows.Count;
                if (count <= 0)
                {
                    MessageManager.ShowMsgBox("DKFPXZ-0001");
                }
                else
                {
                    SelectSSQ selectSsq = new SelectSSQ();
                    if (DialogResult.OK == selectSsq.ShowDialog(this))
                    {
                        string selectedPath = string.Empty;
                        FolderBrowserDialog dialog = new FolderBrowserDialog {
                            Description = "选择导出的目录",
                            RootFolder = Environment.SpecialFolder.Desktop,
                            ShowNewFolderButton = true
                        };
                        DialogResult result = dialog.ShowDialog(this);
                        string crcValue = "";
                        if (result == DialogResult.OK)
                        {
                            selectedPath = dialog.SelectedPath;
                            if (this.aisinoGrid.SelectedRows.Count > 0)
                            {
                                string str3 = "";
                                int num2 = 0;
                                int num3 = 0;
                                string str4 = "";
                                string fileName = string.Empty;
                                string str6 = string.Empty;
                                XmlDocument document = new XmlDocument();
                                XmlDocument document2 = new XmlDocument();
                                num2 = this.aisinoGrid.SelectedRows.Count / this.MAX_DKFP;
                                if ((this.aisinoGrid.SelectedRows.Count % this.MAX_DKFP) != 0)
                                {
                                    num2++;
                                }
                                int sNum = 0;
                                int eNum = 0;
                                this.progressBar.SetTip("正在导出发票", "请等待任务完成", "发票导出中");
                                this.progressBar.fpxf_progressBar.Value = 500;
                                this.progressBar.Visible = true;
                                this.progressBar.Show();
                                this.progressBar.Refresh();
                                this.ProcessStartThread(this.step);
                                this.progressBar.Refresh();
                                for (int i = 1; i <= num2; i++)
                                {
                                    num3 = i;
                                    str4 = string.Concat(new object[] { @"\taxML_ZZSJXDKMX_", num2, "_", num3, "_", selectSsq.ssqQ, "_", selectSsq.ssqZ, "_", base.TaxCardInstance.TaxCode });
                                    str3 = string.Concat(new object[] { "taxML_ZZSJXDKMX_", num2, "_", num3, "_", selectSsq.ssqQ, "_", selectSsq.ssqZ, "_", base.TaxCardInstance.TaxCode });
                                    fileName = str3 + "_V10.xml";
                                    str6 = str3 + "_CRC.xml";
                                    if (!Directory.Exists(selectedPath + str4))
                                    {
                                        Directory.CreateDirectory(selectedPath + str4);
                                    }
                                    sNum = (i - 1) * this.MAX_DKFP;
                                    eNum = (i * this.MAX_DKFP) - 1;
                                    if ((this.aisinoGrid.SelectedRows.Count - (this.MAX_DKFP * i)) < 0)
                                    {
                                        eNum = this.aisinoGrid.SelectedRows.Count - 1;
                                    }
                                    try
                                    {
                                        this.genDKFPXml(sNum, eNum, selectSsq).Save(selectedPath + str4 + @"\" + fileName);
                                        crcValue = Convert.ToString((long) Crc32.GetFileCRC32(selectedPath + str4 + @"\" + fileName), 0x10).ToUpper();
                                        this.genCRCXml(fileName, crcValue).Save(selectedPath + str4 + @"\" + str6);
                                        ZipUtil.Zip(selectedPath + str4, selectedPath + str4 + ".zip", "");
                                        if (Directory.Exists(selectedPath + str4))
                                        {
                                            Directory.Delete(selectedPath + str4, true);
                                        }
                                    }
                                    catch (Exception exception)
                                    {
                                        if (Directory.Exists(selectedPath + str4))
                                        {
                                            Directory.Delete(selectedPath + str4, true);
                                        }
                                        this.progressBar.Visible = false;
                                        this.loger.Error("[发票导出]" + exception.Message);
                                        MessageManager.ShowMsgBox("DKFPXZ-0014", "错误");
                                        return;
                                    }
                                }
                                this.progressBar.SetTip("正在导出发票", "请等待任务完成", "发票导出中");
                                this.ProcessStartThread(this.step * 3);
                                this.progressBar.Refresh();
                                this.progressBar.Visible = false;
                            }
                            MessageManager.ShowMsgBox("DKFPXZ-0008", "提示", new string[] { count.ToString() });
                        }
                    }
                }
            }
            catch (Exception exception2)
            {
                this.progressBar.Visible = false;
                this.loger.Error("[发票导出]" + exception2.Message);
                MessageManager.ShowMsgBox("DKFPXZ-0011", "提示", new string[] { exception2.ToString() });
            }
            finally
            {
                this.progressBar.Visible = false;
                this.Refresh();
            }
        }

        private void tool_geshi_Click(object sender, EventArgs e)
        {
            this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoGrid").SetColumnsStyle(this.xmlComponentLoader1.XMLPath, this);
        }

        private void tool_shanchu_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = this.aisinoGrid.SelectedRows;
            if (rows.Count <= 0)
            {
                MessageManager.ShowMsgBox("DKFPXZ-0001");
            }
            else if (DialogResult.OK == MessageBoxHelper.Show("确定要删除选择行", "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk))
            {
                int num = 0;
                for (int i = 0; i < rows.Count; i++)
                {
                    DataGridViewRow row = rows[i];
                    string fPDM = row.Cells["FPDM"].Value.ToString();
                    int fPHM = int.Parse(row.Cells["FPHM"].Value.ToString());
                    this.dkfpDal.Delete(fPDM, fPHM);
                    num++;
                }
                if (num == rows.Count)
                {
                    MessageManager.ShowMsgBox("DKFPXZ-0009", "提示", new string[] { rows.Count.ToString() });
                }
                else
                {
                    string[] strArray2 = new string[] { rows.Count.ToString(), num.ToString(), (rows.Count - num).ToString() };
                    MessageManager.ShowMsgBox("DKFPXZ-0010", "提示", strArray2);
                }
                this.BindGridData();
            }
        }

        private void tool_time_daochu_Click(object sender, EventArgs e)
        {
            try
            {
                SelectTime time = new SelectTime {
                    Text = "抵扣发票导出"
                };
                if (DialogResult.OK == time.ShowDialog())
                {
                    SelectSSQ selectSsq = new SelectSSQ();
                    if (DialogResult.OK == selectSsq.ShowDialog(this))
                    {
                        List<Aisino.Fwkp.Fpkj.Model.DKFP> dkfpList = this.dkfpDal.SelectDkpf_DKFPDaoChu(SelectTime.OutPutCondition);
                        if (dkfpList.Count == 0)
                        {
                            MessageManager.ShowMsgBox("DKFPXZ-0012", "提示");
                        }
                        else
                        {
                            string selectedPath = string.Empty;
                            FolderBrowserDialog dialog = new FolderBrowserDialog {
                                Description = "选择导出的目录",
                                RootFolder = Environment.SpecialFolder.Desktop,
                                ShowNewFolderButton = true
                            };
                            DialogResult result = dialog.ShowDialog(this);
                            string crcValue = "";
                            if (result == DialogResult.OK)
                            {
                                selectedPath = dialog.SelectedPath;
                                if (dkfpList.Count > 0)
                                {
                                    string str3 = "";
                                    int num = 0;
                                    int num2 = 0;
                                    string str4 = "";
                                    string fileName = string.Empty;
                                    string str6 = string.Empty;
                                    XmlDocument document = new XmlDocument();
                                    XmlDocument document2 = new XmlDocument();
                                    num = dkfpList.Count / this.MAX_DKFP;
                                    if ((dkfpList.Count % this.MAX_DKFP) != 0)
                                    {
                                        num++;
                                    }
                                    int sNum = 0;
                                    int eNum = 0;
                                    this.progressBar.SetTip("正在导出发票", "请等待任务完成", "发票导出中");
                                    this.progressBar.fpxf_progressBar.Value = 1;
                                    this.progressBar.Visible = true;
                                    this.progressBar.Show();
                                    this.progressBar.Refresh();
                                    Thread.Sleep(200);
                                    this.ProcessStartThread(this.step);
                                    this.progressBar.Refresh();
                                    Thread.Sleep(200);
                                    for (int i = 1; i <= num; i++)
                                    {
                                        num2 = i;
                                        str4 = string.Concat(new object[] { @"\taxML_ZZSJXDKMX_", num, "_", num2, "_", selectSsq.ssqQ, "_", selectSsq.ssqZ, "_", base.TaxCardInstance.TaxCode });
                                        str3 = string.Concat(new object[] { "taxML_ZZSJXDKMX_", num, "_", num2, "_", selectSsq.ssqQ, "_", selectSsq.ssqZ, "_", base.TaxCardInstance.TaxCode });
                                        fileName = str3 + "_V10.xml";
                                        str6 = str3 + "_CRC.xml";
                                        if (!Directory.Exists(selectedPath + str4))
                                        {
                                            Directory.CreateDirectory(selectedPath + str4);
                                        }
                                        sNum = (i - 1) * this.MAX_DKFP;
                                        eNum = (i * this.MAX_DKFP) - 1;
                                        if ((dkfpList.Count - (this.MAX_DKFP * i)) < 0)
                                        {
                                            eNum = dkfpList.Count - 1;
                                        }
                                        try
                                        {
                                            this.genDKFPXml(sNum, eNum, selectSsq, dkfpList).Save(selectedPath + str4 + @"\" + fileName);
                                            crcValue = Convert.ToString((long) Crc32.GetFileCRC32(selectedPath + str4 + @"\" + fileName), 0x10).ToUpper();
                                            this.genCRCXml(fileName, crcValue).Save(selectedPath + str4 + @"\" + str6);
                                            ZipUtil.Zip(selectedPath + str4, selectedPath + str4 + ".zip", "");
                                            if (Directory.Exists(selectedPath + str4))
                                            {
                                                Directory.Delete(selectedPath + str4, true);
                                            }
                                        }
                                        catch (Exception exception)
                                        {
                                            if (Directory.Exists(selectedPath + str4))
                                            {
                                                Directory.Delete(selectedPath + str4, true);
                                            }
                                            this.progressBar.Visible = false;
                                            this.loger.Error("[发票导出]" + exception.Message);
                                            MessageManager.ShowMsgBox("DKFPXZ-0014", "错误");
                                            return;
                                        }
                                    }
                                    this.ProcessStartThread(this.step * 3);
                                    this.progressBar.Visible = false;
                                    this.Refresh();
                                }
                                MessageManager.ShowMsgBox("DKFPXZ-0008", "提示", new string[] { dkfpList.Count.ToString() });
                            }
                        }
                    }
                }
            }
            catch (Exception exception2)
            {
                this.loger.Error("[发票导出]" + exception2.Message);
                MessageManager.ShowMsgBox("DKFPXZ-0011", "提示", new string[] { exception2.ToString() });
            }
            finally
            {
                this.progressBar.Visible = false;
                this.Refresh();
            }
        }

        private void tool_tuichu_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void tool_xiazai_Click(object sender, EventArgs e)
        {
            DiKouFaPiaoXiaZai zai = new DiKouFaPiaoXiaZai();
            switch (zai.ShowDialog(this))
            {
                case DialogResult.OK:
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    this.aisinoGrid.DataSource=this.dkfpDal.SelectDkfplist(this.dkfpDal.CurrentPage, this.dkfpDal.Pagesize, dictionary);
                    return;
                }
                case DialogResult.Retry:
                    this.tool_xiazai_Click(sender, e);
                    break;
            }
        }

        private delegate void PerformStepHandle(int step);
    }
}

