namespace Aisino.Fwkp.Fptk.Form
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fptk;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;
    using Framework.Plugin.Core;
    public class ManualImport : Form
    {
        private List<Aisino.Fwkp.Fptk.Form.Djfp> allmDjList = new List<Aisino.Fwkp.Fptk.Form.Djfp>();
        public static Fpxx blueFpxx;
        private AisinoBTN btnQuery;
        private AisinoBTN btnQueryAll;
        private IContainer components;
        private CustomStyleDataGrid dgDJ;
        private FPDJHelper djHelper = new FPDJHelper();
        public AisinoLBL lblGfmc;
        public AisinoLBL lblGfsh;
        private Aisino.Fwkp.Fptk.Form.Djfp mDjfp;
        private List<Aisino.Fwkp.Fptk.Form.Djfp> mDjList = new List<Aisino.Fwkp.Fptk.Form.Djfp>();
        private FPLX mFplx;
        internal ZYFP_LX mZyfplx;
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();
        private ToolStripButton tool_exit;
        private ToolStripButton tool_select;
        private ToolStrip toolStrip1;
        private AisinoTXT txtdjh;
        private AisinoTXT txtgfmc;
        private AisinoTXT txtgfsh;
        private XmlComponentLoader xmlComponentLoader1;
        private List<string> ykdjList = new List<string>();

        public ManualImport(FPLX fplx, int zyfplx)
        {
            this.mFplx = fplx;
            this.Initialize();
            this.dgDJ.AllowUserToAddRows = false;
            this.dgDJ.MultiSelect = false;
            if (((this.mFplx == 0) || ((int)this.mFplx == 2)) || ((int)this.mFplx == 0x33))
            {
                if ((int)zyfplx == 1)
                {
                    this.lblGfmc.Text = "销方名称";
                    this.lblGfsh.Text = "销方税号";
                    this.dgDJ.Columns["colGfmc"].HeaderText = "销方名称";
                    this.dgDJ.Columns["colGfsh"].HeaderText = "销方税号";
                }
                else
                {
                    this.lblGfmc.Text = "购方名称";
                    this.lblGfsh.Text = "购方税号";
                    this.dgDJ.Columns["colGfmc"].HeaderText = "购方名称";
                    this.dgDJ.Columns["colGfsh"].HeaderText = "购方税号";
                }
            }
            else if ((int)this.mFplx == 11)
            {
                this.lblGfmc.Text = "实际受票方名称";
                this.lblGfsh.Text = "实际受票方税号";
                this.dgDJ.Columns["colGfmc"].HeaderText = "实际受票方名称";
                this.dgDJ.Columns["colGfsh"].HeaderText = "实际受票方税号";
                this.txtgfmc.Location = new Point(this.lblGfmc.Location.X + 0x73, this.txtgfmc.Location.Y);
                this.txtgfsh.Location = new Point(this.lblGfsh.Location.X + 0x73, this.txtgfsh.Location.Y);
            }
            else if ((int)this.mFplx == 12)
            {
                this.lblGfmc.Text = "购方名称";
                this.lblGfsh.Text = "购方税号";
                this.dgDJ.Columns["colGfmc"].HeaderText = "购方名称";
                this.dgDJ.Columns["colGfsh"].HeaderText = "购方税号";
                this.dgDJ.Columns["colHjje"].HeaderText = "价税合计";
            }
            this.dgDJ.CellDoubleClick += new DataGridViewCellEventHandler(this.dgDJ_CellDoubleClick);
            base.Load += new EventHandler(this.ImportDJXZ_Load);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string str = this.txtgfmc.Text.Trim();
            string str2 = this.txtgfsh.Text.Trim();
            string djh = this.txtdjh.Text.Trim();
            List<Aisino.Fwkp.Fptk.Form.Djfp> collection = this.SelectByDjh(djh);
            if (str2 != "")
            {
                for (int i = collection.Count - 1; i >= 0; i--)
                {
                    Aisino.Fwkp.Fptk.Form.Djfp djfp = collection[i];
                    if ((djfp != null) && (djfp.Fpxx != null))
                    {
                        string gfsh = "";
                        if (((this.mFplx == 0) || ((int)this.mFplx == 2)) || ((int)this.mFplx == 0x33))
                        {
                            gfsh = djfp.Fpxx.gfsh;
                        }
                        else if ((int)this.mFplx == 11)
                        {
                            gfsh = djfp.Fpxx.spfnsrsbh;
                        }
                        else if ((int)this.mFplx == 12)
                        {
                            gfsh = djfp.Fpxx.gfsh;
                        }
                        if ((gfsh != "") && !gfsh.Contains(str2))
                        {
                            collection.RemoveAt(i);
                        }
                    }
                }
            }
            if (str != "")
            {
                for (int j = collection.Count - 1; j >= 0; j--)
                {
                    Aisino.Fwkp.Fptk.Form.Djfp djfp2 = collection[j];
                    if ((djfp2 != null) && (djfp2.Fpxx != null))
                    {
                        string gfmc = "";
                        if (((this.mFplx == 0) || ((int)this.mFplx == 2)) || ((int)this.mFplx == 0x33))
                        {
                            gfmc = djfp2.Fpxx.gfmc;
                        }
                        else if ((int)this.mFplx == 11)
                        {
                            gfmc = djfp2.Fpxx.spfmc;
                        }
                        else if ((int)this.mFplx == 12)
                        {
                            gfmc = djfp2.Fpxx.gfmc;
                        }
                        if ((gfmc != "") && !gfmc.Contains(str))
                        {
                            collection.RemoveAt(j);
                        }
                    }
                }
            }
            this.dgDJ.Rows.Clear();
            this.DjfpList.Clear();
            this.DjfpList.AddRange(collection);
            this.FillGridView(collection);
        }

        private void btnQueryAll_Click(object sender, EventArgs e)
        {
            this.txtgfmc.Text = "";
            this.txtdjh.Text = "";
            this.txtgfsh.Text = "";
            this.dgDJ.Rows.Clear();
            this.DjfpList.Clear();
            this.DjfpList.AddRange(this.allmDjList);
            this.FillGridView(this.allmDjList);
        }

        private string CanEmptyRedFp(string dm, string hm, string djje)
        {
            string str = "";
            if ((((int)this.mFplx != 2) && ((int)this.mFplx != 0x33)) || this.taxCard.QYLX.ISTDQY)
            {
                return str;
            }
            string xml = "";
            int num = int.Parse(hm);
            FpManager manager = new FpManager();
            string xfsh = manager.GetXfsh();
            string str4 = new StringBuilder().Append("<?xml version=\"1.0\" encoding=\"GBK\"?>").Append("<FPXT><INPUT>").Append("<NSRSBH>").Append(xfsh).Append("</NSRSBH>").Append("<KPJH>").Append(this.taxCard.Machine).Append("</KPJH>").Append("<SBBH>").Append(manager.GetMachineNum()).Append("</SBBH>").Append("<LZFPDM>").Append(dm).Append("</LZFPDM>").Append("<LZFPHM>").Append(string.Format("{0:00000000}", num)).Append("</LZFPHM>").Append("<FPZL>").Append(((int)this.mFplx == 2) ? "c" : "p").Append("</FPZL>").Append("</INPUT></FPXT>").ToString();
            if (HttpsSender.SendMsg("0007", str4, out xml) == 0)
            {
                XmlDocument document1 = new XmlDocument();
                document1.LoadXml(xml);
                XmlNode node1 = document1.SelectSingleNode("/FPXT/OUTPUT");
                string innerText = node1.SelectSingleNode("HZJE").InnerText;
                if (node1.SelectSingleNode("CODE").InnerText.Trim().Equals("0000"))
                {
                    if (!innerText.Trim().Equals("-0"))
                    {
                        decimal num2;
                        decimal num3;
                        decimal.TryParse(innerText, out num2);
                        decimal.TryParse(djje, out num3);
                        if (num2 <= decimal.Zero)
                        {
                            return "数据库中未找到对应的蓝票，且受理平台无可开红字发票金额，不能开红字发票";
                        }
                        if (num2.CompareTo(Math.Abs(num3)) < 0)
                        {
                            str = string.Format("销项红字发票填开金额超过对应蓝字发票金额！\n本张发票填开限额：￥{0}\n所开发票已填金额：￥{1}", decimal.Negate(num2).ToString("F2"), num3.ToString("F2"));
                        }
                    }
                    return str;
                }
                return "数据库中未找到对应的蓝票，且与受理平台网络不通，不能开红字发票";
            }
            return "数据库中未找到对应的蓝票，且与受理平台网络不通，不能开红字发票";
        }

        private string CanRedFp(string djje)
        {
            string str = "";
            if (decimal.Parse(blueFpxx.je).CompareTo(decimal.Parse("0.00")) < 0)
            {
                return "发票为红字发票";
            }
            if (blueFpxx.zfbz)
            {
                return "发票已作废";
            }
            if (decimal.Parse(blueFpxx.je).CompareTo(Math.Abs(decimal.Parse(djje))) < 0)
            {
                return string.Format("销项红字发票填开金额超过对应蓝字发票金额！\n本张发票填开限额：￥{0}\n所开发票已填金额：￥{1}", decimal.Negate(decimal.Parse(blueFpxx.je)).ToString("F2"), decimal.Parse(djje).ToString("F2"));
            }
            if (((int)blueFpxx.fplx != 2) && ((int)blueFpxx.fplx != 0x33))
            {
                return str;
            }
            if ((((this.mZyfplx != 0) || (blueFpxx.Zyfplx != (ZYFP_LX)9)) && ((this.mZyfplx != (ZYFP_LX)8) || (blueFpxx.Zyfplx == (ZYFP_LX)8))) && ((this.mZyfplx != (ZYFP_LX)9) || (blueFpxx.Zyfplx == (ZYFP_LX)9)))
            {
                return str;
            }
            return "发票类型不一致";
        }

        public string CheckDZRed(Aisino.Fwkp.Fptk.Form.Djfp dj)
        {
            Fpxx fpxx = dj.Fpxx;
            string blueFpdm = fpxx.blueFpdm;
            string blueFphm = fpxx.blueFphm;
            string str3 = "";
            int result = 0;
            if (int.TryParse(blueFphm, out result))
            {
                blueFpxx = new FpManager().GetXxfp(this.mFplx, blueFpdm, result);
            }
            else
            {
                str3 = string.Format("本张发票不能开红字发票！原因：YFP_HM有误", new object[0]);
            }
            if (blueFpxx != null)
            {
                string str4 = this.CanRedFp(fpxx.je);
                if (str4 != "")
                {
                    str3 = string.Format("本张发票不能开红字发票！原因：{0}", str4);
                }
                return str3;
            }
            string str5 = this.CanEmptyRedFp(blueFpdm, blueFphm, fpxx.je);
            if (str5 != "")
            {
                str3 = string.Format("本张发票不能开红字发票！原因：{0}", str5);
            }
            return str3;
        }

        private void dgDJ_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.SelectDjRow();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FillGridView(List<Aisino.Fwkp.Fptk.Form.Djfp> djList)
        {
            int num = 1;
            foreach (Aisino.Fwkp.Fptk.Form.Djfp djfp in djList)
            {
                Fpxx fp = djfp.Fpxx;
                if (fp != null)
                {
                    this.GetHjje(fp);
                    string[] values = new string[5];
                    if (((this.mFplx == 0) || ((int)this.mFplx == 2)) || ((int)this.mFplx == 0x33))
                    {
                        values = new string[] { num.ToString(), djfp.Djh, fp.gfsh, fp.gfmc, fp.je };
                    }
                    else if ((int)this.mFplx == 11)
                    {
                        values = new string[] { num.ToString(), djfp.Djh, fp.spfnsrsbh, fp.spfmc, fp.je };
                    }
                    else if ((int)this.mFplx == 12)
                    {
                        values = new string[] { num.ToString(), djfp.Djh, fp.gfsh, fp.gfmc, decimal.Add(decimal.Parse(fp.je), decimal.Parse(fp.se)).ToString() };
                    }
                    int num2 = this.dgDJ.Rows.Add(values);
                    if ((num2 >= 0) && this.ykdjList.Contains(djfp.Djh))
                    {
                        this.dgDJ.Rows[num2].ReadOnly = true;
                        this.dgDJ.Rows[num2].DefaultCellStyle.BackColor = Color.LightGray;
                    }
                    num++;
                }
            }
        }

        private void GetHjje(Fpxx fp)
        {
            if (string.IsNullOrEmpty(fp.je))
            {
                decimal num = new decimal();
                decimal result = new decimal();
                using (List<Dictionary<SPXX, string>>.Enumerator enumerator = fp.Mxxx.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        if (decimal.TryParse(enumerator.Current[(SPXX)7], out result))
                        {
                            num = decimal.Add(num, result);
                        }
                    }
                }
                fp.je = num.ToString();
            }
        }

        private void ImportDJXZ_Load(object sender, EventArgs e)
        {
            this.QueryYkdjList();
            this.FillGridView(this.allmDjList);
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.lblGfmc = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblGfmc");
            this.txtgfmc = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtgfmc");
            this.lblGfsh = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblGfsh");
            this.txtgfsh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtgfsh");
            this.txtdjh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtdjh");
            this.btnQuery = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnQuery");
            this.btnQueryAll = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnQueryAll");
            this.dgDJ = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("dgDj");
            this.tool_exit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_exit");
            this.tool_select = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_select");
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            ControlStyleUtil.SetToolStripStyle(this.toolStrip1);
            this.dgDJ.MultiSelect = false;
            this.dgDJ.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgDJ.RowHeadersWidth = 30;
            this.dgDJ.ReadOnly = true;
            this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
            this.btnQueryAll.Click += new EventHandler(this.btnQueryAll_Click);
            this.tool_exit.Click += new EventHandler(this.tool_exit_Click);
            this.tool_select.Click += new EventHandler(this.tool_select_Click);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(690, 0x1e1);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath=@"..\Config\Components\Aisino.Fwkp.Fpkj.Form.FPDR.ImportDJXZ\Aisino.Fwkp.Fpkj.Form.FPDR.ImportDJXZ.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(690, 0x1e1);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ImportDJXZ";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "导入单据信息查询选择";
            base.ResumeLayout(false);
        }

        private void QueryYkdjList()
        {
            if (this.allmDjList.Count > 0)
            {
                string file = this.allmDjList[0].File;
                List<string> collection = this.djHelper.QueryYkdj(file);
                this.ykdjList.AddRange(collection);
            }
        }

        private List<Aisino.Fwkp.Fptk.Form.Djfp> SelectByDjh(string djh)
        {
            List<Aisino.Fwkp.Fptk.Form.Djfp> list = new List<Aisino.Fwkp.Fptk.Form.Djfp>();
            if (djh != "")
            {
                foreach (Aisino.Fwkp.Fptk.Form.Djfp djfp in this.allmDjList)
                {
                    if (djfp.Djh == djh)
                    {
                        list.Add(djfp);
                    }
                }
                return list;
            }
            list.AddRange(this.allmDjList);
            return list;
        }

        private void SelectDjRow()
        {
            if (this.dgDJ.SelectedRows.Count > 0)
            {
                DataGridViewRow row = this.dgDJ.SelectedRows[0];
                string item = row.Cells["colDjh"].Value.ToString();
                if (!this.ykdjList.Contains(item))
                {
                    this.mDjfp = this.mDjList[row.Index];
                    if (((int)this.mFplx == 0x33) && this.mDjfp.Fpxx.isRed)
                    {
                        string str2 = this.CheckDZRed(this.mDjfp);
                        if (str2 != "")
                        {
                            string[] textArray1 = new string[] { str2 };
                            MessageManager.ShowMsgBox("INP-241007", textArray1);
                            return;
                        }
                    }
                    base.DialogResult = DialogResult.OK;
                }
                else
                {
                    string[] textArray2 = new string[] { "所选单据已成功填开过发票，不可重复开具！" };
                    MessageManager.ShowMsgBox("INP-241007", textArray2);
                }
            }
        }

        private void tool_exit_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        private void tool_select_Click(object sender, EventArgs e)
        {
            this.SelectDjRow();
        }

        public List<Aisino.Fwkp.Fptk.Form.Djfp> AllDjfpList
        {
            get
            {
                return this.allmDjList;
            }
        }

        public Aisino.Fwkp.Fptk.Form.Djfp Djfp
        {
            get
            {
                return this.mDjfp;
            }
        }

        public List<Aisino.Fwkp.Fptk.Form.Djfp> DjfpList
        {
            get
            {
                return this.mDjList;
            }
        }
    }
}

