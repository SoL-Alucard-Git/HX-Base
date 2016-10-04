namespace Aisino.Fwkp.DataMigrationTool.Form
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.DataMigrationTool.Common;
    using log4net;
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    public class DataMigrationToolForm : BaseForm
    {
        private int _iIndexComma;
        private int _iIndexTable = 1;
        private bool _IsFuZhiState;
        public static List<string> _listExistTbNames = new List<string>();
        private static ILog _Loger = LogUtil.GetLogger<DataMigrationToolForm>();
        private Data_Operator_XML _OperXML;
        private AisinoBTN btn_Cancle;
        private AisinoBTN btn_Copy_WanCheng;
        private AisinoBTN btn_Select_DB;
        private ComboBox comboBoxBackupPaths;
        private IContainer components;
        private string CurTableStatus = "";
        private Thread cycleThread;
        public static int existOldVerNum = 3;
        private Thread fuZhiThread;
        private AisinoGRP groupBox1;
        private ImageList imageList1;
        private bool IsFuZhiEnd = true;
        public static bool isJZKPFlag = false;
        private bool isNoCompleted = true;
        private bool IsRunResult;
        public static bool isWorkDirSelected = false;
        private AisinoLBL lab_FuZhiing;
        private AisinoLBL label1;
        private Label label3;
        private const uint MF_BYCOMMAND = 0;
        private const uint MF_DISABLED = 2;
        private const uint MF_GRAYED = 1;
        private AisinoPRG progressBar2;
        private AisinoRDO rad_FuGai;
        private AisinoRDO rad_TiaoGuo;
        private RadioButton radioBtnBackup;
        private RadioButton radioBtnOldDBInfo;
        private Thread runThread;
        private const uint SC_CLOSE = 0xf060;
        private AisinoSPL splitContainer;
        private SplitContainer splitContainer1;
        private static TaxCard taxCard = TaxCardFactory.CreateTaxCard();
        private ToolTip toolTip_selDB;
        private TreeView treeView1;
        public static bool useSelfPathFlag = false;

        public DataMigrationToolForm()
        {
            try
            {
                this.InitializeComponent();
                DingYiZhiFuChuan.Initialize();
                if (this._OperXML == null)
                {
                    this._OperXML = new Data_Operator_XML(this);
                }
                this.btn_Copy_WanCheng.Text = DingYiZhiFuChuan.strFuZhi;
                this.lab_FuZhiing.Text = string.Empty;
                this.progressBar2.Visible = false;
                this.SetShowProgressBar(false);
                this.InitializeStatement();
                this.InitializeTreeView();
                this.InitBackupPaths();
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._IsFuZhiState)
                {
                    base.DialogResult = DialogResult.Cancel;
                }
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void btn_Copy_WanCheng_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._IsFuZhiState)
                {
                    if (this.btn_Copy_WanCheng.Text.Equals(DingYiZhiFuChuan.strFuZhi))
                    {
                        if (string.IsNullOrEmpty(DingYiZhiFuChuan.strParadoxPath))
                        {
                            MessageBoxHelper.Show("请先选择数据库！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else if (!this.DeterLinkData())
                        {
                            this.btn_Select_DB.Enabled = true;
                        }
                        else
                        {
                            DialogResult result = MessageManager.ShowMsgBox("INP-241016");
                            if (DialogResult.Yes == result)
                            {
                                if ((this.radioBtnBackup.Checked && (this.comboBoxBackupPaths.Items.Count > 0)) && ((this.comboBoxBackupPaths.SelectedIndex >= 0) && !string.IsNullOrEmpty(this.IsWPath(this.comboBoxBackupPaths.SelectedValue.ToString()))))
                                {
                                    DingYiZhiFuChuan.strParadoxPath = Path.Combine(Path.GetTempPath(), "DATABASE");
                                    this.InitializeTreeView();
                                }
                                this.radioBtnOldDBInfo.Enabled = false;
                                this.radioBtnBackup.Enabled = false;
                                this.comboBoxBackupPaths.Enabled = false;
                                this.SetEnableClose(false);
                                this.btn_Select_DB.Enabled = false;
                                this.btn_Cancle.Enabled = false;
                                this.btn_Copy_WanCheng.Enabled = false;
                                this.SetEnabledRad_FuGai_TiaoGuo(false);
                                this.SetShowProgressBar(true);
                                this._IsFuZhiState = true;
                                this.isNoCompleted = true;
                                this.StartFuZhi();
                                this.StartCycle();
                                this.StartRun();
                            }
                        }
                    }
                    else if (this.btn_Copy_WanCheng.Text.Equals(DingYiZhiFuChuan.strWanCheng))
                    {
                        base.DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void btn_Select_DB_Click(object sender, EventArgs e)
        {
            isJZKPFlag = false;
            isWorkDirSelected = false;
            FolderBrowserDialog dialog = new FolderBrowserDialog {
                Description = "请选择旧版开票软件DATABASE目录\n或者包含旧版数据的目录(例如：Work目录)"
            };
            if (DialogResult.OK == dialog.ShowDialog())
            {
                DingYiZhiFuChuan.strParadoxPath = dialog.SelectedPath;
                useSelfPathFlag = true;
                if (!string.IsNullOrEmpty(this.IsWPath(DingYiZhiFuChuan.strParadoxPath)))
                {
                    isWorkDirSelected = true;
                    DingYiZhiFuChuan.strShowParadoxPath = DingYiZhiFuChuan.strParadoxPath;
                    DingYiZhiFuChuan.strParadoxPath = Path.Combine(Path.GetTempPath(), "DATABASE");
                }
                this.InitializeStatement();
                this.InitializeTreeView();
                this.InitBackupPaths();
            }
        }

        private void comboBoxBackupPaths_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxBackupPaths.Enabled && (((this.comboBoxBackupPaths.Items.Count > 0) && (this.comboBoxBackupPaths.SelectedIndex >= 0)) && !string.IsNullOrEmpty(this.IsWPath(this.comboBoxBackupPaths.SelectedValue.ToString()))))
            {
                string strParadoxPath = DingYiZhiFuChuan.strParadoxPath;
                DingYiZhiFuChuan.strParadoxPath = Path.Combine(Path.GetTempPath(), "DATABASE");
                this.InitializeTreeView();
                DingYiZhiFuChuan.strParadoxPath = strParadoxPath;
            }
        }

        public bool CopyData_XML()
        {
            try
            {
                return this._OperXML.Run(this.rad_FuGai.Checked);
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
            }
        }

        private static string CreateTempDATABASE(string srcPath, string desPath)
        {
            try
            {
                DirectoryInfo[] directories = new DirectoryInfo(srcPath).GetDirectories();
                List<string> list = new List<string>();
                foreach (DirectoryInfo info2 in directories)
                {
                    list.Add(info2.Name);
                }
                foreach (string str in list)
                {
                    string path = Path.Combine(srcPath, str);
                    string str3 = string.Empty;
                    if ((str != "Arg") && (str != "Work"))
                    {
                        str3 = Path.Combine(desPath, @"DATABASE\DEFAULT\Backup", str, @"Default\Work");
                    }
                    else
                    {
                        str3 = Path.Combine(desPath, @"DATABASE\DEFAULT", str.ToUpper());
                    }
                    if (!Directory.Exists(str3))
                    {
                        Directory.CreateDirectory(str3);
                    }
                    DirectoryInfo info3 = new DirectoryInfo(path);
                    foreach (FileInfo info4 in info3.GetFiles("*.*"))
                    {
                        string destFileName = Path.Combine(str3, info4.Name);
                        File.Copy(info4.FullName, destFileName, true);
                    }
                }
                return Path.Combine(desPath, "DATABASE");
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private void DataMigrationToolForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                DingYiZhiFuChuan.strShowParadoxPath = string.Empty;
                isWorkDirSelected = false;
                isJZKPFlag = false;
                DingYiZhiFuChuan.strParadoxPath = string.Empty;
                useSelfPathFlag = false;
                _listExistTbNames.Clear();
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void DataMigrationToolForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!this.IsFuZhiEnd)
                {
                    e.Cancel = true;
                }
                else if (this.btn_Copy_WanCheng.Text.Equals(DingYiZhiFuChuan.strWanCheng))
                {
                    base.DialogResult = DialogResult.OK;
                }
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private bool DeterLinkData()
        {
            try
            {
                string str = string.Empty;
                if (!this.LinkSQLite())
                {
                    MessageManager.ShowMsgBox("INP-241018");
                    _Loger.Error(MessageManager.GetMessageInfo("INP-241018"));
                    return false;
                }
                str = "链接SQLite成功。";
                _Loger.Info(str);
                if (!LinkParadox())
                {
                    MessageManager.ShowMsgBox("INP-241019");
                    _Loger.Error(MessageManager.GetMessageInfo("INP-241019"));
                    return false;
                }
                str = "链接Paradox成功。";
                _Loger.Info(str);
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
            }
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        [DllImport("user32.dll")]
        private static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);
        public void Flush_Lab_FuZhiing()
        {
            try
            {
                if (base.InvokeRequired)
                {
                    base.Invoke(new MethodInvoker(this.Flush_Lab_FuZhiing));
                }
                else
                {
                    string str = this._OperXML.listTableTypeAll[this._iIndexTable - 1];
                    if (0 > this._iIndexComma)
                    {
                        this._iIndexComma = 0;
                    }
                    else if (2 <= this._iIndexComma)
                    {
                        this._iIndexComma = 0;
                    }
                    else
                    {
                        this._iIndexComma++;
                    }
                    string str2 = "迁移:" + str + ":" + this.CurTableStatus + DingYiZhiFuChuan.strProgressBar[this._iIndexComma];
                    this.lab_FuZhiing.Text = str2;
                    this.lab_FuZhiing.Refresh();
                }
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void Flushcontrl(bool IsActionResult)
        {
            try
            {
                if (base.InvokeRequired)
                {
                    base.BeginInvoke(new delegateFlushcontrl(this.Flushcontrl), new object[] { IsActionResult });
                }
                else if (IsActionResult)
                {
                    this.btn_Copy_WanCheng.Text = DingYiZhiFuChuan.strWanCheng;
                    this.btn_Select_DB.Enabled = false;
                    this.btn_Cancle.Enabled = false;
                }
                else
                {
                    this.btn_Select_DB.Enabled = true;
                    this.btn_Cancle.Enabled = true;
                }
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        public static string GetParadoxConnString()
        {
            try
            {
                if (((existOldVerNum == 3) && !useSelfPathFlag) && ((Registry.LocalMachine.OpenSubKey(@"HKEY_LOCAL_MACHINE\SOFTWARE\航天信息\防伪开票\金税盘版集中开票软件") != null) || ("Exist!" != LocateParadoxPath(@"SOFTWARE\航天信息\防伪开票"))))
                {
                    existOldVerNum--;
                    DingYiZhiFuChuan.strParadoxPath = string.Empty;
                }
                if (((existOldVerNum == 2) && !useSelfPathFlag) && ("Exist!" != LocateParadoxPath(@"SOFTWARE\航天信息\税务代开")))
                {
                    existOldVerNum--;
                    DingYiZhiFuChuan.strParadoxPath = string.Empty;
                }
                if (((existOldVerNum == 1) && !useSelfPathFlag) && ("Exist!" != LocateParadoxPath(@"SOFTWARE\航天信息\集中开票")))
                {
                    existOldVerNum--;
                    DingYiZhiFuChuan.strParadoxPath = string.Empty;
                }
                if (useSelfPathFlag)
                {
                    string str = string.Empty;
                    if (!Directory.Exists(DingYiZhiFuChuan.strParadoxPath + @"\DEFAULT\WORK\"))
                    {
                        str = "自定义数据库路径不存在。";
                        _Loger.Error(str);
                        DingYiZhiFuChuan.strParadoxPath = string.Empty;
                        return string.Empty;
                    }
                    string str3 = MatchTaxCodeAndMachineXXFP();
                    if (("false" == str3) || (("NoRecord" == str3) && ("true" != MatchTaxCodeAndMachineXTZTXX())))
                    {
                        DingYiZhiFuChuan.strParadoxPath = string.Empty;
                        return string.Empty;
                    }
                }
                if ((existOldVerNum == 0) && !useSelfPathFlag)
                {
                    string str4 = "未设置数据库路径";
                    _Loger.Error(str4);
                    DingYiZhiFuChuan.strParadoxPath = string.Empty;
                    return string.Empty;
                }
                return ("Driver={Microsoft Paradox Driver (*.db )};DriverID=538;Fil=Paradox 5.X;DefaultDir=" + DingYiZhiFuChuan.strParadoxPath + @"\DEFAULT\WORK\;Dbq=" + DingYiZhiFuChuan.strParadoxPath + @"\DEFAULT\WORK\;CollatingSequence=ASCII;PWD=jIGGAe;");
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                return string.Empty;
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                return string.Empty;
            }
        }

        public string GetSQLiteConnString()
        {
            try
            {
                string startupPath = Application.StartupPath;
                string str3 = Path.Combine(startupPath.Substring(0, startupPath.LastIndexOf(@"\")), @"Bin\cc3268.dll");
                return string.Format("Data Source={0};Pooling=true;Password=LoveR1314;", str3);
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return string.Empty;
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return string.Empty;
            }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hwnd, bool bRevert);
        private void InitBackupPaths()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Path", typeof(string));
            if (!string.IsNullOrEmpty(DingYiZhiFuChuan.strParadoxPath) && DingYiZhiFuChuan.strParadoxPath.Trim(new char[] { '\\' }).EndsWith("DATABASE"))
            {
                string path = Path.Combine(DingYiZhiFuChuan.strParadoxPath, @"DEFAULT\Backup");
                if (!Directory.Exists(path))
                {
                    return;
                }
                foreach (string str2 in Directory.GetDirectories(path))
                {
                    string str3 = Path.Combine(str2, @"Default\Work");
                    if (Directory.Exists(str3))
                    {
                        FileInfo[] files = new DirectoryInfo(str3).GetFiles("*.DB");
                        List<string> list = new List<string>();
                        foreach (FileInfo info2 in files)
                        {
                            list.Add(info2.Name);
                        }
                        if ((list.Contains("销项发票.DB") && list.Contains("销项发票明细.DB")) && list.Contains("销项发票销货清单.DB"))
                        {
                            table.Rows.Add(new object[] { str2.Substring(str2.LastIndexOf('\\') + 1), str3 });
                        }
                    }
                }
            }
            if (table.Rows.Count > 0)
            {
                this.comboBoxBackupPaths.DisplayMember = "Name";
                this.comboBoxBackupPaths.ValueMember = "Path";
                this.comboBoxBackupPaths.DataSource = table;
                this.comboBoxBackupPaths.SelectedIndex = 0;
                this.radioBtnBackup.Enabled = true;
                if (this.radioBtnBackup.Checked)
                {
                    this.comboBoxBackupPaths.Enabled = true;
                }
                else
                {
                    this.comboBoxBackupPaths.Enabled = false;
                }
            }
            else
            {
                this.comboBoxBackupPaths.DataSource = null;
                this.radioBtnBackup.Enabled = false;
                this.comboBoxBackupPaths.Enabled = false;
            }
        }

        private static bool InitExistTbList()
        {
            _listExistTbNames.Clear();
            if (string.IsNullOrEmpty(DingYiZhiFuChuan.strParadoxPath))
            {
                return false;
            }
            string path = string.Empty;
            foreach (string str2 in DingYiZhiFuChuan.strTableNameCB)
            {
                if (str2 != "行政区域编码")
                {
                    path = DingYiZhiFuChuan.strParadoxPath + @"\DEFAULT\WORK\" + str2 + ".DB";
                }
                else
                {
                    path = DingYiZhiFuChuan.strParadoxPath + @"\SYSTEM\INFO\" + str2 + ".DB";
                }
                if (File.Exists(path) && !_listExistTbNames.Contains(str2))
                {
                    _listExistTbNames.Add(str2);
                }
            }
            return true;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(DataMigrationToolForm));
            this.imageList1 = new ImageList(this.components);
            this.toolTip_selDB = new ToolTip(this.components);
            this.btn_Select_DB = new AisinoBTN();
            this.treeView1 = new TreeView();
            this.progressBar2 = new AisinoPRG();
            this.groupBox1 = new AisinoGRP();
            this.rad_TiaoGuo = new AisinoRDO();
            this.rad_FuGai = new AisinoRDO();
            this.label1 = new AisinoLBL();
            this.lab_FuZhiing = new AisinoLBL();
            this.btn_Cancle = new AisinoBTN();
            this.btn_Copy_WanCheng = new AisinoBTN();
            this.splitContainer1 = new SplitContainer();
            this.splitContainer = new AisinoSPL();
            this.radioBtnBackup = new RadioButton();
            this.label3 = new Label();
            this.radioBtnOldDBInfo = new RadioButton();
            this.comboBoxBackupPaths = new ComboBox();
            this.groupBox1.SuspendLayout();
            this.splitContainer1.BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer.BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            base.SuspendLayout();
            this.imageList1.ImageStream = (ImageListStreamer) manager.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "CopyNot.png");
            this.imageList1.Images.SetKeyName(1, "CopyErr.png");
            this.imageList1.Images.SetKeyName(2, "Copied.png");
            this.btn_Select_DB.set_BackColorActive(Color.FromArgb(0x19, 0x76, 210));
            this.btn_Select_DB.set_ColorDefaultA(Color.FromArgb(0, 0xac, 0xfb));
            this.btn_Select_DB.set_ColorDefaultB(Color.FromArgb(0, 0x91, 0xe0));
            this.btn_Select_DB.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btn_Select_DB.set_FontColor(Color.FromArgb(0xff, 0xff, 0xff));
            this.btn_Select_DB.ForeColor = Color.White;
            this.btn_Select_DB.ImageAlign = ContentAlignment.MiddleLeft;
            this.btn_Select_DB.Location = new Point(0x19b, 0x26);
            this.btn_Select_DB.Name = "btn_Select_DB";
            this.btn_Select_DB.Size = new Size(0x60, 0x1c);
            this.btn_Select_DB.TabIndex = 0x4d;
            this.btn_Select_DB.Tag = "自定义数据库选择";
            this.btn_Select_DB.Text = "数据库选择";
            this.toolTip_selDB.SetToolTip(this.btn_Select_DB, "自定义数据库选择\r\n(旧版开票软件DATABASE目录或者Work备份目录)");
            this.btn_Select_DB.UseVisualStyleBackColor = true;
            this.btn_Select_DB.Click += new EventHandler(this.btn_Select_DB_Click);
            this.treeView1.Font = new Font("宋体", 10.25f);
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.ItemHeight = 0x15;
            this.treeView1.Location = new Point(0x1d, 0x1a);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new Size(0x164, 0xc6);
            this.treeView1.TabIndex = 0;
            this.progressBar2.Location = new Point(30, 0xe1);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new Size(0x164, 0x10);
            this.progressBar2.TabIndex = 0x49;
            this.groupBox1.BackColor = Color.Transparent;
            this.groupBox1.Controls.Add(this.rad_TiaoGuo);
            this.groupBox1.Controls.Add(this.rad_FuGai);
            this.groupBox1.Location = new Point(0x18b, 0x16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x65, 0x63);
            this.groupBox1.TabIndex = 0x44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "有相同数据时";
            this.rad_TiaoGuo.AutoSize = true;
            this.rad_TiaoGuo.Location = new Point(0x1a, 0x3d);
            this.rad_TiaoGuo.Name = "rad_TiaoGuo";
            this.rad_TiaoGuo.Size = new Size(0x2f, 0x10);
            this.rad_TiaoGuo.TabIndex = 1;
            this.rad_TiaoGuo.TabStop = true;
            this.rad_TiaoGuo.Text = "跳过";
            this.rad_TiaoGuo.UseVisualStyleBackColor = true;
            this.rad_FuGai.AutoSize = true;
            this.rad_FuGai.Checked = true;
            this.rad_FuGai.Location = new Point(0x1a, 0x1c);
            this.rad_FuGai.Name = "rad_FuGai";
            this.rad_FuGai.Size = new Size(0x2f, 0x10);
            this.rad_FuGai.TabIndex = 0;
            this.rad_FuGai.TabStop = true;
            this.rad_FuGai.Text = "覆盖";
            this.rad_FuGai.UseVisualStyleBackColor = true;
            this.label1.BackColor = Color.Transparent;
            this.label1.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = SystemColors.ActiveCaptionText;
            this.label1.Location = new Point(0x1d, 2);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x164, 0x13);
            this.label1.TabIndex = 0x41;
            this.label1.Text = "旧版数据表列表：";
            this.lab_FuZhiing.AutoSize = true;
            this.lab_FuZhiing.BackColor = Color.Transparent;
            this.lab_FuZhiing.Location = new Point(0x1b, 14);
            this.lab_FuZhiing.Name = "lab_FuZhiing";
            this.lab_FuZhiing.Size = new Size(0xc5, 12);
            this.lab_FuZhiing.TabIndex = 0x48;
            this.lab_FuZhiing.Text = "正在复制 {0} 到 {1} ({2}/{3})...";
            this.btn_Cancle.set_BackColorActive(Color.FromArgb(0x19, 0x76, 210));
            this.btn_Cancle.set_ColorDefaultA(Color.FromArgb(0, 0xac, 0xfb));
            this.btn_Cancle.set_ColorDefaultB(Color.FromArgb(0, 0x91, 0xe0));
            this.btn_Cancle.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btn_Cancle.set_FontColor(Color.FromArgb(0xff, 0xff, 0xff));
            this.btn_Cancle.ForeColor = Color.White;
            this.btn_Cancle.ImageAlign = ContentAlignment.MiddleLeft;
            this.btn_Cancle.Location = new Point(0x1a5, 6);
            this.btn_Cancle.Name = "btn_Cancle";
            this.btn_Cancle.Size = new Size(0x4b, 0x1c);
            this.btn_Cancle.TabIndex = 1;
            this.btn_Cancle.Text = "取消";
            this.btn_Cancle.UseVisualStyleBackColor = true;
            this.btn_Cancle.Click += new EventHandler(this.btn_Cancle_Click);
            this.btn_Copy_WanCheng.set_BackColorActive(Color.FromArgb(0x19, 0x76, 210));
            this.btn_Copy_WanCheng.set_ColorDefaultA(Color.FromArgb(0, 0xac, 0xfb));
            this.btn_Copy_WanCheng.set_ColorDefaultB(Color.FromArgb(0, 0x91, 0xe0));
            this.btn_Copy_WanCheng.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btn_Copy_WanCheng.set_FontColor(Color.FromArgb(0xff, 0xff, 0xff));
            this.btn_Copy_WanCheng.ForeColor = Color.White;
            this.btn_Copy_WanCheng.ImageAlign = ContentAlignment.MiddleLeft;
            this.btn_Copy_WanCheng.Location = new Point(340, 6);
            this.btn_Copy_WanCheng.Name = "btn_Copy_WanCheng";
            this.btn_Copy_WanCheng.Size = new Size(0x4b, 0x1c);
            this.btn_Copy_WanCheng.TabIndex = 0;
            this.btn_Copy_WanCheng.Text = "复制";
            this.btn_Copy_WanCheng.UseVisualStyleBackColor = true;
            this.btn_Copy_WanCheng.Click += new EventHandler(this.btn_Copy_WanCheng_Click);
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = Orientation.Horizontal;
            this.splitContainer1.Panel1.BackColor = Color.Transparent;
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Panel1.Controls.Add(this.progressBar2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel2.BackColor = SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add(this.lab_FuZhiing);
            this.splitContainer1.Panel2.Controls.Add(this.btn_Cancle);
            this.splitContainer1.Panel2.Controls.Add(this.btn_Copy_WanCheng);
            this.splitContainer1.Size = new Size(0x20b, 0x120);
            this.splitContainer1.SplitterDistance = 240;
            this.splitContainer1.TabIndex = 30;
            this.splitContainer.Dock = DockStyle.Fill;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = Orientation.Horizontal;
            this.splitContainer.Panel1.BackColor = SystemColors.Control;
            this.splitContainer.Panel1.Controls.Add(this.radioBtnBackup);
            this.splitContainer.Panel1.Controls.Add(this.label3);
            this.splitContainer.Panel1.Controls.Add(this.radioBtnOldDBInfo);
            this.splitContainer.Panel1.Controls.Add(this.comboBoxBackupPaths);
            this.splitContainer.Panel1.Controls.Add(this.btn_Select_DB);
            this.splitContainer.Panel2.BackColor = Color.White;
            this.splitContainer.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer.Size = new Size(0x20b, 0x1a6);
            this.splitContainer.SplitterDistance = 130;
            this.splitContainer.TabIndex = 0;
            this.splitContainer.TabStop = false;
            this.radioBtnBackup.AutoSize = true;
            this.radioBtnBackup.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.radioBtnBackup.ForeColor = SystemColors.GrayText;
            this.radioBtnBackup.Location = new Point(0x13, 0x60);
            this.radioBtnBackup.Name = "radioBtnBackup";
            this.radioBtnBackup.Size = new Size(0x7b, 0x12);
            this.radioBtnBackup.TabIndex = 0x53;
            this.radioBtnBackup.TabStop = true;
            this.radioBtnBackup.Text = "备份目录列表：";
            this.radioBtnBackup.UseVisualStyleBackColor = true;
            this.radioBtnBackup.Click += new EventHandler(this.radioButton_Click);
            this.label3.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label3.Location = new Point(0x1d, 9);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x16e, 0x13);
            this.label3.TabIndex = 0x52;
            this.label3.Text = "迁移数据源选择：";
            this.radioBtnOldDBInfo.Checked = true;
            this.radioBtnOldDBInfo.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.radioBtnOldDBInfo.Location = new Point(0x13, 0x1c);
            this.radioBtnOldDBInfo.Name = "radioBtnOldDBInfo";
            this.radioBtnOldDBInfo.Size = new Size(0x178, 0x3e);
            this.radioBtnOldDBInfo.TabIndex = 0x51;
            this.radioBtnOldDBInfo.TabStop = true;
            this.radioBtnOldDBInfo.Text = "旧版开票软件版本：v1.12\r\n旧版开票软件数据库位置：C:\\Program Files\\航天信息\\防伪开票\\DATABASE\r\n";
            this.radioBtnOldDBInfo.UseVisualStyleBackColor = true;
            this.radioBtnOldDBInfo.Click += new EventHandler(this.radioButton_Click);
            this.comboBoxBackupPaths.Enabled = false;
            this.comboBoxBackupPaths.FormattingEnabled = true;
            this.comboBoxBackupPaths.Location = new Point(0x94, 0x5f);
            this.comboBoxBackupPaths.Name = "comboBoxBackupPaths";
            this.comboBoxBackupPaths.Size = new Size(0xf7, 20);
            this.comboBoxBackupPaths.TabIndex = 80;
            this.comboBoxBackupPaths.SelectedIndexChanged += new EventHandler(this.comboBoxBackupPaths_SelectedIndexChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x20b, 0x1a6);
            base.Controls.Add(this.splitContainer);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "DataMigrationToolForm";
            this.Text = "旧版升级数据迁移工具";
            base.FormClosing += new FormClosingEventHandler(this.DataMigrationToolForm_FormClosing);
            base.FormClosed += new FormClosedEventHandler(this.DataMigrationToolForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.EndInit();
            this.splitContainer.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public void InitializeStatement()
        {
            try
            {
                if (string.IsNullOrEmpty(GetParadoxConnString()))
                {
                    DingYiZhiFuChuan.strVerOld = string.Empty;
                    DingYiZhiFuChuan.strParadoxPath = string.Empty;
                    isWorkDirSelected = false;
                    DingYiZhiFuChuan.strShowParadoxPath = string.Empty;
                }
                string str = DingYiZhiFuChuan.strStatement + "\n\n";
                str = string.Empty + ((DingYiZhiFuChuan.strVerOld == string.Empty) ? "" : (DingYiZhiFuChuan.strVerOldText + ((DingYiZhiFuChuan.strVerOld == string.Empty) ? "\n" : ("V" + DingYiZhiFuChuan.strVerOld + "\n"))));
                if (isWorkDirSelected || isJZKPFlag)
                {
                    str = str + DingYiZhiFuChuan.strParadoxPathText + ((DingYiZhiFuChuan.strShowParadoxPath == string.Empty) ? "不存在！" : (DingYiZhiFuChuan.strShowParadoxPath + "\n"));
                }
                else
                {
                    str = str + DingYiZhiFuChuan.strParadoxPathText + ((DingYiZhiFuChuan.strParadoxPath == string.Empty) ? "不存在！" : (DingYiZhiFuChuan.strParadoxPath + "\n"));
                }
                this.radioBtnOldDBInfo.Text = str;
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        public bool InitializeTreeView()
        {
            try
            {
                InitExistTbList();
                this.treeView1.Nodes.Clear();
                this._OperXML.GetTableTyp_TableNum();
                List<string> listTableTypeAll = this._OperXML.listTableTypeAll;
                int num = 0;
                foreach (string str in listTableTypeAll)
                {
                    TreeNode node = new TreeNode {
                        Text = str
                    };
                    if (str == "编码类表")
                    {
                        node.ForeColor = Color.Red;
                        node.Text = node.Text + "(总是清空)";
                    }
                    else
                    {
                        node.Text = node.Text + "(选项有效)";
                    }
                    node.ImageIndex = 0;
                    this.treeView1.Nodes.Add(node);
                    foreach (string str2 in this._OperXML.ListSubTable[num])
                    {
                        if (_listExistTbNames.Contains(str2))
                        {
                            TreeNode node2 = new TreeNode {
                                Text = str2,
                                ImageIndex = 0
                            };
                            node.Nodes.Add(node2);
                        }
                    }
                    num++;
                }
                if (0 >= listTableTypeAll.Count)
                {
                    MessageManager.ShowMsgBox("INP-441127");
                    return false;
                }
                return true;
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
            }
        }

        private string IsWPath(string path)
        {
            FileInfo[] files = new DirectoryInfo(path).GetFiles("*.*");
            List<string> list = new List<string>();
            foreach (FileInfo info2 in files)
            {
                list.Add(info2.Name);
            }
            if ((!list.Contains("销项发票.DB") || !list.Contains("销项发票明细.DB")) || !list.Contains("销项发票销货清单.DB"))
            {
                return string.Empty;
            }
            string str = Path.Combine(Path.GetTempPath(), @"DATABASE\DEFAULT\WORK");
            if (!Directory.Exists(str))
            {
                Directory.CreateDirectory(str);
            }
            foreach (FileInfo info3 in files)
            {
                if ((info3.Name.Contains("销项发票") || info3.Name.Contains("销项发票明细")) || info3.Name.Contains("销项发票销货清单"))
                {
                    string destFileName = Path.Combine(str, info3.Name);
                    File.Copy(info3.FullName, destFileName, true);
                }
            }
            return str;
        }

        public static bool LinkParadox()
        {
            try
            {
                string paradoxConnString = GetParadoxConnString();
                if (string.IsNullOrEmpty(paradoxConnString))
                {
                    return false;
                }
                ParadoxHelper.ConnValue(paradoxConnString);
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
            }
            return true;
        }

        private bool LinkSQLite()
        {
            try
            {
                string sQLiteConnString = this.GetSQLiteConnString();
                if (string.IsNullOrEmpty(sQLiteConnString))
                {
                    return false;
                }
                SQLiteHelper.ConnValue(sQLiteConnString);
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
            }
            return true;
        }

        private static string LocateParadoxPath(string strRegKP)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(strRegKP + @"\路径");
            string str = string.Empty;
            if (key == null)
            {
                return string.Empty;
            }
            DingYiZhiFuChuan.strParadoxPath = (string) key.GetValue("");
            if (DingYiZhiFuChuan.strParadoxPath.EndsWith("集中开票"))
            {
                string str2 = SearchSuit(DingYiZhiFuChuan.strParadoxPath);
                if (!string.IsNullOrEmpty(str2))
                {
                    string desPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                    string str4 = CreateTempDATABASE(str2, desPath);
                    if (!string.IsNullOrEmpty(str4))
                    {
                        DingYiZhiFuChuan.strParadoxPath = str4;
                        isJZKPFlag = true;
                        DingYiZhiFuChuan.strShowParadoxPath = str2;
                    }
                    else
                    {
                        DingYiZhiFuChuan.strParadoxPath = string.Empty;
                    }
                }
            }
            else
            {
                DingYiZhiFuChuan.strParadoxPath = DingYiZhiFuChuan.strParadoxPath + @"\DATABASE";
            }
            if (string.IsNullOrEmpty(DingYiZhiFuChuan.strParadoxPath))
            {
                return string.Empty;
            }
            if (!Directory.Exists(DingYiZhiFuChuan.strParadoxPath))
            {
                return string.Empty;
            }
            if (!Directory.Exists(DingYiZhiFuChuan.strParadoxPath + @"\DEFAULT\WORK\"))
            {
                return string.Empty;
            }
            string str6 = MatchTaxCodeAndMachineXXFP();
            if (("false" == str6) || (("NoRecord" == str6) && ("true" != MatchTaxCodeAndMachineXTZTXX())))
            {
                str = "数据库税号或开票机号与现有软件不匹配。";
                _Loger.Error(str);
                return string.Empty;
            }
            RegistryKey key2 = Registry.LocalMachine.OpenSubKey(strRegKP + @"\版本号");
            if (key2 == null)
            {
                DingYiZhiFuChuan.strVerOld = string.Empty;
                return string.Empty;
            }
            DingYiZhiFuChuan.strVerOld = (string) key2.GetValue("");
            if (!strRegKP.EndsWith("集中开票"))
            {
                RegistryKey key3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\kp.exe");
                if (key3 == null)
                {
                    return string.Empty;
                }
                string str7 = (string) key3.GetValue("code");
                if ((str7 != taxCard.get_TaxCode()) && (str7 != taxCard.get_CompressCode()))
                {
                    _Loger.Error("新旧版税号不一致，数据迁移不可用！");
                    return string.Empty;
                }
                string str8 = (string) key3.GetValue("machine");
                if (str8 != taxCard.get_Machine().ToString())
                {
                    _Loger.Error("新旧版税号一致，但开票机号不一致，数据迁移不可用！");
                    return string.Empty;
                }
            }
            return "Exist!";
        }

        private static string MatchTaxCodeAndMachineXTZTXX()
        {
            string str = "系统状态信息";
            string safeSql = "select * from " + str;
            string str3 = DingYiZhiFuChuan.strParadoxPath + @"\SYSTEM\SUIT\";
            string connectionString = "Driver={Microsoft Paradox Driver (*.db )};DriverID=538;Fil=Paradox 5.X;DefaultDir=" + str3 + ";Dbq=" + str3 + ";CollatingSequence=ASCII;";
            if (!File.Exists(str3 + str + ".DB"))
            {
                string str6 = "系统状态信息表不存在。无法判断数据库税号和开票机号,\n请检查所选数据库是否完整！";
                _Loger.Error(str6);
                return "false";
            }
            ParadoxHelperNoStatic @static = new ParadoxHelperNoStatic();
            if (@static.ConnValue(connectionString) == null)
            {
                _Loger.Error("Paradox数据库连接失败!");
                return "false";
            }
            DataTable dataSet = @static.GetDataSet(safeSql);
            if (dataSet == null)
            {
                _Loger.Error("Paradox数据库读取失败!");
                return "false";
            }
            DataColumnCollection columns = dataSet.Columns;
            DataRowCollection rows = dataSet.Rows;
            if (rows.Count > 0)
            {
                string str7 = ShareMethod.getString(rows[0][columns["税号"]]);
                string str8 = ShareMethod.getString(rows[0][columns["开票机号"]]);
                if ((str7 != taxCard.get_TaxCode()) && (str7 != taxCard.get_CorpCode()))
                {
                    string str9 = "系统状态信息表：所选数据库对应的税号与现有开票软件不一致，\n请选择同一税号的旧版数据库！";
                    if (useSelfPathFlag)
                    {
                        MessageBox.Show(str9, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    _Loger.Error(str9);
                    return "false";
                }
                if (!(str8 != taxCard.get_Machine().ToString()))
                {
                    return "true";
                }
                string text = "系统状态信息表：所选数据库对应的开票机号与现有开票软件不一致，\n请选择同一开票机号的旧版数据库！";
                if (useSelfPathFlag)
                {
                    MessageBox.Show(text, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                _Loger.Error(text);
                return "false";
            }
            string str11 = "系统状态信息表：表中无记录，无法判断数据库税号和开票机号";
            _Loger.Error(str11);
            return "NoRecord";
        }

        private static string MatchTaxCodeAndMachineXXFP()
        {
            string str = "销项发票";
            string safeSql = "select * from " + str;
            string str3 = DingYiZhiFuChuan.strParadoxPath + @"\DEFAULT\WORK\";
            string str4 = string.Empty;
            if (DingYiZhiFuChuan.strParadoxPath.Contains(""))
            {
                str4 = "防伪开票";
            }
            else
            {
                str4 = "税务代开";
            }
            string connectionString = "Driver={Microsoft Paradox Driver (*.db )};DriverID=538;Fil=Paradox 5.X;DefaultDir=" + str3 + ";Dbq=" + str3 + ";CollatingSequence=ASCII;PWD=jIGGAe;";
            if (!File.Exists(str3 + str + ".DB"))
            {
                string str7 = str4 + ":\n销项发票表不存在。无法判断数据库税号和开票机号,\n请检查所选数据库是否完整！";
                _Loger.Error(str7);
                return "NoRecord";
            }
            try
            {
                ParadoxHelperNoStatic @static = new ParadoxHelperNoStatic();
                if (@static.ConnValue(connectionString) == null)
                {
                    _Loger.Error("Paradox数据库连接失败!");
                    if (useSelfPathFlag)
                    {
                        MessageBoxHelper.Show(str4 + ":\nParadox数据库连接失败!\n请尝试安装旧版软件或BDE数据库驱动程序。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand);
                    }
                    return "false";
                }
                DataTable dataSet = @static.GetDataSet(safeSql);
                if (dataSet == null)
                {
                    _Loger.Error("Paradox数据库读取失败!");
                    if (useSelfPathFlag)
                    {
                        MessageBoxHelper.Show(str4 + ":\nParadox数据库连接失败!\n请尝试安装旧版软件或BDE数据库驱动程序。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand);
                    }
                    return "false";
                }
                DataColumnCollection columns = dataSet.Columns;
                DataRowCollection rows = dataSet.Rows;
                if (rows.Count > 0)
                {
                    if (!columns.Contains("销方税号") || !columns.Contains("开票机号"))
                    {
                        _Loger.Error(str4 + ":\n销项发票表不存在销方税号和开票机号字段。");
                        return "false";
                    }
                    string str8 = ShareMethod.getString(rows[0][columns["销方税号"]]);
                    string str9 = ShareMethod.getString(rows[0][columns["开票机号"]]);
                    if ((str8 != taxCard.get_TaxCode()) && (str8 != taxCard.get_CompressCode()))
                    {
                        string str10 = str4 + ":\n数据库对应的税号与现有开票软件不一致，\n请选择同一税号的旧版数据库！";
                        _Loger.Error(str10);
                        if (useSelfPathFlag)
                        {
                            MessageBoxHelper.Show(str10, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        return "false";
                    }
                    if (!(str9 != taxCard.get_Machine().ToString()))
                    {
                        return "true";
                    }
                    string str11 = str4 + ":\n数据库对应的开票机号与现有开票软件不一致，\n请选择同一开票机号的旧版数据库！";
                    _Loger.Error(str11);
                    if (useSelfPathFlag)
                    {
                        MessageBoxHelper.Show(str11, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    return "false";
                }
                string str12 = str4 + "销项发票表:\n表中无记录，无法判断数据库税号和开票机号";
                _Loger.Error(str12);
                return "NoRecord";
            }
            catch (Exception exception)
            {
                _Loger.Error(exception.Message);
                if (exception.Message == "ERROR [HY000] [Microsoft][ODBC Paradox 驱动程序] 外部数据库驱动程序 (12034) 中的意外错误。")
                {
                    MessageBoxHelper.Show("数据库读取错误，请确认已关闭旧版软件或使用旧版软件修复数据库。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand);
                }
                else if (exception.Message == "ERROR [HY000] [Microsoft][ODBC Paradox 驱动程序] 外部表不是预期的格式。")
                {
                    MessageBoxHelper.Show("数据库读取错误，请确认已关闭旧版软件；\n若未安装旧版软件，请尝试安装旧版软件或BDE数据库驱动程序。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand);
                }
                else
                {
                    ExceptionHandler.HandleError(exception);
                }
                return "false";
            }
        }

        private void radioButton_Click(object sender, EventArgs e)
        {
            if (this.radioBtnOldDBInfo.Checked)
            {
                this.radioBtnOldDBInfo.ForeColor = Color.Black;
                if (!string.IsNullOrEmpty(DingYiZhiFuChuan.strParadoxPath))
                {
                    this.InitializeTreeView();
                }
            }
            else
            {
                this.radioBtnOldDBInfo.ForeColor = Color.Gray;
            }
            if (this.radioBtnBackup.Checked)
            {
                this.radioBtnBackup.ForeColor = Color.Black;
                if (this.comboBoxBackupPaths.Items.Count <= 0)
                {
                    this.comboBoxBackupPaths.Enabled = false;
                }
                else
                {
                    this.comboBoxBackupPaths.Enabled = true;
                    if ((this.comboBoxBackupPaths.SelectedIndex >= 0) && !string.IsNullOrEmpty(this.IsWPath(this.comboBoxBackupPaths.SelectedValue.ToString())))
                    {
                        string strParadoxPath = DingYiZhiFuChuan.strParadoxPath;
                        DingYiZhiFuChuan.strParadoxPath = Path.Combine(Path.GetTempPath(), "DATABASE");
                        this.InitializeTreeView();
                        DingYiZhiFuChuan.strParadoxPath = strParadoxPath;
                    }
                }
            }
            else
            {
                this.radioBtnBackup.ForeColor = Color.Gray;
                this.comboBoxBackupPaths.Enabled = false;
            }
        }

        public bool Run()
        {
            try
            {
                return this.CopyData_XML();
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
            }
        }

        private void RunProgress()
        {
            try
            {
                if (this.runThread.ThreadState == ThreadState.Running)
                {
                    this.IsRunResult = this.Run();
                    this.isNoCompleted = false;
                }
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private static string SearchSuit(string path)
        {
            string str = "帐套路径表";
            string safeSql = "select * from " + str + " where 纳税人识别号 = '" + taxCard.get_TaxCode() + "'";
            string str3 = path + @"\DATABASE\SYSTEM\SUIT\";
            string connectionString = "Driver={Microsoft Paradox Driver (*.db )};DriverID=538;Fil=Paradox 5.X;DefaultDir=" + str3 + ";Dbq=" + str3 + ";CollatingSequence=ASCII;PWD=jIGGAe;";
            if (!File.Exists(str3 + str + ".DB"))
            {
                string str6 = "数据迁移-集中开票:\n帐套路径表不存在。无法查找账套对应的数据库,\n请检查所选数据库是否完整！";
                _Loger.Error(str6);
                if (useSelfPathFlag)
                {
                    MessageBoxHelper.Show(str6, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                return string.Empty;
            }
            try
            {
                ParadoxHelperNoStatic @static = new ParadoxHelperNoStatic();
                if (@static.ConnValue(connectionString) == null)
                {
                    _Loger.Error("Paradox数据库连接失败!");
                    if (useSelfPathFlag)
                    {
                        MessageBoxHelper.Show("数据迁移-集中开票:\nParadox数据库连接失败!\n请尝试安装旧版软件或BDE数据库驱动程序。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand);
                    }
                    return string.Empty;
                }
                DataTable dataSet = @static.GetDataSet(safeSql);
                if (dataSet == null)
                {
                    _Loger.Error("Paradox数据库读取失败!");
                    if (useSelfPathFlag)
                    {
                        MessageBoxHelper.Show("数据迁移-集中开票:\nParadox数据库连接失败!\n请尝试安装旧版软件或BDE数据库驱动程序。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand);
                    }
                    return string.Empty;
                }
                DataColumnCollection columns = dataSet.Columns;
                DataRowCollection rows = dataSet.Rows;
                if (rows.Count > 0)
                {
                    if (!columns.Contains("帐套路径") || !columns.Contains("纳税人识别号"))
                    {
                        _Loger.Error("数据迁移-集中开票:\n销项发票表不存在帐套路径和纳税人识别号。");
                        return string.Empty;
                    }
                    string str7 = ShareMethod.getString(rows[0][columns["帐套路径"]]);
                    ShareMethod.getString(rows[0][columns["纳税人识别号"]]);
                    return str7;
                }
                string str8 = "数据迁移-集中开票:\n无法判断数据库帐套路径和纳税人识别号,\n请检查所选数据库是否完整！";
                _Loger.Error(str8);
                if (useSelfPathFlag)
                {
                    MessageBoxHelper.Show(str8, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                return string.Empty;
            }
            catch (Exception exception)
            {
                _Loger.Error(exception.Message);
                if (exception.Message == "ERROR [HY000] [Microsoft][ODBC Paradox 驱动程序] 外部数据库驱动程序 (12034) 中的意外错误。")
                {
                    MessageBoxHelper.Show("数据库读取错误，请确认已关闭旧版软件或使用旧版软件修复数据库。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand);
                }
                else if (exception.Message == "ERROR [HY000] [Microsoft][ODBC Paradox 驱动程序] 外部表不是预期的格式。")
                {
                    MessageBoxHelper.Show("数据库读取错误，请确认已关闭旧版软件；\n若未安装旧版软件，请尝试安装旧版软件或BDE数据库驱动程序。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand);
                }
                else
                {
                    ExceptionHandler.HandleError(exception);
                }
                return string.Empty;
            }
        }

        private void SetEnableClose(bool IsEnable)
        {
            try
            {
                if (base.InvokeRequired)
                {
                    base.BeginInvoke(new delegateSetEnableClose(this.SetEnableClose), new object[] { IsEnable });
                }
                else
                {
                    IntPtr systemMenu = GetSystemMenu(base.Handle, false);
                    if (IsEnable)
                    {
                        if (systemMenu != IntPtr.Zero)
                        {
                            EnableMenuItem(systemMenu, 0xf060, 0);
                        }
                    }
                    else if (systemMenu != IntPtr.Zero)
                    {
                        EnableMenuItem(systemMenu, 0xf060, 3);
                    }
                }
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void SetEnableCopy_WanCheng(bool IsEnable)
        {
            try
            {
                if (base.InvokeRequired)
                {
                    base.BeginInvoke(new delegateSetEnableCopy_WanCheng(this.SetEnableCopy_WanCheng), new object[] { IsEnable });
                }
                else
                {
                    this.btn_Copy_WanCheng.Enabled = IsEnable;
                }
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void SetEnabledRad_FuGai_TiaoGuo(bool IsEnabled)
        {
            try
            {
                this.rad_FuGai.Enabled = IsEnabled;
                this.rad_TiaoGuo.Enabled = IsEnabled;
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        public void SetFuZhiBtnV(bool isVisible)
        {
            this.btn_Select_DB.Visible = isVisible;
        }

        public void SetProgressBarPosition(int iIndexTable, int iFlag)
        {
            try
            {
                if (base.InvokeRequired)
                {
                    base.BeginInvoke(new delegateSetProgressBarPositionNew(this.SetProgressBarPosition), new object[] { iIndexTable, iFlag });
                }
                else
                {
                    this._iIndexTable = iIndexTable;
                    int num = iIndexTable - 1;
                    int num2 = this.progressBar2.Value;
                    if (iFlag == 0)
                    {
                        num2 = 0;
                    }
                    else
                    {
                        num2++;
                    }
                    string local1 = this._OperXML.listTableTypeAll[num];
                    int num3 = this._OperXML.listTableNum[num];
                    if (num3 != this.progressBar2.Maximum)
                    {
                        this.progressBar2.Maximum = num3;
                    }
                    if (num2 <= this.progressBar2.Maximum)
                    {
                        this.progressBar2.Value = num2;
                    }
                }
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        public void SetSelectTreeViewTableNode(string curTableName, bool isDone)
        {
            try
            {
                if (base.InvokeRequired)
                {
                    base.BeginInvoke(new delegateSetSelectTreeViewTableNode(this.SetSelectTreeViewTableNode), new object[] { curTableName, isDone });
                }
                else
                {
                    foreach (TreeNode node in this.treeView1.SelectedNode.Nodes)
                    {
                        if ((node.Text == curTableName) && isDone)
                        {
                            node.ImageIndex = 2;
                            node.SelectedImageIndex = 2;
                        }
                        if ((node.Text == curTableName) && !isDone)
                        {
                            node.ImageIndex = 1;
                            node.SelectedImageIndex = 1;
                        }
                    }
                }
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        public void SetSelectTreeViewTopNode(string curNodeName)
        {
            try
            {
                if (base.InvokeRequired)
                {
                    base.BeginInvoke(new delegateSetSelectTreeViewTopNode(this.SetSelectTreeViewTopNode), new object[] { curNodeName });
                }
                else
                {
                    foreach (TreeNode node in this.treeView1.Nodes)
                    {
                        if (node.Text == curNodeName)
                        {
                            this.treeView1.SelectedNode = node;
                            this.treeView1.SelectedNode.Expand();
                            node.ImageIndex = 2;
                            node.SelectedImageIndex = 2;
                        }
                    }
                }
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void SetShowProgressBar(bool IsShow)
        {
            try
            {
                if (base.InvokeRequired)
                {
                    base.BeginInvoke(new delegateSetShowProgressBar(this.SetShowProgressBar), new object[] { IsShow });
                }
                else
                {
                    this.lab_FuZhiing.Visible = IsShow;
                    this.progressBar2.Visible = IsShow;
                }
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        public void StartCycle()
        {
            try
            {
                this.isNoCompleted = true;
                if (this.cycleThread == null)
                {
                    this.cycleThread = new Thread(new ThreadStart(this.UpdateCycleProgress));
                }
                this.cycleThread.Start();
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        public void StartFuZhi()
        {
            try
            {
                this.isNoCompleted = true;
                if (this.fuZhiThread == null)
                {
                    this.fuZhiThread = new Thread(new ThreadStart(this.UpdateLab_FuZhi));
                }
                this.fuZhiThread.Start();
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        public void StartRun()
        {
            try
            {
                this.isNoCompleted = true;
                if (this.runThread == null)
                {
                    this.runThread = new Thread(new ThreadStart(this.RunProgress));
                }
                this.runThread.Start();
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        public void UpdateCurTableStatus(string strStatus)
        {
            this.CurTableStatus = strStatus;
        }

        private void UpdateCycleProgress()
        {
            try
            {
                this.IsFuZhiEnd = false;
                if (this.cycleThread.ThreadState == ThreadState.Running)
                {
                    goto Label_0020;
                }
                return;
            Label_0016:
                Thread.Sleep(200);
            Label_0020:
                if (this.isNoCompleted)
                {
                    goto Label_0016;
                }
                this.Flushcontrl(this.IsRunResult);
                this._IsFuZhiState = false;
                this.SetShowProgressBar(false);
                this.IsFuZhiEnd = true;
                this.SetEnableCopy_WanCheng(true);
                this.SetEnableClose(true);
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void UpdateLab_FuZhi()
        {
            try
            {
                if (this.fuZhiThread.ThreadState == ThreadState.Running)
                {
                    goto Label_001F;
                }
                return;
            Label_000F:
                this.Flush_Lab_FuZhiing();
                Thread.Sleep(500);
            Label_001F:
                if (this.isNoCompleted)
                {
                    goto Label_000F;
                }
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        public delegate void delegateFlushcontrl(bool IsActionResult);

        public delegate void delegateSetEnableClose(bool IsEnable);

        public delegate void delegateSetEnableCopy_WanCheng(bool IsEnable);

        public delegate void delegateSetProgressBarPositionNew(int iIndexTable, int iFlag);

        public delegate void delegateSetSelectTreeViewTableNode(string curTableName, bool isDone);

        public delegate void delegateSetSelectTreeViewTopNode(string curNodeName);

        public delegate void delegateSetShowProgressBar(bool IsShow);

        public delegate void delegateUpdateCycleProgress();
    }
}

