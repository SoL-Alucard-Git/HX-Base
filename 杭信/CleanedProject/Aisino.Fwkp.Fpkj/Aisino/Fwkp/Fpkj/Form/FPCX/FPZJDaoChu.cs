namespace Aisino.Fwkp.Fpkj.Form.FPCX
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fpkj.Common;
    using Aisino.Fwkp.Fpkj.DAL;
    using Aisino.Fwkp.Fpkj.Form.FPXF;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using BusinessObject;
    public class FPZJDaoChu : BaseForm
    {
        private AisinoDataGrid _AisinoGrid;
        private DateTime _CardClock;
        public static AisinoDataGrid aisinoGrid;
        public bool blDateEnd_Bkjr;
        public bool blDateStart_Bkjr;
        public bool blNameQry;
        public bool blTaxCodeQry;
        private AisinoBTN but_DaochuShuju;
        private AisinoBTN but_FPChaxun;
        private AisinoCHK check_tongbutxt;
        private AisinoCMB com_fpzl;
        private AisinoCMB com_zfbz;
        private AisinoCMB com_zffp;
        private IContainer components;
        private ContextMenuStrip ConTextMenu_Daochu;
        private DateTimePicker date_jsrq;
        private DateTimePicker date_ksrq;
        public DateTime DateEnd;
        public DateTime DateStart;
        private Dictionary<string, object> dict = new Dictionary<string, object>();
        public AisinoGRP group_box;
        public string KindQry = string.Empty;
        private string KindStr = " ";
        private ILog loger = LogUtil.GetLogger<ChaXunTiaoJian>();
        private readonly DateTime MaxDateTime = new DateTime(0x270e, 12, DateTime.DaysInMonth(0x270e, 12));
        private ToolStripMenuItem MenuItem_DaoChuAll;
        private ToolStripMenuItem MenuItem_DaoChuSelect;
        public int Month;
        public string NameQry = string.Empty;
        private FPProgressBar progressBar;
        private int step = 0x7d0;
        public string TaxCodeQry = string.Empty;
        private AisinoTXT txt_bz;
        private AisinoTXT txt_fpdm;
        private AisinoTXT txt_fphm_end;
        private AisinoTXT txt_fphm_start;
        private AisinoTXT txt_gfmc;
        private AisinoTXT txt_gfsh;
        public string VersionName = string.Empty;
        private XmlComponentLoader xmlComponentLoader1;
        private XXFP xxfpChaXunBll = new XXFP(false);
        public int Year = 0x76b;

        public FPZJDaoChu(DateTime CardClock)
        {
            try
            {
                this.Initialize();
                this._CardClock = CardClock;
                this.Year = CardClock.Year;
                this.Month = CardClock.Month;
                this.VersionName = "11111111111111税务代开";
                this.SetSearchControlAttritute();
                this.NameQry = this.txt_gfmc.Text = string.Empty;
                this.TaxCodeQry = this.txt_gfsh.Text = string.Empty;
                this.InsertColumn();
            }
            catch (Exception exception)
            {
                this.loger.Error("[ChaXunTiaoJian构造函数异常]：" + exception.Message);
            }
        }

        private void aisinoGrid_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            try
            {
                PropertyUtil.SetValue("Aisino.Fwkp.Fpkj.Form.FPCX_FaPiaoChaXun_aisinoGrid_PageSize", e.PageSize.ToString());
                AisinoDataSet set = this.xxfpChaXunBll.SelectPage(e.PageNO, e.PageSize, this.dict);
                aisinoGrid.DataSource = set;
                aisinoGrid.Refresh();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
            }
        }

        private void but_DaochuShuju_Click(object sender, EventArgs e)
        {
            try
            {
                Rectangle bounds = this.but_DaochuShuju.Bounds;
                Point p = new Point(bounds.X, bounds.Y + bounds.Height);
                if (this.ConTextMenu_Daochu != null)
                {
                    this.ConTextMenu_Daochu.Show(base.PointToScreen(p));
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void but_FPChaxun_Click(object sender, EventArgs e)
        {
            try
            {
                this.GetFormData();
                int num = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(PropertyUtil.GetValue("Aisino.Fwkp.Fpkj.Form.FPCX_FaPiaoChaXun_aisinoGrid_PageSize"));
                if (num <= 0)
                {
                    num = 20;
                }
                this.GetFormData();
                AisinoDataSet set = this.xxfpChaXunBll.SelectPage(1, num, this.dict);
                aisinoGrid.DataSource = set;
                aisinoGrid.Refresh();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void com_fpzl_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void data_jsrq_TextChanged(object sender, EventArgs e)
        {
            this.date_jsrq.MinDate = this.date_ksrq.Value;
            this.date_ksrq.MaxDate = this.date_jsrq.Value;
        }

        private void data_ksrq_TextChanged(object sender, EventArgs e)
        {
            this.date_ksrq.MaxDate = this.date_jsrq.Value;
            this.date_jsrq.MinDate = this.date_ksrq.Value;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FPZJDaoChu_FormClosing(object sender, EventArgs e)
        {
            this._AisinoGrid = null;
            this.xxfpChaXunBll = null;
            this.progressBar = null;
            this.dict = null;
            base.Dispose();
        }

        private void GetFormData()
        {
            try
            {
                this.dict.Clear();
                this.dict.Add("StartDate", this.date_ksrq.Value.ToString("yyyy-MM-dd") + " 00:00:00");
                this.dict.Add("EndDate", this.date_jsrq.Value.ToString("yyyy-MM-dd") + " 23:59:59");
                this.dict.Add("GFMC", "%" + this.txt_gfmc.Text.Trim() + "%");
                this.dict.Add("GFSH", "%" + this.txt_gfsh.Text.Trim() + "%");
                this.dict.Add("FPDM", "%" + this.txt_fpdm.Text.Trim() + "%");
                string str = this.txt_fphm_start.Text.Trim();
                string str2 = this.txt_fphm_end.Text.Trim();
                if (Aisino.Fwkp.Fpkj.Common.Tool.CharNumInString(str, '0') != str.Length)
                {
                    str = str.TrimStart(new char[] { '0' });
                }
                if (Aisino.Fwkp.Fpkj.Common.Tool.CharNumInString(str2, '0') != str2.Length)
                {
                    str2 = str2.TrimStart(new char[] { '0' });
                }
                this.dict.Add("StartFphm", str);
                this.dict.Add("EndFphm", str2);
                this.dict.Add("FPZL", Aisino.Fwkp.Fpkj.Common.Tool.GetFPDBType(this.com_fpzl.Text));
                this.dict.Add("ZFBZ", this.MapComboxString(this.com_zfbz.Text));
                this.dict.Add("ZFFP", this.MapComboxString(this.com_zffp.Text));
                string str3 = this.txt_bz.Text.Trim();
                this.dict.Add("BZ", str3);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        public static string GetSubString(string s, int len)
        {
            if ((s == null) || (s.Length == 0))
            {
                return string.Empty;
            }
            if (ToolUtil.GetByteCount(s) > len)
            {
                for (int i = s.Length; i >= 0; i--)
                {
                    s = s.Substring(0, i);
                    if (ToolUtil.GetByteCount(s) <= len)
                    {
                        return s;
                    }
                }
            }
            return s;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.date_ksrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("date_ksrq");
            this.date_jsrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("date_jsrq");
            this.txt_gfmc = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_gfmc");
            this.txt_gfsh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_gfsh");
            this.txt_fpdm = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_fpdm");
            this.txt_fphm_start = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_fphm_start");
            this.txt_fphm_end = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_fphm_end");
            this.txt_bz = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_bz");
            this.com_fpzl = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("com_fpzl");
            this.com_zfbz = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("com_zfbz");
            this.com_zffp = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("com_zffp");
            this.but_FPChaxun = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_FPChaxun");
            this.but_DaochuShuju = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_DaochuShuju");
            this.group_box = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox1");
            this.but_FPChaxun.Click += new EventHandler(this.but_FPChaxun_Click);
            this.but_DaochuShuju.Click += new EventHandler(this.but_DaochuShuju_Click);
            this.check_tongbutxt = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("check_tongbutxt");
            this.ConTextMenu_Daochu = new ContextMenuStrip();
            this.MenuItem_DaoChuAll = new ToolStripMenuItem("导出全部");
            this.MenuItem_DaoChuSelect = new ToolStripMenuItem("导出选择");
            this.ConTextMenu_Daochu.Items.Add(this.MenuItem_DaoChuAll);
            this.ConTextMenu_Daochu.Items.Add(this.MenuItem_DaoChuSelect);
            this.MenuItem_DaoChuAll.Click += new EventHandler(this.MenuItem_DaoChuAll_Click);
            this.MenuItem_DaoChuSelect.Click += new EventHandler(this.MenuItem_DaoChuSelect_Click);
            this.date_ksrq.TextChanged += new EventHandler(this.data_ksrq_TextChanged);
            this.date_jsrq.TextChanged += new EventHandler(this.data_jsrq_TextChanged);
            this.txt_gfmc.MaxLength = 100;
            this.txt_gfsh.MaxLength = 20;
            this.txt_fphm_start.MaxLength = 8;
            this.txt_fphm_end.MaxLength = 8;
            this.txt_fpdm.MaxLength = 20;
            this.txt_bz.MaxLength = 230;
            this.txt_gfmc.KeyPress += new KeyPressEventHandler(this.textBox_GFMC_KeyPress);
            this.txt_gfmc.TextChanged += new EventHandler(this.txt_gfmc_TextChanged);
            this.txt_gfsh.KeyPress += new KeyPressEventHandler(this.textBox_GFSH_KeyPress);
            this.txt_fpdm.KeyPress += new KeyPressEventHandler(this.txt_fpdm_KeyPress);
            this.txt_fphm_start.KeyPress += new KeyPressEventHandler(this.txt_fpdm_KeyPress);
            this.txt_fphm_end.KeyPress += new KeyPressEventHandler(this.txt_fpdm_KeyPress);
            aisinoGrid = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoGrid");
            aisinoGrid.ReadOnly = true;
            aisinoGrid.DataGrid.AllowUserToDeleteRows = false;
            aisinoGrid.GoToPageEvent += aisinoGrid_GoToPageEvent;
            aisinoGrid.AutoSize = false;
            aisinoGrid.Height = 0x17f;
            aisinoGrid.Dock = DockStyle.Bottom;
            this.Text = "增值税发票加密数据导出(机动车专用)";
            this.but_FPChaxun.TabIndex = 0;
            this.but_FPChaxun.Focus();
            base.FormClosing += new FormClosingEventHandler(this.FPZJDaoChu_FormClosing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FPZJDaoChu));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x359, 0x23c);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "查询条件";
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Fpkj.Form.FPCX.FPZJDaoChu\Aisino.Fwkp.Fpkj.Form.FPCX.FPZJDaoChu.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x359, 0x23c);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FPZJDaoChu";
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "发票组件导出";
            base.ResumeLayout(false);
        }

        private void InsertColumn()
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> item = new Dictionary<string, string>();
            string str = "122";
            string str2 = "127";
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "发票种类");
            item.Add("Property", "FPZL");
            item.Add("Width", str);
            item.Add("Type", "Text");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "发票代码");
            item.Add("Property", "FPDM");
            item.Add("Width", str);
            item.Add("Type", "Text");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "发票号码");
            item.Add("Property", "FPHM");
            item.Add("Width", str);
            item.Add("Type", "Text");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "开票日期");
            item.Add("Property", "KPRQ");
            item.Add("Width", str);
            item.Add("Type", "Text");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "开票机号");
            item.Add("Property", "KPJH");
            item.Add("Width", str);
            item.Add("Type", "Text");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "购方名称");
            item.Add("Property", "GFMC");
            item.Add("Width", str);
            item.Add("Type", "Text");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "购方税号");
            item.Add("Property", "GFSH");
            item.Add("Width", str);
            item.Add("Type", "Text");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "主要商品名称");
            item.Add("Property", "ZYSPMC");
            item.Add("Width", str);
            item.Add("Type", "Text");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            item = new Dictionary<string, string>();
            item.Add("AisinoLBL", "备注");
            item.Add("Property", "BZ");
            item.Add("Width", str2);
            item.Add("Type", "Text");
            item.Add("Align", "MiddleCenter");
            item.Add("HeadAlign", "MiddleCenter");
            item.Add("Visible", "False");
            list.Add(item);
            aisinoGrid.ColumeHead = list;
            DataGridViewColumn column = aisinoGrid.Columns["KPRQ"];
            if (column != null)
            {
                DataGridViewCellStyle defaultCellStyle = column.DefaultCellStyle;
                defaultCellStyle.ForeColor = Color.Maroon;
                defaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            }
            aisinoGrid.DataSource = new AisinoDataSet();
        }

        private string MapComboxString(string item)
        {
            switch (item)
            {
                case "全部":
                    return "2";

                case "是":
                case "正票":
                    return "1";

                case "否":
                case "负票":
                    return "0";
            }
            return "2";
        }

        private void MenuItem_DaoChuAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (((aisinoGrid == null) || (aisinoGrid.Rows == null)) || (aisinoGrid.Rows.Count == 0))
                {
                    MessageManager.ShowMsgBox("FPCX-000039");
                }
                else
                {
                    this.GetFormData();
                    FolderBrowserDialog dialog = new FolderBrowserDialog();
                    string str = string.Empty;
                    if (!string.IsNullOrEmpty(str))
                    {
                        if (Directory.Exists(str))
                        {
                            dialog.SelectedPath = str;
                        }
                    }
                    else
                    {
                        dialog.SelectedPath = Application.StartupPath;
                    }
                    if (dialog.ShowDialog() != DialogResult.OK)
                    {
                        dialog.Dispose();
                    }
                    else
                    {
                        dialog.Dispose();
                        this.Refresh();
                        if (this.progressBar == null)
                        {
                            this.progressBar = new FPProgressBar();
                        }
                        this.progressBar.SetTip("正在生成发票记录", "请等待任务完成", "加密发票导出过程");
                        this.progressBar.fpxf_progressBar.Value = 1;
                        this.progressBar.Visible = true;
                        this.progressBar.Show();
                        this.progressBar.Refresh();
                        this.ProcessStartThread(2 * this.step);
                        this.progressBar.Refresh();
                        List<Fpxx> fpList = this.xxfpChaXunBll.SelectFpxx_FPZJDaoChu(this.dict);
                        if ((fpList == null) || (fpList.Count == 0))
                        {
                            this.ProcessStartThread(4 * this.step);
                            this.progressBar.Refresh();
                            this.progressBar.Visible = false;
                            MessageManager.ShowMsgBox("FPCX-000039");
                        }
                        else
                        {
                            this.progressBar.SetTip("正在生成导出文件", "请等待任务完成", "加密发票导出过程");
                            this.ProcessStartThread(this.step);
                            this.progressBar.Refresh();
                            string str2 = "增值税发票明细(";
                            if (this.com_fpzl.Text == "普通发票")
                            {
                                str2 = "增值税普通发票明细(";
                            }
                            else if (this.com_fpzl.Text == "专用发票")
                            {
                                str2 = "增值税专用发票明细(";
                            }
                            string str3 = dialog.SelectedPath + @"\" + str2 + this.date_ksrq.Value.ToString("yyyy.MM.dd") + "-" + this.date_jsrq.Value.ToString("yyyy.MM.dd") + ")";
                            XMLOperation operation = new XMLOperation();
                            operation.EncryptXML(fpList, str3 + ".Dat");
                            if (this.check_tongbutxt.Checked)
                            {
                                this.progressBar.SetTip("正在导出同步TXT文件", "请等待任务完成", "加密发票导出过程");
                                this.ProcessStartThread(this.step);
                                this.progressBar.Refresh();
                                operation.SaveAllFPxxToTxt(fpList, str3 + ".txt", this.date_ksrq.Value, this.date_jsrq.Value);
                            }
                            else
                            {
                                this.ProcessStartThread(this.step);
                                this.progressBar.Refresh();
                            }
                            this.ProcessStartThread(this.step);
                            this.progressBar.Refresh();
                            this.progressBar.Visible = false;
                            this.progressBar.Refresh();
                            MessageManager.ShowMsgBox("FPCX-000038");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[MenuItem_DaoChuAll_Click异常]" + exception.Message);
            }
            finally
            {
                if (this.progressBar != null)
                {
                    this.progressBar.Visible = false;
                    this.progressBar.Close();
                    this.progressBar.Dispose();
                    this.progressBar = null;
                    GC.Collect();
                }
            }
        }

        private void MenuItem_DaoChuSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (((aisinoGrid == null) || (aisinoGrid.Rows == null)) || (aisinoGrid.Rows.Count == 0))
                {
                    MessageManager.ShowMsgBox("FPCX-000039");
                }
                else if ((aisinoGrid.SelectedRows == null) || (aisinoGrid.SelectedRows.Count == 0))
                {
                    MessageManager.ShowMsgBox("FPCX-000040");
                }
                else
                {
                    FolderBrowserDialog dialog = new FolderBrowserDialog();
                    string str = string.Empty;
                    if (!string.IsNullOrEmpty(str))
                    {
                        if (Directory.Exists(str))
                        {
                            dialog.SelectedPath = str;
                        }
                    }
                    else
                    {
                        dialog.SelectedPath = Application.StartupPath;
                    }
                    if (dialog.ShowDialog() != DialogResult.OK)
                    {
                        dialog.Dispose();
                    }
                    else
                    {
                        dialog.Dispose();
                        this.Refresh();
                        if (this.progressBar == null)
                        {
                            this.progressBar = new FPProgressBar();
                        }
                        this.progressBar.SetTip("正在生成发票记录", "请等待任务完成", "加密发票导出过程");
                        this.progressBar.fpxf_progressBar.Value = 1;
                        this.progressBar.Visible = true;
                        this.progressBar.Show();
                        this.progressBar.Refresh();
                        this.ProcessStartThread(2 * this.step);
                        this.progressBar.Refresh();
                        this.progressBar.SetTip("正在生成导出文件", "请等待任务完成", "加密发票导出过程");
                        this.ProcessStartThread(this.step);
                        this.progressBar.Refresh();
                        string str2 = "增值税发票明细(";
                        if (this.com_fpzl.Text == "普通发票")
                        {
                            str2 = "增值税普通发票明细(";
                        }
                        else if (this.com_fpzl.Text == "专用发票")
                        {
                            str2 = "增值税专用发票明细(";
                        }
                        string path = dialog.SelectedPath + @"\" + str2 + this.date_ksrq.Value.ToString("yyyy.MM.dd") + "-" + this.date_jsrq.Value.ToString("yyyy.MM.dd") + ")";
                        XMLOperation operation = new XMLOperation();
                        if (this.check_tongbutxt.Checked)
                        {
                            this.progressBar.SetTip("正在导出同步TXT文件", "请等待任务完成", "加密发票导出过程");
                            this.ProcessStartThread(this.step);
                            this.progressBar.Refresh();
                        }
                        else
                        {
                            this.ProcessStartThread(this.step);
                            this.progressBar.Refresh();
                        }
                        operation.SaveSelectToTxt(aisinoGrid.SelectedRows, path, this.date_ksrq.Value, this.date_jsrq.Value, this.check_tongbutxt.Checked);
                        this.ProcessStartThread(this.step);
                        this.progressBar.Refresh();
                        this.progressBar.Visible = false;
                        this.progressBar.Refresh();
                        MessageManager.ShowMsgBox("FPCX-000038");
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[MenuItem_DaoChuAll_Click异常]" + exception.Message);
            }
            finally
            {
                if (this.progressBar != null)
                {
                    this.progressBar.Visible = false;
                    this.progressBar.Close();
                    this.progressBar.Dispose();
                    this.progressBar = null;
                    GC.Collect();
                }
            }
        }

        private void PerformStep(int step)
        {
            for (int i = 0; i < step; i++)
            {
                if ((this.progressBar.fpxf_progressBar.Value + 1) > this.progressBar.fpxf_progressBar.Maximum)
                {
                    this.progressBar.fpxf_progressBar.Value = this.progressBar.fpxf_progressBar.Maximum;
                }
                else
                {
                    this.progressBar.fpxf_progressBar.Value++;
                }
                this.progressBar.fpxf_progressBar.Refresh();
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
                this.loger.Error("[ThreadFun]" + exception.Message);
            }
        }

        public void ProcessStartThread(int value)
        {
            this.PerformStep(value);
        }

        private string ReadKzyf()
        {
            try
            {
                string str = this.xxfpChaXunBll.SelectMinKprq(null);
                if (str.CompareTo(base.TaxCardInstance.CardEffectDate) > 0)
                {
                    str = base.TaxCardInstance.CardEffectDate;
                }
                return str;
            }
            catch (Exception exception)
            {
                this.loger.Error("[函数ReadKzyf异常]读取金税设备发行时间异常，" + exception.Message);
                MessageManager.ShowMsgBox("读取金税设备发行时间异常" + exception.Message);
                return this._CardClock.ToString("yyyyMM");
            }
        }

        private void SetFPZLAuthorization()
        {
            try
            {
                string str = this.xxfpChaXunBll.SelectFPZLListFromDB(null);
                if (str == null)
                {
                    str = "";
                }
                this.com_fpzl.Items.Add("全部发票");
                if ((((int)base.TaxCardInstance.TaxMode == 2) && base.TaxCardInstance.QYLX.ISZYFP) || (str.IndexOf('s') != -1))
                {
                    this.com_fpzl.Items.Add("专用发票");
                    this.KindStr = this.KindStr + "s";
                }
                if ((((int)base.TaxCardInstance.TaxMode == 2) && base.TaxCardInstance.QYLX.ISPTFP) || (str.IndexOf('c') != -1))
                {
                    this.com_fpzl.Items.Add("普通发票");
                    this.KindStr = this.KindStr + "c";
                }
                if ((((int)base.TaxCardInstance.TaxMode == 2) && base.TaxCardInstance.QYLX.ISHY) || (str.IndexOf('f') != -1))
                {
                    this.com_fpzl.Items.Add("货物运输业增值税专用发票");
                    this.KindStr = this.KindStr + "f";
                }
                if ((((int)base.TaxCardInstance.TaxMode == 2) && base.TaxCardInstance.QYLX.ISJDC) || (str.IndexOf('j') != -1))
                {
                    this.com_fpzl.Items.Add("机动车销售统一发票");
                    this.KindStr = this.KindStr + "j";
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("SetFPZLAuthorization 异常" + exception.Message);
            }
        }

        public void SetSearchControlAttritute()
        {
            try
            {
                string str = this.ReadKzyf();
                int year = this._CardClock.Year;
                int month = this._CardClock.Month;
                if (!string.IsNullOrEmpty(str))
                {
                    year = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(str.Substring(0, 4));
                    month = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(str.Substring(4, 2));
                }
                this.date_ksrq.MinDate = new DateTime(year, month, 1);
                this.date_ksrq.MaxDate = new DateTime(this.Year, this.Month, this._CardClock.Day);
                this.date_ksrq.Value = new DateTime(this.Year, this.Month, 1);
                this.DateStart = this.date_ksrq.Value;
                this.date_jsrq.MinDate = new DateTime(year, month, 1);
                this.date_jsrq.MaxDate = new DateTime(this.Year, this.Month, this._CardClock.Day);
                this.date_jsrq.Value = new DateTime(this.Year, this.Month, this._CardClock.Day);
                this.DateEnd = this.date_jsrq.Value;
                if (this.com_fpzl != null)
                {
                    this.com_fpzl.Items.Clear();
                    this.com_fpzl.Items.Add("全部");
                    this.com_fpzl.Items.Add("专用发票");
                    this.com_fpzl.Items.Add("普通发票");
                    this.com_fpzl.SelectedIndex = 0;
                    this.com_fpzl.DropDownStyle = ComboBoxStyle.DropDownList;
                }
                if (this.com_zfbz != null)
                {
                    this.com_zfbz.Items.Clear();
                    this.com_zfbz.Items.Add("全部");
                    this.com_zfbz.Items.Add("是");
                    this.com_zfbz.Items.Add("否");
                    this.com_zfbz.SelectedIndex = 0;
                    this.com_zfbz.DropDownStyle = ComboBoxStyle.DropDownList;
                }
                if (this.com_zffp != null)
                {
                    this.com_zffp.Items.Clear();
                    this.com_zffp.Items.Add("全部");
                    this.com_zffp.Items.Add("正票");
                    this.com_zffp.Items.Add("负票");
                    this.com_zffp.SelectedIndex = 0;
                    this.com_zffp.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void textBox_GFMC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.txt_gfmc.Text.Length > 100)
            {
                e.Handled = true;
            }
        }

        private void textBox_GFSH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsLetterOrDigit(e.KeyChar) && (e.KeyChar != '\r')) && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
        }

        private void txt_fpdm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsDigit(e.KeyChar) && (e.KeyChar != '\r')) && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
        }

        private void txt_gfmc_TextChanged(object sender, EventArgs e)
        {
            AisinoTXT otxt = (AisinoTXT) sender;
            int selectionStart = otxt.SelectionStart;
            otxt.Text = GetSubString(otxt.Text, 100).Trim();
            otxt.SelectionStart = selectionStart;
        }

        private void txt_gfsh_TextChanged(object sender, EventArgs e)
        {
            AisinoTXT otxt = (AisinoTXT) sender;
            int selectionStart = otxt.SelectionStart;
            otxt.Text = GetSubString(otxt.Text, 100).Trim();
            otxt.SelectionStart = selectionStart;
        }

        public AisinoDataGrid AisinoGrid
        {
            set
            {
                this._AisinoGrid = value;
            }
        }

        public DateTime DateTimeEnd
        {
            get
            {
                return this.DateEnd;
            }
        }

        public DateTime DateTimeStart
        {
            get
            {
                return this.DateStart;
            }
        }
    }
}

