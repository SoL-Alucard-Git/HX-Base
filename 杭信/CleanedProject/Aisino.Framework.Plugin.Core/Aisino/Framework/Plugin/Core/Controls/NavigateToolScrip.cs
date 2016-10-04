namespace Aisino.Framework.Plugin.Core.Controls
{
    using Aisino.Framework.Plugin.Core.Util;
    using ns8;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;

    public class NavigateToolScrip : UserControl
    {
        private IContainer icontainer_0;
        private List<string> list_0;
        private Control0 navigateToolScripBody1;
        private Control4 navigateToolScripHideBar1;

        public event EventHandler ItemAdd;

        public NavigateToolScrip()
        {
            
            this.InitializeComponent();
            this.list_0 = new List<string>();
            this.navigateToolScripBody1.SizeChanged += new EventHandler(this.navigateToolScripBody1_SizeChanged);
            this.navigateToolScripHideBar1.SizeChanged += new EventHandler(this.navigateToolScripHideBar1_SizeChanged);
            this.navigateToolScripHideBar1.Event_0 += new Delegate38(this.method_3);
            this.navigateToolScripBody1.Event_0 += new Delegate34(this.method_2);
            this.navigateToolScripBody1.Event_1 += new EventHandler(this.method_0);
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
            this.navigateToolScripBody1 = new Control0();
            this.navigateToolScripHideBar1 = new Control4();
            base.SuspendLayout();
            this.navigateToolScripBody1.Dock = DockStyle.Top;
            this.navigateToolScripBody1.Location = new Point(0, 0);
            this.navigateToolScripBody1.Name = "navigateToolScripBody1";
            this.navigateToolScripBody1.Size = new Size(0x31e, 0x52);
            this.navigateToolScripBody1.TabIndex = 0;
            this.navigateToolScripBody1.Text = "navigateToolScripBody1";
            this.navigateToolScripHideBar1.Dock = DockStyle.Fill;
            this.navigateToolScripHideBar1.Location = new Point(0, 0x52);
            this.navigateToolScripHideBar1.Name = "navigateToolScripHideBar1";
            this.navigateToolScripHideBar1.Size = new Size(0x31e, 7);
            this.navigateToolScripHideBar1.TabIndex = 1;
            this.navigateToolScripHideBar1.Text = "navigateToolScripHideBar1";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.navigateToolScripHideBar1);
            base.Controls.Add(this.navigateToolScripBody1);
            base.Name = "NavigateToolScrip";
            base.Size = new Size(0x31e, 0x59);
            base.ResumeLayout(false);
        }

        public void Load(string string_0, NavigateMenuNode navigateMenuNode_0)
        {
            if (File.Exists(string_0))
            {
                this.navigateToolScripBody1.Items.Clear();
                try
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(string_0);
                    if ((document == null) || !document.HasChildNodes)
                    {
                        throw new Exception("XML文件格式不正确！");
                    }
                    this.list_0.Clear();
                    this.method_5(navigateMenuNode_0.Node);
                    XmlNodeList list = document.SelectNodes("root/ToolBarItemCollection/ToolBarItem");
                    if ((list == null) || (list.Count <= 0))
                    {
                        throw new Exception("XML文件格式不正确！");
                    }
                    foreach (XmlNode node in list)
                    {
                        XmlAttributeCollection attributes = node.Attributes;
                        string str = attributes["text"].Value;
                        string str2 = attributes["image"].Value;
                        string item = attributes["menuID"].Value;
                        string str4 = attributes["tooltip"].Value;
                        Bitmap bitmap = ResourceUtil.GetBitmap(str2);
                        if (bitmap == null)
                        {
                            bitmap = Class131.smethod_38();
                        }
                        if (this.list_0.Contains(item))
                        {
                            this.navigateToolScripBody1.Items.Add(new NavigateMenuNode(str, item, bitmap, str4));
                        }
                    }
                    return;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            throw new Exception("文件不存在！");
        }

        private void method_0(object sender, EventArgs e)
        {
            this.method_1(sender, e);
        }

        private void method_1(object sender, EventArgs e)
        {
            if (this.ItemAdd != null)
            {
                this.ItemAdd(sender, e);
            }
        }

        private void method_2(object sender, EventArgs0 e)
        {
            if (((e.method_0() != null) && (e.method_0().Name != null)) && (e.method_0().Name != ""))
            {
                ToolUtil.RunFuction(e.method_0().Name);
            }
        }

        private void method_3(object object_0, Class125 class125_0)
        {
            this.navigateToolScripBody1.Visible = !class125_0.Hide;
            this.method_4();
        }

        private void method_4()
        {
            int num = 0;
            int height = 0;
            if (this.navigateToolScripHideBar1.Visible)
            {
                height = this.navigateToolScripHideBar1.Height;
            }
            if (this.navigateToolScripBody1.Visible)
            {
                num = this.navigateToolScripBody1.Height;
            }
            base.Height = height + num;
        }

        private void method_5(NavigateMenuNodeCollection navigateMenuNodeCollection_0)
        {
            foreach (NavigateMenuNode node in navigateMenuNodeCollection_0)
            {
                if ((node.Node != null) && (node.Node.Count > 0))
                {
                    this.method_5(node.Node);
                }
                else
                {
                    this.list_0.Add(node.Name);
                }
            }
        }

        private void navigateToolScripBody1_SizeChanged(object sender, EventArgs e)
        {
            this.method_4();
        }

        private void navigateToolScripHideBar1_SizeChanged(object sender, EventArgs e)
        {
            this.method_4();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.method_4();
        }

        public bool IsShowAddButton
        {
            get
            {
                return this.navigateToolScripBody1.method_0();
            }
            set
            {
                this.navigateToolScripBody1.method_1(value);
            }
        }
    }
}

