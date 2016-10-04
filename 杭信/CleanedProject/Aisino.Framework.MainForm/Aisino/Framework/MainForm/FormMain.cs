namespace Aisino.Framework.MainForm
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Menu;
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using Aisino.Framework.Plugin.Core.Toolbar;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.Startup.Login;
    using InternetWare.Form;
    using ns4;
    using ns5;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Net.NetworkInformation;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class FormMain : DockForm
    {
        private BackgroundWorker backgroundWorker_0;
        private BackgroundWorker backgroundWorker_1;
        private BackgroundWorker backgroundWorker_2;
        public static bool bContinuUpload;
        private bool bool_0;
        private bool bool_1;
        private bool bool_2;
        private bool bool_3;
        private static bool bool_4;
        private Class105 class105_0;
        private Class85 class85_0;
        public static Control control_0;
        private IContainer icontainer_3;
        private int int_0;
        private int int_1;
        private int int_2;
        private ServerUP serverUP_0;
        private ToolStripStatusLabel stat1;
        private ToolStripStatusLabel stat2;
        private ToolStripStatusLabel stat3;
        private ToolStripStatusLabel stat4;
        private ToolStripStatusLabel stat5;
        private ToolStripStatusLabel stat6;
        private ToolStripStatusLabel stat7;
        private ToolStripStatusLabel stat8;
        protected StatusStrip statusStrip1;

        public static  event ExecuteAfterLoadDelegate ExecuteAfterLoadEvent;

        public static  event ExecuteAfterShowDelegate ExecuteAfterShowEvent;

        public static  event ExecuteBeforeExitDelegate ExecuteBeforeExitEvent;

        public static  event UpdateUserNameDelegate UpdateUserNameEvent;

        static FormMain()
        {
            
            bContinuUpload = false;
            bool_4 = false;
        }

        protected FormMain()
        {
            
            this.int_0 = 60;
            this.int_1 = 600;
            this.int_2 = Class87.int_6;
            this.serverUP_0 = new ServerUP();
            this.InitializeComponent_1();
            this.backgroundWorker_0.DoWork += new DoWorkEventHandler(this.backgroundWorker_0_DoWork);
            this.backgroundWorker_0.ProgressChanged += new ProgressChangedEventHandler(this.backgroundWorker_0_ProgressChanged);
            this.backgroundWorker_0.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker_0_RunWorkerCompleted);
            this.serverUP_0.TestIsServerUp();
            if (!Class87.bool_4)
            {
                this.backgroundWorker_1.WorkerReportsProgress = false;
                this.backgroundWorker_1.WorkerSupportsCancellation = true;
                this.backgroundWorker_1.DoWork += new DoWorkEventHandler(this.backgroundWorker_1_DoWork);
                this.backgroundWorker_1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker_1_RunWorkerCompleted);
                this.backgroundWorker_2.WorkerReportsProgress = false;
                this.backgroundWorker_2.WorkerSupportsCancellation = true;
                this.backgroundWorker_2.DoWork += new DoWorkEventHandler(this.backgroundWorker_2_DoWork);
                this.backgroundWorker_2.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker_2_RunWorkerCompleted);
            }
            else
            {
                this.backgroundWorker_1.WorkerReportsProgress = false;
                this.backgroundWorker_1.WorkerSupportsCancellation = true;
                this.backgroundWorker_1.DoWork += new DoWorkEventHandler(this.backgroundWorker_1_DoWork_1);
                this.backgroundWorker_1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker_1_RunWorkerCompleted_1);
            }
            base.TaxCardInstance.ChangeDateEvent += new EventHandler(this.method_3);
        }

        private void backgroundWorker_0_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DateTime now = DateTime.Now;
                while (!this.backgroundWorker_0.CancellationPending)
                {
                    if (now.AddSeconds((double) this.int_0) < DateTime.Now)
                    {
                        Thread.Sleep(0x3e8);
                    }
                    else
                    {
                        now = DateTime.Now;
                        string userState = string.Empty;
                        if (this.method_7())
                        {
                            userState = "在线";
                        }
                        else
                        {
                            userState = "离线";
                        }
                        this.backgroundWorker_0.ReportProgress(1, userState);
                    }
                }
                e.Cancel = true;
                this.bool_2 = true;
                BackWorkCancleStyle style = new BackWorkCancleStyle {
                    CanclType = CancleStyle.Cancle
                };
                e.Result = style;
            }
            catch (Exception)
            {
                e.Cancel = true;
                this.bool_2 = true;
                BackWorkCancleStyle style2 = new BackWorkCancleStyle {
                    CanclType = CancleStyle.Exception
                };
                e.Result = style2;
            }
        }

        private void backgroundWorker_0_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.SetInfoStatus(e.UserState.ToString());
        }

        private void backgroundWorker_0_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.SetInfoStatus("");
        }

        private void backgroundWorker_1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (base.TaxCardInstance.QYLX.ISTDQY)
                {
                    this.bool_0 = true;
                }
                else
                {
                    this.bool_0 = false;
                    bool flag2 = true;
                    DateTime now = DateTime.Now;
                    bool flag = true;
                    bool shouGongShangChuan = false;
                    this.class85_0.class90_0.method_0();
                    while (!this.backgroundWorker_1.CancellationPending)
                    {
                        if (UpLoadCheckState.CheckState())
                        {
                            Thread.Sleep(0x3e8);
                        }
                        else
                        {
                            if ((!bContinuUpload && !flag2) && (!this.class85_0.method_4(now) && !UpLoadCheckState.ShouGongShangChuan))
                            {
                                Thread.Sleep(0x3e8);
                                continue;
                            }
                            if ((!bContinuUpload && !flag) && ((now.AddMinutes((double) (Class87.int_6 / 3)) > DateTime.Now) && !UpLoadCheckState.ShouGongShangChuan))
                            {
                                Thread.Sleep(0x3e8);
                                continue;
                            }
                            shouGongShangChuan = UpLoadCheckState.ShouGongShangChuan;
                            this.class85_0.method_1(false, flag2);
                            if (shouGongShangChuan)
                            {
                                UpLoadCheckState.ShouGongShangChuan = false;
                                shouGongShangChuan = false;
                            }
                            flag = this.class85_0.method_6();
                            now = DateTime.Now;
                            flag2 = false;
                            if (bContinuUpload)
                            {
                                bContinuUpload = false;
                            }
                        }
                    }
                    e.Cancel = true;
                    this.bool_0 = true;
                    BackWorkCancleStyle style = new BackWorkCancleStyle {
                        CanclType = CancleStyle.Cancle
                    };
                    e.Result = style;
                    Class101.smethod_0("(上传线程)线程终止，软件准备退出！");
                }
            }
            catch (Exception exception)
            {
                e.Cancel = true;
                BackWorkCancleStyle style2 = new BackWorkCancleStyle {
                    CanclType = CancleStyle.Exception
                };
                this.bool_0 = true;
                e.Result = style2;
                Class101.smethod_1("(上传线程异常：)" + exception.ToString());
            }
        }

        private void backgroundWorker_1_DoWork_1(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (base.TaxCardInstance.QYLX.ISTDQY)
                {
                    this.bool_0 = true;
                }
                else
                {
                    this.bool_0 = false;
                    this.class85_0.class90_0.method_0();
                    while (!this.backgroundWorker_1.CancellationPending)
                    {
                        if (UpLoadCheckState.CheckState())
                        {
                            Thread.Sleep(0x3e8);
                        }
                        else
                        {
                            this.serverUP_0.UPDownLoadFP();
                            Thread.Sleep(0x3e8);
                        }
                    }
                    e.Cancel = true;
                    this.bool_0 = true;
                    BackWorkCancleStyle style = new BackWorkCancleStyle {
                        CanclType = CancleStyle.Cancle
                    };
                    e.Result = style;
                    Class101.smethod_0("(上传线程backWorkUploadForServerUp_DoWork)线程终止，软件准备退出！");
                }
            }
            catch (Exception exception)
            {
                e.Cancel = true;
                BackWorkCancleStyle style2 = new BackWorkCancleStyle {
                    CanclType = CancleStyle.Exception
                };
                this.bool_0 = true;
                e.Result = style2;
                Class101.smethod_1("(上传线程backWorkUploadForServerUp_DoWork异常：)" + exception.ToString());
            }
        }

        private void backgroundWorker_1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void backgroundWorker_1_RunWorkerCompleted_1(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void backgroundWorker_2_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (base.TaxCardInstance.QYLX.ISTDQY)
                {
                    this.bool_1 = true;
                }
                else
                {
                    this.bool_1 = false;
                    bool shouGongXiaZai = false;
                    DateTime now = DateTime.Now;
                    while (!this.backgroundWorker_2.CancellationPending)
                    {
                        if ((now.AddSeconds((double) this.int_2) > DateTime.Now) && !UpLoadCheckState.ShouGongXiaZai)
                        {
                            Thread.Sleep(0x3e8);
                        }
                        else
                        {
                            if (UpLoadCheckState.CheckState())
                            {
                                Thread.Sleep(0x3e8);
                                continue;
                            }
                            now = DateTime.Now;
                            shouGongXiaZai = UpLoadCheckState.ShouGongXiaZai;
                            this.class105_0.method_0();
                            if (shouGongXiaZai)
                            {
                                UpLoadCheckState.ShouGongXiaZai = false;
                                shouGongXiaZai = false;
                            }
                        }
                    }
                    e.Cancel = true;
                    this.bool_1 = true;
                    BackWorkCancleStyle style = new BackWorkCancleStyle {
                        CanclType = CancleStyle.Cancle
                    };
                    e.Result = style;
                    Class101.smethod_0("<下载线程>下载线程退出，准备软件退出！");
                }
            }
            catch (Exception exception)
            {
                e.Cancel = true;
                BackWorkCancleStyle style2 = new BackWorkCancleStyle {
                    CanclType = CancleStyle.Exception
                };
                this.bool_1 = false;
                e.Result = style2;
                Class101.smethod_1("(下载线程异常：)" + exception.ToString());
            }
        }

        private void backgroundWorker_2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        public static void CallUpload()
        {
            Class101.smethod_0("开始呼叫上传线程进行发票上传...");
            if (bContinuUpload)
            {
                Class101.smethod_0("上传线程处于呼叫中...");
            }
            else
            {
                try
                {
                    bContinuUpload = true;
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("呼叫上传线异常：" + exception.ToString());
                }
                Class101.smethod_0("呼叫上传线程进行发票上传结束...");
            }
        }

        public static void CloseForm()
        {
            try
            {
                if (control_0 is Form)
                {
                    (control_0 as Form).Close();
                }
                else
                {
                    (control_0.Parent as Form).Close();
                }
            }
            catch (Exception)
            {
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_3 != null))
            {
                this.icontainer_3.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!bool_4)
            {
                Thread.Sleep(0x7d0);
                Environment.Exit(0);
            }
            bool_4 = false;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!e.Cancel)
            {
                this.bool_3 = true;
                MessageHelper.MsgWait("正在关闭系统，请您耐心等待，不要关闭电源或拔掉金税盘。");
                Thread.Sleep(500);
                try
                {
                    MessageHelper.MsgWait("正在关闭系统，请您耐心等待，不要关闭电源或拔掉金税盘：保存用户信息。");
                    PropertyUtil.Save();
                    if (base.TaxCardInstance.QYLX.ISTDQY)
                    {
                        goto Label_00DA;
                    }
                    MessageHelper.MsgWait("正在关闭系统，请您耐心等待，不要关闭电源或拔掉金税盘：关闭系统执行中的线程。");
                    this.backgroundWorker_0.CancelAsync();
                    this.backgroundWorker_1.CancelAsync();
                    if (!Class87.bool_4)
                    {
                        this.backgroundWorker_2.CancelAsync();
                    }
                    while (!Class87.bool_4)
                    {
                        if ((this.bool_0 && this.bool_1) && this.bool_2)
                        {
                            goto Label_00C3;
                        }
                        Thread.Sleep(0x3e8);
                        continue;
                    }
                    if (this.bool_0 && this.bool_2)
                    {
                        goto Label_00DA;
                    }
                    Thread.Sleep(0x3e8);
                    Label_00C3:
                    MessageHelper.MsgWait("正在关闭系统，请您耐心等待，不要关闭电源或拔掉金税盘：上传离线发票。");
                    this.class85_0.method_1(true, false);
                Label_00DA:
                    MessageHelper.MsgWait("正在关闭系统，请您耐心等待，不要关闭电源或拔掉金税盘：执行数据备份等操作。");
                    if (ExecuteAfterLoadEvent != null)
                    {
                        ExecuteAfterLoadEvent();
                    }
                    MessageHelper.MsgWait("正在关闭系统，请您耐心等待，不要关闭电源或拔掉金税盘：关闭数据库连接。");
                    BaseDAOFactory.GetBaseDAOSQLite().ClearResource();
                    MessageHelper.MsgWait("正在关闭系统，请您耐心等待，不要关闭电源或拔掉金税盘：关闭金税盘。");
                    base.TaxCardInstance.TaxCardClose();
                    Thread.Sleep(500);
                    MessageHelper.MsgWait();
                }
                catch (Exception)
                {
                    Thread.Sleep(500);
                    MessageHelper.MsgWait();
                }
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.Text = ProductName + PropertyUtil.GetValue("MAIN_VER", "");
            if (ExecuteAfterLoadEvent != null)
            {
                ExecuteAfterLoadEvent();
            }
            MenuCommand.SetStatusTextEvent += new CoreStartup.SetStatusTextDelegate(this.method_5);
            MenuLabel.SetStatusTextEvent += new CoreStartup.SetStatusTextDelegate(this.method_5);
            MenuCheckBox.SetStatusTextEvent += new CoreStartup.SetStatusTextDelegate(this.method_5);
            Aisino.Framework.Plugin.Core.Toolbar.ToolBarButton.SetStatusTextEvent += new CoreStartup.SetStatusTextDelegate(this.method_5);
            ToolBarCommand.SetStatusTextEvent += new CoreStartup.SetStatusTextDelegate(this.method_5);
            ToolBarLabel.SetStatusTextEvent += new CoreStartup.SetStatusTextDelegate(this.method_5);
            CheckRegFileOutDate date = new CheckRegFileOutDate();
            date.Init();
            if (date.iTotalRegFiles > 0)
            {
                date.Show();
            }
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            if (ExecuteAfterLoadEvent != null)
            {
                ExecuteAfterLoadEvent();
            }
            this.method_6();
            this.class85_0 = new Class85();
            this.class105_0 = new Class105();
            try
            {
                if (!base.TaxCardInstance.QYLX.ISTDQY)
                {
                    if (this.method_7())
                    {
                        this.SetInfoStatus("在线");
                    }
                    else
                    {
                        this.SetInfoStatus("离线");
                    }
                    this.backgroundWorker_0.RunWorkerAsync();
                    this.backgroundWorker_1.RunWorkerAsync();
                    this.backgroundWorker_2.RunWorkerAsync();
                }
            }
            catch (Exception)
            {
            }
            LodgingWindow win = new LodgingWindow();
            win.Show();
        }

        private void InitializeComponent_1()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormMain));
            this.statusStrip1 = new StatusStrip();
            this.stat1 = new ToolStripStatusLabel();
            this.stat2 = new ToolStripStatusLabel();
            this.stat3 = new ToolStripStatusLabel();
            this.stat4 = new ToolStripStatusLabel();
            this.stat5 = new ToolStripStatusLabel();
            this.stat6 = new ToolStripStatusLabel();
            this.stat7 = new ToolStripStatusLabel();
            this.stat8 = new ToolStripStatusLabel();
            this.backgroundWorker_0 = new BackgroundWorker();
            this.backgroundWorker_1 = new BackgroundWorker();
            this.backgroundWorker_2 = new BackgroundWorker();
            this.statusStrip1.SuspendLayout();
            base.SuspendLayout();
            this.statusStrip1.Items.AddRange(new ToolStripItem[] { this.stat1, this.stat2, this.stat3, this.stat4, this.stat5, this.stat6, this.stat7, this.stat8 });
            this.statusStrip1.LayoutStyle = ToolStripLayoutStyle.Flow;
            this.statusStrip1.Location = new Point(0, 0x228);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new Size(0x373, 0x15);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.TabStop = true;
            this.statusStrip1.Text = "statusStrip1";
            this.stat1.BorderSides = ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Left;
            this.stat1.Name = "stat1";
            this.stat1.Size = new Size(0x24, 0x15);
            this.stat1.Text = "税号";
            this.stat1.ToolTipText = "企业税号";
            this.stat2.BorderSides = ToolStripStatusLabelBorderSides.Right;
            this.stat2.Name = "stat2";
            this.stat2.Size = new Size(0x24, 0x15);
            this.stat2.Text = "名称";
            this.stat2.ToolTipText = "企业名称";
            this.stat3.BorderSides = ToolStripStatusLabelBorderSides.Right;
            this.stat3.Name = "stat3";
            this.stat3.Size = new Size(60, 0x15);
            this.stat3.Text = "会计月份";
            this.stat3.ToolTipText = "金税卡会计月份";
            this.stat4.BorderSides = ToolStripStatusLabelBorderSides.Right;
            this.stat4.Name = "stat4";
            this.stat4.Size = new Size(0x24, 0x15);
            this.stat4.Text = "状态";
            this.stat4.ToolTipText = "金税卡状态";
            this.stat5.BorderSides = ToolStripStatusLabelBorderSides.Right;
            this.stat5.Name = "stat5";
            this.stat5.Size = new Size(0x24, 0x15);
            this.stat5.Text = "日期";
            this.stat5.ToolTipText = "金税卡日期";
            this.stat6.BorderSides = ToolStripStatusLabelBorderSides.Right;
            this.stat6.Name = "stat6";
            this.stat6.Size = new Size(60, 0x15);
            this.stat6.Text = "当前用户";
            this.stat6.ToolTipText = "当前用户";
            this.stat7.BorderSides = ToolStripStatusLabelBorderSides.Right;
            this.stat7.Name = "stat7";
            this.stat7.Size = new Size(60, 0x15);
            this.stat7.Text = "登录成功";
            this.stat7.ToolTipText = "系统功能";
            this.stat8.BorderSides = ToolStripStatusLabelBorderSides.Right;
            this.stat8.Name = "stat8";
            this.stat8.Size = new Size(20, 0x15);
            this.stat8.Text = "  ";
            this.stat8.ToolTipText = "状态";
            this.backgroundWorker_0.WorkerReportsProgress = true;
            this.backgroundWorker_0.WorkerSupportsCancellation = true;
            this.backgroundWorker_1.WorkerReportsProgress = true;
            this.backgroundWorker_1.WorkerSupportsCancellation = true;
            this.backgroundWorker_2.WorkerReportsProgress = true;
            this.backgroundWorker_2.WorkerSupportsCancellation = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x373, 0x23d);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.IsMdiContainer = true;
            this.MinimumSize = new Size(800, 580);
            base.Name = "FormMain";
            base.TabText = "税控发票开票软件（金税盘版）";
            this.Text = "税控发票开票软件（金税盘版）";
            base.FormClosing += new FormClosingEventHandler(this.FormMain_FormClosing);
            base.FormClosed += new FormClosedEventHandler(this.FormMain_FormClosed);
            base.Load += new EventHandler(this.FormMain_Load);
            base.Shown += new EventHandler(this.FormMain_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void method_3(object sender, EventArgs e)
        {
            RefreashStatus();
        }

        //没有引用，暂时注释掉
        //private void method_4(string string_0)
        //{
        //    EventHandler method = null;
        //    string txt = string_0;
        //    if (!this.bool_3 && this.statusStrip1.InvokeRequired)
        //    {
        //        if (method == null)
        //        {
        //            <> c__DisplayClass2 class2;
        //            method = new EventHandler(class2.< SetStatusTextEvent > b__0);
        //        }
        //        this.statusStrip1.Invoke(method);
        //    }
        //}

        private void method_5(string string_0)
        {
        }

        private void method_6()
        {
            if (string.Equals(base.TaxCardInstance.SoftVersion, "FWKP_V2.0_Svr_Client"))
            {
                this.stat1.Text = base.TaxCardInstance.TaxCode + "." + base.TaxCardInstance.Reserve;
            }
            else
            {
                this.stat1.Text = base.TaxCardInstance.TaxCode + "." + base.TaxCardInstance.Machine.ToString();
            }
            this.stat2.Text = base.TaxCardInstance.Corporation;
            this.stat3.Text = base.TaxCardInstance.SysMonth.ToString();
            string str = "正常";
            if (base.TaxCardInstance.QYLX.ISPTFP || base.TaxCardInstance.QYLX.ISZYFP)
            {
                if (base.TaxCardInstance.StateInfo.IsRepReached == 1)
                {
                    str = "报税期";
                }
                if (base.TaxCardInstance.StateInfo.IsLockReached == 1)
                {
                    str = "锁死期";
                }
            }
            if (base.TaxCardInstance.QYLX.ISHY || base.TaxCardInstance.QYLX.ISJDC)
            {
                for (int i = 0; i < base.TaxCardInstance.StateInfo.InvTypeInfo.Count; i++)
                {
                    if ((base.TaxCardInstance.StateInfo.InvTypeInfo[i].InvType == 11) || (base.TaxCardInstance.StateInfo.InvTypeInfo[i].InvType == 12))
                    {
                        if ((base.TaxCardInstance.StateInfo.InvTypeInfo[i].IsRepTime == 1) && (str != "锁死期"))
                        {
                            str = "报税期";
                        }
                        if (base.TaxCardInstance.StateInfo.InvTypeInfo[i].IsLockTime == 1)
                        {
                            str = "锁死期";
                        }
                    }
                }
            }
            this.stat4.Text = str;
            this.stat5.Text = base.TaxCardInstance.GetCardClock().ToShortDateString();
            this.stat6.Text = UserInfo.Yhmc;
        }

        private bool method_7()
        {
            bool flag = false;
            try
            {
                Ping ping = new Ping();
                for (int i = 0; i < 4; i++)
                {
                    if (ping.Send("www.baidu.com", 0x3e8).Status == IPStatus.Success)
                    {
                        return true;
                    }
                    if (ping.Send("www.aisino.com", 0x3e8).Status == IPStatus.Success)
                    {
                        return true;
                    }
                    if (ping.Send("www.sina.com", 0x3e8).Status == IPStatus.Success)
                    {
                        return true;
                    }
                    if (ping.Send("www.hao123.com", 0x3e8).Status == IPStatus.Success)
                    {
                        return true;
                    }
                    if (ping.Send("www.microsoft.com", 0x3e8).Status == IPStatus.Success)
                    {
                        goto Label_00A9;
                    }
                }
                return flag;
            Label_00A9:
                flag = true;
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }

        public static void RefreashStatus()
        {
            Class101.smethod_0("刷新状态栏");
            FormMain main = control_0 as FormMain;
            if (main != null)
            {
                main.method_6();
                Class101.smethod_0("完成");
            }
        }

        public static void ResetForm()
        {
            try
            {
                bool_4 = true;
                if (control_0 is Form)
                {
                    (control_0 as Form).Close();
                }
                else
                {
                    (control_0.Parent as Form).Close();
                }
                string fileName = Path.Combine(PropertyUtil.GetValue("MAIN_PATH", ""), @"bin\Aisino.Framework.Startup.exe");
                try
                {
                    Process process = new Process();
                    ProcessStartInfo info = new ProcessStartInfo(fileName);
                    process.StartInfo = info;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    process.Start();
                }
                catch (Exception)
                {
                }
            }
            catch (Exception)
            {
            }
        }

        public void SetInfoStatus(string string_0)
        {
            if (string_0 != null)
            {
                this.stat8.Text = string_0;
                this.stat8.BorderSides = (this.stat8.Text.Length == 0) ? ToolStripStatusLabelBorderSides.None : ToolStripStatusLabelBorderSides.Right;
                this.stat8.Invalidate();
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (!string.Equals(TaxCardFactory.CreateTaxCard().SoftVersion, "FWKP_V2.0_Svr_Client"))
            {
                switch (Class104.smethod_0(ref m))
                {
                    case 2:
                        if (!base.TaxCardInstance.CheckIsSelf())
                        {
                            MessageManager.ShowMsgBox("FRM-000014");
                            Environment.Exit(0);
                            return;
                        }
                        break;

                    case 1:
                        this.SetInfoStatus("新插入了一个设备!");
                        return;

                    case 3:
                        this.SetInfoStatus("设备状态改变!");
                        return;

                    case 4:
                        this.SetInfoStatus("移除了一个设备!");
                        return;

                    case 5:
                        MessageHelper.MsgWait("插入了一个报税盘，正在读取报税盘信息。");
                        try
                        {
                            try
                            {
                                this.SetInfoStatus("插入报税盘!");
                                base.TaxCardInstance.GetStateInfo(false);
                            }
                            catch (Exception)
                            {
                            }
                            break;
                        }
                        finally
                        {
                            MessageHelper.MsgWait();
                        }
                        break;

                    case 6:
                        base.TaxCardInstance.CheckInOutBSP();
                        break;
                }
            }
        }

        public delegate void ExecuteAfterLoadDelegate();

        public delegate void ExecuteAfterShowDelegate();

        public delegate void ExecuteBeforeExitDelegate();

        public delegate void UpdateUserNameDelegate(string yhmc);
    }
}

