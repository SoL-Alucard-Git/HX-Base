namespace InvAutomation
{
    using log4net;
    using Microsoft.Win32;
    using RegMakeFile;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading;
    using System.Timers;
    using System.Web;
    using System.Windows.Forms;
    using System.Xml;
    using Update.Model;
    using Update.Tool;

    public class DlgDown : Form
    {
        protected bool _IsCloseVisble;
        protected bool _IsMoveFlag;
        private bool bool_0;
        private Button button1;
        private ContextMenuStrip contextMenuStrip1;
        private Dictionary<string, string> dictionary_0;
        private IContainer icontainer_0;
        private static ILog ilog_0;
        private ImageList imageList_0;
        private static int int_0;
        private int int_1;
        private int int_2;
        private int int_3;
        private int int_4;
        private int int_5;
        private Label label1;
        private Label label2;
        private Label lblMsg;
        private Label lblTitle;
        private List<DownloadInfo> list_0;
        private List<DownloadInfo> list_1;
        private NotifyIcon notifyIcon_0;
        private Panel panel1;
        private Panel panel2;
        private PictureBox pictureBox1;
        private Point point_0;
        public static string startFilePath;
        private string string_0;
        private string string_1;
        private string string_2;
        private string string_3;
        private string string_4;
        private string string_5;
        private TextBox textBox1;
        private Thread thread_0;
        private ToolStripMenuItem ToolStripMenuItemStop;

        static DlgDown()
        {
            
            int_0 = 0;
            ilog_0 = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            startFilePath = ConfigurationManager.AppSettings["StartFileName"];
        }

        public DlgDown()
        {
            
            this._IsMoveFlag = true;
            this._IsCloseVisble = true;
            this.string_1 = "";
            this.int_2 = 3;
            this.InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.lblTitle.MouseDown += new MouseEventHandler(this.lblTitle_MouseDown);
            this.lblTitle.MouseMove += new MouseEventHandler(this.lblTitle_MouseMove);
            this.lblTitle.MouseUp += new MouseEventHandler(this.lblTitle_MouseUp);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.method_45();
        }

        public void CheckStartFile()
        {
            if ((startFilePath == null) || (startFilePath == ""))
            {
                startFilePath = ConfigurationManager.AppSettings["StartFileName"];
                if ((startFilePath == null) || (startFilePath == ""))
                {
                    return;
                }
            }
            if (this.method_14(this.string_2))
            {
                startFilePath = Path.Combine(this.string_2, startFilePath);
                if (!System.IO.File.Exists(startFilePath))
                {
                    startFilePath = "";
                }
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

        private void DlgDown_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void DlgDown_Load(object sender, EventArgs e)
        {
            try
            {
                this.string_5 = Environment.SystemDirectory;
                this.string_2 = this.method_38();
                smethod_0("localFilePath=" + this.string_2);
                if ((this.string_2 == null) || (this.string_2 == ""))
                {
                    this.string_2 = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName + @"\";
                }
                if (!this.method_14(this.string_2))
                {
                    ilog_0.Info("软件路径不存在");
                    Application.Exit();
                }
                int_0 = 0;
                if (System.IO.File.Exists(Path.Combine(this.string_2, "logauto")))
                {
                    int_0 = 1;
                }
                this.string_3 = Path.Combine(this.string_2, "UpdateFiles");
                this.string_4 = Path.Combine(this.string_2, "BackupFiles");
                if (!this.method_14(this.string_4))
                {
                    ilog_0.Info("创建备份路径失败");
                    Application.Exit();
                }
                if (!this.method_14(this.string_3))
                {
                    ilog_0.Info("创建升级包存放路径失败");
                    Application.Exit();
                }
                startFilePath = ConfigurationManager.AppSettings["StartFileName"];
                this.CheckStartFile();
                RegisterManager.SetupRegFile();
                this.dictionary_0 = this.method_26();
                if ((this.dictionary_0 != null) && (this.dictionary_0.Count > 0))
                {
                    foreach (KeyValuePair<string, string> pair in this.dictionary_0)
                    {
                        smethod_0("cnbbh:" + (pair.Key + " | " + pair.Value));
                    }
                }
                this.notifyIcon_0.Visible = false;
                this.lblMsg.Text = "正在连接";
                this.textBox1.Visible = false;
                this.label1.Visible = false;
                this.label2.Visible = false;
                this.button1.Visible = false;
                this.string_0 = ConfigurationManager.AppSettings["MSURL"];
                this.string_1 = this.method_28();
                smethod_0("bserverUrl:" + this.string_1);
                if (this.string_1 == "MSURL")
                {
                    Application.Exit();
                }
                else
                {
                    this.string_1 = (this.string_1 == "") ? this.string_0 : this.string_1;
                    Thread thread = new Thread(new ThreadStart(this.method_1)) {
                        IsBackground = true
                    };
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                }
            }
            catch (Exception exception)
            {
                ilog_0.Debug("AutoUpdate：" + exception.Message);
            }
        }

        [DllImport("JSDiskTools.dll", CallingConvention=CallingConvention.Cdecl)]
        private static extern int DownloadAppJ(byte[] byte_0, byte[] byte_1, int int_6, byte byte_2 = 3, byte[] byte_3 = null);
        [DllImport("ReadAreaCode.dll", CallingConvention=CallingConvention.Cdecl)]
        private static extern int GetVerNo(byte[] byte_0, byte[] byte_1);
        [DllImport("ReadAreaCode.dll", CallingConvention=CallingConvention.Cdecl)]
        private static extern int GetVerNo_T(byte[] byte_0, byte[] byte_1, string string_6, int int_6);
        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(DlgDown));
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.pictureBox1 = new PictureBox();
            this.lblMsg = new Label();
            this.panel2 = new Panel();
            this.textBox1 = new TextBox();
            this.label2 = new Label();
            this.label1 = new Label();
            this.button1 = new Button();
            this.panel1 = new Panel();
            this.lblTitle = new Label();
            this.notifyIcon_0 = new NotifyIcon(this.icontainer_0);
            this.contextMenuStrip1 = new ContextMenuStrip(this.icontainer_0);
            this.ToolStripMenuItemStop = new ToolStripMenuItem();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.imageList_0.ImageStream = (ImageListStreamer) manager.GetObject("imageList.ImageStream");
            this.imageList_0.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_0.Images.SetKeyName(0, "close.bmp");
            this.imageList_0.Images.SetKeyName(1, "close2.bmp");
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = (Image) manager.GetObject("pictureBox1.Image");
            this.pictureBox1.Location = new Point(13, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x30, 0x30);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            this.lblMsg.Dock = DockStyle.Fill;
            this.lblMsg.Font = new Font("微软雅黑", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lblMsg.ForeColor = System.Drawing.Color.Black;
            this.lblMsg.Location = new Point(0, 0);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new Size(0x182, 0x69);
            this.lblMsg.TabIndex = 0x10;
            this.lblMsg.TextAlign = ContentAlignment.MiddleCenter;
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.lblMsg);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(6, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x182, 0x69);
            this.panel2.TabIndex = 1;
            this.textBox1.BackColor = SystemColors.ActiveCaptionText;
            this.textBox1.BorderStyle = BorderStyle.None;
            this.textBox1.Cursor = Cursors.Arrow;
            this.textBox1.Location = new Point(0x1d, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(250, 0x17);
            this.textBox1.TabIndex = 20;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x117, 0x58);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x29, 12);
            this.label2.TabIndex = 0x13;
            this.label2.Text = "label2";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x40, 0x58);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x59, 12);
            this.label1.TabIndex = 0x11;
            this.label1.Text = "正在检测新版本";
            this.button1.FlatStyle = FlatStyle.Popup;
            this.button1.Location = new Point(0x134, 2);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 0x12;
            this.button1.Text = "隐藏";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.panel1.BackColor = System.Drawing.Color.FromArgb(0x33, 0x57, 0x74);
            this.panel1.BorderStyle = BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0x1b);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new Padding(6, 0, 6, 6);
            this.panel1.Size = new Size(400, 0x71);
            this.panel1.TabIndex = 0x12;
            this.lblTitle.AllowDrop = true;
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(0x33, 0x57, 0x74);
            this.lblTitle.Dock = DockStyle.Top;
            this.lblTitle.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(400, 0x1b);
            this.lblTitle.TabIndex = 0x13;
            this.lblTitle.Text = "升级提示";
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.notifyIcon_0.Icon = (Icon) manager.GetObject("notifyIcon1.Icon");
            this.notifyIcon_0.Text = "自动更新";
            this.notifyIcon_0.MouseClick += new MouseEventHandler(this.notifyIcon_0_MouseClick);
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.ToolStripMenuItemStop });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0x77, 0x1a);
            this.ToolStripMenuItemStop.Name = "ToolStripMenuItemStop";
            this.ToolStripMenuItemStop.Size = new Size(0x76, 0x16);
            this.ToolStripMenuItemStop.Text = "停止下载";
            this.ToolStripMenuItemStop.Click += new EventHandler(this.ToolStripMenuItemStop_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(400, 140);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.lblTitle);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "DlgDown";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "自动更新";
            base.WindowState = FormWindowState.Minimized;
            base.FormClosing += new FormClosingEventHandler(this.DlgDown_FormClosing);
            base.Load += new EventHandler(this.DlgDown_Load);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (this._IsMoveFlag && (e.Button == MouseButtons.Left))
            {
                this.point_0 = new Point(-e.X, -e.Y);
                this.bool_0 = true;
            }
        }

        private void lblTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (this._IsMoveFlag && this.bool_0)
            {
                Point mousePosition = Control.MousePosition;
                mousePosition.Offset(this.point_0.X, this.point_0.Y);
                base.Location = mousePosition;
            }
        }

        private void lblTitle_MouseUp(object sender, MouseEventArgs e)
        {
            if (this._IsMoveFlag && this.bool_0)
            {
                this.bool_0 = false;
            }
        }

        private string method_0()
        {
            string str = string.Empty;
            try
            {
                using (FileStream stream = new FileStream(this.string_2 + @"\Bin\verflag.dat", FileMode.Open))
                {
                    StreamReader reader = new StreamReader(stream);
                    byte[] buffer = Convert.FromBase64String(reader.ReadToEnd());
                    str = Encoding.GetEncoding("GBK").GetString(smethod_1("<RSAKeyValue><Modulus>ghCLpZiwHiZIDYve7yGZusVydX406Qd4JqIYFsl/wUK/y1xjEsT3zQvCfpwASRwpHg0bi8XZ4EILPJt4NXVXftRlD7ZlG17sAIDp3OUSSGxI5hkXB7BJPrw2wbqs/6hfZr6vmYnwpDb8IAZmt8xlJucIUWjEVuu4NnOx1/iiqlM=</Modulus><Exponent>AQAB</Exponent><P>tvY6Rtiwahg6keaITiLw42GCjXLK3BDjtHfa2uMSndK5qBhQQ+7bMM11H/7spU+25SgXBdSHVWy/y8KFvT5ISw==</P><Q>tfx+BCB6dw/4ShgTqbxX3X8xoRapWr4XMvVrdLnc/txHpHhn9pNjtM2Xb3GVlltzCEQkzBcXnk0SeBYjIR3xGQ==</Q><DP>T6PIZDRIPjZDsGSHqnNdJay5NjbkhHw5kcGmGydCYD5sn/XNYnSjJpAYTpAZlC+prgAQXXJQYmfO6LPIoUJuFw==</DP><DQ>n891ngwjXxDgGbjg84oYosLCg1KSL8SEPNS1o1BgWFJ6e1zc9vRhd3GfTVcyZFI0RwsIQUz6CaJm2JugB8HyaQ==</DQ><InverseQ>oxe42fx2yLATcCG4lbQ5f8Qo8c8ACkT4NxqYl3GXdrojBorBzbvht2+KHq2bJorWtcPNnsmumhV6BIV7zCW0kw==</InverseQ><D>E92nFsH9lH1QYBFPGcNOEcL6uotuVXF4np3/g+t/AevKE6umzkUbfEwhhukY+hG9DgP+gxjTMHel87njYHbtyA+23TdhIzhyYcSg0ifotDhgD8+9lBrn29hyddFigLDoXnZR1SQmvn7xjuGKtZ/HaKZPetSxgVf1mSPdzl37CGE=</D></RSAKeyValue>", buffer));
                    reader.Close();
                }
            }
            catch (FileNotFoundException)
            {
                str = "";
            }
            catch (Exception exception)
            {
                ilog_0.Error(exception.Message);
            }
            return str;
        }

        private void method_1()
        {
            int num = 0;
            num = this.method_8();
            if (!this.method_33(startFilePath))
            {
                this.StartProcess(startFilePath, false);
                smethod_0("已启动:" + startFilePath);
            }
            if (num == 2)
            {
                string path = Path.Combine(this.string_2, @"Automation\bin\UpdateInvAutomation.exe");
                if (System.IO.File.Exists(path))
                {
                    this.StartProcess(path, false);
                }
                Application.Exit();
            }
            else
            {
                List<DownloadInfo> latestList = this.method_27();
                if ((latestList != null) && (latestList.Count > 0))
                {
                    for (int i = 0; i < latestList.Count; i++)
                    {
                        smethod_0("latestVserList:" + latestList[i].SoftVer.SoftName + latestList[i].SoftVer.Version);
                    }
                }
                else
                {
                    smethod_0("latestVserList: null ");
                    string str2 = Path.Combine(this.string_3, @"新版开票\temp\", "UpdateDownloadFlagList.xml");
                    XmlDocument document = new XmlDocument();
                    if (System.IO.File.Exists(str2))
                    {
                        document.Load(str2);
                        XmlElement element = (XmlElement) document.SelectSingleNode("Base");
                        foreach (XmlNode node in document.SelectNodes("/Base/DownloadListInfo"))
                        {
                            if (node.LastChild.InnerText == "")
                            {
                                DownloadInfo info = XmlTools.XmlToDownloadInfo(node);
                                if (this.method_23(info) != "")
                                {
                                    element.RemoveChild(node);
                                    document.Save(str2);
                                }
                            }
                        }
                    }
                }
                if ((latestList != null) && (latestList.Count > 0))
                {
                    new Dictionary<string, DownloadInfo>();
                    List<DownloadInfo> list2 = new List<DownloadInfo>();
                    for (int j = 0; j < latestList.Count; j++)
                    {
                        if (this.dictionary_0.ContainsKey(latestList[j].SoftVer.SoftName) && (string.Compare(this.dictionary_0[latestList[j].SoftVer.SoftName], latestList[j].SoftVer.Version, true) < 0))
                        {
                            list2.Add(latestList[j]);
                        }
                    }
                    latestList = list2;
                    this.int_1 = this.method_25(latestList, 0);
                    if (this.int_1 == 0)
                    {
                        this.method_24(latestList);
                    }
                    this.method_45();
                    try
                    {
                        System.Timers.Timer timer = new System.Timers.Timer(2000.0);
                        timer.Elapsed += new ElapsedEventHandler(this.method_48);
                        timer.Enabled = true;
                        timer.AutoReset = true;
                        timer.Start();
                        this.method_5();
                        return;
                    }
                    catch (Exception exception)
                    {
                        ilog_0.Info("启动更新线程出现异常");
                        ilog_0.Info(exception.Message);
                        if (this.notifyIcon_0.Visible)
                        {
                            this.notifyIcon_0.Visible = false;
                        }
                        return;
                    }
                }
                Application.Exit();
            }
        }

        private int method_10(string string_6, Dictionary<string, string> cvbnbmhbxws)
        {
            string path = Path.Combine(this.string_3, string_6 + @"\temp\UpdateFileList.xml");
            if (!System.IO.File.Exists(path))
            {
                return -1;
            }
            base.WindowState = FormWindowState.Normal;
            base.Show();
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);
                XmlElement documentElement = document.DocumentElement;
                DownloadInfo info = XmlTools.XmlToFileListInfo(document.InnerXml);
                if (cvbnbmhbxws.ContainsKey(info.SoftVer.SoftName) && (string.Compare(cvbnbmhbxws[info.SoftVer.SoftName], info.SoftVer.Version, true) < 0))
                {
                    List<DownloadInfo> latestList = new List<DownloadInfo> {
                        info
                    };
                    this.method_24(latestList);
                    if ((this.list_0 == null) || (this.list_0[0].FileList.Count > 0))
                    {
                        ilog_0.Info("downInfoList[0].FileList.Count = 0" + string_6);
                        return 7;
                    }
                    this.method_7(string_6);
                    if ((documentElement.SelectSingleNode("Force").InnerText.Trim() == "1") || (MessageBox.Show("检测到" + string_6 + "有新版本，是否进行升级？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        this.lblMsg.Text = "正在升级" + string_6 + "...";
                        if (!this.method_22(true, string_6))
                        {
                            ilog_0.Info("true方式执行小exe失败" + string_6);
                            return 3;
                        }
                        if (!"金税盘底层".Equals(string_6))
                        {
                            if (!this.method_19(string_6))
                            {
                                ilog_0.Info("备份失败" + string_6);
                                return 4;
                            }
                            if (!this.method_20(string_6))
                            {
                                ilog_0.Info("更新失败：" + string_6);
                                return 5;
                            }
                        }
                        else if (this.method_12("") != 0)
                        {
                            return 8;
                        }
                        if (!this.method_22(false, string_6))
                        {
                            ilog_0.Info("false方式执行小exe失败" + string_6);
                            return 6;
                        }
                        document.Save(path);
                        return 0;
                    }
                }
                else
                {
                    ilog_0.Info("当前版本：" + cvbnbmhbxws[info.SoftVer.SoftName] + " Upload存版本：" + info.SoftVer.Version);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    return 11;
                }
            }
            catch (Exception exception)
            {
                ilog_0.Info("ExecUpdateApp try" + exception.ToString());
                return 6;
            }
            return 9;
        }

        private int method_11(string string_6, int int_6)
        {
            DownloadInfo info;
            string str3;
            string str = "";
            int num = 0;
            if ((int_6 <= 0) || (int_6 >= 10))
            {
                str3 = Path.Combine(this.string_3, string_6 + @"\temp\", "UpdateFileList.xml");
                if (!System.IO.File.Exists(str3) || str.Contains(string_6))
                {
                    goto Label_0293;
                }
                XmlDocument document2 = new XmlDocument();
                document2.Load(str3);
                info = XmlTools.XmlToFileListInfo(document2.InnerXml);
                if (!this.dictionary_0.ContainsKey(info.SoftVer.SoftName) || (string.Compare(this.dictionary_0[info.SoftVer.SoftName], info.SoftVer.Version, true) >= 0))
                {
                    goto Label_0293;
                }
                if (string_6 == "新版开票")
                {
                    this.method_41(info.SoftVer.Version);
                }
                else if (!string_6.Equals("金税盘底层"))
                {
                    using (List<SetupFile>.Enumerator enumerator = IniFileUtil.ReadSetupConfig(Path.Combine(this.string_2, @"Automation\Config.ini")).GetEnumerator())
                    {
                        SetupFile current;
                        while (enumerator.MoveNext())
                        {
                            current = enumerator.Current;
                            if (current.Name.Equals(string_6))
                            {
                                goto Label_0193;
                            }
                        }
                        goto Label_01CE;
                    Label_0193:
                        current.Ver = info.SoftVer.Version;
                        IniFileUtil.WriteSetupConfig(current, Path.Combine(this.string_2, @"Automation\Config.ini"));
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.list_0.Count; i++)
                {
                    if (this.list_0[i].SoftVer.SoftName == string_6)
                    {
                        DownloadInfo info2 = this.list_0[i];
                        info2.Xzbz = 3;
                        this.method_23(info2);
                        break;
                    }
                }
                ilog_0.Error("更新失败，恢复");
                this.method_18(string_6);
                goto Label_0293;
            }
        Label_01CE:
            info.Xzbz = 2;
            if (this.method_23(info) == "")
            {
                XmlElement element;
                XmlDocument doc = new XmlDocument();
                string path = Path.Combine(this.string_3, string_6 + @"\temp\", "UpdateDownloadFlagList.xml");
                if (!System.IO.File.Exists(path))
                {
                    XmlDeclaration newChild = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    doc.PreserveWhitespace = false;
                    doc.AppendChild(newChild);
                    element = doc.CreateElement("Base");
                    doc.AppendChild(element);
                }
                else
                {
                    doc.Load(path);
                    element = (XmlElement) doc.SelectSingleNode("Base");
                }
                doc.LoadXml(XmlTools.DownloadListInfoToXml(info, doc, element));
                doc.Save(path);
            }
            System.IO.File.Delete(str3);
            num = 2;
        Label_0293:
            if (num == 0)
            {
                return (100 + int_6);
            }
            return num;
        }

        private int method_12(string string_6)
        {
            try
            {
                foreach (SoftFileInfo info in this.list_1[0].FileList)
                {
                    string_6 = info.RelativePath.Substring(info.RelativePath.LastIndexOf(@"\") + 1);
                    string destFileName = Path.Combine(this.string_2, "Automation", string_6);
                    string str2 = Path.Combine(this.string_2, @"UpdateFiles\金税盘底层", info.RelativePath);
                    try
                    {
                        if (System.IO.File.Exists(str2))
                        {
                            System.IO.File.Copy(str2, destFileName, true);
                        }
                    }
                    catch (Exception exception)
                    {
                        ilog_0.Info("金税盘升级拷贝覆盖目录中的文件出现异常" + exception.Message);
                        return 4;
                    }
                }
                byte[] buffer2 = new byte[1];
                string[] strArray = Directory.GetFiles(Path.Combine(this.string_2, "Automation"), "*.bin", SearchOption.TopDirectoryOnly);
                string path = "";
                if (strArray.Length > 0)
                {
                    path = strArray[0];
                    byte[] buffer = new byte[0x40000];
                    int count = 0;
                    using (FileStream stream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read))
                    {
                        count = (int) stream.Length;
                        stream.Read(buffer, 0, count);
                    }
                    int num4 = DownloadAppJ(buffer2, buffer, count, 0x83, null);
                    if (num4 != 0)
                    {
                        string str4 = "金税盘升级失败";
                        switch (num4)
                        {
                            case 1:
                                str4 = "没有找到设备，请插紧金税盘";
                                break;

                            case 2:
                                str4 = "找到多个设备，请插紧一个金税盘\r\n拔掉多插入的金税盘";
                                break;
                        }
                        MessageBox.Show(str4 + "\r\n错误号：" + num4, "金税盘升级失败", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        ilog_0.Info("金税盘升级失败 错误号：" + num4);
                        return 1;
                    }
                    System.IO.File.Delete(path);
                    return 0;
                }
                ilog_0.Info("金税盘升级拷贝覆盖Automation目录中的文件没有bin文件");
                return 5;
            }
            catch (Exception exception2)
            {
                ilog_0.Debug("金税盘升级失败异常 " + exception2.Message);
                return 2;
            }
        }

        private string method_13(string string_6)
        {
            string str3 = string.Empty;
            string str = "";
            try
            {
                string path = Path.Combine(this.string_3, string_6 + @"\temp\", "版本说明.txt");
                if (!System.IO.File.Exists(path))
                {
                    return str;
                }
                using (StreamReader reader = new StreamReader(path, Encoding.Default))
                {
                    str = reader.ReadToEnd();
                }
            }
            catch (Exception exception)
            {
                ilog_0.Info(exception.ToString());
                str3 = "";
            }
            return str3;
        }

        private bool method_14(string string_6)
        {
            if ((string_6 == null) || (string_6 == ""))
            {
                return false;
            }
            try
            {
                if (System.IO.File.Exists(string_6))
                {
                    return false;
                }
                if (!Directory.Exists(string_6))
                {
                    Directory.CreateDirectory(string_6);
                }
                return true;
            }
            catch (Exception exception)
            {
                ilog_0.Info("CreatDir" + exception.Message);
                return false;
            }
        }

        private string method_15(double double_0)
        {
            string[] strArray2 = new string[] { "B", "KB", "MB", "GB" };
            double num = 1024.0;
            int index = 0;
            while (double_0 >= num)
            {
                double_0 /= num;
                index++;
            }
            return (Math.Round(double_0) + strArray2[index]);
        }

        private int method_16(DownloadInfo downloadInfo_0, int int_6)
        {
            string url = this.string_1 + "/HttpHandler/DownloadHandler.ashx";
            string str2 = "action=DownLoadFileSynchronousBi&FilePath=";
            str2 = (str2 + HttpUtility.UrlEncode(HttpUtility.HtmlEncode(downloadInfo_0.SoftVer.FilePath), Encoding.UTF8) + "&TaxCode=") + HttpUtility.UrlEncode(HttpUtility.HtmlEncode(this.method_35()), Encoding.UTF8) + "&FileName=";
            string contentType = "application/x-www-form-urlencoded";
            int count = downloadInfo_0.FileList.Count;
            int num = 0;
            int num2 = 0;
            for (int i = 0; i < downloadInfo_0.FileList.Count; i++)
            {
                SoftFileInfo info = downloadInfo_0.FileList[i];
                if (System.IO.File.Exists(Path.Combine(this.string_3, downloadInfo_0.SoftVer.SoftName + @"\", info.RelativePath)))
                {
                    FileInfo info2 = new FileInfo(Path.Combine(this.string_3, downloadInfo_0.SoftVer.SoftName + @"\", info.RelativePath));
                    if (((info.Length == info2.Length) && (info.LastWriteTime == info2.LastWriteTime)) && (info.CRCValue == MD5Tools.smethod_0(info2.FullName)))
                    {
                        continue;
                    }
                }
                string body = str2 + HttpUtility.UrlEncode(HttpUtility.HtmlEncode(info.RelativePath), Encoding.UTF8);
                int buffJIh = 0;
                string strFileName = Path.Combine(this.string_3, downloadInfo_0.SoftVer.SoftName + @"\", info.RelativePath);
                string str6 = this.method_46(url, body, contentType, out buffJIh, strFileName);
                num += buffJIh;
                this.method_15((double) num);
                num2 += (int) info.Length;
                string str8 = this.method_15((double) num2);
                string str9 = string.Format("{0} 已下载{1}/{2}", downloadInfo_0.SoftVer.SoftName, str8, this.method_15((double) downloadInfo_0.TotalSize));
                if (base.InvokeRequired)
                {
                    base.Invoke(new Delegate3(this.method_2), new object[] { str9 });
                }
                if (str6.Trim() != "ok")
                {
                    ilog_0.Info("下载文件失败DownLoadFileSynchronous:" + info.RelativePath);
                    return -1;
                }
                if (!this.method_14(Path.GetDirectoryName(strFileName)))
                {
                    ilog_0.Info("创建下载文件路径失败" + strFileName);
                    return -2;
                }
                try
                {
                    if (System.IO.File.Exists(strFileName))
                    {
                        System.IO.File.Delete(strFileName);
                    }
                }
                catch (Exception exception)
                {
                    ilog_0.Info("删除更新包存放目录中的下载文件出现异常" + strFileName);
                    ilog_0.Info(exception.Message);
                    return -3;
                }
                string path = strFileName + ".tzg";
                if (!System.IO.File.Exists(path))
                {
                    return -6;
                }
                System.IO.File.Move(path, strFileName);
                FileInfo info3 = new FileInfo(strFileName) {
                    IsReadOnly = false,
                    CreationTime = info.CreationTime,
                    LastAccessTime = info.LastAccessTime,
                    LastWriteTime = info.LastWriteTime
                };
                if (info.CRCValue != MD5Tools.smethod_0(strFileName))
                {
                    ilog_0.Info("下载文件失败fileInfo.CRCValue != GetMd5Hash(fileName):" + strFileName);
                    return -4;
                }
            }
            XmlDocument document = new XmlDocument();
            document.LoadXml(XmlTools.FileListInfoToXml(this.list_1[int_6]));
            XmlElement documentElement = document.DocumentElement;
            document.Save(Path.Combine(this.string_3, downloadInfo_0.SoftVer.SoftName + @"\temp\", "UpdateFileList.xml"));
            return 1;
        }

        private int method_17(DownloadInfo downloadInfo_0, int int_6)
        {
            string url = this.string_1 + "/HttpHandler/DownloadHandler.ashx";
            string str2 = "action=DownLoadFileSynchronous&FilePath=";
            str2 = ((str2 + HttpUtility.UrlEncode(HttpUtility.HtmlEncode(downloadInfo_0.SoftVer.FilePath), Encoding.UTF8) + "&TaxCode=") + HttpUtility.UrlEncode(HttpUtility.HtmlEncode(this.method_35()), Encoding.UTF8) + "&ClientFlag=") + HttpUtility.UrlEncode(HttpUtility.HtmlEncode("New"), Encoding.UTF8) + "&FileName=";
            string contentType = "application/x-www-form-urlencoded";
            int count = downloadInfo_0.FileList.Count;
            int num = 0;
            int num2 = 0;
            for (int i = 0; i < downloadInfo_0.FileList.Count; i++)
            {
                SoftFileInfo info = downloadInfo_0.FileList[i];
                if (System.IO.File.Exists(Path.Combine(this.string_3, downloadInfo_0.SoftVer.SoftName + @"\", info.RelativePath)))
                {
                    FileInfo info2 = new FileInfo(Path.Combine(this.string_3, downloadInfo_0.SoftVer.SoftName + @"\", info.RelativePath));
                    if (((info.Length == info2.Length) && (info.LastWriteTime == info2.LastWriteTime)) && (info.CRCValue == MD5Tools.smethod_0(info2.FullName)))
                    {
                        continue;
                    }
                }
                string body = str2 + HttpUtility.UrlEncode(HttpUtility.HtmlEncode(info.RelativePath), Encoding.UTF8);
                int buffJIh = 0;
                string s = this.PostHttpGTBeiFen(url, body, contentType, out buffJIh);
                num += buffJIh;
                this.method_15((double) num);
                num2 += (int) info.Length;
                string str7 = this.method_15((double) num2);
                string str8 = string.Format("{0} 已下载{1}/{2}", downloadInfo_0.SoftVer.SoftName, str7, this.method_15((double) downloadInfo_0.TotalSize));
                if (base.InvokeRequired)
                {
                    base.Invoke(new Delegate3(this.method_2), new object[] { str8 });
                }
                if (s.Trim() == "")
                {
                    ilog_0.Info("下载文件失败DownLoadFileSynchronous:" + info.RelativePath);
                    return -1;
                }
                string path = Path.Combine(this.string_3, downloadInfo_0.SoftVer.SoftName + @"\", info.RelativePath);
                if (!this.method_14(Path.GetDirectoryName(path)))
                {
                    ilog_0.Info("创建下载文件路径失败" + path);
                    return -2;
                }
                try
                {
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }
                catch (Exception exception)
                {
                    ilog_0.Info("删除更新包存放目录中的下载文件出现异常" + path);
                    ilog_0.Info(exception.Message);
                    return -3;
                }
                using (FileStream stream = System.IO.File.Open(path, FileMode.Create, FileAccess.Write))
                {
                    byte[] buffer = ZipTools.UnCompressZipFile(Convert.FromBase64String(s));
                    stream.Write(buffer, 0, (int) info.Length);
                    stream.Close();
                }
                FileInfo info3 = new FileInfo(path) {
                    IsReadOnly = false,
                    CreationTime = info.CreationTime,
                    LastAccessTime = info.LastAccessTime,
                    LastWriteTime = info.LastWriteTime
                };
                if (info.CRCValue != MD5Tools.smethod_0(path))
                {
                    ilog_0.Info("下载文件失败fileInfo.CRCValue != GetMd5Hash(fileName):" + path);
                    return -4;
                }
            }
            XmlDocument document = new XmlDocument();
            document.LoadXml(XmlTools.FileListInfoToXml(this.list_1[int_6]));
            XmlElement documentElement = document.DocumentElement;
            document.Save(Path.Combine(this.string_3, downloadInfo_0.SoftVer.SoftName + @"\temp\", "UpdateFileList.xml"));
            return 1;
        }

        private bool method_18(string string_6)
        {
            bool flag;
            try
            {
                string path = Path.Combine(this.string_3, string_6 + @"\temp\", "UpdateFileList.xml");
                if (!System.IO.File.Exists(path))
                {
                    return true;
                }
                XmlDocument document = new XmlDocument();
                document.Load(path);
                XmlElement documentElement = document.DocumentElement;
                using (IEnumerator<SoftFileInfo> enumerator = XmlTools.XmlToFileListInfo(document.InnerXml).FileList.GetEnumerator())
                {
                    string str3;
                    while (enumerator.MoveNext())
                    {
                        SoftFileInfo current = enumerator.Current;
                        string str2 = Path.Combine(this.string_4, string_6 + @"\", current.RelativePath);
                        str3 = string.Empty;
                        string str4 = current.RelativePath.ToLower();
                        if (str4.StartsWith(@"app\"))
                        {
                            str3 = Path.Combine(this.string_2, current.RelativePath.Substring(4));
                        }
                        else if (str4.StartsWith(@"system\"))
                        {
                            str3 = Path.Combine(this.string_5, current.RelativePath.Substring(7));
                        }
                        else
                        {
                            str3 = Path.Combine(this.string_2, current.RelativePath);
                        }
                        if (!this.method_14(Path.GetDirectoryName(str3)))
                        {
                            goto Label_015F;
                        }
                        try
                        {
                            if (System.IO.File.Exists(str2))
                            {
                                System.IO.File.Copy(str2, str3, true);
                            }
                            continue;
                        }
                        catch (Exception exception)
                        {
                            ilog_0.Info("RecoverFiles出现异常" + str3);
                            ilog_0.Info(exception.Message);
                            return false;
                        }
                    }
                    goto Label_0186;
                Label_015F:
                    ilog_0.Info("创建下载文件路径失败" + str3);
                    return false;
                }
            Label_0186:
                flag = true;
            }
            catch (Exception exception2)
            {
                ilog_0.Info("RecoverFiles" + exception2.Message);
                flag = false;
            }
            return flag;
        }

        private bool method_19(string string_6)
        {
            bool flag;
            try
            {
                using (IEnumerator<SoftFileInfo> enumerator = this.list_1[0].FileList.GetEnumerator())
                {
                    string str3;
                    while (enumerator.MoveNext())
                    {
                        SoftFileInfo current = enumerator.Current;
                        string path = string.Empty;
                        string str2 = current.RelativePath.ToLower();
                        if (str2.StartsWith(@"app\"))
                        {
                            path = Path.Combine(this.string_2, current.RelativePath.Substring(4));
                        }
                        else if (str2.StartsWith(@"system\"))
                        {
                            path = Path.Combine(this.string_5, current.RelativePath.Substring(7));
                        }
                        else
                        {
                            path = Path.Combine(this.string_2, current.RelativePath);
                        }
                        str3 = Path.Combine(this.string_4, string_6 + @"\", current.RelativePath);
                        if (!this.method_14(Path.GetDirectoryName(str3)))
                        {
                            goto Label_011F;
                        }
                        try
                        {
                            if (System.IO.File.Exists(str3))
                            {
                                System.IO.File.Delete(str3);
                            }
                        }
                        catch (Exception exception)
                        {
                            ilog_0.Info("删除备份目录中的文件出现异常" + str3);
                            ilog_0.Info(exception.Message);
                            return false;
                        }
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Copy(path, str3, true);
                        }
                    }
                    goto Label_0145;
                Label_011F:
                    ilog_0.Info("创建备份文件路径失败" + str3);
                    return false;
                }
            Label_0145:
                flag = true;
            }
            catch (Exception exception2)
            {
                ilog_0.Info("BackupFiles" + exception2.Message);
                flag = false;
            }
            return flag;
        }

        private void method_2(string string_6)
        {
            this.label1.Text = string_6;
        }

        private bool method_20(string string_6)
        {
            bool flag;
            try
            {
                using (IEnumerator<SoftFileInfo> enumerator = this.list_1[0].FileList.GetEnumerator())
                {
                    string str2;
                    while (enumerator.MoveNext())
                    {
                        SoftFileInfo current = enumerator.Current;
                        string path = Path.Combine(this.string_3, string_6 + @"\", current.RelativePath);
                        str2 = string.Empty;
                        string str3 = current.RelativePath.ToLower();
                        if (str3.StartsWith(@"app\"))
                        {
                            str2 = Path.Combine(this.string_2, current.RelativePath.Substring(4));
                        }
                        else if (str3.StartsWith(@"system\"))
                        {
                            str2 = Path.Combine(this.string_5, current.RelativePath.Substring(7));
                        }
                        else
                        {
                            str2 = Path.Combine(this.string_2, current.RelativePath);
                        }
                        if (!this.method_14(Path.GetDirectoryName(str2)))
                        {
                            goto Label_0137;
                        }
                        try
                        {
                            if (System.IO.File.Exists(path))
                            {
                                System.IO.File.Copy(path, str2, true);
                            }
                            continue;
                        }
                        catch (Exception exception)
                        {
                            ilog_0.Info("删除旧版本下载文件出现异常" + str2);
                            ilog_0.Info(exception.Message);
                            if (exception.Message.Contains("CTptkcs.dll"))
                            {
                                MessageBox.Show("请重新插拔一次金税盘或重启电脑再启动开票软件", "升级更新失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                            return false;
                        }
                    }
                    goto Label_015D;
                Label_0137:
                    ilog_0.Info("创建待更新文件路径失败" + str2);
                    return false;
                }
            Label_015D:
                flag = true;
            }
            catch (Exception exception2)
            {
                ilog_0.Info("UpdateFiles" + exception2.Message);
                flag = false;
            }
            return flag;
        }

        private bool method_21(string string_6)
        {
            try
            {
                string path = Path.Combine(this.string_3, string_6 + @"\temp\", "UpdateDesc.xml");
                if (System.IO.File.Exists(path))
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(path);
                    foreach (XmlNode node2 in document.DocumentElement.SelectSingleNode("CloseFiles").ChildNodes)
                    {
                        string innerText = node2.InnerText;
                        if (!this.method_34(innerText))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                ilog_0.Info("CloseFiles" + exception.Message);
                return false;
            }
        }

        private bool method_22(bool bool_1, string string_6)
        {
            try
            {
                XmlNode node;
                string path = Path.Combine(this.string_3, string_6 + @"\temp\", "UpdateDesc.xml");
                if (!System.IO.File.Exists(path))
                {
                    return true;
                }
                XmlDocument document = new XmlDocument();
                document.Load(path);
                XmlElement documentElement = document.DocumentElement;
                if (bool_1)
                {
                    node = documentElement.SelectSingleNode("ExecFilesBegin");
                }
                else
                {
                    node = documentElement.SelectSingleNode("ExecFilesEnd");
                }
                IEnumerator enumerator = node.ChildNodes.GetEnumerator();
                {
                Label_006C:
                    if (!enumerator.MoveNext())
                    {
                        goto Label_01C0;
                    }
                    XmlNode current = (XmlNode) enumerator.Current;
                    string str2 = current.InnerText.ToLower();
                    string innerText = "";
                    try
                    {
                        innerText = current.Attributes["ignoreresult"].InnerText;
                    }
                    catch
                    {
                        innerText = "";
                    }
                    goto Label_0193;
                Label_00C5:
                    if (str2 == "")
                    {
                        goto Label_006C;
                    }
                    if (str2.Contains("ExecSecurity.exe".ToLower()))
                    {
                        if (System.IO.File.Exists(Path.Combine(this.string_2, current.InnerText)))
                        {
                            System.IO.File.Delete(Path.Combine(this.string_2, current.InnerText));
                        }
                        if (System.IO.File.Exists(Path.Combine(this.string_2, "Security.dll")))
                        {
                            System.IO.File.Delete(Path.Combine(this.string_2, "Security.dll"));
                        }
                        goto Label_006C;
                    }
                    str2 = Path.Combine(this.string_3, string_6 + @"\", current.InnerText);
                    if (System.IO.File.Exists(str2) && (this.StartProcess(str2, true) || !(innerText != "1")))
                    {
                        goto Label_006C;
                    }
                    return false;
                Label_0193:
                    if (str2 == null)
                    {
                        goto Label_006C;
                    }
                    goto Label_00C5;
                }
            Label_01C0:
                if (!bool_1)
                {
                    System.IO.File.Delete(path);
                }
                return true;
            }
            catch (Exception exception)
            {
                ilog_0.Info("ExecFiles" + exception.Message);
                return false;
            }
        }

        private string method_23(DownloadInfo downloadInfo_0)
        {
            if (downloadInfo_0 != null)
            {
                string url = this.string_1 + "/HttpHandler/SoftVersionHandler.ashx";
                string body = "action=DownLoadLatestVersion&ClientName=";
                body = (((((((((body + HttpUtility.UrlEncode(HttpUtility.HtmlEncode(downloadInfo_0.SoftVer.ClientName), Encoding.UTF8)) + "&ClientIp=" + HttpUtility.UrlEncode(HttpUtility.HtmlEncode(downloadInfo_0.SoftVer.ClientIp), Encoding.UTF8)) + "&ClientPort=" + HttpUtility.UrlEncode(HttpUtility.HtmlEncode(downloadInfo_0.SoftVer.ClientPort), Encoding.UTF8)) + "&SoftName=" + HttpUtility.UrlEncode(HttpUtility.HtmlEncode(downloadInfo_0.SoftVer.SoftName), Encoding.UTF8)) + "&Version=" + HttpUtility.UrlEncode(HttpUtility.HtmlEncode(downloadInfo_0.SoftVer.Version), Encoding.UTF8)) + "&VerDesc=" + HttpUtility.UrlEncode(HttpUtility.HtmlEncode(downloadInfo_0.SoftVer.VerDesc), Encoding.UTF8)) + "&FilePath=" + HttpUtility.UrlEncode(HttpUtility.HtmlEncode(downloadInfo_0.SoftVer.FilePath), Encoding.UTF8)) + "&TaxCode=" + HttpUtility.UrlEncode(HttpUtility.HtmlEncode(this.method_35() + '.' + this.method_36()), Encoding.UTF8)) + "&Xzbz=" + HttpUtility.UrlEncode(HttpUtility.HtmlEncode(downloadInfo_0.Xzbz), Encoding.UTF8)) + "&PreVersion=" + HttpUtility.UrlEncode(HttpUtility.HtmlEncode(downloadInfo_0.PreVersion), Encoding.UTF8);
                string contentType = "application/x-www-form-urlencoded";
                return HttpTool.PostHttp(url, body, contentType);
            }
            return "";
        }

        private void method_24(List<DownloadInfo> latestList)
        {
            if (this.list_0 == null)
            {
                this.list_0 = new List<DownloadInfo>();
            }
            this.list_0.Clear();
            if (this.list_1 == null)
            {
                this.list_1 = new List<DownloadInfo>();
            }
            this.list_1.Clear();
            foreach (DownloadInfo info in latestList)
            {
                DownloadInfo item = new DownloadInfo {
                    SoftVer = { ClientName = info.SoftVer.ClientName, ClientIp = info.SoftVer.ClientIp, ClientPort = info.SoftVer.ClientPort, SoftName = info.SoftVer.SoftName, Version = info.SoftVer.Version, Force = info.SoftVer.Force, VerDesc = info.SoftVer.VerDesc, FilePath = info.SoftVer.FilePath }
                };
                if (this.dictionary_0.ContainsKey(info.SoftVer.SoftName))
                {
                    item.PreVersion = this.dictionary_0[info.SoftVer.SoftName];
                }
                item.TotalSize = 0L;
                item.FileList.Clear();
                DownloadInfo info3 = new DownloadInfo {
                    SoftVer = { ClientName = info.SoftVer.ClientName, ClientIp = info.SoftVer.ClientIp, ClientPort = info.SoftVer.ClientPort, SoftName = info.SoftVer.SoftName, Version = info.SoftVer.Version, Force = info.SoftVer.Force, VerDesc = info.SoftVer.VerDesc, FilePath = info.SoftVer.FilePath },
                    TotalSize = 0L
                };
                info3.FileList.Clear();
                foreach (SoftFileInfo info4 in info.FileList)
                {
                    string path = info4.RelativePath.ToLower();
                    if (path.StartsWith(@"app\"))
                    {
                        if (path.StartsWith(@"app\automation"))
                        {
                            path = Path.Combine(this.string_2, "Automation", info4.RelativePath.Substring(info4.RelativePath.LastIndexOf(@"\") + 1));
                        }
                        else
                        {
                            path = Path.Combine(this.string_2, info4.RelativePath.Substring(4));
                        }
                    }
                    else if (path.StartsWith(@"system\"))
                    {
                        path = Path.Combine(this.string_5, info4.RelativePath.Substring(7));
                    }
                    else
                    {
                        path = Path.Combine(this.string_2, info4.RelativePath);
                    }
                    if (System.IO.File.Exists(path))
                    {
                        FileInfo info5 = new FileInfo(path);
                        if ((info4.Length == info5.Length) && (info4.CRCValue == MD5Tools.smethod_0(info5.FullName)))
                        {
                            continue;
                        }
                    }
                    SoftFileInfo info6 = new SoftFileInfo {
                        RelativePath = info4.RelativePath,
                        Length = info4.Length
                    };
                    item.TotalSize += info4.Length;
                    info6.CreationTime = info4.CreationTime;
                    info6.LastAccessTime = info4.LastAccessTime;
                    info6.LastWriteTime = info4.LastWriteTime;
                    info6.CRCValue = info4.CRCValue;
                    if (!info6.RelativePath.StartsWith(@"temp\"))
                    {
                        info3.FileList.Add(info6);
                    }
                    if (System.IO.File.Exists(Path.Combine(this.string_3, info.SoftVer.SoftName, info4.RelativePath)))
                    {
                        FileInfo info7 = new FileInfo(Path.Combine(this.string_3, info.SoftVer.SoftName, info4.RelativePath));
                        if ((info4.Length == info7.Length) && (info4.CRCValue == MD5Tools.smethod_0(info7.FullName)))
                        {
                            continue;
                        }
                    }
                    item.FileList.Add(info6);
                }
                this.list_0.Add(item);
                this.list_1.Add(info3);
            }
        }

        private int method_25(List<DownloadInfo> latestList, int int_6 = 0)
        {
            if (this.list_0 == null)
            {
                this.list_0 = new List<DownloadInfo>();
            }
            this.list_0.Clear();
            if (this.list_1 == null)
            {
                this.list_1 = new List<DownloadInfo>();
            }
            this.list_1.Clear();
            foreach (DownloadInfo info in latestList)
            {
                DownloadInfo item = new DownloadInfo {
                    SoftVer = { ClientName = info.SoftVer.ClientName, ClientIp = info.SoftVer.ClientIp, ClientPort = info.SoftVer.ClientPort, SoftName = info.SoftVer.SoftName, Version = info.SoftVer.Version, Force = info.SoftVer.Force, VerDesc = info.SoftVer.VerDesc, FilePath = info.SoftVer.FilePath }
                };
                if (this.dictionary_0.ContainsKey(info.SoftVer.SoftName))
                {
                    item.PreVersion = this.dictionary_0[info.SoftVer.SoftName];
                }
                item.TotalSize = 0L;
                item.FileList.Clear();
                DownloadInfo info3 = new DownloadInfo {
                    SoftVer = { ClientName = info.SoftVer.ClientName, ClientIp = info.SoftVer.ClientIp, ClientPort = info.SoftVer.ClientPort, SoftName = info.SoftVer.SoftName, Version = info.SoftVer.Version, Force = info.SoftVer.Force, VerDesc = info.SoftVer.VerDesc, FilePath = info.SoftVer.FilePath },
                    TotalSize = 0L
                };
                info3.FileList.Clear();
                foreach (SoftFileInfo info4 in info.FileList)
                {
                    string str = info4.RelativePath.ToLower();
                    string path = "";
                    if (str.StartsWith(@"app\automation") && (str != @"app\automation\bin\invautomation.exe.config"))
                    {
                        if (str == @"app\automation\bin\updateinvautomation.exe")
                        {
                            path = Path.Combine(this.string_2, "Automation", info4.RelativePath.Substring(@"app\Automation\".Length));
                        }
                        else
                        {
                            path = Path.Combine(this.string_2, "Automation", info4.RelativePath.Substring(@"app\Automation\Bin\".Length));
                        }
                        if (System.IO.File.Exists(path))
                        {
                            FileInfo info5 = new FileInfo(path);
                            if ((info4.Length == info5.Length) && (info4.CRCValue == MD5Tools.smethod_0(info5.FullName)))
                            {
                                continue;
                            }
                        }
                        SoftFileInfo info6 = new SoftFileInfo {
                            RelativePath = info4.RelativePath,
                            Length = info4.Length
                        };
                        item.TotalSize += info4.Length;
                        info6.CreationTime = info4.CreationTime;
                        info6.LastAccessTime = info4.LastAccessTime;
                        info6.LastWriteTime = info4.LastWriteTime;
                        info6.CRCValue = info4.CRCValue;
                        info3.FileList.Add(info6);
                        if (System.IO.File.Exists(Path.Combine(this.string_3, info.SoftVer.SoftName, info4.RelativePath)))
                        {
                            FileInfo info7 = new FileInfo(Path.Combine(this.string_3, info.SoftVer.SoftName, info4.RelativePath));
                            if ((info4.Length == info7.Length) && (info4.CRCValue == MD5Tools.smethod_0(info7.FullName)))
                            {
                                continue;
                            }
                        }
                        item.FileList.Add(info6);
                    }
                }
                this.list_0.Add(item);
                this.list_1.Add(info3);
                if ((info3.FileList.Count > 0) && (int_6 != 0))
                {
                    return 1;
                }
            }
            return 0;
        }

        private Dictionary<string, string> method_26()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string key = ConfigurationManager.AppSettings["SoftName"];
            string str2 = this.method_40();
            dictionary.Add(key, str2);
            List<string> list = new List<string>(this.method_0().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries));
            foreach (SetupFile file in IniFileUtil.ReadSetupConfig(Path.Combine(this.string_2, @"Automation\Config.ini")))
            {
                if (list.Contains(file.Kind))
                {
                    dictionary.Add(file.Name, file.Ver);
                }
            }
            dictionary.Add("金税盘底层", this.method_39());
            return dictionary;
        }

        private List<DownloadInfo> method_27()
        {
            string s = "";
            string str2 = "";
            if ((this.dictionary_0 != null) && (this.dictionary_0.Count > 0))
            {
                foreach (KeyValuePair<string, string> pair in this.dictionary_0)
                {
                    if ((s.Length == 0) && (str2.Length == 0))
                    {
                        s = s + pair.Key;
                        str2 = str2 + pair.Value;
                    }
                    else
                    {
                        s = s + ";" + pair.Key;
                        str2 = str2 + ";" + pair.Value;
                    }
                }
            }
            string url = this.string_1 + "/HttpHandler/SoftVersionHandler.ashx";
            string body = "action=GetLatestVersion&ClientName=";
            body = (((((((body + HttpUtility.UrlEncode(HttpUtility.HtmlDecode(this.method_37()), Encoding.UTF8) + "&SoftName=") + HttpUtility.UrlEncode(HttpUtility.HtmlDecode(ConfigurationManager.AppSettings["SoftName"]), Encoding.UTF8) + "&SoftVersion=") + HttpUtility.UrlEncode(HttpUtility.HtmlDecode(this.method_40()), Encoding.UTF8) + "&TaxCode=") + HttpUtility.UrlEncode(HttpUtility.HtmlEncode(this.method_35()), Encoding.UTF8) + "&AreaCode=") + HttpUtility.UrlEncode(HttpUtility.HtmlEncode(this.method_37()), Encoding.UTF8) + "&SoftList=") + HttpUtility.UrlEncode(HttpUtility.HtmlEncode(s), Encoding.UTF8) + "&VerList=") + HttpUtility.UrlEncode(HttpUtility.HtmlEncode(str2), Encoding.UTF8) + "&ClientType=2") + "&ClientPort=" + HttpUtility.UrlEncode(HttpUtility.HtmlDecode(ConfigurationManager.AppSettings["ClientPort"]), Encoding.UTF8);
            string contentType = "application/x-www-form-urlencoded";
            return XmlTools.XmlToVerList(HttpTool.PostHttp(url, body, contentType));
        }

        private string method_28()
        {
            string url = ConfigurationManager.AppSettings["MSURL"] + "/HttpHandler/RegisterHandler.ashx";
            string body = "action=GetBServer&ClientName=";
            body = body + HttpUtility.UrlEncode(HttpUtility.HtmlDecode(this.method_37()), Encoding.UTF8);
            string contentType = "application/x-www-form-urlencoded";
            string str4 = HttpTool.PostHttp(url, body, contentType);
            if (str4 == "M1")
            {
                return ConfigurationManager.AppSettings["MSURL"];
            }
            if (str4 != "")
            {
                System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().Location);
                configuration.AppSettings.Settings.Remove("LOCALURL");
                configuration.AppSettings.Settings.Add("LOCALURL", str4);
                configuration.Save(ConfigurationSaveMode.Modified);
            }
            return str4;
        }

        private int method_29(string string_6, string string_7, string string_8)
        {
            try
            {
                if ((string_6 != null) && (string_7 != null))
                {
                    string path = Path.Combine(string_6, string_7);
                    string url = this.string_1 + "/HttpHandler/NoticeHandler.ashx";
                    string body = "action=GetUpdateGGDZ";
                    string contentType = "application/x-www-form-urlencoded";
                    string str5 = HttpTool.PostHttp(url, body, contentType);
                    if (str5.Length == 0)
                    {
                        str5 = this.string_1 + ":3288";
                    }
                    string[] strArray = str5.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    if (strArray.Length > 1)
                    {
                        dictionary.Add("PUB_SERVER_IP", strArray[1].Replace("//", ""));
                        if (strArray.Length > 2)
                        {
                            dictionary.Add("PUB_SERVER_PORT", strArray[2]);
                        }
                    }
                    if (System.IO.File.Exists(path))
                    {
                        XmlDocument document = new XmlDocument();
                        document.Load(path);
                        foreach (KeyValuePair<string, string> pair in dictionary)
                        {
                            XmlNode node = document.DocumentElement.SelectSingleNode(pair.Key);
                            if (node == null)
                            {
                                XmlElement newChild = document.CreateElement(pair.Key);
                                document.DocumentElement.AppendChild(newChild);
                                node = newChild;
                            }
                            Convert.ToBase64String(Encoding.Unicode.GetBytes(pair.Value));
                            (node as XmlElement).SetAttribute("value", Convert.ToBase64String(Encoding.Unicode.GetBytes(pair.Value)));
                        }
                        document.Save(path);
                    }
                }
                return 0;
            }
            catch (Exception exception)
            {
                ilog_0.Debug(exception.Message);
                return 1;
            }
        }

        private void method_3(string string_6)
        {
            this.label2.Text = string_6;
        }

        private string method_30()
        {
            string url = ConfigurationManager.AppSettings["MSURL"] + "/HttpHandler/ConfigHandler.ashx";
            string body = "action=Checkflsz&ClientName=";
            body = body + HttpUtility.UrlEncode(HttpUtility.HtmlDecode(this.method_37()), Encoding.UTF8);
            string contentType = "application/x-www-form-urlencoded";
            return HttpTool.PostHttp(url, body, contentType);
        }

        private void method_31(object sender, EventArgs e)
        {
        }

        private void method_32(object sender, EventArgs e)
        {
            if (base.WindowState == FormWindowState.Normal)
            {
                base.Hide();
                base.WindowState = FormWindowState.Minimized;
            }
            else
            {
                base.WindowState = FormWindowState.Normal;
                base.Show();
                base.Activate();
            }
        }

        private bool method_33(string string_6)
        {
            try
            {
                if (((string_6 != null) && (string_6 != "")) && System.IO.File.Exists(string_6))
                {
                    string str = string_6.Substring(string_6.LastIndexOf(@"\") + 1);
                    Process[] processesByName = Process.GetProcessesByName(str.Substring(0, str.LastIndexOf(".")));
                    return ((processesByName != null) && (processesByName.Length > 0));
                }
                return false;
            }
            catch (Exception exception)
            {
                ilog_0.Info("IsProcessRun" + exception.Message);
                return false;
            }
        }

        private bool method_34(string string_6)
        {
            try
            {
                if ((string_6 != null) && (string_6 != ""))
                {
                    string processName = string_6.Substring(string_6.LastIndexOf(@"\") + 1);
                    processName = processName.Substring(0, processName.LastIndexOf("."));
                    Process[] processesByName = Process.GetProcessesByName(processName);
                    if ((processesByName != null) && (processesByName.Length > 0))
                    {
                        foreach (Process process in processesByName)
                        {
                            process.Kill();
                            while (!process.WaitForExit(0x3e8))
                            {
                                if (!process.HasExited)
                                {
                                    break;
                                }
                                process.Refresh();
                            }
                            if (process.HasExited)
                            {
                                ilog_0.Info("KillProcess：结束进程失败" + processName);
                            }
                        }
                    }
                    return true;
                }
                return true;
            }
            catch (Exception exception)
            {
                ilog_0.Info("KillProcess" + exception.Message);
                return false;
            }
        }

        private string method_35()
        {
            string str4;
            try
            {
                string keyName = ConfigurationManager.AppSettings["TaxCodeRegPath"];
                string valueName = ConfigurationManager.AppSettings["TaxCodeRegKey"];
                return Registry.GetValue(keyName, valueName, "").ToString();
            }
            catch (Exception)
            {
                str4 = "";
            }
            return str4;
        }

        private string method_36()
        {
            string str2;
            try
            {
                return Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe", "machine", "").ToString();
            }
            catch (Exception)
            {
                str2 = "";
            }
            return str2;
        }

        private string method_37()
        {
            string str4;
            try
            {
                string keyName = ConfigurationManager.AppSettings["AreaCodeRegPath"];
                string valueName = ConfigurationManager.AppSettings["AreaCodeRegKey"];
                return Registry.GetValue(keyName, valueName, "").ToString();
            }
            catch (Exception)
            {
                str4 = "";
            }
            return str4;
        }

        private string method_38()
        {
            string str4;
            try
            {
                string keyName = ConfigurationManager.AppSettings["SoftPathRegPath"];
                string valueName = ConfigurationManager.AppSettings["SoftPathRegKey"];
                return Registry.GetValue(keyName, valueName, "").ToString();
            }
            catch (Exception)
            {
                str4 = "";
            }
            return str4;
        }

        private string method_39()
        {
            try
            {
                string keyName = ConfigurationManager.AppSettings["SoftPathRegPath"];
                string str3 = Registry.GetValue(keyName, "code", "").ToString();
                int result = 0;
                int.TryParse(Registry.GetValue(keyName, "machine", "").ToString(), out result);
                byte[] buffer = new byte[30];
                int verNo = 1;
                try
                {
                    verNo = GetVerNo_T(buffer, Encoding.GetEncoding("GBK").GetBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JSDiskDLL.dll")), str3, result);
                }
                catch (EntryPointNotFoundException exception)
                {
                    if (exception.Message.StartsWith("无法在 DLL“ReadAreaCode.dll”中找到名为“GetVerNo_T”的入口点"))
                    {
                        verNo = GetVerNo(buffer, Encoding.GetEncoding("GBK").GetBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JSDiskDLL.dll")));
                    }
                }
                switch (verNo)
                {
                    case 0:
                        return Encoding.GetEncoding("GBK").GetString(buffer).Trim(new char[1]);

                    case 1:
                        MessageBox.Show("未检测到金税盘，请检查金税盘连接是否正常！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        ilog_0.Info("未检测到金税盘GetVerNo = " + verNo);
                        Application.Exit();
                        return "Z";
                }
                ilog_0.Info("金税盘GetVerNo = " + verNo);
                return "Z";
            }
            catch (Exception exception2)
            {
                ilog_0.Debug("GetJspVer CW " + exception2.Message);
                return "Z";
            }
        }

        private void method_4(string string_6)
        {
            this.textBox1.Text = this.textBox1.Text + string_6 + "\r\n";
        }

        private string method_40()
        {
            string str4;
            string str = "";
            try
            {
                string keyName = ConfigurationManager.AppSettings["VersionRegPath"];
                string valueName = ConfigurationManager.AppSettings["VersionRegKey"];
                str = Registry.GetValue(keyName, valueName, "").ToString();
                if ((str != null) && !(str == ""))
                {
                    return str;
                }
                return "V";
            }
            catch (Exception)
            {
                str4 = "V";
            }
            return str4;
        }

        private void method_41(string string_6)
        {
            try
            {
                string keyName = ConfigurationManager.AppSettings["VersionRegPath"];
                string valueName = ConfigurationManager.AppSettings["VersionRegKey"];
                Registry.SetValue(keyName, valueName, string_6);
                Registry.SetValue(keyName + @"\" + this.method_35() + "." + this.method_36(), valueName, string_6);
                string str3 = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\开票软件";
                string str4 = "DisplayName";
                Registry.SetValue(str3, str4, "开票软件 " + string_6);
                Registry.SetValue(str3, "DisplayVersion", string_6);
            }
            catch (Exception exception)
            {
                ilog_0.Info("设置本地版本号:" + exception.Message);
            }
        }

        private void method_42(object sender, EventArgs e)
        {
            SetForm.GetSetInstance().ShowDialog();
        }

        private void method_43(object sender, EventArgs e)
        {
            base.Hide();
            base.WindowState = FormWindowState.Minimized;
        }

        private void method_44(object sender, MouseEventArgs e)
        {
        }

        private void method_45()
        {
            base.Hide();
            this.notifyIcon_0.Visible = true;
            this.notifyIcon_0.ShowBalloonTip(0x3e8, "注意在线更新", "正在下载防伪开票软件\r\n请不要关机", ToolTipIcon.Info);
            this.notifyIcon_0.Text = "正在下载防伪开票软件\r\n请不要关机";
        }

        public string method_46(string url, string body, string contentType, out int buffJIh, string strFileName)
        {
            FileStream stream = null;
            Stream responseStream = null;
            string str;
            try
            {
                buffJIh = 0;
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
                long offset = 0L;
                strFileName = strFileName + ".tzg";
                if (System.IO.File.Exists(strFileName))
                {
                    stream = System.IO.File.OpenWrite(strFileName);
                    offset = stream.Length;
                    stream.Seek(offset, SeekOrigin.Current);
                }
                else
                {
                    if (!this.method_14(Path.GetDirectoryName(strFileName)))
                    {
                        ilog_0.Info("创建下载文件路径失败" + strFileName);
                        return "e3";
                    }
                    stream = new FileStream(strFileName, FileMode.Create);
                    offset = 0L;
                }
                if (offset > 0L)
                {
                    request.AddRange((int) offset);
                }
                request.ContentType = contentType;
                request.Method = "POST";
                request.Timeout = 0x1d4c0;
                byte[] bytes = Encoding.UTF8.GetBytes(body);
                request.ContentLength = bytes.Length;
                request.GetRequestStream().Write(bytes, 0, bytes.Length);
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                responseStream = response.GetResponseStream();
                byte[] buffer = new byte[0x2800];
                int count = 0;
                this.int_5 = 0;
                int num3 = 0x400;
                while ((count = responseStream.Read(buffer, 0, num3)) > 0)
                {
                    this.int_4 += count;
                    this.int_5 += count;
                    stream.Write(buffer, 0, count);
                }
                buffJIh = this.int_5;
                response.Close();
                request.Abort();
                response.Close();
                str = "ok";
            }
            catch (TimeoutException exception)
            {
                buffJIh = 0;
                ilog_0.Debug("下载超时:" + strFileName + exception.Message);
                str = "e1";
            }
            catch (Exception exception2)
            {
                buffJIh = 0;
                ilog_0.Debug("下载文件:" + strFileName + exception2.Message);
                str = "e2";
            }
            finally
            {
                stream.Close();
                responseStream.Close();
            }
            return str;
        }

        private void method_47(object object_0)
        {
            this.int_3 = this.int_4 / this.int_2;
            this.int_4 = 0;
            string str = string.Format("下载速度{0}/s  ", this.method_15((double) this.int_3));
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new Delegate3(this.method_3), new object[] { str });
            }
        }

        private void method_48(object sender, ElapsedEventArgs e)
        {
            int hour = e.SignalTime.Hour;
            int minute = e.SignalTime.Minute;
            int second = e.SignalTime.Second;
            DayOfWeek dayOfWeek = e.SignalTime.DayOfWeek;
            this.int_3 = this.int_4 / this.int_2;
            this.int_4 = 0;
            string str = string.Format("下载速度{0}/s  ", this.method_15((double) this.int_3));
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new Delegate3(this.method_3), new object[] { str });
            }
        }

        private void method_5()
        {
            if ((this.list_0 != null) && (this.list_0.Count > 0))
            {
                string str2 = "";
                base.WindowState = FormWindowState.Minimized;
                this.lblMsg.Text = "正在下载";
                this.textBox1.Text = "";
                this.textBox1.Visible = true;
                this.label1.Visible = true;
                this.label2.Visible = true;
                this.button1.Visible = true;
                if (base.InvokeRequired)
                {
                    base.BeginInvoke(new Delegate3(this.method_2), new object[] { "正在下载:" });
                    Thread.CurrentThread.Join(100);
                }
                for (int i = 0; i < this.list_0.Count; i++)
                {
                    if (this.list_0[i].FileList.Count > 0)
                    {
                        string str = "正在下载:" + this.list_0[i].SoftVer.SoftName + " " + this.list_0[i].SoftVer.Version;
                        if (base.InvokeRequired)
                        {
                            base.BeginInvoke(new Delegate3(this.method_2), new object[] { str });
                        }
                        string str4 = (this.list_0[i].SoftVer.Version.Length > 20) ? this.list_0[i].SoftVer.Version.Substring(0, 0x13) : this.list_0[i].SoftVer.Version;
                        this.lblMsg.Text = " 正在下载:" + this.list_0[i].SoftVer.SoftName + " " + str4;
                        this.list_0[i].Xzbz = 0;
                        this.method_23(this.list_0[i]);
                        int num2 = this.method_17(this.list_0[i], i);
                        if (num2 == -1)
                        {
                            string str3 = "网速太慢下载失败" + this.list_0[i].SoftVer.SoftName + " " + this.list_0[i].SoftVer.Version;
                            if (base.InvokeRequired)
                            {
                                base.BeginInvoke(new Delegate3(this.method_4), new object[] { str3 });
                            }
                        }
                        this.list_0[i].Xzbz = num2;
                        this.method_23(this.list_0[i]);
                        if (num2 != 1)
                        {
                            break;
                        }
                        string str5 = str2;
                        str2 = str5 + "  " + this.list_0[i].SoftVer.SoftName + this.list_0[i].SoftVer.Version + "\r\n";
                        ilog_0.Debug("之前版本号:" + this.dictionary_0[this.list_0[i].SoftVer.SoftName] + " 下载完成:" + str2);
                    }
                    else
                    {
                        XmlDocument document = new XmlDocument();
                        document.LoadXml(XmlTools.FileListInfoToXml(this.list_1[i]));
                        XmlElement documentElement = document.DocumentElement;
                        document.Save(Path.Combine(this.string_3, this.list_1[i].SoftVer.SoftName + @"\temp\", "UpdateFileList.xml"));
                    }
                }
                if (str2.Trim().Length > 0)
                {
                    base.Hide();
                    this.notifyIcon_0.Visible = false;
                    if (this.int_1 == 0)
                    {
                        MessageBox.Show("最新版本:\r\n" + str2 + "已经下载完成，请退出开票软件，重新进入系统!", "更新提示", MessageBoxButtons.OK);
                    }
                }
            }
            this.notifyIcon_0.Visible = false;
            Application.Exit();
        }

        private void method_6()
        {
        }

        private void method_7(string string_6)
        {
            UpdateDescForm form = new UpdateDescForm(this.method_13(string_6));
            base.Hide();
            form.ShowDialog();
            base.Show();
        }

        private int method_8()
        {
            int num = 0;
            smethod_0("启动开票软件;" + startFilePath);
            if (this.method_33(startFilePath))
            {
                return -1;
            }
            string str = ConfigurationManager.AppSettings["SoftName"];
            int num3 = this.method_10(str, this.dictionary_0);
            if (num3 >= 0)
            {
                num = this.method_11(str, num3);
            }
            int num2 = this.method_10("金税盘底层", this.dictionary_0);
            if (num2 >= 0)
            {
                num = this.method_11("金税盘底层", num2);
            }
            foreach (SetupFile file in IniFileUtil.ReadSetupConfig(Path.Combine(this.string_2, @"Automation\Config.ini")))
            {
                string name = file.Name;
                int num6 = this.method_10(name, this.dictionary_0);
                if (num6 >= 0)
                {
                    num = this.method_11(name, num6);
                }
            }
            return num;
        }

        private int method_9()
        {
            string str = "新版开票";
            string path = Path.Combine(this.string_3, str + @"\temp\UpdateFileList.xml");
            if (!System.IO.File.Exists(path))
            {
                return -1;
            }
            string fileName = Path.Combine(this.string_3, str, @"app\Automation\Bin\InvAutomation.exe");
            string str3 = Path.Combine(this.string_2, @"Automation\InvAutomation.exe");
            FileInfo info2 = new FileInfo(str3);
            FileInfo info5 = new FileInfo(fileName);
            if (info5.Exists && info2.Exists)
            {
                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(str3);
                FileVersionInfo info4 = FileVersionInfo.GetVersionInfo(fileName);
                Console.WriteLine("文件名称=" + versionInfo.FileName);
                Console.WriteLine("产品名称=" + versionInfo.ProductName);
                Console.WriteLine("公司名称=" + versionInfo.CompanyName);
                Console.WriteLine("文件版本=" + versionInfo.FileVersion);
                Console.WriteLine("产品版本=" + versionInfo.ProductVersion);
                if (string.Compare(versionInfo.FileVersion, info4.FileVersion, true) >= 0)
                {
                    return -1;
                }
            }
            XmlDocument document = new XmlDocument();
            document.Load(path);
            XmlElement documentElement = document.DocumentElement;
            DownloadInfo info = XmlTools.XmlToFileListInfo(document.InnerXml);
            if (this.dictionary_0.ContainsKey(info.SoftVer.SoftName) && (string.Compare(this.dictionary_0[info.SoftVer.SoftName], info.SoftVer.Version, true) < 0))
            {
                List<DownloadInfo> latestList = new List<DownloadInfo> {
                    info
                };
                this.method_25(latestList, 1);
                if ((this.list_0 != null) && (this.list_0[0].FileList.Count <= 0))
                {
                    if (!this.method_19(str))
                    {
                        ilog_0.Info("备份失败" + str);
                        return 4;
                    }
                    if (!this.method_20(str))
                    {
                        ilog_0.Info("更新失败：" + str);
                        return 5;
                    }
                }
            }
            System.IO.File.Delete(path);
            return 0;
        }

        private void notifyIcon_0_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                base.Visible = true;
                base.WindowState = FormWindowState.Normal;
            }
        }

        public string PostHttpGTBeiFen(string url, string body, string contentType, out int buffJIh)
        {
            string str2;
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
                request.ContentType = contentType;
                request.Method = "POST";
                request.Timeout = 0x1d4c0;
                byte[] bytes = Encoding.UTF8.GetBytes(body);
                request.ContentLength = bytes.Length;
                request.GetRequestStream().Write(bytes, 0, bytes.Length);
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                char[] buffer = new char[0x2800];
                int num = 0;
                int length = 0;
                this.int_5 = 0;
                int count = 0x200;
                StringBuilder builder = new StringBuilder("");
                while ((length = reader.Read(buffer, 0, count)) > 0)
                {
                    this.int_4 += length;
                    this.int_5 += length;
                    if (length != 0x200)
                    {
                        num++;
                    }
                    string str3 = new string(buffer, 0, length);
                    builder.Append(str3);
                }
                string str = builder.ToString();
                buffJIh = this.int_5;
                response.Close();
                reader.Close();
                request.Abort();
                response.Close();
                str2 = str;
            }
            catch (Exception exception)
            {
                ilog_0.Debug("下载文件:" + exception.Message);
                throw;
            }
            return str2;
        }

        private static void smethod_0(object object_0)
        {
            if (int_0 == 1)
            {
                ilog_0.Info(object_0);
            }
        }

        private static byte[] smethod_1(string string_6, byte[] byte_0)
        {
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.FromXmlString(string_6);
                return provider.Decrypt(byte_0, false);
            }
            catch (Exception exception)
            {
                ilog_0.Error(exception.Message);
                return null;
            }
        }

        public bool StartProcess(string _strExePath, bool blWait)
        {
            try
            {
                if (((_strExePath != null) && (_strExePath != "")) && System.IO.File.Exists(_strExePath))
                {
                    Process process = new Process {
                        StartInfo = { FileName = _strExePath, WindowStyle = ProcessWindowStyle.Normal, CreateNoWindow = true }
                    };
                    process.Start();
                    if (blWait)
                    {
                        process.WaitForExit();
                        return (process.ExitCode == 0);
                    }
                    return true;
                }
                return false;
            }
            catch (Exception exception)
            {
                ilog_0.Info("StartProcess" + exception.Message);
                return false;
            }
        }

        public static bool TestLink(string url)
        {
            url = url + "/HttpHandler/ConfigHandler.ashx";
            string body = "action=TestLink";
            string contentType = "application/x-www-form-urlencoded";
            return (HttpTool.PostHttp(url, body, contentType) == "0000");
        }

        private void ToolStripMenuItemStop_Click(object sender, EventArgs e)
        {
            this.notifyIcon_0.Visible = false;
            Application.Exit();
        }

        private delegate void Delegate3(string strMsg);
    }
}

