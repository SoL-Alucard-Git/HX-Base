namespace Aisino.Framework.MainForm
{
    using Aisino.Framework.MainForm.Properties;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Menu;
    using Aisino.Framework.Plugin.Core.Util;
    using Microsoft.Win32;
    using ns5;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class MDIMainForm : FormMain
    {
        private BaseNavForm baseNavForm_0;
        private IContainer icontainer_4;
        private List<string> list_0;
        private List<string> list_1;
        private MenuStrip menuStrip_0;
        private ToolStripLabel toolStripLabel1;
        private ToolStrip toolStripMenu;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton tsbExist;
        private ToolStripButton tsbHelp;
        private ToolStripButton tsbInvMake;
        private ToolStripButton tsbLock;
        private ToolStripButton tsbOnline;
        private ToolStripButton tsbPublish;
        private ToolStripButton tsbSetup;
        private ToolStripButton tsbSystem;
        private ToolStripButton tsbTaxReport;
        private ToolStripButton tsbTextInterface;

        public MDIMainForm()
        {
            
            this.InitializeComponent_2();
            base.Load += new EventHandler(this.MDIMainForm_Load);
            base.Shown += new EventHandler(this.MDIMainForm_Shown);
            FormMain.control_0 = this;
            DockForm._showStyle = FormStyle.Old;
            this.list_0 = new List<string>();
            this.list_1 = new List<string>();
            this.method_8();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_4 != null))
            {
                this.icontainer_4.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent_2()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(MDIMainForm));
            this.toolStripMenu = new ToolStrip();
            this.tsbSetup = new ToolStripButton();
            this.tsbInvMake = new ToolStripButton();
            this.tsbTaxReport = new ToolStripButton();
            this.tsbSystem = new ToolStripButton();
            this.tsbTextInterface = new ToolStripButton();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.toolStripLabel1 = new ToolStripLabel();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.tsbOnline = new ToolStripButton();
            this.tsbHelp = new ToolStripButton();
            this.tsbExist = new ToolStripButton();
            this.tsbPublish = new ToolStripButton();
            this.tsbLock = new ToolStripButton();
            this.toolStripMenu.SuspendLayout();
            base.SuspendLayout();
            this.toolStripMenu.BackColor = SystemColors.Control;
            this.toolStripMenu.BackgroundImage = Resources.smethod_35();
            this.toolStripMenu.BackgroundImageLayout = ImageLayout.Stretch;
            this.toolStripMenu.Font = new Font("Microsoft Sans Serif", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.toolStripMenu.ImageScalingSize = new Size(0x20, 0x20);
            this.toolStripMenu.Items.AddRange(new ToolStripItem[] { this.tsbSetup, this.tsbInvMake, this.tsbTaxReport, this.tsbSystem, this.tsbTextInterface, this.toolStripSeparator1, this.toolStripLabel1, this.toolStripSeparator2, this.tsbPublish, this.tsbOnline, this.tsbHelp, this.tsbLock, this.tsbExist });
            this.toolStripMenu.Location = new Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.RightToLeft = RightToLeft.No;
            this.toolStripMenu.Size = new Size(0x3d0, 0x38);
            this.toolStripMenu.TabIndex = 1;
            this.tsbSetup.Image = Resources.smethod_7();
            this.tsbSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSetup.Name = "tsbSetup";
            this.tsbSetup.Size = new Size(0x44, 0x35);
            this.tsbSetup.Text = "系统设置";
            this.tsbSetup.TextImageRelation = TextImageRelation.ImageAboveText;
            this.tsbSetup.Click += new EventHandler(this.tsbSetup_Click);
            this.tsbInvMake.Image = Resources.smethod_1();
            this.tsbInvMake.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInvMake.Name = "tsbInvMake";
            this.tsbInvMake.Size = new Size(0x44, 0x35);
            this.tsbInvMake.Text = "发票管理";
            this.tsbInvMake.TextImageRelation = TextImageRelation.ImageAboveText;
            this.tsbInvMake.Click += new EventHandler(this.tsbInvMake_Click);
            this.tsbTaxReport.Image = Resources.smethod_4();
            this.tsbTaxReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTaxReport.Name = "tsbTaxReport";
            this.tsbTaxReport.Size = new Size(0x44, 0x35);
            this.tsbTaxReport.Text = "报税处理";
            this.tsbTaxReport.TextImageRelation = TextImageRelation.ImageAboveText;
            this.tsbTaxReport.Click += new EventHandler(this.tsbTaxReport_Click);
            this.tsbSystem.Image = Resources.smethod_6();
            this.tsbSystem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSystem.Name = "tsbSystem";
            this.tsbSystem.Size = new Size(0x44, 0x35);
            this.tsbSystem.Text = "系统维护";
            this.tsbSystem.TextImageRelation = TextImageRelation.ImageAboveText;
            this.tsbSystem.Click += new EventHandler(this.tsbSystem_Click);
            this.tsbTextInterface.Image = Resources.smethod_5();
            this.tsbTextInterface.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTextInterface.Name = "tsbTextInterface";
            this.tsbTextInterface.Size = new Size(0x44, 0x35);
            this.tsbTextInterface.Text = "单据管理";
            this.tsbTextInterface.TextImageRelation = TextImageRelation.ImageAboveText;
            this.tsbTextInterface.Click += new EventHandler(this.tsbTextInterface_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(6, 0x38);
            this.toolStripLabel1.ActiveLinkColor = SystemColors.Control;
            this.toolStripLabel1.AutoSize = false;
            this.toolStripLabel1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new Size(300, 0x33);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(6, 0x38);
            this.tsbOnline.Image = Resources.smethod_3();
            this.tsbOnline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOnline.Name = "tsbOnline";
            this.tsbOnline.Size = new Size(0x44, 0x35);
            this.tsbOnline.Text = "在线支持";
            this.tsbOnline.TextImageRelation = TextImageRelation.ImageAboveText;
            this.tsbOnline.ToolTipText = "  在线支持  ";
            this.tsbOnline.Click += new EventHandler(this.tsbOnline_Click);
            this.tsbHelp.Image = Resources.smethod_2();
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new Size(0x38, 0x35);
            this.tsbHelp.Text = "  帮助  ";
            this.tsbHelp.TextImageRelation = TextImageRelation.ImageAboveText;
            this.tsbHelp.Click += new EventHandler(this.tsbHelp_Click);
            this.tsbExist.Image = Resources.smethod_8();
            this.tsbExist.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExist.Name = "tsbExist";
            this.tsbExist.Size = new Size(0x38, 0x35);
            this.tsbExist.Text = "  退出  ";
            this.tsbExist.TextImageRelation = TextImageRelation.ImageAboveText;
            this.tsbExist.Click += new EventHandler(this.tsbExist_Click);
            this.tsbPublish.Image = Resources.smethod_30();
            this.tsbPublish.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPublish.Name = "tsbPublish";
            this.tsbPublish.Size = new Size(0x44, 0x35);
            this.tsbPublish.Text = "  公告  ";
            this.tsbPublish.TextImageRelation = TextImageRelation.ImageAboveText;
            this.tsbPublish.Click += new EventHandler(this.tsbPublish_Click);
            this.tsbLock.Image = Resources.smethod_9();
            this.tsbLock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLock.Name = "tsbLock";
            this.tsbLock.Size = new Size(0x38, 0x35);
            this.tsbLock.Text = "  锁屏  ";
            this.tsbLock.TextImageRelation = TextImageRelation.ImageAboveText;
            this.tsbLock.Click += new EventHandler(this.tsbLock_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = ImageLayout.None;
            base.ClientSize = new Size(0x3d0, 0x222);
            base.Controls.Add(this.toolStripMenu);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "MDIMainForm";
            base.TabText = "税控发票开票软件（金税盘版） ";
            this.Text = "税控发票开票软件（金税盘版） ";
            base.Controls.SetChildIndex(this.toolStripMenu, 0);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        [DllImport("user32.dll")]
        public static extern bool LockWorkStation();
        [CompilerGenerated]
        private void MDIMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            PropertyUtil.SetRectangle("Site", base.Bounds);
        }

        private void MDIMainForm_Load(object sender, EventArgs e)
        {
            Class104.oRegisterDeviceInterface(base.Handle);
            foreach (Control control in base.Controls)
            {
                try
                {
                    MdiClient client = (MdiClient) control;
                    client.BackColor = this.BackColor;
                }
                catch
                {
                }
            }
        }

        private void MDIMainForm_Shown(object sender, EventArgs e)
        {
            foreach (string str3 in this.list_1)
            {
                this.method_12(str3, false);
            }
            if (this.list_0.Count > 0)
            {
                foreach (string str2 in this.list_0)
                {
                    this.method_12(str2, true);
                }
                string str = PropertyUtil.GetValue("MDIForm_OldNavButton");
                if (string.IsNullOrEmpty(str))
                {
                    str = this.list_0[0];
                }
                if (this.method_9(str))
                {
                    this.method_13(str);
                }
            }
            this.toolStripMenu.BackgroundImage = Resources.smethod_35();
        }

        private void method_10(string string_0)
        {
            foreach (ToolStripItem item in this.menuStrip_0.Items)
            {
                MenuLabel label = item as MenuLabel;
                if (label != null)
                {
                    if (string_0 == "Menu.Fpgl")
                    {
                        if ((!(label.FunctionID == "Menu.Fpgl") && !(label.FunctionID == "Menu.Fplygl")) && !(label.FunctionID == "Menu.Hzfp"))
                        {
                            if (this.list_1.Contains(label.FunctionID))
                            {
                                item.Visible = false;
                            }
                        }
                        else
                        {
                            item.Visible = true;
                        }
                    }
                    else if (string_0 == "Menu.Bsgl")
                    {
                        if ((!(label.FunctionID == "Menu.Bsgl") && !(label.FunctionID == "Menu.Bsgl.Jskgl")) && !(label.FunctionID == "Menu.Bsgl.InvData"))
                        {
                            if (this.list_1.Contains(label.FunctionID))
                            {
                                item.Visible = false;
                            }
                        }
                        else
                        {
                            item.Visible = true;
                        }
                    }
                    else if (string_0 == "Menu.Xtsz")
                    {
                        if (!(label.FunctionID == "Menu.Xtsz") && !(label.FunctionID == "Menu.Xtsz.Bmgl"))
                        {
                            if (this.list_1.Contains(label.FunctionID))
                            {
                                item.Visible = false;
                            }
                        }
                        else
                        {
                            item.Visible = true;
                        }
                    }
                    else if (this.list_1.Contains(label.FunctionID))
                    {
                        if (string_0 == label.FunctionID)
                        {
                            item.Visible = true;
                        }
                        else
                        {
                            item.Visible = false;
                        }
                    }
                }
            }
        }

        private string method_11(Form form_0)
        {
            string str = string.Empty;
            if (form_0 is SystemSettingNavForm)
            {
                return "Menu.Xtsz";
            }
            if (form_0 is InvManagNavForm)
            {
                return "Menu.Fpgl";
            }
            if (form_0 is TaxReportNavForm)
            {
                return "Menu.Bsgl";
            }
            if (form_0 is SysModifyNavForm)
            {
                return "Menu.Xtwh";
            }
            if (form_0 is TextIFaceNavForm)
            {
                str = "Menu.Wbjk";
            }
            return str;
        }

        private void method_12(string string_0, bool bool_5)
        {
            switch (string_0)
            {
                case "Menu.Xtsz":
                case "Menu.Xtsz.Bmgl":
                    this.tsbSetup.Visible = bool_5;
                    return;

                case "Menu.Fpgl":
                case "Menu.Fplygl":
                case "Menu.Hzfp":
                    this.tsbInvMake.Visible = bool_5;
                    return;

                case "Menu.Bsgl":
                case "Menu.Bsgl.Jskgl":
                case "Menu.Bsgl.InvData":
                    this.tsbTaxReport.Visible = bool_5;
                    return;

                case "Menu.Xtwh":
                    this.tsbSystem.Visible = bool_5;
                    return;

                case "Menu.Wbjk":
                    this.tsbTextInterface.Visible = bool_5;
                    return;
            }
        }

        private void method_13(string string_0)
        {
            this.tsbSetup.Enabled = true;
            this.tsbInvMake.Enabled = true;
            this.tsbTaxReport.Enabled = true;
            this.tsbSystem.Enabled = true;
            this.tsbTextInterface.Enabled = true;
            switch (string_0)
            {
                case "Menu.Xtsz":
                    this.tsbSetup.Enabled = false;
                    return;

                case "Menu.Fpgl":
                    this.tsbInvMake.Enabled = false;
                    return;

                case "Menu.Bsgl":
                    this.tsbTaxReport.Enabled = false;
                    return;

                case "Menu.Xtwh":
                    this.tsbSystem.Enabled = false;
                    break;

                case "Menu.Wbjk":
                    this.tsbTextInterface.Enabled = false;
                    break;
            }
        }

        private void method_14(object sender, EventArgs e)
        {
            if (MessageBoxHelper.Show("是否确定登录到【增值税发票查询系统】？", "登录【增值税发票查询系统】", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string str2 = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Config\Common\OnlineLogin.ini");
                string str = IniFileUtil.ReadIniData("OnlineLogin", base.TaxCardInstance.RegionCode.Substring(0, 4), string.Empty, str2);
                if (!string.IsNullOrEmpty(str))
                {
                    Process.Start(str);
                }
                else
                {
                    str = IniFileUtil.ReadIniData("OnlineLogin", base.TaxCardInstance.RegionCode.Substring(0, 2), string.Empty, str2);
                    if (!string.IsNullOrEmpty(str))
                    {
                        Process.Start(str);
                    }
                    else
                    {
                        MessageBoxHelper.Show("【增值税发票查询系统】的网址配置不正确，请联系服务人员解决。");
                    }
                }
            }
        }

        private void method_15(object sender, EventArgs e)
        {
            new AboutUSForm().ShowDialog();
        }

        private void method_8()
        {
            FormSplashHelper.MsgWait("正在准备系统主界面...");
            base.WindowState = FormWindowState.Maximized;
            Rectangle rectangle = PropertyUtil.GetRectangle("Site", new Rectangle(10, 10, base.Width, base.Height));
            rectangle.X = (rectangle.X < 100) ? 0 : rectangle.X;
            rectangle.Y = (rectangle.Y < 100) ? 0 : rectangle.Y;
            base.StartPosition = FormStartPosition.Manual;
            base.Bounds = rectangle;
            base.FormClosing += new FormClosingEventHandler(this.MDIMainForm_FormClosing);
            this.menuStrip_0 = new MenuStrip();
            this.menuStrip_0.Font = new Font("黑体", 10f);
            MenuLoader.Load(this.menuStrip_0.Items, this, "/Aisino/Menu");
            ToolStripMenuItem item2 = new ToolStripMenuItem {
                Name = "Menu_Help",
                Text = "帮助"
            };
            ToolStripMenuItem item3 = new ToolStripMenuItem {
                Name = "Menu_Online",
                Text = "在线支持"
            };
            item3.Click += new EventHandler(this.tsbOnline_Click);
            item2.DropDownItems.Add(item3);
            ToolStripMenuItem item4 = new ToolStripMenuItem {
                Name = "Menu_OnlineLogin",
                Text = "登录【增值税发票查询系统】"
            };
            item4.Click += new EventHandler(this.method_14);
            item2.DropDownItems.Add(item4);
            ToolStripMenuItem item5 = new ToolStripMenuItem {
                Name = "Menu_Help",
                Text = "帮助"
            };
            item5.Click += new EventHandler(this.tsbHelp_Click);
            item2.DropDownItems.Add(item5);
            ToolStripMenuItem item6 = new ToolStripMenuItem {
                Name = "Menu_AboutUS",
                Text = "关于"
            };
            item6.Click += new EventHandler(this.method_15);
            item2.DropDownItems.Add(item6);
            this.menuStrip_0.Items.Add(item2);
            if (this.menuStrip_0.Items.Count > 0)
            {
                base.Controls.Add(this.menuStrip_0);
                base.MainMenuStrip = this.menuStrip_0;
            }
            base.Controls.Add(base.statusStrip1);
            this.list_1.Add("Menu.Xtsz");
            this.list_1.Add("Menu.Xtsz.Bmgl");
            this.list_1.Add("Menu.Fpgl");
            this.list_1.Add("Menu.Fplygl");
            this.list_1.Add("Menu.Hzfp");
            this.list_1.Add("Menu.Bsgl");
            this.list_1.Add("Menu.Bsgl.Jskgl");
            this.list_1.Add("Menu.Bsgl.InvData");
            this.list_1.Add("Menu.Xtwh");
            this.list_1.Add("Menu.Wbjk");
            foreach (ToolStripItem item in this.menuStrip_0.Items)
            {
                item.Image = null;
                MenuLabel label = item as MenuLabel;
                if ((label != null) && this.list_1.Contains(label.FunctionID))
                {
                    this.list_0.Add(label.FunctionID);
                }
            }
            FormSplashHelper.MsgWait();
            this.tsbPublish.Visible = false;
            this.tsbPublish.Enabled = false;
        }

        private bool method_9(string string_0)
        {
            bool flag = false;
            string str = string_0;
            if (str != null)
            {
                if (str == "Menu.Xtsz")
                {
                    this.baseNavForm_0 = new SystemSettingNavForm();
                }
                else if (str == "Menu.Fpgl")
                {
                    this.baseNavForm_0 = new InvManagNavForm();
                }
                else if (str == "Menu.Bsgl")
                {
                    this.baseNavForm_0 = new TaxReportNavForm();
                }
                else if (!(str == "Menu.Xtwh"))
                {
                    if (!(str == "Menu.Wbjk"))
                    {
                        goto Label_008C;
                    }
                    this.baseNavForm_0 = new TextIFaceNavForm();
                }
                else
                {
                    this.baseNavForm_0 = new SysModifyNavForm();
                }
                goto Label_0093;
            }
        Label_008C:
            this.baseNavForm_0 = null;
        Label_0093:
            if ((this.baseNavForm_0 != null) && this.NavFormShow(this.baseNavForm_0))
            {
                this.method_10(string_0);
                PropertyUtil.SetValue("MDIForm_OldNavButton", string_0);
                flag = true;
            }
            return flag;
        }

        public bool NavFormShow(Form form_0)
        {
            bool flag = false;
            Form[] mdiChildren = base.MdiChildren;
            if (mdiChildren != null)
            {
                foreach (Form form2 in mdiChildren)
                {
                    if (form2 is DockForm)
                    {
                        form2.Close();
                    }
                }
            }
            Form[] formArray4 = base.MdiChildren;
            bool flag2 = true;
            if (formArray4 != null)
            {
                foreach (Form form in formArray4)
                {
                    if (form is DockForm)
                    {
                        flag2 = false;
                        break;
                    }
                }
            }
            if (!flag2)
            {
                return flag;
            }
            Form[] formArray6 = base.MdiChildren;
            if (formArray6 != null)
            {
                foreach (Form form3 in formArray6)
                {
                    form3.Close();
                }
            }
            form_0.MdiParent = this;
            form_0.ControlBox = false;
            form_0.MaximizeBox = false;
            form_0.MinimizeBox = false;
            form_0.FormBorderStyle = FormBorderStyle.None;
            form_0.Show();
            return true;
        }

        public override void SetTSBStates(object object_0)
        {
            string str = "  退出  ";
            Form[] mdiChildren = base.MdiChildren;
            if (mdiChildren != null)
            {
                foreach (Form form in mdiChildren)
                {
                    if ((form is DockForm) && (object_0 != form))
                    {
                        str = "  关闭  ";
                        break;
                    }
                }
            }
            this.tsbExist.Text = str;
        }

        private void tsbExist_Click(object sender, EventArgs e)
        {
            if (this.tsbExist.Text.Trim() == "关闭")
            {
                Form[] mdiChildren = base.MdiChildren;
                if (mdiChildren != null)
                {
                    foreach (Form form in mdiChildren)
                    {
                        if (form is DockForm)
                        {
                            form.Close();
                        }
                    }
                }
            }
            else if (MessageBoxHelper.Show("此操作将关闭系统，确认退出吗？", "退出系统", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                base.Close();
            }
        }

        private void tsbHelp_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Config\Help\税控发票开票软件（金税盘版）V2.0帮助.chm");
            if (File.Exists(path))
            {
                Help.ShowHelp(new Control(), path);
            }
        }

        private void tsbInvMake_Click(object sender, EventArgs e)
        {
            if (this.method_9("Menu.Fpgl"))
            {
                this.method_13("Menu.Fpgl");
            }
        }

        private void tsbLock_Click(object sender, EventArgs e)
        {
            LockWorkStation();
        }

        private void tsbOnline_Click(object sender, EventArgs e)
        {
            string str = string.Empty;
            try
            {
                RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"IMMain\DefaultIcon");
                if (key != null)
                {
                    object obj2 = key.GetValue("");
                    if (obj2 != null)
                    {
                        str = obj2.ToString().Split(new char[] { ',' })[0];
                    }
                }
            }
            catch (Exception)
            {
            }
            if (string.IsNullOrEmpty(str))
            {
                if (MessageBoxHelper.Show("检测到本计算机没有安装【AISINO在线服务平台客户端】，点击“确定”下载。", "下载【AISINO在线服务平台客户端】", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    Process.Start("http://www.cnnsr.com.cn/fwzx/cpzx_zxfwpt.html");
                }
            }
            else
            {
                ProcessStartInfo startInfo = new ProcessStartInfo {
                    FileName = str,
                    Arguments = "",
                    WindowStyle = ProcessWindowStyle.Normal
                };
                Process.Start(startInfo);
            }
        }

        private void tsbPublish_Click(object sender, EventArgs e)
        {
            ToolUtil.RunFuction("_PUBQUERY");
        }

        private void tsbSetup_Click(object sender, EventArgs e)
        {
            if (this.method_9("Menu.Xtsz"))
            {
                this.method_13("Menu.Xtsz");
            }
        }

        private void tsbSystem_Click(object sender, EventArgs e)
        {
            if (this.method_9("Menu.Xtwh"))
            {
                this.method_13("Menu.Xtwh");
            }
        }

        private void tsbTaxReport_Click(object sender, EventArgs e)
        {
            if (this.method_9("Menu.Bsgl"))
            {
                this.method_13("Menu.Bsgl");
            }
        }

        private void tsbTextInterface_Click(object sender, EventArgs e)
        {
            if (this.method_9("Menu.Wbjk"))
            {
                this.method_13("Menu.Wbjk");
            }
        }

        public override void vmethod_0(string string_0)
        {
            this.tsbExist.Text = string_0;
        }

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                string operateName = CheckSystemUtil.GetOperateName();
                if (((!(operateName == "Windows95") && !(operateName == "Windows98")) && (!(operateName == "WindowsMe") && !(operateName == "WindowsNT3.5"))) && ((!(operateName == "WindowsNT4.0") && !(operateName == "Windows2000")) && !(operateName == "WindowsXP")))
                {
                    System.Windows.Forms.CreateParams createParams = base.CreateParams;
                    createParams.ExStyle |= 0x2000000;
                    return createParams;
                }
                return base.CreateParams;
            }
        }
    }
}

