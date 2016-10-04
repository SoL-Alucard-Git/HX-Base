namespace Aisino.Framework.MainForm
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;

    public class ButtonManager : SkinForm
    {
        private AisinoBTN but_cancal;
        private AisinoBTN but_save;
        private IContainer icontainer_1;
        private LastTree lastTree1;

        public event EventHandler SaveChange;

        public ButtonManager()
        {
            
            this.InitializeComponent_1();
            this.TreeLoad();
        }

        private void but_cancal_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void but_save_Click(object sender, EventArgs e)
        {
            this.method_2();
            if (this.SaveChange != null)
            {
                this.SaveChange(this, null);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_1 != null))
            {
                this.icontainer_1.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent_1()
        {
            this.lastTree1 = new LastTree();
            this.but_save = new AisinoBTN();
            this.but_cancal = new AisinoBTN();
            base.SuspendLayout();
            this.lastTree1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.lastTree1.DrawMode = TreeViewDrawMode.OwnerDrawAll;
            this.lastTree1.Location = new Point(0, 0);
            this.lastTree1.Name = "lastTree1";
            this.lastTree1.Size = new Size(0x193, 0x106);
            this.lastTree1.TabIndex = 0;
            this.but_save.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.but_save.Location = new Point(0xdf, 270);
            this.but_save.Name = "but_save";
            this.but_save.Size = new Size(0x4b, 0x17);
            this.but_save.TabIndex = 1;
            this.but_save.Text = "保存";
            this.but_save.UseVisualStyleBackColor = true;
            this.but_save.Click += new EventHandler(this.but_save_Click);
            this.but_cancal.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.but_cancal.Location = new Point(0x13a, 270);
            this.but_cancal.Name = "but_cancal";
            this.but_cancal.Size = new Size(0x4b, 0x17);
            this.but_cancal.TabIndex = 2;
            this.but_cancal.Text = "取消";
            this.but_cancal.UseVisualStyleBackColor = true;
            this.but_cancal.Click += new EventHandler(this.but_cancal_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x191, 0x131);
            base.Controls.Add(this.but_cancal);
            base.Controls.Add(this.but_save);
            base.Controls.Add(this.lastTree1);
            base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            base.Location = new Point(0, 0);
            base.Name = "ButtonManager";
            base.ShowInTaskbar = true;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "标签管理";
            base.TopMost = true;
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            string path = Path.Combine(PropertyUtil.GetValue("MAIN_PATH", ""), @"Config\ToolBar\Menu.xml");
            if (File.Exists(path))
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);
                XmlNodeList list = document.SelectNodes("/root/ToolBarItemCollection/ToolBarItem");
                Dictionary<string, string> dict = new Dictionary<string, string>();
                foreach (XmlNode node in list)
                {
                    XmlElement element = node as XmlElement;
                    if (element != null)
                    {
                        string attribute = element.GetAttribute("menuID");
                        if (!dict.ContainsKey(attribute))
                        {
                            dict.Add(attribute, attribute);
                        }
                    }
                }
                foreach (TreeNode node2 in this.lastTree1.Nodes)
                {
                    this.method_1(node2, dict);
                }
            }
        }

        private void method_1(TreeNode treeNode_0, Dictionary<string, string> dict)
        {
            if (dict.ContainsKey(treeNode_0.Name) && (treeNode_0.Nodes.Count == 0))
            {
                treeNode_0.Checked = true;
            }
            else
            {
                foreach (TreeNode node in treeNode_0.Nodes)
                {
                    this.method_1(node, dict);
                }
            }
        }

        private void method_2()
        {
            if (this.lastTree1.Nodes.Count > 0)
            {
                string path = Path.Combine(PropertyUtil.GetValue("MAIN_PATH", ""), @"Config\ToolBar\Menu.xml");
                if (File.Exists(path))
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(path);
                    XmlNode node = document.SelectSingleNode("/root/ToolBarItemCollection");
                    if (node != null)
                    {
                        node.RemoveAll();
                        foreach (TreeNode node2 in this.lastTree1.Nodes)
                        {
                            this.method_3(node2, node as XmlElement, document);
                        }
                        document.Save(path);
                    }
                }
            }
        }

        private void method_3(TreeNode treeNode_0, XmlElement xmlElement_0, XmlDocument xmlDocument_0)
        {
            if (treeNode_0.Checked && (treeNode_0.Nodes.Count == 0))
            {
                XmlElement newChild = xmlDocument_0.CreateElement("ToolBarItem");
                newChild.SetAttribute("text", treeNode_0.Text);
                newChild.SetAttribute("tooltip", treeNode_0.Text);
                newChild.SetAttribute("image", treeNode_0.ImageKey);
                newChild.SetAttribute("menuId", treeNode_0.Tag.ToString());
                xmlElement_0.AppendChild(newChild);
            }
            else
            {
                foreach (TreeNode node in treeNode_0.Nodes)
                {
                    this.method_3(node, xmlElement_0, xmlDocument_0);
                }
            }
        }

        public void TreeLoad()
        {
            this.lastTree1.Nodes.Clear();
            TreeLoader.Load(this.lastTree1, this, "/Aisino/Tree", false);
        }
    }
}

