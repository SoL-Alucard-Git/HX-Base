namespace Aisino.Fwkp.Print
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class PrintSetUp : Form
    {
        private bool bool_0;
        private AisinoBTN but_close;
        private AisinoBTN but_Preview;
        private AisinoBTN but_print;
        private ComboBox comboBox_DefaultPrinter;
        public string CurrentPrinterName;
        private AisinoGRP groupBox1;
        private AisinoGRP groupBox2;
        private IContainer icontainer_0;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private AisinoLBL label4;
        private List<string> list_0;
        protected ILog loger;
        private object[] object_0;
        public static PageSetupDialog pageSetupDialog;
        private PaperSize paperSize_0;
        private PaperSize paperSize_1;
        private PaperSize paperSize_2;
        private PaperSize paperSize_3;
        private PaperSize paperSize_4;
        private PaperSize paperSize_5;
        private PaperSize paperSize_6;
        private PaperSize paperSize_7;
        private PrintDocument printDocument_0;
        private Printer printer_0;
        private GroupBox printerDefault;
        private AisinoRDO r1;
        private AisinoRDO r2;
        private AisinoTXT txt_left;
        private AisinoTXT txt_top;

        public event PrintSet OnClose;

        public event PrintSet OnPreview;

        public event PrintSet OnPrint;

        static PrintSetUp()
        {
            
        }

        public PrintSetUp(object[] object_1)
        {
            
            this.loger = LogUtil.GetLogger<PrintSetUp>();
            this.list_0 = new List<string>();
            this.printDocument_0 = new PrintDocument();
            this.bool_0 = true;
            this.CurrentPrinterName = "";
            this.paperSize_0 = new PaperSize("航天信息纸张", 550, 850);
            this.paperSize_1 = new PaperSize("航天信息纸张", 700, 0x3b5);
            this.paperSize_2 = new PaperSize("航天信息纸张", 300, 0x2b9);
            this.paperSize_3 = new PaperSize("航天信息纸张", 300, 0x256);
            this.paperSize_4 = new PaperSize("航天信息纸张", 300, 500);
            this.paperSize_5 = new PaperSize("航天信息纸张", 0xe0, 0x2b9);
            this.paperSize_6 = new PaperSize("航天信息纸张", 0xe0, 0x256);
            this.paperSize_7 = new PaperSize("航天信息纸张", 0xe0, 500);
            this.InitializeComponent();
            try
            {
                this.txt_top.KeyPress += new KeyPressEventHandler(this.txt_left_KeyPress);
                this.txt_top.MaxLength = 8;
                this.txt_top.ImeMode = ImeMode.Disable;
                this.txt_top.ShortcutsEnabled = false;
                this.txt_left.KeyPress += new KeyPressEventHandler(this.txt_left_KeyPress);
                this.txt_left.MaxLength = 8;
                this.txt_left.ImeMode = ImeMode.Disable;
                this.txt_left.ShortcutsEnabled = false;
                this.printer_0 = new Printer(object_1);
                PrinterEventArgs printerArgs = this.printer_0.GetPrinterArgs(IPrint.IsZjFlag);
                this.method_0(printerArgs.PrinterName);
                if (this.comboBox_DefaultPrinter.Items.Count == 0)
                {
                    this.loger.Error("没有打印机,请设置打印机！");
                }
                else
                {
                    this.object_0 = object_1;
                    if ((printerArgs != null) && (object_1 != null))
                    {
                        this.txt_top.Text = printerArgs.Top.ToString();
                        this.txt_left.Text = printerArgs.Left.ToString();
                        this.txt_left.TextChanged += new EventHandler(this.txt_left_TextChanged);
                        this.txt_top.TextChanged += new EventHandler(this.txt_top_TextChanged);
                        this.CurrentPrinterName = printerArgs.PrinterName;
                        if (((this.object_0 != null) && (this.object_0.Length > 3)) && (this.object_0[3].ToString() == "_QD"))
                        {
                            this.r1.Text = "全打";
                            this.r2.Text = "套打";
                            this.groupBox2.Text = "套打";
                            if (printerArgs.IsQuanDa)
                            {
                                this.r1.Checked = true;
                                this.r2.Checked = false;
                            }
                            else
                            {
                                this.r1.Checked = false;
                                this.r2.Checked = true;
                            }
                        }
                        else if (((this.object_0 != null) && (this.object_0.Length > 0)) && (this.object_0[0].ToString() == "q"))
                        {
                            this.r1.Text = "横打";
                            this.r2.Text = "竖打";
                            this.groupBox2.Text = "打印方式";
                            if (printerArgs.IsQuanDa)
                            {
                                this.r1.Checked = true;
                                this.r2.Checked = false;
                            }
                            else
                            {
                                this.r1.Checked = false;
                                this.r2.Checked = true;
                            }
                        }
                        this.method_1();
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
            }
        }

        private void but_close_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.OnClose != null) && (this.printer_0 != null))
                {
                    PrintSetEventArgs args = new PrintSetEventArgs();
                    this.printer_0.GetPrinterArgs(false);
                    args.Offset = new PointF(this.printer_0.RealPrinterArgs.Left, this.printer_0.RealPrinterArgs.Top);
                    args.IsTaoDa = this.r1.Checked;
                    this.OnClose(this, args);
                }
                base.Close();
                base.Dispose();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                this.loger.Error(exception.ToString());
            }
        }

        private void but_Preview_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.OnPreview != null) && (this.printer_0 != null))
                {
                    PrintSetEventArgs args = new PrintSetEventArgs();
                    this.printer_0.GetPrinterArgs(false);
                    args.Offset = new PointF(this.printer_0.UserPrinterArgs.Left, this.printer_0.UserPrinterArgs.Top);
                    args.IsTaoDa = this.r1.Checked;
                    this.OnPreview(this, args);
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[but_Preview_Click函数]" + exception.Message);
                this.loger.Error(exception.ToString());
            }
        }

        private void but_print_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.OnPrint != null) && (this.printer_0 != null))
                {
                    PrintSetEventArgs args = new PrintSetEventArgs();
                    this.printer_0.GetPrinterArgs(false);
                    args.Offset = new PointF(this.printer_0.RealPrinterArgs.Left, this.printer_0.RealPrinterArgs.Top);
                    args.IsTaoDa = this.r1.Checked;
                    this.OnPrint(this, args);
                }
                base.Close();
            }
            catch (Exception exception)
            {
                this.loger.Error("[but_Preview_Click函数]" + exception.Message);
                this.loger.Error(exception.ToString());
            }
        }

        private void comboBox_DefaultPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    if ((this.comboBox_DefaultPrinter.SelectedItem != null) && (pageSetupDialog != null))
                    {
                        this.method_1();
                    }
                    else
                    {
                        this.loger.Info("[设置为默认打印机失败]");
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[打印机设置异常]" + exception.ToString());
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.comboBox_DefaultPrinter = new ComboBox();
            this.printerDefault = new GroupBox();
            this.groupBox2 = new AisinoGRP();
            this.r2 = new AisinoRDO();
            this.r1 = new AisinoRDO();
            this.but_close = new AisinoBTN();
            this.but_print = new AisinoBTN();
            this.but_Preview = new AisinoBTN();
            this.groupBox1 = new AisinoGRP();
            this.txt_left = new AisinoTXT();
            this.txt_top = new AisinoTXT();
            this.label4 = new AisinoLBL();
            this.label3 = new AisinoLBL();
            this.label2 = new AisinoLBL();
            this.label1 = new AisinoLBL();
            this.printerDefault.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.comboBox_DefaultPrinter.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox_DefaultPrinter.FormattingEnabled = true;
            this.comboBox_DefaultPrinter.Location = new Point(14, 0x16);
            this.comboBox_DefaultPrinter.Name = "comboBox_DefaultPrinter";
            this.comboBox_DefaultPrinter.Size = new Size(0x103, 20);
            this.comboBox_DefaultPrinter.TabIndex = 11;
            this.printerDefault.Controls.Add(this.comboBox_DefaultPrinter);
            this.printerDefault.Location = new Point(15, 11);
            this.printerDefault.Name = "printerDefault";
            this.printerDefault.Size = new Size(0x148, 0x36);
            this.printerDefault.TabIndex = 13;
            this.printerDefault.TabStop = false;
            this.printerDefault.Text = "设置默认打印机";
            this.groupBox2.Controls.Add(this.r2);
            this.groupBox2.Controls.Add(this.r1);
            this.groupBox2.Location = new Point(190, 0x47);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x99, 0x56);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "套打";
            this.groupBox2.Visible = false;
            this.r2.AutoSize = true;
            this.r2.Location = new Point(0x21, 0x31);
            this.r2.Name = "r2";
            this.r2.Size = new Size(0x2f, 0x10);
            this.r2.TabIndex = 1;
            this.r2.Text = "套打";
            this.r2.UseVisualStyleBackColor = true;
            this.r2.CheckedChanged += new EventHandler(this.r1_CheckedChanged);
            this.r1.AutoSize = true;
            this.r1.Checked = true;
            this.r1.Location = new Point(0x21, 0x15);
            this.r1.Name = "r1";
            this.r1.Size = new Size(0x2f, 0x10);
            this.r1.TabIndex = 0;
            this.r1.TabStop = true;
            this.r1.Text = "全打";
            this.r1.UseVisualStyleBackColor = true;
            this.r1.CheckedChanged += new EventHandler(this.r1_CheckedChanged);
            this.but_close.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.but_close.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.but_close.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.but_close.Font = new Font("微软雅黑", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.but_close.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.but_close.Location = new Point(0x10a, 0xaf);
            this.but_close.Name = "but_close";
            this.but_close.Size = new Size(0x4b, 30);
            this.but_close.TabIndex = 8;
            this.but_close.Text = "不打印";
            this.but_close.UseVisualStyleBackColor = true;
            this.but_close.Click += new EventHandler(this.but_close_Click);
            this.but_print.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.but_print.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.but_print.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.but_print.Font = new Font("微软雅黑", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.but_print.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.but_print.Location = new Point(0xb0, 0xaf);
            this.but_print.Name = "but_print";
            this.but_print.Size = new Size(0x4b, 30);
            this.but_print.TabIndex = 7;
            this.but_print.Text = "打印";
            this.but_print.UseVisualStyleBackColor = true;
            this.but_print.Click += new EventHandler(this.but_print_Click);
            this.but_Preview.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.but_Preview.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.but_Preview.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.but_Preview.Font = new Font("微软雅黑", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.but_Preview.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.but_Preview.Location = new Point(80, 0xaf);
            this.but_Preview.Name = "but_Preview";
            this.but_Preview.Size = new Size(0x4b, 30);
            this.but_Preview.TabIndex = 6;
            this.but_Preview.Text = "预览";
            this.but_Preview.UseVisualStyleBackColor = true;
            this.but_Preview.Click += new EventHandler(this.but_Preview_Click);
            this.groupBox1.Controls.Add(this.txt_left);
            this.groupBox1.Controls.Add(this.txt_top);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(15, 0x47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xa9, 0x56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "边距调整";
            this.txt_left.Location = new Point(0x43, 0x2d);
            this.txt_left.Name = "txt_left";
            this.txt_left.Size = new Size(60, 0x15);
            this.txt_left.TabIndex = 11;
            this.txt_left.Text = "0";
            this.txt_top.Location = new Point(0x43, 0x12);
            this.txt_top.Name = "txt_top";
            this.txt_top.Size = new Size(60, 0x15);
            this.txt_top.TabIndex = 10;
            this.txt_top.Text = "0";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x85, 0x34);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "毫米";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(9, 0x35);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x41, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "向右调整：";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x85, 0x15);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "毫米";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(7, 0x15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "向下调整：";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x162, 0xdb);
            base.ControlBox = false;
            base.Controls.Add(this.printerDefault);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.but_close);
            base.Controls.Add(this.but_print);
            base.Controls.Add(this.but_Preview);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "PrintSetUp";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "打印";
            base.TopMost = true;
            this.printerDefault.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void method_0(string string_0)
        {
            this.loger.Debug("[发票打印]输入打印机名称：" + string_0);
            this.list_0.Clear();
            this.comboBox_DefaultPrinter.Items.Clear();
            bool flag = false;
            foreach (string str in PrinterSettings.InstalledPrinters)
            {
                this.comboBox_DefaultPrinter.Items.Add(str);
                if (string_0 == str)
                {
                    this.comboBox_DefaultPrinter.SelectedIndex = this.comboBox_DefaultPrinter.Items.IndexOf(string_0);
                    flag = true;
                }
            }
            if (!flag)
            {
                this.loger.Debug("[发票打印]默认打印机名称：" + this.printDocument_0.PrinterSettings.PrinterName);
                if (this.comboBox_DefaultPrinter.Items.IndexOf(this.printDocument_0.PrinterSettings.PrinterName) != -1)
                {
                    this.comboBox_DefaultPrinter.SelectedIndex = this.comboBox_DefaultPrinter.Items.IndexOf(this.printDocument_0.PrinterSettings.PrinterName);
                }
                else
                {
                    this.comboBox_DefaultPrinter.SelectedIndex = 0;
                }
            }
            if (this.comboBox_DefaultPrinter.SelectedItem != null)
            {
                this.loger.Debug("[发票打印]应该设置打印机名称：" + this.comboBox_DefaultPrinter.SelectedItem.ToString());
            }
            else if (this.comboBox_DefaultPrinter.Items.Count > 0)
            {
                this.comboBox_DefaultPrinter.SelectedIndex = 0;
                this.loger.Debug("[发票打印]设置打印机下标为0");
            }
            this.comboBox_DefaultPrinter.SelectedIndexChanged += new EventHandler(this.comboBox_DefaultPrinter_SelectedIndexChanged);
        }

        private void method_1()
        {
            string str3;
            if ((pageSetupDialog == null) || (this.comboBox_DefaultPrinter.SelectedItem == null))
            {
                this.loger.Error("SetCurPrinter函数：当前pageSetupDialog为空");
                return;
            }
            pageSetupDialog.Document.PrinterSettings = new PrinterSettings();
            pageSetupDialog.Document.PrinterSettings.PrinterName = this.comboBox_DefaultPrinter.SelectedItem.ToString();
            PrinterSettings.PaperSizeCollection paperSizes = pageSetupDialog.Document.PrinterSettings.PaperSizes;
            this.CurrentPrinterName = pageSetupDialog.Document.PrinterSettings.PrinterName;
            if (((this.object_0 != null) && (this.object_0.Length > 0)) && ((str3 = this.object_0[0].ToString()) != null))
            {
                if ((str3 == "c") || (str3 == "s"))
                {
                    pageSetupDialog.Document.PrinterSettings.DefaultPageSettings.PaperSize = this.paperSize_0;
                }
                else if ((str3 == "j") || (str3 == "f"))
                {
                    pageSetupDialog.Document.PrinterSettings.DefaultPageSettings.PaperSize = this.paperSize_1;
                }
                else if ((str3 == "q") && (this.object_0.Length >= 5))
                {
                    int num = Common.ObjectToInt(this.object_0[4].ToString());
                    string str2 = "NEW76mmX177mm";
                    Dictionary<string, int> jsPrintTemplate = ToolUtil.GetJsPrintTemplate();
                    if (jsPrintTemplate.Count > 0)
                    {
                        foreach (string str in jsPrintTemplate.Keys)
                        {
                            if (jsPrintTemplate[str] == num)
                            {
                                str2 = str;
                            }
                        }
                        if (str2.IndexOf("76mmX177mm") != -1)
                        {
                            pageSetupDialog.Document.PrinterSettings.DefaultPageSettings.PaperSize = this.paperSize_2;
                        }
                        else if (str2.IndexOf("76mmX152mm") != -1)
                        {
                            pageSetupDialog.Document.PrinterSettings.DefaultPageSettings.PaperSize = this.paperSize_3;
                        }
                        else if (str2.IndexOf("76mmX127mm") != -1)
                        {
                            pageSetupDialog.Document.PrinterSettings.DefaultPageSettings.PaperSize = this.paperSize_4;
                        }
                        else if (str2.IndexOf("57mmX177mm") != -1)
                        {
                            pageSetupDialog.Document.PrinterSettings.DefaultPageSettings.PaperSize = this.paperSize_5;
                        }
                        else if (str2.IndexOf("57mmX152mm") != -1)
                        {
                            pageSetupDialog.Document.PrinterSettings.DefaultPageSettings.PaperSize = this.paperSize_6;
                        }
                        else if (str2.IndexOf("57mmX127mm") != -1)
                        {
                            pageSetupDialog.Document.PrinterSettings.DefaultPageSettings.PaperSize = this.paperSize_7;
                        }
                        else
                        {
                            pageSetupDialog.Document.PrinterSettings.DefaultPageSettings.PaperSize = this.paperSize_2;
                        }
                    }
                    else
                    {
                        pageSetupDialog.Document.PrinterSettings.DefaultPageSettings.PaperSize = this.paperSize_2;
                    }
                }
            }
            this.bool_0 = false;
            IEnumerator enumerator2 = pageSetupDialog.Document.PrinterSettings.PrinterResolutions.GetEnumerator();
            {
                PrinterResolution current;
                while (enumerator2.MoveNext())
                {
                    current = (PrinterResolution) enumerator2.Current;
                    if ((current.X == 180) && (current.Y == 180))
                    {
                        goto Label_038B;
                    }
                }
                goto Label_03BF;
            Label_038B:
                pageSetupDialog.Document.DefaultPageSettings.PrinterResolution = current;
                this.bool_0 = true;
            }
        Label_03BF:
            if ((this.printer_0 != null) && !IPrint.IsZjFlag)
            {
                PrinterEventArgs args = new PrinterEventArgs {
                    Name = ((this.object_0 == null) || (this.object_0.Length <= 0)) ? "user" : this.object_0[0].ToString(),
                    Left = Common.ObjectToFloat(this.txt_left.Text),
                    Top = Common.ObjectToFloat(this.txt_top.Text),
                    System = "1",
                    PageLenght = 0,
                    PrinterName = this.comboBox_DefaultPrinter.SelectedItem.ToString(),
                    IsQuanDa = this.r1.Checked
                };
                this.printer_0.SaveUserPrinterEdge(args);
            }
        }

        private void r1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.printer_0 != null)
                {
                    PrinterEventArgs args = new PrinterEventArgs {
                        Name = ((this.object_0 == null) || (this.object_0.Length <= 0)) ? "user" : this.object_0[0].ToString(),
                        Left = Common.ObjectToFloat(this.txt_left.Text),
                        Top = Common.ObjectToFloat(this.txt_top.Text),
                        System = "1",
                        PageLenght = 0,
                        PrinterName = this.comboBox_DefaultPrinter.SelectedItem.ToString(),
                        IsQuanDa = this.r1.Checked
                    };
                    this.printer_0.SaveUserPrinterEdge(args);
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
            }
        }

        private void txt_left_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((!char.IsDigit(e.KeyChar) && (e.KeyChar != '\r')) && ((e.KeyChar != '\b') && (e.KeyChar != '+'))) && ((e.KeyChar != '-') && (e.KeyChar != '.')))
            {
                e.Handled = true;
            }
        }

        private void txt_left_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.printer_0 != null)
                {
                    PrinterEventArgs args = new PrinterEventArgs {
                        Name = ((this.object_0 == null) || (this.object_0.Length <= 0)) ? "user" : this.object_0[0].ToString(),
                        Left = Common.ObjectToFloat(this.txt_left.Text),
                        Top = Common.ObjectToFloat(this.txt_top.Text),
                        System = "1",
                        PageLenght = 0,
                        PrinterName = this.comboBox_DefaultPrinter.SelectedItem.ToString(),
                        IsQuanDa = this.r1.Checked
                    };
                    this.printer_0.SaveUserPrinterEdge(args);
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[txt_left_TextChanged函数异常]" + exception.Message);
                this.loger.Error(exception.ToString());
            }
        }

        private void txt_top_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.printer_0 != null)
                {
                    PrinterEventArgs args = new PrinterEventArgs {
                        Name = ((this.object_0 == null) || (this.object_0.Length <= 0)) ? "user" : this.object_0[0].ToString(),
                        Left = Common.ObjectToFloat(this.txt_left.Text),
                        Top = Common.ObjectToFloat(this.txt_top.Text),
                        System = "1",
                        PageLenght = 0,
                        PrinterName = this.comboBox_DefaultPrinter.SelectedItem.ToString(),
                        IsQuanDa = this.r1.Checked
                    };
                    this.printer_0.SaveUserPrinterEdge(args);
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[txt_top_TextChanged函数异常]" + exception.Message);
                this.loger.Error(exception.ToString());
            }
        }

        public bool IsGroupTDbutton
        {
            get
            {
                return this.groupBox2.Visible;
            }
            set
            {
                this.groupBox2.Visible = value;
            }
        }

        public bool IsSupportHZFW
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        public delegate void PrintSet(object sender, PrintSetEventArgs e);
    }
}

