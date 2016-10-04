namespace Aisino.Framework.Plugin.Core.PrintGrid
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using ns15;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.IO;
    using System.Windows.Forms;

    public class PrintOptionsForm : Form
    {
        private bool bool_0;
        private bool bool_1;
        private bool bool_2;
        private bool bool_3;
        private AisinoBTN btnBodyFont;
        private AisinoBTN btnBodyFontPub;
        private AisinoBTN btnCancel;
        private AisinoBTN btnChangePage;
        private AisinoBTN btnCurReset;
        private AisinoBTN btnCurSave;
        private AisinoBTN btnPageSettingsPub;
        private AisinoBTN btnPrint;
        private AisinoBTN btnResetPub;
        private AisinoBTN btnResetPubPara;
        private AisinoBTN btnSavePub;
        private AisinoBTN btnTitleFont;
        private AisinoBTN btnTitleFontPub;
        private AisinoBTN btnView;
        private AisinoCHK chkCounter;
        private AisinoCHK chkCounterPub;
        private AisinoCHK chkPrintBlank;
        private AisinoCHK chkPrintBlankPub;
        private AisinoCHK chkPrintBorder;
        private AisinoCHK chkPrintBorderPub;
        private AisinoCHK chkPrintFooter;
        private AisinoCHK chkPrintFooterPub;
        private AisinoCHK chkPrintSeqnoPub;
        private AisinoCHK chkPrintSqeno;
        private Class136 class136_0;
        private Class136 class136_1;
        private Class137 class137_0;
        private Class137 class137_1;
        private DataGridView dataGridView_0;
        private DataRow dataRow_0;
        private DataRow dataRow_1;
        private DataTable dataTable_0;
        private float float_0;
        private float float_1;
        private float float_2;
        private float float_3;
        private float float_4;
        private float float_5;
        private Font font_0;
        private FontDialog fontDialog_0;
        private AisinoGRP groupBox1;
        private AisinoGRP groupBox2;
        private AisinoGRP groupBox3;
        private AisinoGRP groupBox4;
        private AisinoGRP groupBox5;
        private AisinoGRP groupBox6;
        private AisinoGRP groupBox7;
        private IContainer icontainer_0;
        private ILog ilog_0;
        private int int_0;
        private int int_1;
        private int int_2;
        private int int_3;
        private int int_4;
        private AisinoLBL label1;
        private AisinoLBL label10;
        private AisinoLBL label11;
        private AisinoLBL label12;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private AisinoLBL label5;
        private AisinoLBL label8;
        private AisinoLBL label9;
        private AisinoLBL lblPageck;
        private AisinoLBL lblPageSet;
        private AisinoLBL lblPageSize;
        private AisinoLBL lblPageSizePub;
        private AisinoLBL lblPrintDeriction;
        private AisinoLBL lblPrintDerictionPub;
        private AisinoLBL lblPrinter;
        private AisinoLBL lblPrinterPub;
        private List<DataGridViewColumn> list_0;
        private List<int> list_1;
        private List<PrinterItems> list_2;
        private List<PrinterItems> list_3;
        private AisinoNUD nuPrintRowsPerPage;
        private object object_0;
        private PageSetupDialog pageSetupDialog_0;
        private PageSetupDialog pageSetupDialog_1;
        private PrintDialog printDialog_0;
        private PrintDocument printDocument_0;
        private PrintDocument printDocument_1;
        private PrintPreviewDialog printPreviewDialog1;
        private string string_0;
        private string string_1;
        private string string_2;
        private string string_3;
        private StringFormat stringFormat_0;
        private AisinoTAB tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private AisinoTXT txtBodyFont;
        private AisinoTXT txtBodyFontPub;
        private AisinoTXT txtTitle;
        private AisinoTXT txtTitleFont;
        private AisinoTXT txtTitleFontPub;

        public PrintOptionsForm(DataGridView dataGridView_1, object object_1, string string_4, List<PrinterItems> hearder, List<PrinterItems> footer) : this(dataGridView_1, dataGridView_1.Name, object_1, string_4, hearder, footer)
        {
            
        }

        public PrintOptionsForm(bool bool_4, DataGridView dataGridView_1, object object_1, string string_4, List<PrinterItems> hearder, List<PrinterItems> footer) : this(dataGridView_1, dataGridView_1.Name, object_1, string_4, hearder, footer)
        {
            
            if (!bool_4)
            {
                this.chkCounter.Enabled = false;
            }
        }

        internal PrintOptionsForm(DataGridView dataGridView_1, string string_4, object object_1, string string_5, List<PrinterItems> hearder, List<PrinterItems> footer)
        {
            
            this.list_0 = new List<DataGridViewColumn>();
            this.list_1 = new List<int>();
            this.string_3 = string.Empty;
            this.ilog_0 = LogUtil.GetLogger<PrintOptionsForm>();
            this.InitializeComponent();
            this.object_0 = object_1;
            this.dataGridView_0 = dataGridView_1;
            this.class136_0 = new Class136();
            this.class137_0 = new Class137();
            this.class136_1 = new Class136();
            this.class137_1 = new Class137();
            this.class136_0.method_1(string_5);
            this.float_0 = this.dataGridView_0.RowTemplate.Height;
            this.font_0 = this.dataGridView_0.Font;
            this.float_1 = this.method_12("汉字", this.font_0).Height / this.float_0;
            this.float_2 = 20f;
            this.float_3 = 20f;
            this.int_1 = 20;
            this.int_0 = 40;
            this.list_2 = hearder;
            this.list_3 = footer;
            DataView defaultView = null;
            if (this.dataGridView_0.DataSource != null)
            {
                if (this.dataGridView_0.DataSource.GetType().Name == "DataTable")
                {
                    defaultView = ((DataTable) this.dataGridView_0.DataSource).DefaultView;
                }
                else
                {
                    defaultView = (DataView) this.dataGridView_0.DataSource;
                }
                if (defaultView != null)
                {
                    this.dataTable_0 = defaultView.Table;
                }
            }
            string fullName = object_1.GetType().FullName;
            this.string_0 = fullName + "." + string_4 + ".print";
            this.string_1 = "Aisino.GridPrint.Common.print";
            string path = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AisinoInvoice"), "PrintSetting");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            this.string_2 = path;
            this.method_17();
            this.method_18();
        }

        private void btnBodyFont_Click(object sender, EventArgs e)
        {
            this.fontDialog_0.Color = this.class136_0.method_14();
            this.fontDialog_0.Font = this.class136_0.method_12();
            if (this.fontDialog_0.ShowDialog() == DialogResult.OK)
            {
                string str = this.fontDialog_0.Font.Name + "    大小:" + Math.Round((double) this.fontDialog_0.Font.Size, 0).ToString();
                this.txtBodyFont.Font = this.fontDialog_0.Font;
                this.txtBodyFont.ForeColor = this.fontDialog_0.Color;
                this.txtBodyFont.Text = str;
                this.class136_0.method_17(str);
                this.class136_0.method_13(this.fontDialog_0.Font);
                this.class136_0.method_15(this.fontDialog_0.Color);
                this.method_14();
            }
        }

        private void btnBodyFontPub_Click(object sender, EventArgs e)
        {
            this.fontDialog_0.Color = this.class137_0.method_12();
            this.fontDialog_0.Font = this.class137_0.method_10();
            if (this.fontDialog_0.ShowDialog() == DialogResult.OK)
            {
                string str = this.fontDialog_0.Font.Name + "    大小:" + this.fontDialog_0.Font.Size.ToString();
                this.txtBodyFontPub.Font = this.fontDialog_0.Font;
                this.txtBodyFontPub.ForeColor = this.fontDialog_0.Color;
                this.txtBodyFontPub.Text = str;
                this.class137_0.method_15(str);
                this.class137_0.method_11(this.fontDialog_0.Font);
                this.class137_0.method_13(this.fontDialog_0.Color);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnChangePage_Click(object sender, EventArgs e)
        {
            if (this.pageSetupDialog_0.ShowDialog() == DialogResult.OK)
            {
                this.lblPageSize.Text = this.method_22(this.pageSetupDialog_0.PageSettings.PaperSize);
                this.lblPrintDeriction.Text = this.pageSetupDialog_0.PageSettings.Landscape ? "横向打印" : "纵向打印";
                this.class136_0.method_31(this.pageSetupDialog_0.PageSettings);
                this.lblPrinter.Text = this.pageSetupDialog_0.PrinterSettings.PrinterName;
            }
            this.printDialog_0.PrinterSettings = this.pageSetupDialog_0.PrinterSettings;
            if (this.printDialog_0.ShowDialog() == DialogResult.OK)
            {
                this.lblPrinter.Text = this.printDialog_0.PrinterSettings.PrinterName;
                this.class136_0.method_30().PrinterSettings = this.printDialog_0.PrinterSettings;
            }
            this.method_14();
            this.printDocument_0.DefaultPageSettings = this.pageSetupDialog_0.PageSettings;
            this.printDocument_0.PrinterSettings = this.printDialog_0.PrinterSettings;
        }

        private void btnCurReset_Click(object sender, EventArgs e)
        {
            string path = this.string_2 + this.string_0;
            if (File.Exists(path))
            {
                this.method_26(path);
            }
            else
            {
                this.lblPageSize.Text = this.method_22(this.class136_1.method_30().PaperSize);
                this.lblPrintDeriction.Text = this.class136_1.method_30().Landscape ? "横向打印" : "纵向打印";
                this.pageSetupDialog_0.PageSettings = this.method_19(this.class136_1.method_30());
                this.lblPrinter.Text = this.class136_1.method_30().PrinterSettings.PrinterName;
                this.txtTitle.Text = this.class136_1.method_0();
                this.txtTitleFont.Font = this.class136_1.method_6();
                this.txtTitleFont.Text = this.class136_1.method_10();
                this.txtBodyFont.Font = this.class136_1.method_12();
                this.txtBodyFont.Text = this.class136_1.method_16();
                this.chkPrintBorder.Checked = this.class136_1.method_18();
                this.chkCounter.Checked = this.class136_1.method_20();
                this.chkPrintBlank.Checked = this.class136_1.method_22();
                this.chkPrintSqeno.Checked = this.class136_1.method_24();
                this.chkPrintFooter.Checked = this.class136_1.method_26();
                this.nuPrintRowsPerPage.Value = this.class136_1.method_28();
                this.method_21(this.class136_1, this.class136_0);
            }
            this.printDocument_0.DefaultPageSettings = this.method_19(this.class136_0.method_30());
            this.method_14();
        }

        private void btnCurSave_Click(object sender, EventArgs e)
        {
            string str = Path.Combine(this.string_2, this.string_0);
            SerializeUtil.Serialize(true, str, this.class136_0);
        }

        private void btnPageSettingsPub_Click(object sender, EventArgs e)
        {
            if (this.pageSetupDialog_1.ShowDialog() == DialogResult.OK)
            {
                this.lblPageSizePub.Text = this.method_22(this.pageSetupDialog_1.PageSettings.PaperSize);
                this.lblPrintDerictionPub.Text = this.pageSetupDialog_1.PageSettings.Landscape ? "横向打印" : "纵向打印";
                this.class137_0.method_27(this.pageSetupDialog_1.PageSettings);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (this.method_27(this.class136_0.method_30().PrinterSettings.PrinterName))
            {
                this.printDocument_0.Print();
            }
            else
            {
                MessageBoxHelper.Show("打印机不存在，请设置打印机", "表格打印", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void btnResetPub_Click(object sender, EventArgs e)
        {
            string path = this.string_2 + this.string_1;
            if (File.Exists(path))
            {
                this.method_24(path);
            }
            else
            {
                this.lblPageSizePub.Text = this.method_22(this.class137_1.method_26().PaperSize);
                this.lblPrintDerictionPub.Text = this.class137_1.method_26().Landscape ? "横向打印" : "纵向打印";
                this.pageSetupDialog_1.PageSettings = this.method_19(this.class137_1.method_26());
                this.lblPrinterPub.Text = this.class137_1.method_26().PrinterSettings.PrinterName;
                this.txtTitleFontPub.Font = this.class137_1.method_4();
                this.txtTitleFontPub.Text = this.class137_1.method_8();
                this.txtBodyFontPub.Font = this.class137_1.method_10();
                this.txtBodyFontPub.Text = this.class137_1.method_14();
                this.chkPrintBorderPub.Checked = this.class137_1.method_16();
                this.chkCounterPub.Checked = this.class137_1.method_18();
                this.chkPrintBlankPub.Checked = this.class137_1.method_20();
                this.chkPrintSeqnoPub.Checked = this.class137_1.method_22();
                this.chkPrintFooterPub.Checked = this.class137_1.method_24();
                this.method_20(this.class137_1, this.class137_0);
            }
        }

        private void btnResetPubPara_Click(object sender, EventArgs e)
        {
            this.method_23();
            this.printDocument_0.DefaultPageSettings = this.method_19(this.class136_0.method_30());
            this.method_14();
        }

        private void btnSavePub_Click(object sender, EventArgs e)
        {
            string str = Path.Combine(this.string_2, this.string_1);
            SerializeUtil.Serialize(true, str, this.class137_0);
        }

        private void btnTitleFont_Click(object sender, EventArgs e)
        {
            this.fontDialog_0.Color = this.class136_0.method_8();
            this.fontDialog_0.Font = this.class136_0.method_6();
            if (this.fontDialog_0.ShowDialog() == DialogResult.OK)
            {
                string str = this.fontDialog_0.Font.Name + "    大小:" + this.fontDialog_0.Font.Size.ToString();
                this.txtTitleFont.Font = this.fontDialog_0.Font;
                this.txtTitleFont.ForeColor = this.fontDialog_0.Color;
                this.txtTitleFont.Text = str;
                this.class136_0.method_11(str);
                this.class136_0.method_7(this.fontDialog_0.Font);
                this.class136_0.method_9(this.fontDialog_0.Color);
                this.method_14();
            }
        }

        private void btnTitleFontPub_Click(object sender, EventArgs e)
        {
            this.fontDialog_0.Color = this.class137_0.method_6();
            this.fontDialog_0.Font = this.class137_0.method_4();
            if (this.fontDialog_0.ShowDialog() == DialogResult.OK)
            {
                string str = this.fontDialog_0.Font.Name + "    大小:" + this.fontDialog_0.Font.Size.ToString();
                this.txtTitleFontPub.Font = this.fontDialog_0.Font;
                this.txtTitleFontPub.ForeColor = this.fontDialog_0.Color;
                this.txtTitleFontPub.Text = str;
                this.class137_0.method_9(str);
                this.class137_0.method_5(this.fontDialog_0.Font);
                this.class137_0.method_7(this.fontDialog_0.Color);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (this.method_27(this.class136_0.method_30().PrinterSettings.PrinterName))
            {
                this.printPreviewDialog1.ShowDialog();
            }
            else
            {
                MessageBoxHelper.Show("打印机不存在，请设置打印机", "表格打印", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            this.bool_2 = false;
        }

        private void chkCounter_CheckedChanged(object sender, EventArgs e)
        {
            this.class136_0.method_21(this.chkCounter.Checked);
            this.method_14();
        }

        private void chkCounterPub_CheckedChanged(object sender, EventArgs e)
        {
            this.class137_0.method_19(this.chkCounterPub.Checked);
        }

        private void chkPrintBlank_CheckedChanged(object sender, EventArgs e)
        {
            this.class136_0.method_23(this.chkPrintBlank.Checked);
        }

        private void chkPrintBlankPub_CheckedChanged(object sender, EventArgs e)
        {
            this.class137_0.method_21(this.chkPrintBlankPub.Checked);
        }

        private void chkPrintBorder_CheckedChanged(object sender, EventArgs e)
        {
            this.class136_0.method_19(this.chkPrintBorder.Checked);
        }

        private void chkPrintBorderPub_CheckedChanged(object sender, EventArgs e)
        {
            this.class137_0.method_17(this.chkPrintBorderPub.Checked);
        }

        private void chkPrintFooter_CheckedChanged(object sender, EventArgs e)
        {
            this.class136_0.method_27(this.chkPrintFooter.Checked);
        }

        private void chkPrintFooterPub_CheckedChanged(object sender, EventArgs e)
        {
            this.class137_0.method_25(this.chkPrintFooterPub.Checked);
        }

        private void chkPrintSeqnoPub_CheckedChanged(object sender, EventArgs e)
        {
            this.class137_0.method_23(this.chkPrintSeqnoPub.Checked);
        }

        private void chkPrintSqeno_CheckedChanged(object sender, EventArgs e)
        {
            this.class136_0.method_25(this.chkPrintSqeno.Checked);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(PrintOptionsForm));
            this.pageSetupDialog_0 = new PageSetupDialog();
            this.printDocument_0 = new PrintDocument();
            this.printPreviewDialog1 = new PrintPreviewDialog();
            this.pageSetupDialog_1 = new PageSetupDialog();
            this.printDocument_1 = new PrintDocument();
            this.fontDialog_0 = new FontDialog();
            this.printDialog_0 = new PrintDialog();
            this.btnCancel = new AisinoBTN();
            this.btnPrint = new AisinoBTN();
            this.btnView = new AisinoBTN();
            this.tabControl1 = new AisinoTAB();
            this.tabPage1 = new TabPage();
            this.groupBox2 = new AisinoGRP();
            this.groupBox4 = new AisinoGRP();
            this.lblPrinter = new AisinoLBL();
            this.label8 = new AisinoLBL();
            this.lblPrintDeriction = new AisinoLBL();
            this.lblPageSize = new AisinoLBL();
            this.lblPageck = new AisinoLBL();
            this.lblPageSet = new AisinoLBL();
            this.btnChangePage = new AisinoBTN();
            this.btnBodyFont = new AisinoBTN();
            this.btnTitleFont = new AisinoBTN();
            this.txtBodyFont = new AisinoTXT();
            this.label3 = new AisinoLBL();
            this.txtTitleFont = new AisinoTXT();
            this.label2 = new AisinoLBL();
            this.txtTitle = new AisinoTXT();
            this.label1 = new AisinoLBL();
            this.groupBox3 = new AisinoGRP();
            this.groupBox5 = new AisinoGRP();
            this.nuPrintRowsPerPage = new AisinoNUD();
            this.chkPrintFooter = new AisinoCHK();
            this.chkPrintSqeno = new AisinoCHK();
            this.chkPrintBlank = new AisinoCHK();
            this.chkCounter = new AisinoCHK();
            this.chkPrintBorder = new AisinoCHK();
            this.btnResetPubPara = new AisinoBTN();
            this.btnCurSave = new AisinoBTN();
            this.btnCurReset = new AisinoBTN();
            this.tabPage2 = new TabPage();
            this.groupBox1 = new AisinoGRP();
            this.groupBox6 = new AisinoGRP();
            this.lblPrinterPub = new AisinoLBL();
            this.label5 = new AisinoLBL();
            this.lblPrintDerictionPub = new AisinoLBL();
            this.lblPageSizePub = new AisinoLBL();
            this.label9 = new AisinoLBL();
            this.label10 = new AisinoLBL();
            this.btnPageSettingsPub = new AisinoBTN();
            this.btnBodyFontPub = new AisinoBTN();
            this.btnTitleFontPub = new AisinoBTN();
            this.txtBodyFontPub = new AisinoTXT();
            this.label11 = new AisinoLBL();
            this.txtTitleFontPub = new AisinoTXT();
            this.label12 = new AisinoLBL();
            this.groupBox7 = new AisinoGRP();
            this.chkPrintFooterPub = new AisinoCHK();
            this.chkPrintSeqnoPub = new AisinoCHK();
            this.chkPrintBlankPub = new AisinoCHK();
            this.chkCounterPub = new AisinoCHK();
            this.chkPrintBorderPub = new AisinoCHK();
            this.btnSavePub = new AisinoBTN();
            this.btnResetPub = new AisinoBTN();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.nuPrintRowsPerPage.BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            base.SuspendLayout();
            this.pageSetupDialog_0.Document = this.printDocument_0;
            this.pageSetupDialog_0.EnableMetric = true;
            this.printPreviewDialog1.AutoScrollMargin = new Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new Size(0, 0);
            this.printPreviewDialog1.ClientSize = new Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument_0;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = (Icon) manager.GetObject("printPreviewDialog1.Icon");
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            this.pageSetupDialog_1.Document = this.printDocument_1;
            this.pageSetupDialog_1.EnableMetric = true;
            this.fontDialog_0.Font = new Font("黑体", 11f);
            this.fontDialog_0.ShowColor = true;
            this.printDialog_0.AllowPrintToFile = false;
            this.printDialog_0.Document = this.printDocument_0;
            this.printDialog_0.UseEXDialog = true;
            this.btnCancel.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnCancel.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnCancel.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnCancel.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btnCancel.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnCancel.Location = new Point(0x18f, 0x199);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(80, 30);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "放弃";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnPrint.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnPrint.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnPrint.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnPrint.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btnPrint.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnPrint.Location = new Point(300, 0x199);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new Size(80, 30);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
            this.btnView.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnView.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnView.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnView.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btnView.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnView.Location = new Point(0xcb, 0x199);
            this.btnView.Name = "btnView";
            this.btnView.Size = new Size(80, 30);
            this.btnView.TabIndex = 0;
            this.btnView.Text = "预览";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new EventHandler(this.btnView_Click);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = DockStyle.Top;
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x1f3, 0x193);
            this.tabControl1.TabIndex = 0;
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.btnResetPubPara);
            this.tabPage1.Controls.Add(this.btnCurSave);
            this.tabPage1.Controls.Add(this.btnCurReset);
            this.tabPage1.Location = new Point(4, 0x16);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(0x1eb, 0x179);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "当前表格打印参数";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.btnBodyFont);
            this.groupBox2.Controls.Add(this.btnTitleFont);
            this.groupBox2.Controls.Add(this.txtBodyFont);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtTitleFont);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtTitle);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Dock = DockStyle.Top;
            this.groupBox2.Location = new Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x1e5, 0x145);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox4.Controls.Add(this.lblPrinter);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.lblPrintDeriction);
            this.groupBox4.Controls.Add(this.lblPageSize);
            this.groupBox4.Controls.Add(this.lblPageck);
            this.groupBox4.Controls.Add(this.lblPageSet);
            this.groupBox4.Controls.Add(this.btnChangePage);
            this.groupBox4.Location = new Point(0xf8, 0x7f);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0xe3, 0xbf);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "页面及打印机设置";
            this.groupBox4.Enter += new EventHandler(this.groupBox4_Enter);
            this.lblPrinter.AutoSize = true;
            this.lblPrinter.Location = new Point(0x2b, 0x8a);
            this.lblPrinter.Name = "lblPrinter";
            this.lblPrinter.Size = new Size(0x95, 12);
            this.lblPrinter.TabIndex = 15;
            this.lblPrinter.Text = "GoldGrid virtual printer";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x18, 0x74);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x41, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "〖打印机〗";
            this.lblPrintDeriction.AutoSize = true;
            this.lblPrintDeriction.Location = new Point(0x2b, 0x56);
            this.lblPrintDeriction.Name = "lblPrintDeriction";
            this.lblPrintDeriction.Size = new Size(0x35, 12);
            this.lblPrintDeriction.TabIndex = 13;
            this.lblPrintDeriction.Text = "纵向打印";
            this.lblPageSize.AutoSize = true;
            this.lblPageSize.Location = new Point(0x2b, 0x44);
            this.lblPageSize.Name = "lblPageSize";
            this.lblPageSize.Size = new Size(0x71, 12);
            this.lblPageSize.TabIndex = 12;
            this.lblPageSize.Text = "279.4\x00d7215.9(毫米)";
            this.lblPageck.AutoSize = true;
            this.lblPageck.Location = new Point(0x2b, 50);
            this.lblPageck.Name = "lblPageck";
            this.lblPageck.Size = new Size(0x35, 12);
            this.lblPageck.TabIndex = 11;
            this.lblPageck.Text = "长\x00d7宽：";
            this.lblPageSet.AutoSize = true;
            this.lblPageSet.Location = new Point(0x1a, 0x20);
            this.lblPageSet.Name = "lblPageSet";
            this.lblPageSet.Size = new Size(0x35, 12);
            this.lblPageSet.TabIndex = 10;
            this.lblPageSet.Text = "〖纸张〗";
            this.btnChangePage.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnChangePage.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnChangePage.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnChangePage.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btnChangePage.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnChangePage.Location = new Point(0x84, 0x9c);
            this.btnChangePage.Name = "btnChangePage";
            this.btnChangePage.Size = new Size(80, 30);
            this.btnChangePage.TabIndex = 14;
            this.btnChangePage.Text = "更改";
            this.btnChangePage.UseVisualStyleBackColor = true;
            this.btnChangePage.Click += new EventHandler(this.btnChangePage_Click);
            this.btnBodyFont.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnBodyFont.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnBodyFont.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnBodyFont.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btnBodyFont.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnBodyFont.Location = new Point(0x14d, 0x59);
            this.btnBodyFont.Name = "btnBodyFont";
            this.btnBodyFont.Size = new Size(0x86, 30);
            this.btnBodyFont.TabIndex = 7;
            this.btnBodyFont.Text = "表体字体设置";
            this.btnBodyFont.UseVisualStyleBackColor = true;
            this.btnBodyFont.Click += new EventHandler(this.btnBodyFont_Click);
            this.btnTitleFont.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnTitleFont.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnTitleFont.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnTitleFont.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btnTitleFont.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnTitleFont.Location = new Point(0x14d, 0x34);
            this.btnTitleFont.Name = "btnTitleFont";
            this.btnTitleFont.Size = new Size(0x86, 30);
            this.btnTitleFont.TabIndex = 6;
            this.btnTitleFont.Text = "标题字体设置";
            this.btnTitleFont.UseVisualStyleBackColor = true;
            this.btnTitleFont.Click += new EventHandler(this.btnTitleFont_Click);
            this.txtBodyFont.Font = new Font("黑体", 11f);
            this.txtBodyFont.Location = new Point(0x4b, 0x5b);
            this.txtBodyFont.Name = "txtBodyFont";
            this.txtBodyFont.ReadOnly = true;
            this.txtBodyFont.Size = new Size(0xfc, 0x18);
            this.txtBodyFont.TabIndex = 5;
            this.txtBodyFont.Text = "黑体 大小:11";
            this.label3.AutoSize = true;
            this.label3.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label3.Location = new Point(6, 0x5c);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x3f, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "表体字体";
            this.txtTitleFont.Font = new Font("黑体", 14f);
            this.txtTitleFont.ForeColor = SystemColors.WindowText;
            this.txtTitleFont.Location = new Point(0x4b, 0x36);
            this.txtTitleFont.Name = "txtTitleFont";
            this.txtTitleFont.ReadOnly = true;
            this.txtTitleFont.Size = new Size(0xfc, 0x1d);
            this.txtTitleFont.TabIndex = 4;
            this.txtTitleFont.Text = "黑体 大小:14";
            this.label2.AutoSize = true;
            this.label2.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label2.Location = new Point(6, 0x37);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x3f, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "标题字体";
            this.txtTitle.Location = new Point(0x4b, 0x12);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new Size(0x188, 0x15);
            this.txtTitle.TabIndex = 3;
            this.txtTitle.TextChanged += new EventHandler(this.txtTitle_TextChanged);
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label1.Location = new Point(6, 0x13);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x3f, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "打印标题";
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.chkPrintFooter);
            this.groupBox3.Controls.Add(this.chkPrintSqeno);
            this.groupBox3.Controls.Add(this.chkPrintBlank);
            this.groupBox3.Controls.Add(this.chkCounter);
            this.groupBox3.Controls.Add(this.chkPrintBorder);
            this.groupBox3.Location = new Point(9, 0x7f);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0xe3, 0xbf);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "表体参数";
            this.groupBox5.Controls.Add(this.nuPrintRowsPerPage);
            this.groupBox5.Location = new Point(0x70, 0x73);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(0x53, 0x3d);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "打印行数";
            this.nuPrintRowsPerPage.Location = new Point(0x18, 0x19);
            this.nuPrintRowsPerPage.Name = "nuPrintRowsPerPage";
            this.nuPrintRowsPerPage.Size = new Size(0x25, 0x15);
            this.nuPrintRowsPerPage.TabIndex = 13;
            int[] bits = new int[4];
            bits[0] = 30;
            this.nuPrintRowsPerPage.Value = new decimal(bits);
            this.nuPrintRowsPerPage.ValueChanged += new EventHandler(this.nuPrintRowsPerPage_ValueChanged);
            this.chkPrintFooter.AutoSize = true;
            this.chkPrintFooter.Checked = true;
            this.chkPrintFooter.CheckState = CheckState.Checked;
            this.chkPrintFooter.Location = new Point(0x12, 0xa7);
            this.chkPrintFooter.Name = "chkPrintFooter";
            this.chkPrintFooter.Size = new Size(0x48, 0x10);
            this.chkPrintFooter.TabIndex = 12;
            this.chkPrintFooter.Text = "打印页脚";
            this.chkPrintFooter.UseVisualStyleBackColor = true;
            this.chkPrintFooter.CheckedChanged += new EventHandler(this.chkPrintFooter_CheckedChanged);
            this.chkPrintSqeno.AutoSize = true;
            this.chkPrintSqeno.Checked = true;
            this.chkPrintSqeno.CheckState = CheckState.Checked;
            this.chkPrintSqeno.Location = new Point(0x12, 0x86);
            this.chkPrintSqeno.Name = "chkPrintSqeno";
            this.chkPrintSqeno.Size = new Size(0x48, 0x10);
            this.chkPrintSqeno.TabIndex = 11;
            this.chkPrintSqeno.Text = "打印序号";
            this.chkPrintSqeno.UseVisualStyleBackColor = true;
            this.chkPrintSqeno.CheckedChanged += new EventHandler(this.chkPrintSqeno_CheckedChanged);
            this.chkPrintBlank.AutoSize = true;
            this.chkPrintBlank.Location = new Point(0x12, 0x63);
            this.chkPrintBlank.Name = "chkPrintBlank";
            this.chkPrintBlank.Size = new Size(0x48, 0x10);
            this.chkPrintBlank.TabIndex = 10;
            this.chkPrintBlank.Text = "打印空行";
            this.chkPrintBlank.UseVisualStyleBackColor = true;
            this.chkPrintBlank.CheckedChanged += new EventHandler(this.chkPrintBlank_CheckedChanged);
            this.chkCounter.AutoSize = true;
            this.chkCounter.Location = new Point(0x12, 0x41);
            this.chkCounter.Name = "chkCounter";
            this.chkCounter.Size = new Size(0x84, 0x10);
            this.chkCounter.TabIndex = 9;
            this.chkCounter.Text = "打印页末累计和总计";
            this.chkCounter.UseVisualStyleBackColor = true;
            this.chkCounter.CheckedChanged += new EventHandler(this.chkCounter_CheckedChanged);
            this.chkPrintBorder.AutoSize = true;
            this.chkPrintBorder.Checked = true;
            this.chkPrintBorder.CheckState = CheckState.Checked;
            this.chkPrintBorder.Location = new Point(0x12, 0x21);
            this.chkPrintBorder.Name = "chkPrintBorder";
            this.chkPrintBorder.Size = new Size(0x6c, 0x10);
            this.chkPrintBorder.TabIndex = 8;
            this.chkPrintBorder.Text = "打印行间分隔线";
            this.chkPrintBorder.UseVisualStyleBackColor = true;
            this.chkPrintBorder.CheckedChanged += new EventHandler(this.chkPrintBorder_CheckedChanged);
            this.btnResetPubPara.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnResetPubPara.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnResetPubPara.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnResetPubPara.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btnResetPubPara.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnResetPubPara.Location = new Point(0x9e, 0x14e);
            this.btnResetPubPara.Name = "btnResetPubPara";
            this.btnResetPubPara.Size = new Size(0x7b, 30);
            this.btnResetPubPara.TabIndex = 15;
            this.btnResetPubPara.Text = "恢复公共打印参数";
            this.btnResetPubPara.UseVisualStyleBackColor = true;
            this.btnResetPubPara.Click += new EventHandler(this.btnResetPubPara_Click);
            this.btnCurSave.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnCurSave.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnCurSave.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnCurSave.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btnCurSave.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnCurSave.Location = new Point(0x18b, 0x14e);
            this.btnCurSave.Name = "btnCurSave";
            this.btnCurSave.Size = new Size(80, 30);
            this.btnCurSave.TabIndex = 0x11;
            this.btnCurSave.Text = "保存";
            this.btnCurSave.UseVisualStyleBackColor = true;
            this.btnCurSave.Click += new EventHandler(this.btnCurSave_Click);
            this.btnCurReset.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnCurReset.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnCurReset.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnCurReset.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btnCurReset.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnCurReset.Location = new Point(0x12a, 0x14e);
            this.btnCurReset.Name = "btnCurReset";
            this.btnCurReset.Size = new Size(80, 30);
            this.btnCurReset.TabIndex = 0x10;
            this.btnCurReset.Text = "复原";
            this.btnCurReset.UseVisualStyleBackColor = true;
            this.btnCurReset.Click += new EventHandler(this.btnCurReset_Click);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.btnSavePub);
            this.tabPage2.Controls.Add(this.btnResetPub);
            this.tabPage2.Location = new Point(4, 0x16);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(0x1eb, 0x179);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "公用打印参数";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.btnBodyFontPub);
            this.groupBox1.Controls.Add(this.btnTitleFontPub);
            this.groupBox1.Controls.Add(this.txtBodyFontPub);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtTitleFontPub);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Location = new Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1e5, 0x14c);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox6.Controls.Add(this.lblPrinterPub);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.lblPrintDerictionPub);
            this.groupBox6.Controls.Add(this.lblPageSizePub);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.btnPageSettingsPub);
            this.groupBox6.Location = new Point(0xf8, 0x7f);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new Size(0xe3, 0xbf);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "页面及打印机设置";
            this.lblPrinterPub.AutoSize = true;
            this.lblPrinterPub.Location = new Point(0x30, 0x80);
            this.lblPrinterPub.Name = "lblPrinterPub";
            this.lblPrinterPub.Size = new Size(0x95, 12);
            this.lblPrinterPub.TabIndex = 0x11;
            this.lblPrinterPub.Text = "GoldGrid virtual printer";
            this.lblPrinterPub.Visible = false;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x1d, 0x6a);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x41, 12);
            this.label5.TabIndex = 0x10;
            this.label5.Text = "〖打印机〗";
            this.label5.Visible = false;
            this.lblPrintDerictionPub.AutoSize = true;
            this.lblPrintDerictionPub.Location = new Point(0x2b, 0x56);
            this.lblPrintDerictionPub.Name = "lblPrintDerictionPub";
            this.lblPrintDerictionPub.Size = new Size(0x35, 12);
            this.lblPrintDerictionPub.TabIndex = 13;
            this.lblPrintDerictionPub.Text = "纵向打印";
            this.lblPageSizePub.AutoSize = true;
            this.lblPageSizePub.Location = new Point(0x2b, 0x44);
            this.lblPageSizePub.Name = "lblPageSizePub";
            this.lblPageSizePub.Size = new Size(0x71, 12);
            this.lblPageSizePub.TabIndex = 12;
            this.lblPageSizePub.Text = "279.4\x00d7215.9(毫米)";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x2b, 50);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x35, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "长\x00d7宽：";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x1a, 0x20);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x35, 12);
            this.label10.TabIndex = 10;
            this.label10.Text = "〖纸张〗";
            this.btnPageSettingsPub.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnPageSettingsPub.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnPageSettingsPub.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnPageSettingsPub.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold);
            this.btnPageSettingsPub.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnPageSettingsPub.Location = new Point(0x76, 0x9c);
            this.btnPageSettingsPub.Name = "btnPageSettingsPub";
            this.btnPageSettingsPub.Size = new Size(0x59, 30);
            this.btnPageSettingsPub.TabIndex = 12;
            this.btnPageSettingsPub.Text = "页面设置";
            this.btnPageSettingsPub.UseVisualStyleBackColor = true;
            this.btnPageSettingsPub.Click += new EventHandler(this.btnPageSettingsPub_Click);
            this.btnBodyFontPub.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnBodyFontPub.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnBodyFontPub.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnBodyFontPub.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold);
            this.btnBodyFontPub.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnBodyFontPub.Location = new Point(0x14d, 0x4d);
            this.btnBodyFontPub.Name = "btnBodyFontPub";
            this.btnBodyFontPub.Size = new Size(0x86, 30);
            this.btnBodyFontPub.TabIndex = 6;
            this.btnBodyFontPub.Text = "表体字体设置";
            this.btnBodyFontPub.UseVisualStyleBackColor = true;
            this.btnBodyFontPub.Click += new EventHandler(this.btnBodyFontPub_Click);
            this.btnTitleFontPub.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnTitleFontPub.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnTitleFontPub.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnTitleFontPub.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold);
            this.btnTitleFontPub.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnTitleFontPub.Location = new Point(0x14d, 0x1d);
            this.btnTitleFontPub.Name = "btnTitleFontPub";
            this.btnTitleFontPub.Size = new Size(0x86, 30);
            this.btnTitleFontPub.TabIndex = 4;
            this.btnTitleFontPub.Text = "标题字体设置";
            this.btnTitleFontPub.UseVisualStyleBackColor = true;
            this.btnTitleFontPub.Click += new EventHandler(this.btnTitleFontPub_Click);
            this.txtBodyFontPub.Font = new Font("黑体", 11f);
            this.txtBodyFontPub.Location = new Point(0x4b, 0x4f);
            this.txtBodyFontPub.Name = "txtBodyFontPub";
            this.txtBodyFontPub.ReadOnly = true;
            this.txtBodyFontPub.Size = new Size(0xfc, 0x18);
            this.txtBodyFontPub.TabIndex = 5;
            this.txtBodyFontPub.Text = "黑体  大小:11";
            this.txtBodyFontPub.TextChanged += new EventHandler(this.txtBodyFontPub_TextChanged);
            this.label11.AutoSize = true;
            this.label11.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label11.Location = new Point(6, 80);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x3f, 14);
            this.label11.TabIndex = 6;
            this.label11.Text = "表体字体";
            this.txtTitleFontPub.Font = new Font("黑体", 14f);
            this.txtTitleFontPub.Location = new Point(0x4b, 0x1f);
            this.txtTitleFontPub.Name = "txtTitleFontPub";
            this.txtTitleFontPub.ReadOnly = true;
            this.txtTitleFontPub.Size = new Size(0xfc, 0x1d);
            this.txtTitleFontPub.TabIndex = 3;
            this.txtTitleFontPub.Text = "黑体 大小:14";
            this.txtTitleFontPub.TextChanged += new EventHandler(this.txtTitleFontPub_TextChanged);
            this.label12.AutoSize = true;
            this.label12.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label12.Location = new Point(6, 0x20);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x3f, 14);
            this.label12.TabIndex = 4;
            this.label12.Text = "标题字体";
            this.groupBox7.Controls.Add(this.chkPrintFooterPub);
            this.groupBox7.Controls.Add(this.chkPrintSeqnoPub);
            this.groupBox7.Controls.Add(this.chkPrintBlankPub);
            this.groupBox7.Controls.Add(this.chkCounterPub);
            this.groupBox7.Controls.Add(this.chkPrintBorderPub);
            this.groupBox7.Location = new Point(9, 0x7f);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new Size(0xe3, 0xbf);
            this.groupBox7.TabIndex = 0;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "表体参数";
            this.chkPrintFooterPub.AutoSize = true;
            this.chkPrintFooterPub.Checked = true;
            this.chkPrintFooterPub.CheckState = CheckState.Checked;
            this.chkPrintFooterPub.Location = new Point(0x12, 0xa7);
            this.chkPrintFooterPub.Name = "chkPrintFooterPub";
            this.chkPrintFooterPub.Size = new Size(0x48, 0x10);
            this.chkPrintFooterPub.TabIndex = 11;
            this.chkPrintFooterPub.Text = "打印页脚";
            this.chkPrintFooterPub.UseVisualStyleBackColor = true;
            this.chkPrintFooterPub.CheckedChanged += new EventHandler(this.chkPrintFooterPub_CheckedChanged);
            this.chkPrintSeqnoPub.AutoSize = true;
            this.chkPrintSeqnoPub.Checked = true;
            this.chkPrintSeqnoPub.CheckState = CheckState.Checked;
            this.chkPrintSeqnoPub.Location = new Point(0x12, 0x86);
            this.chkPrintSeqnoPub.Name = "chkPrintSeqnoPub";
            this.chkPrintSeqnoPub.Size = new Size(0x48, 0x10);
            this.chkPrintSeqnoPub.TabIndex = 10;
            this.chkPrintSeqnoPub.Text = "打印序号";
            this.chkPrintSeqnoPub.UseVisualStyleBackColor = true;
            this.chkPrintSeqnoPub.CheckedChanged += new EventHandler(this.chkPrintSeqnoPub_CheckedChanged);
            this.chkPrintBlankPub.AutoSize = true;
            this.chkPrintBlankPub.Location = new Point(0x12, 0x63);
            this.chkPrintBlankPub.Name = "chkPrintBlankPub";
            this.chkPrintBlankPub.Size = new Size(0x48, 0x10);
            this.chkPrintBlankPub.TabIndex = 9;
            this.chkPrintBlankPub.Text = "打印空行";
            this.chkPrintBlankPub.UseVisualStyleBackColor = true;
            this.chkPrintBlankPub.CheckedChanged += new EventHandler(this.chkPrintBlankPub_CheckedChanged);
            this.chkCounterPub.AutoSize = true;
            this.chkCounterPub.Location = new Point(0x12, 0x41);
            this.chkCounterPub.Name = "chkCounterPub";
            this.chkCounterPub.Size = new Size(0x84, 0x10);
            this.chkCounterPub.TabIndex = 8;
            this.chkCounterPub.Text = "打印页末累计和总计";
            this.chkCounterPub.UseVisualStyleBackColor = true;
            this.chkCounterPub.CheckedChanged += new EventHandler(this.chkCounterPub_CheckedChanged);
            this.chkPrintBorderPub.AutoSize = true;
            this.chkPrintBorderPub.Checked = true;
            this.chkPrintBorderPub.CheckState = CheckState.Checked;
            this.chkPrintBorderPub.Location = new Point(0x12, 0x21);
            this.chkPrintBorderPub.Name = "chkPrintBorderPub";
            this.chkPrintBorderPub.Size = new Size(0x6c, 0x10);
            this.chkPrintBorderPub.TabIndex = 7;
            this.chkPrintBorderPub.Text = "打印行间分隔线";
            this.chkPrintBorderPub.UseVisualStyleBackColor = true;
            this.chkPrintBorderPub.CheckedChanged += new EventHandler(this.chkPrintBorderPub_CheckedChanged);
            this.btnSavePub.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnSavePub.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnSavePub.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnSavePub.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold);
            this.btnSavePub.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnSavePub.Location = new Point(0x18b, 0x156);
            this.btnSavePub.Name = "btnSavePub";
            this.btnSavePub.Size = new Size(0x4b, 30);
            this.btnSavePub.TabIndex = 14;
            this.btnSavePub.Text = "保 存";
            this.btnSavePub.UseVisualStyleBackColor = true;
            this.btnSavePub.Click += new EventHandler(this.btnSavePub_Click);
            this.btnResetPub.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnResetPub.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnResetPub.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnResetPub.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold);
            this.btnResetPub.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnResetPub.Location = new Point(0x11e, 0x156);
            this.btnResetPub.Name = "btnResetPub";
            this.btnResetPub.Size = new Size(0x4b, 30);
            this.btnResetPub.TabIndex = 13;
            this.btnResetPub.Text = "复 原";
            this.btnResetPub.UseVisualStyleBackColor = true;
            this.btnResetPub.Click += new EventHandler(this.btnResetPub_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1f3, 0x1bd);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnPrint);
            base.Controls.Add(this.btnView);
            base.Controls.Add(this.tabControl1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "PrintOptionsForm";
            base.ShowIcon = false;
            this.Text = "表格打印";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.nuPrintRowsPerPage.EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            base.ResumeLayout(false);
        }

        private int method_0(PrintPageEventArgs printPageEventArgs_0, float float_6, float float_7, ref int int_5, float float_8, int int_6)
        {
            Color white;
            bool flag = false;
            string headerText = "";
            if (this.class136_0.method_24())
            {
                int num4 = int_6 + 1;
                switch (int_6)
                {
                    case -4:
                        headerText = this.string_3;
                        white = Color.White;
                        break;

                    case -3:
                        headerText = "小计";
                        white = Color.White;
                        break;

                    case -2:
                        headerText = "";
                        white = Color.White;
                        break;

                    case -1:
                        headerText = "序号";
                        white = Color.LightGray;
                        break;

                    default:
                        headerText = num4.ToString();
                        white = Color.White;
                        break;
                }
                this.method_8(printPageEventArgs_0.Graphics, headerText, this.class136_0.method_12(), this.class136_0.method_14(), white, new RectangleF(float_6, float_7, (float) this.int_0, this.float_3), DataGridViewContentAlignment.MiddleCenter, this.class136_0.method_18());
                float_6 += this.int_0;
            }
            for (int i = this.int_3; i < this.list_0.Count; i++)
            {
                printPageEventArgs_0.Graphics.FillRectangle(new SolidBrush(Color.White), new RectangleF(float_6 + 1f, float_7, (printPageEventArgs_0.PageBounds.Width - float_6) - 1f, this.float_3));
                int_5 = i;
                float width = this.list_0[i].Width * float_8;
                if ((float_6 + width) > (printPageEventArgs_0.MarginBounds.X + printPageEventArgs_0.MarginBounds.Width))
                {
                    if (i == this.int_3)
                    {
                        MessageBoxHelper.Show("当前纸张不足以打印此列", "表格打印错误提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        printPageEventArgs_0.HasMorePages = false;
                        return -1;
                    }
                    flag = true;
                    break;
                }
                switch (int_6)
                {
                    case -4:
                        headerText = this.dataRow_1[i].ToString();
                        white = Color.White;
                        break;

                    case -3:
                        headerText = this.dataRow_0[i].ToString();
                        white = Color.White;
                        break;

                    case -2:
                        headerText = "";
                        white = Color.White;
                        break;

                    case -1:
                        headerText = this.list_0[i].HeaderText;
                        white = Color.LightGray;
                        break;

                    default:
                        if (this.dataGridView_0.Rows[int_6].Cells[this.list_0[i].Name].Value != null)
                        {
                            headerText = this.dataGridView_0.Rows[int_6].Cells[this.list_0[i].Name].Value.ToString();
                        }
                        else
                        {
                            headerText = "";
                        }
                        white = Color.White;
                        break;
                }
                this.method_8(printPageEventArgs_0.Graphics, headerText, this.class136_0.method_12(), this.class136_0.method_14(), white, new RectangleF(float_6, float_7, width, this.float_3), this.list_0[i].DefaultCellStyle.Alignment, this.class136_0.method_18());
                float_6 += width;
            }
            if (flag)
            {
                return 1;
            }
            return 0;
        }

        private void method_1()
        {
            if (this.dataTable_0 != null)
            {
                DataTable table = new DataTable();
                for (int i = 0; i < this.list_0.Count; i++)
                {
                    DataColumn column2;
                    DataColumn column = this.dataTable_0.Columns[this.list_0[i].DataPropertyName];
                    if (column == null)
                    {
                        column2 = new DataColumn(this.list_0[i].DataPropertyName) {
                            DataType = typeof(string)
                        };
                    }
                    else
                    {
                        column2 = new DataColumn(column.ColumnName) {
                            DataType = column.DataType
                        };
                    }
                    table.Columns.Add(column2);
                }
                this.dataRow_0 = table.NewRow();
                this.dataRow_1 = table.NewRow();
            }
        }

        private void method_10(Graphics graphics_0, string string_4, Font font_1, Color color_0, Color color_1, RectangleF rectangleF_0, HorizontalAlignment horizontalAlignment_0, bool bool_4)
        {
            float x = rectangleF_0.X;
            SizeF ef = graphics_0.MeasureString(string_4, font_1);
            float y = rectangleF_0.Y + ((ef.Height - rectangleF_0.Height) / 2f);
            float width = ef.Width;
            switch (horizontalAlignment_0)
            {
                case HorizontalAlignment.Left:
                    x = rectangleF_0.X;
                    break;

                case HorizontalAlignment.Right:
                    x = (x + rectangleF_0.Width) - width;
                    break;

                case HorizontalAlignment.Center:
                    x += (rectangleF_0.Width - width) / 2f;
                    break;

                default:
                    x += (rectangleF_0.Width - width) / 2f;
                    break;
            }
            graphics_0.FillRectangle(new SolidBrush(color_1), new RectangleF(rectangleF_0.X, rectangleF_0.Y, rectangleF_0.Width, rectangleF_0.Height));
            if (bool_4)
            {
                graphics_0.DrawRectangle(new Pen(new SolidBrush(color_0)), rectangleF_0.X, rectangleF_0.Y, rectangleF_0.Width, rectangleF_0.Height);
            }
            graphics_0.DrawString(string_4, font_1, new SolidBrush(color_0), new PointF(x, y));
        }

        private float method_11()
        {
            float height = this.method_12("测试", this.font_0).Height;
            return (this.method_12("测试", this.class136_0.method_12()).Height / height);
        }

        private SizeF method_12(string string_4, Font font_1)
        {
            return base.CreateGraphics().MeasureString(string_4, font_1);
        }

        private int method_13(string string_4, string string_5, float float_6)
        {
            Graphics graphics = base.CreateGraphics();
            for (int i = 10; i < 0x3e8; i++)
            {
                Font font = new Font(string_5, (float) (i / 10));
                if (graphics.MeasureString(string_4, font).Height >= float_6)
                {
                    float num4 = (i - 1f) / 10f;
                    return Convert.ToInt32(num4);
                }
            }
            return 0;
        }

        private void method_14()
        {
            this.float_2 = base.CreateGraphics().MeasureString(this.class136_0.method_10(), this.class136_0.method_6()).Height;
            float height = base.CreateGraphics().MeasureString(this.class136_0.method_16(), this.class136_0.method_12()).Height;
            this.float_3 = Convert.ToInt32((float) (height / this.float_1));
            this.method_15();
            int num2 = (int) (((((this.class136_0.method_30().Bounds.Height - this.class136_0.method_30().Margins.Top) - this.class136_0.method_30().Margins.Bottom) - this.float_4) - this.float_5) / this.float_3);
            this.bool_3 = true;
            if (this.class136_0.method_20())
            {
                num2 -= 3;
            }
            else
            {
                num2--;
            }
            if (num2 <= 0)
            {
                MessageBoxHelper.Show("纸张太小，不足以打以一行，请设置打印机。", "表格打印", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                this.nuPrintRowsPerPage.Value = num2;
                this.class136_0.method_29(num2);
            }
        }

        private void method_15()
        {
            if ((this.list_2 != null) && (this.list_2.Count > 0))
            {
                this.float_4 = this.float_3 * this.list_2.Count;
            }
            else
            {
                this.float_4 = 0f;
            }
            if ((this.list_3 != null) && (this.list_3.Count > 0))
            {
                this.float_5 = this.float_3 * this.list_3.Count;
            }
            else
            {
                this.float_5 = 0f;
            }
        }

        private void method_16()
        {
            if (this.bool_3)
            {
                this.bool_3 = false;
            }
            else
            {
                int num = (int) this.nuPrintRowsPerPage.Value;
                this.float_2 = base.CreateGraphics().MeasureString(this.class136_0.method_10(), this.class136_0.method_6()).Height;
                if (this.class136_0.method_20())
                {
                    num += 3;
                }
                else
                {
                    num++;
                }
                this.method_15();
                float num2 = (((((this.class136_0.method_30().Bounds.Height - this.class136_0.method_30().Margins.Top) - this.class136_0.method_30().Margins.Bottom) - this.float_3) - this.float_4) - this.float_5) / ((float) num);
                float num3 = num2 * this.float_1;
                int num4 = this.method_13(this.class136_0.method_16(), this.class136_0.method_12().FontFamily.Name, num3);
                Font font = new Font(this.class136_0.method_12().FontFamily.Name, (float) num4);
                string str = font.FontFamily.Name + "  大小：" + num4.ToString();
                this.txtBodyFont.Text = str;
                this.txtBodyFont.Font = font;
                this.class136_0.method_17(str);
                this.class136_0.method_13(font);
            }
        }

        private void method_17()
        {
            this.printDocument_0.BeginPrint += new PrintEventHandler(this.printDocument_0_BeginPrint);
            this.printDocument_0.PrintPage += new PrintPageEventHandler(this.printDocument_0_PrintPage);
            this.printDocument_0.EndPrint += new PrintEventHandler(this.printDocument_0_EndPrint);
            base.Load += new EventHandler(this.PrintOptionsForm_Load);
        }

        private void method_18()
        {
            PrintDocument document = new PrintDocument();
            this.lblPrinter.Text = document.PrinterSettings.PrinterName;
            this.printDocument_0.PrinterSettings.PrinterName = this.lblPrinter.Text;
            this.pageSetupDialog_0.PrinterSettings.PrinterName = this.lblPrinter.Text;
            this.printDialog_0.PrinterSettings.PrinterName = this.lblPrinter.Text;
            bool flag = false;
            string str = this.string_2 + this.string_1;
            flag = this.method_24(str);
            this.method_20(this.class137_0, this.class137_1);
            this.txtTitle.Text = this.class136_0.method_0();
            string str2 = Path.Combine(this.string_2, this.string_0);
            if (!this.method_26(str2) && flag)
            {
                this.method_23();
            }
            this.method_21(this.class136_0, this.class136_1);
            this.printDocument_0.DefaultPageSettings = this.method_19(this.class136_0.method_30());
            this.method_14();
        }

        private PageSettings method_19(PageSettings pageSettings_0)
        {
            PageSettings settings = new PageSettings(pageSettings_0.PrinterSettings);
            try
            {
                settings.Color = pageSettings_0.Color;
                settings.Landscape = pageSettings_0.Landscape;
                settings.Margins = pageSettings_0.Margins;
                settings.PaperSize = pageSettings_0.PaperSize;
                settings.PaperSource = pageSettings_0.PaperSource;
                settings.PrinterResolution = pageSettings_0.PrinterResolution;
            }
            catch (Exception exception)
            {
                this.ilog_0.Error("PageSettingCopy异常：" + exception.ToString());
            }
            return settings;
        }

        private void method_2(List<DataGridViewColumn> list)
        {
            SortedList list2 = new SortedList();
            if ((list != null) && (list.Count > 0))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list2.Add(list[i].DisplayIndex, list[i]);
                }
                list.Clear();
                IList valueList = list2.GetValueList();
                for (int j = 0; j < list2.Count; j++)
                {
                    list.Add(valueList[j] as DataGridViewColumn);
                }
            }
        }

        private void method_20(Class137 class137_2, Class137 class137_3)
        {
            class137_3.method_5(class137_2.method_4());
            class137_3.method_7(class137_2.method_6());
            class137_3.method_9(class137_2.method_8());
            class137_3.method_1(class137_2.method_0());
            class137_3.method_11(class137_2.method_10());
            class137_3.method_13(class137_2.method_12());
            class137_3.method_15(class137_2.method_14());
            class137_3.method_3(class137_2.method_2());
            class137_3.method_21(class137_2.method_20());
            class137_3.method_17(class137_2.method_16());
            class137_3.method_19(class137_2.method_18());
            class137_3.method_25(class137_2.method_24());
            class137_3.method_23(class137_2.method_22());
            class137_3.method_27(this.method_19(class137_2.method_26()));
        }

        private void method_21(Class136 class136_2, Class136 class136_3)
        {
            class136_3.method_1(class136_2.method_0());
            class136_3.method_3(class136_2.method_2());
            class136_3.method_7(class136_2.method_6());
            class136_3.method_9(class136_2.method_8());
            class136_3.method_11(class136_2.method_10());
            class136_3.method_13(class136_2.method_12());
            class136_3.method_15(class136_2.method_14());
            class136_3.method_17(class136_2.method_16());
            class136_3.method_5(class136_2.method_4());
            class136_3.method_23(class136_2.method_22());
            class136_3.method_19(class136_2.method_18());
            class136_3.method_21(class136_2.method_20());
            class136_3.method_27(class136_2.method_26());
            class136_3.method_25(class136_2.method_24());
            class136_3.method_29(class136_2.method_28());
            class136_3.method_31(this.method_19(class136_2.method_30()));
        }

        private string method_22(PaperSize paperSize_0)
        {
            double num = paperSize_0.Width * 0.254;
            num = Math.Round(num, 1);
            double num2 = paperSize_0.Height * 0.254;
            num2 = Math.Round(num2, 1);
            return (num.ToString() + "\x00d7" + num2.ToString() + "(毫米)");
        }

        private void method_23()
        {
            this.txtTitleFont.Font = this.class137_0.method_4();
            this.txtTitleFont.Text = this.class137_0.method_8();
            this.txtTitleFont.ForeColor = this.class137_0.method_6();
            this.txtBodyFont.Font = this.class137_0.method_10();
            this.txtBodyFont.Text = this.class137_0.method_14();
            this.txtBodyFont.ForeColor = this.class137_0.method_12();
            this.chkPrintBorder.Checked = this.class137_0.method_16();
            this.chkCounter.Checked = this.class137_0.method_18();
            this.chkPrintBlank.Checked = this.class137_0.method_20();
            this.chkPrintSqeno.Checked = this.class137_0.method_22();
            this.chkPrintFooter.Checked = this.class137_0.method_24();
            this.lblPageSize.Text = this.method_22(this.class137_0.method_26().PaperSize);
            this.lblPrintDeriction.Text = this.class137_0.method_26().Landscape ? "横向打印" : "纵向打印";
            this.pageSetupDialog_0.PageSettings = this.method_19(this.class137_0.method_26());
            this.printDialog_0.PrinterSettings = this.pageSetupDialog_0.PrinterSettings;
            this.class136_0.method_7(this.class137_0.method_4());
            this.class136_0.method_13(this.class137_0.method_10());
            this.class136_0.method_9(this.class137_0.method_6());
            this.class136_0.method_15(this.class137_0.method_12());
            this.class136_0.method_31(this.method_19(this.class137_0.method_26()));
        }

        private bool method_24(string string_4)
        {
            object obj2 = SerializeUtil.Deserialize(true, string_4);
            bool flag = false;
            if ((obj2 != null) && (obj2 is Class137))
            {
                this.class137_0 = obj2 as Class137;
                this.lblPageSizePub.Text = this.method_22(this.class137_0.method_26().PaperSize);
                this.lblPrintDerictionPub.Text = this.class137_0.method_26().Landscape ? "横向打印" : "纵向打印";
                this.pageSetupDialog_1.PageSettings = this.method_19(this.class137_0.method_26());
                flag = true;
            }
            this.txtTitleFontPub.Font = this.class137_0.method_4();
            this.txtTitleFontPub.Text = this.class137_0.method_8();
            this.txtBodyFontPub.Font = this.class137_0.method_10();
            this.txtBodyFontPub.Text = this.class137_0.method_14();
            this.txtTitleFontPub.ForeColor = this.class137_0.method_6();
            this.txtBodyFontPub.ForeColor = this.class137_0.method_12();
            this.chkPrintBorderPub.Checked = this.class137_0.method_16();
            this.chkCounterPub.Checked = this.class137_0.method_18();
            this.chkPrintBlankPub.Checked = this.class137_0.method_20();
            this.chkPrintSeqnoPub.Checked = this.class137_0.method_22();
            this.chkPrintFooterPub.Checked = this.class137_0.method_24();
            return flag;
        }

        private void method_25()
        {
            this.class137_0.method_5(this.txtTitleFontPub.Font);
            this.class137_0.method_9(this.txtTitleFontPub.Text);
            this.class137_0.method_11(this.txtBodyFontPub.Font);
            this.class137_0.method_15(this.txtBodyFontPub.Text);
            this.class137_0.method_17(this.chkPrintBorderPub.Checked);
            this.class137_0.method_19(this.chkCounterPub.Checked);
            this.class137_0.method_21(this.chkPrintBlankPub.Checked);
            this.class137_0.method_23(this.chkPrintSeqnoPub.Checked);
            this.class137_0.method_25(this.chkPrintFooterPub.Checked);
            this.class137_0.method_27(this.pageSetupDialog_1.PageSettings);
        }

        private bool method_26(string string_4)
        {
            object obj2 = SerializeUtil.Deserialize(true, string_4);
            bool flag = false;
            if ((obj2 != null) && (obj2 is Class136))
            {
                this.class136_0 = obj2 as Class136;
                if (!this.method_27(this.class136_0.method_30().PrinterSettings.PrinterName))
                {
                    if (PrinterSettings.InstalledPrinters.Count <= 0)
                    {
                        MessageBoxHelper.Show("打印机不存在，请设置打印机", "表格打印", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        this.class136_0.method_30().PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[0].ToString();
                    }
                }
                this.lblPageSize.Text = this.method_22(this.class136_0.method_30().PaperSize);
                this.lblPrintDeriction.Text = this.class136_0.method_30().Landscape ? "横向打印" : "纵向打印";
                this.pageSetupDialog_0.PageSettings = this.method_19(this.class136_0.method_30());
                this.lblPrinter.Text = this.class136_0.method_30().PrinterSettings.PrinterName;
                flag = true;
            }
            this.txtTitle.Text = this.class136_0.method_0();
            this.txtTitleFont.Font = this.class136_0.method_6();
            this.txtTitleFont.Text = this.class136_0.method_10();
            this.txtBodyFont.Font = this.class136_0.method_12();
            this.txtBodyFont.Text = this.class136_0.method_16();
            this.txtTitleFont.ForeColor = this.class136_0.method_8();
            this.txtBodyFont.ForeColor = this.class136_0.method_14();
            this.chkPrintBorder.Checked = this.class136_0.method_18();
            this.chkCounter.Checked = this.class136_0.method_20();
            this.chkPrintBlank.Checked = this.class136_0.method_22();
            this.chkPrintSqeno.Checked = this.class136_0.method_24();
            this.chkPrintFooter.Checked = this.class136_0.method_26();
            this.nuPrintRowsPerPage.Value = this.class136_0.method_28();
            return flag;
        }

        private bool method_27(string string_4)
        {
            bool flag = false;
            foreach (string str in PrinterSettings.InstalledPrinters)
            {
                if (string_4.Equals(str))
                {
                    flag = true;
                }
            }
            return flag;
        }

        private void method_28()
        {
            this.class136_0.method_1(this.txtTitle.Text);
            this.class136_0.method_7(this.txtTitleFont.Font);
            this.class136_0.method_11(this.txtTitleFont.Text);
            this.class136_0.method_13(this.txtBodyFont.Font);
            this.class136_0.method_17(this.txtBodyFont.Text);
            this.class136_0.method_19(this.chkPrintBorder.Checked);
            this.class136_0.method_21(this.chkCounter.Checked);
            this.class136_0.method_23(this.chkPrintBlank.Checked);
            this.class136_0.method_25(this.chkPrintSqeno.Checked);
            this.class136_0.method_27(this.chkPrintFooter.Checked);
            this.class136_0.method_29(Convert.ToInt32(this.nuPrintRowsPerPage.Value));
            this.class136_0.method_31(this.pageSetupDialog_0.PageSettings);
        }

        private int method_3()
        {
            int num = (int) ((((this.class136_0.method_30().Bounds.Height - this.class136_0.method_30().Margins.Top) - this.class136_0.method_30().Margins.Bottom) - this.float_4) - this.float_5);
            int num2 = (this.class136_0.method_30().Bounds.Width - this.class136_0.method_30().Margins.Left) - this.class136_0.method_30().Margins.Right;
            int num3 = 0;
            for (int i = 0; i < this.dataGridView_0.Columns.Count; i++)
            {
                if (this.dataGridView_0.Columns[i].Visible)
                {
                    num3 += this.dataGridView_0.Columns[i].Width;
                }
            }
            int num5 = (num3 / num2) + 1;
            int num7 = (num5 == 0) ? num5 : (num5 + 1);
            int num6 = (int) (((float) num) / this.float_3);
            int num8 = ((this.dataGridView_0.Rows.Count / num6) == 0) ? 1 : (this.dataGridView_0.Rows.Count / num6);
            return (int) ((num7 * num8) * this.method_11());
        }

        private void method_4(PrintPageEventArgs printPageEventArgs_0, float float_6, float float_7)
        {
            foreach (PrinterItems items in this.list_2)
            {
                this.method_10(printPageEventArgs_0.Graphics, items.Text, this.class136_0.method_12(), this.class136_0.method_14(), Color.White, new RectangleF(float_6, float_7, (float) printPageEventArgs_0.MarginBounds.Width, this.float_3), items.Align, false);
                float_7 += this.float_3;
            }
        }

        private void method_5(PrintPageEventArgs printPageEventArgs_0, float float_6, float float_7)
        {
            foreach (PrinterItems items in this.list_3)
            {
                this.method_10(printPageEventArgs_0.Graphics, items.Text, this.class136_0.method_12(), this.class136_0.method_14(), Color.White, new RectangleF(float_6, float_7, (float) printPageEventArgs_0.MarginBounds.Width, this.float_3), items.Align, false);
                float_7 += this.float_3;
            }
        }

        private void method_6(PrintPageEventArgs printPageEventArgs_0)
        {
            this.method_9(printPageEventArgs_0.Graphics, this.class136_0.method_0(), this.class136_0.method_6(), this.class136_0.method_8(), Color.White, new Rectangle(printPageEventArgs_0.MarginBounds.X, (printPageEventArgs_0.MarginBounds.Y - ((int) this.float_2)) - 10, printPageEventArgs_0.MarginBounds.Width, Convert.ToInt32(this.float_2)), this.class136_0.method_2(), false);
        }

        private void method_7(PrintPageEventArgs printPageEventArgs_0)
        {
            int x = printPageEventArgs_0.MarginBounds.X;
            int num2 = printPageEventArgs_0.MarginBounds.Bottom + 10;
            this.method_10(printPageEventArgs_0.Graphics, "第" + this.int_4.ToString() + "页", new Font(this.class136_0.method_12().FontFamily.Name, 11f), Color.Black, Color.White, new RectangleF((float) x, (float) num2, (float) printPageEventArgs_0.MarginBounds.Width, (float) this.int_1), this.class136_0.method_4(), false);
        }

        private void method_8(Graphics graphics_0, string string_4, Font font_1, Color color_0, Color color_1, RectangleF rectangleF_0, DataGridViewContentAlignment dataGridViewContentAlignment_0, bool bool_4)
        {
            float x = rectangleF_0.X;
            SizeF ef = graphics_0.MeasureString(string_4, font_1);
            float y = rectangleF_0.Y;
            float width = ef.Width;
            float height = ef.Height;
            DataGridViewContentAlignment alignment = dataGridViewContentAlignment_0;
            if (alignment > DataGridViewContentAlignment.MiddleCenter)
            {
                if (alignment <= DataGridViewContentAlignment.BottomLeft)
                {
                    if (alignment != DataGridViewContentAlignment.MiddleRight)
                    {
                        if (alignment != DataGridViewContentAlignment.BottomLeft)
                        {
                            goto Label_01A9;
                        }
                        x = rectangleF_0.X;
                        y = (y + rectangleF_0.Height) - height;
                    }
                    else
                    {
                        if (width < rectangleF_0.Width)
                        {
                            x = (x + rectangleF_0.Width) - width;
                        }
                        else
                        {
                            x = rectangleF_0.X;
                        }
                        y += (rectangleF_0.Height - height) / 2f;
                    }
                    goto Label_0214;
                }
                switch (alignment)
                {
                    case DataGridViewContentAlignment.BottomCenter:
                        x += (rectangleF_0.Width - width) / 2f;
                        y = (y + rectangleF_0.Height) - height;
                        goto Label_0214;

                    case DataGridViewContentAlignment.BottomRight:
                        if (width < rectangleF_0.Width)
                        {
                            x = (x + rectangleF_0.Width) - width;
                        }
                        else
                        {
                            x = rectangleF_0.X;
                        }
                        y = (y + rectangleF_0.Height) - height;
                        goto Label_0214;
                }
            }
            else
            {
                switch (alignment)
                {
                    case DataGridViewContentAlignment.NotSet:
                        x = rectangleF_0.X;
                        y += (rectangleF_0.Height - height) / 2f;
                        goto Label_0214;

                    case DataGridViewContentAlignment.TopLeft:
                        x = rectangleF_0.X;
                        y = rectangleF_0.Y;
                        goto Label_0214;

                    case DataGridViewContentAlignment.TopCenter:
                        x += (rectangleF_0.Width - width) / 2f;
                        y = rectangleF_0.Y;
                        goto Label_0214;

                    case DataGridViewContentAlignment.TopRight:
                        if (width >= rectangleF_0.Width)
                        {
                            x = rectangleF_0.X;
                            break;
                        }
                        x = (x + rectangleF_0.Width) - width;
                        break;

                    case DataGridViewContentAlignment.MiddleLeft:
                        x = rectangleF_0.X;
                        y += (rectangleF_0.Height - height) / 2f;
                        goto Label_0214;

                    case DataGridViewContentAlignment.MiddleCenter:
                        x += (rectangleF_0.Width - width) / 2f;
                        y += (rectangleF_0.Height - height) / 2f;
                        goto Label_0214;

                    default:
                        goto Label_01A9;
                }
                y = rectangleF_0.Y;
                goto Label_0214;
            }
        Label_01A9:
            x = rectangleF_0.X;
            y += (rectangleF_0.Height - height) / 2f;
        Label_0214:
            graphics_0.FillRectangle(new SolidBrush(color_1), rectangleF_0);
            if (bool_4)
            {
                graphics_0.DrawRectangle(new Pen(new SolidBrush(color_0)), rectangleF_0.X, rectangleF_0.Y, rectangleF_0.Width, rectangleF_0.Height);
            }
            if (graphics_0.MeasureString(string_4, font_1).Width <= rectangleF_0.Width)
            {
                graphics_0.DrawString(string_4, font_1, new SolidBrush(color_0), new PointF(x, y));
            }
            else
            {
                string text = string.Empty;
                for (int i = string_4.Length; i > 0; i--)
                {
                    text = string_4.Substring(0, i) + "...";
                    if (graphics_0.MeasureString(text, font_1).Width <= rectangleF_0.Width)
                    {
                        break;
                    }
                }
                graphics_0.DrawString(text, font_1, new SolidBrush(color_0), new PointF(rectangleF_0.X, y));
            }
        }

        private void method_9(Graphics graphics_0, string string_4, Font font_1, Color color_0, Color color_1, Rectangle rectangle_0, HorizontalAlignment horizontalAlignment_0, bool bool_4)
        {
            this.method_10(graphics_0, string_4, font_1, color_0, color_1, new RectangleF((float) rectangle_0.X, (float) rectangle_0.Y, (float) rectangle_0.Width, (float) rectangle_0.Height), horizontalAlignment_0, bool_4);
        }

        private void nuPrintRowsPerPage_ValueChanged(object sender, EventArgs e)
        {
            this.class136_0.method_29(Convert.ToInt32(this.nuPrintRowsPerPage.Value));
            this.method_16();
        }

        public bool Print(bool bool_4)
        {
            this.bool_2 = false;
            if (!bool_4)
            {
                this.printDocument_0.Print();
            }
            else
            {
                base.ShowDialog();
            }
            return this.bool_2;
        }

        private void printDocument_0_BeginPrint(object sender, PrintEventArgs e)
        {
            this.bool_2 = false;
            this.stringFormat_0 = new StringFormat();
            this.stringFormat_0.Alignment = StringAlignment.Near;
            this.stringFormat_0.LineAlignment = StringAlignment.Center;
            this.stringFormat_0.Trimming = StringTrimming.EllipsisCharacter;
            this.list_0.Clear();
            this.int_2 = 0;
            this.int_3 = 0;
            this.bool_0 = false;
            this.int_4 = 1;
            this.bool_1 = true;
            this.printDocument_0.DocumentName = this.method_3().ToString();
            for (int i = 0; i < this.dataGridView_0.Columns.Count; i++)
            {
                if (this.dataGridView_0.Columns[i].Visible)
                {
                    this.list_0.Add(this.dataGridView_0.Columns[i]);
                }
            }
            this.method_2(this.list_0);
            this.method_1();
            for (int j = 0; j < this.list_0.Count; j++)
            {
                if (((this.list_0[j].DefaultCellStyle.Alignment == DataGridViewContentAlignment.BottomRight) || (this.list_0[j].DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleRight)) || (this.list_0[j].DefaultCellStyle.Alignment == DataGridViewContentAlignment.TopRight))
                {
                    this.list_1.Add(j);
                }
            }
            float num4 = this.method_11();
            this.float_2 = base.CreateGraphics().MeasureString(this.class136_0.method_10(), this.class136_0.method_6()).Height;
            this.float_3 = this.float_0 * num4;
            this.int_0 = Convert.ToInt32((float) (this.int_0 * num4));
            this.method_15();
        }

        private void printDocument_0_EndPrint(object sender, PrintEventArgs e)
        {
            this.bool_2 = true;
        }

        private void printDocument_0_PrintPage(object sender, PrintPageEventArgs e)
        {
            this.bool_2 = false;
            this.bool_0 = false;
            if (this.bool_1)
            {
                this.method_6(e);
            }
            float num9 = this.method_11();
            float x = e.MarginBounds.X;
            float y = e.MarginBounds.Y;
            int num10 = this.int_3;
            int num2 = this.int_2;
            if (this.float_4 > 0f)
            {
                this.method_4(e, x, y);
                x = e.MarginBounds.X;
                y += this.float_4;
            }
            int num4 = this.method_0(e, x, y, ref num10, num9, -1);
            if (num4 == 1)
            {
                this.bool_0 = true;
            }
            else
            {
                if (num4 != 0)
                {
                    e.HasMorePages = false;
                    return;
                }
                this.bool_0 = false;
            }
            x = e.MarginBounds.X;
            y += this.float_3;
            int num21 = this.dataGridView_0.Rows.Count - this.int_2;
            int num20 = 0;
            int num18 = 0;
            if (num21 <= this.class136_0.method_28())
            {
                num20 = this.class136_0.method_28() - num21;
                num18 = this.int_2 + num21;
                e.HasMorePages = false;
            }
            else
            {
                num18 = this.int_2 + this.class136_0.method_28();
                e.HasMorePages = true;
            }
            for (int i = this.int_2; i < num18; i++)
            {
                num2 = i;
                int num8 = this.int_3;
                num4 = this.method_0(e, x, y, ref num8, num9, i);
                if (num4 == 1)
                {
                    x = e.MarginBounds.X;
                    y += this.float_3;
                    this.bool_0 = true;
                }
                else
                {
                    if (num4 != 0)
                    {
                        e.HasMorePages = false;
                        return;
                    }
                    x = e.MarginBounds.X;
                    y += this.float_3;
                    this.bool_0 = false;
                }
            }
            if (this.class136_0.method_22() && (num20 > 0))
            {
                for (int j = 0; j < num20; j++)
                {
                    int num14 = this.int_3;
                    num4 = this.method_0(e, x, y, ref num14, num9, -2);
                    if (num4 == 1)
                    {
                        x = e.MarginBounds.X;
                        y += this.float_3;
                    }
                    else
                    {
                        if (num4 != 0)
                        {
                            e.HasMorePages = false;
                            return;
                        }
                        x = e.MarginBounds.X;
                        y += this.float_3;
                    }
                }
            }
            if (this.class136_0.method_20())
            {
                double num12;
                string str2;
                if ((this.int_2 + this.class136_0.method_28()) < this.dataGridView_0.Rows.Count)
                {
                    this.string_3 = "累计";
                }
                else
                {
                    this.string_3 = "总计";
                }
                int num = this.int_2;
                int num3 = num2;
                foreach (int num11 in this.list_1)
                {
                    num12 = 0.0;
                    for (int k = num; k <= num3; k++)
                    {
                        string name = this.list_0[num11].Name;
                        try
                        {
                            str2 = this.dataGridView_0.Rows[k].Cells[name].Value.ToString();
                            num12 += Convert.ToDouble(str2);
                        }
                        catch
                        {
                            num12 = 0.0;
                            break;
                        }
                    }
                    if (this.dataRow_0 != null)
                    {
                        this.dataRow_0[num11] = num12.ToString("f2");
                    }
                }
                foreach (int num15 in this.list_1)
                {
                    num12 = 0.0;
                    for (int m = 0; m <= num3; m++)
                    {
                        string str3 = this.list_0[num15].Name;
                        try
                        {
                            str2 = this.dataGridView_0.Rows[m].Cells[str3].Value.ToString();
                            num12 += Convert.ToDouble(str2);
                        }
                        catch
                        {
                            num12 = 0.0;
                            break;
                        }
                    }
                    this.dataRow_1[num15] = num12.ToString("f2");
                }
                int num17 = this.int_3;
                num4 = this.method_0(e, x, y, ref num17, num9, -3);
                x = e.MarginBounds.X;
                y += this.float_3;
                num17 = this.int_3;
                num4 = this.method_0(e, x, y, ref num17, num9, -4);
                x = e.MarginBounds.X;
                y += this.float_3;
            }
            if (this.float_5 > 0f)
            {
                y += 2f;
                this.method_5(e, x, y);
                x = e.MarginBounds.X;
                y += this.float_5;
            }
            if (this.class136_0.method_26())
            {
                this.method_7(e);
            }
            this.int_4++;
            if (this.bool_0)
            {
                this.int_3 = num10;
                e.HasMorePages = true;
            }
            else
            {
                this.int_2 = num2 + 1;
                this.int_3 = 0;
            }
        }

        private void PrintOptionsForm_Load(object sender, EventArgs e)
        {
        }

        public void SetBodyFont(Font font_1)
        {
            if (this.fontDialog_0 != null)
            {
                this.fontDialog_0.Font = font_1;
            }
            string str = this.fontDialog_0.Font.Name + "    大小:" + this.fontDialog_0.Font.Size.ToString();
            this.txtBodyFont.Font = this.fontDialog_0.Font;
            this.txtBodyFont.ForeColor = this.fontDialog_0.Color;
            this.txtBodyFont.Text = str;
            this.class136_0.method_17(str);
            this.class136_0.method_13(this.fontDialog_0.Font);
            this.class136_0.method_15(this.fontDialog_0.Color);
        }

        public void SetChkCounterEnabled(bool bool_4)
        {
            this.chkCounter.Enabled = bool_4;
        }

        public void SetChkCounterPubEnabled(bool bool_4)
        {
            this.chkCounterPub.Enabled = bool_4;
        }

        private void txtBodyFontPub_TextChanged(object sender, EventArgs e)
        {
            this.class137_0.method_15(this.txtBodyFontPub.Text);
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            this.class136_0.method_1(this.txtTitle.Text);
        }

        private void txtTitleFontPub_TextChanged(object sender, EventArgs e)
        {
            this.class137_0.method_9(this.txtTitleFontPub.Text);
        }
    }
}

