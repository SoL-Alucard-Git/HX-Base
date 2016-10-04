namespace Aisino.Framework.MainForm
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using ns5;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using System.Xml;

    public class NormalMainForm : FormMain
    {
        private ContextMenuStrip contextMenuStrip_0;
        private TabControlEx dockPanel1;
        private IContainer icontainer_4;
        private NavigatePage navigatePage_0;
        private NavigateToolScrip navigateToolScrip_0;

        public NormalMainForm()
        {
            
            this.InitializeComponent_2();
            base.Load += new EventHandler(this.NormalMainForm_Load);
            this.method_8();
            FormMain.control_0 = this.dockPanel1;
            DockForm._showStyle = FormStyle.New;
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(NormalMainForm));
            this.dockPanel1 = new TabControlEx();
            base.SuspendLayout();
            this.dockPanel1.ColorActivateA = Color.FromArgb(0xd5, 0xe8, 0xf5);
            this.dockPanel1.ColorActivateB = Color.FromArgb(0xae, 0xc4, 0xd4);
            this.dockPanel1.ColorDefaultA = Color.FromArgb(250, 250, 250);
            this.dockPanel1.ColorDefaultB = Color.FromArgb(0xe0, 0xe0, 0xe0);
            this.dockPanel1.ColorLine = Color.FromArgb(0x9d, 0xa2, 0xa8);
            this.dockPanel1.Dock = DockStyle.Fill;
            this.dockPanel1.Location = new Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.SelectedIndex = 0;
            this.dockPanel1.Size = new Size(0x310, 0x21d);
            this.dockPanel1.TabIndex = 5;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x310, 0x21d);
            base.Controls.Add(this.dockPanel1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "NormalMainForm";
            base.TabText = "税控发票开票软件（金税盘版） ";
            this.Text = "税控发票开票软件（金税盘版） ";
            base.Controls.SetChildIndex(this.dockPanel1, 0);
            base.ResumeLayout(false);
        }

        private void method_10(object object_0, ListView.ListViewItemCollection listViewItemCollection_0)
        {
            if ((listViewItemCollection_0 != null) && (listViewItemCollection_0.Count > 0))
            {
                string filename = this.method_9("2");
                XmlDocument document = new XmlDocument();
                XmlElement newChild = document.CreateElement("root");
                XmlElement element2 = document.CreateElement("ToolBarItemCollection");
                foreach (ListViewItem item in listViewItemCollection_0)
                {
                    XmlElement element3 = document.CreateElement("ToolBarItem");
                    element3.SetAttribute("text", item.Text);
                    element3.SetAttribute("tooltip", item.ToolTipText);
                    element3.SetAttribute("image", item.ImageKey + "_48");
                    element3.SetAttribute("menuID", item.Name);
                    element2.AppendChild(element3);
                }
                newChild.AppendChild(element2);
                document.AppendChild(newChild);
                document.Save(filename);
                this.navigateToolScrip_0.Load(filename, this.navigatePage_0.Nodes);
            }
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
            base.FormClosing += new FormClosingEventHandler(this.NormalMainForm_FormClosing);
            this.navigatePage_0 = new NavigatePage();
            this.navigatePage_0.Dock = DockStyle.Left;
            base.Controls.Add(this.navigatePage_0);
            TreeLoader.Load(this.navigatePage_0, this.dockPanel1, "/Aisino/Tree", false);
            this.navigateToolScrip_0 = new NavigateToolScrip();
            this.navigateToolScrip_0.Dock = DockStyle.Top;
            base.Controls.Add(this.navigateToolScrip_0);
            this.navigateToolScrip_0.Load(this.method_9("1"), this.navigatePage_0.Nodes);
            this.navigateToolScrip_0.ItemAdd += new EventHandler(this.navigateToolScrip_0_ItemAdd);
            this.contextMenuStrip_0 = new ContextMenuStrip();
            MenuLoader.Load(this.contextMenuStrip_0.Items, this.dockPanel1, "/Aisino/Menu");
            if (this.contextMenuStrip_0.Items.Count > 0)
            {
                base._menu = this.contextMenuStrip_0;
            }
            base.Controls.Add(base.statusStrip1);
            FormSplashHelper.MsgWait();
        }

        private string method_9(string string_0)
        {
            string str = string.Empty;
            str = Path.Combine(PropertyUtil.GetValue("MAIN_PATH").ToString(), @"Config\ToolBar\");
            string str2 = "Menu.xml";
            PropertyUtil.GetValue("Login_UserID");
            string str3 = PropertyUtil.GetValue("Login_UserName");
            PropertyUtil.GetValue("Login_IsAdmin");
            if (File.Exists(Path.Combine(str, str3 + ".xml")))
            {
                str2 = str3 + ".xml";
            }
            else
            {
                str2 = "Menu.xml";
            }
            if (string_0 == "2")
            {
                str2 = str3 + ".xml";
            }
            return Path.Combine(str, str2);
        }

        private void navigateToolScrip_0_ItemAdd(object sender, EventArgs e)
        {
            NavigateToolSetting setting = new NavigateToolSetting(this.navigatePage_0.Nodes, this.method_9("1"));
            setting.SaveChange += method_10;
            setting.ShowDialog();
        }

        [CompilerGenerated]
        private void NormalMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            PropertyUtil.SetRectangle("Site", base.Bounds);
        }

        private void NormalMainForm_Load(object sender, EventArgs e)
        {
            Class104.oRegisterDeviceInterface(base.Handle);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
        }
    }
}

