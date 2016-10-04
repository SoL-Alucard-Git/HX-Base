namespace Aisino.Fwkp.HomePage.AisinoDock
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Startup.Login;
    using Aisino.Fwkp.HomePage.AisinoDock.Docks;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;

    public class IconManager : Form
    {
        private AisinoBTN but_selDirectory;
        private AisinoBTN but_selectExe;
        private AisinoBTN button1;
        private IContainer components;
        private FolderBrowserDialog folderBrowserDialog1;
        private ImageList imageList1;
        private IconManagerEventArgs.IconType imea;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private AisinoLBL label4;
        private AisinoLBL label5;
        private LastTree lastTree1;
        private IconDock owner;
        private AisinoPIC pic_softIcon;
        private List<string> PowerList;
        private TabPage tab_wb;
        private AisinoTAB tabControl1;
        private TabPage tabPage1;
        private AisinoTXT txt_selectExePath;
        private AisinoTXT txt_softExeName;

        public event ButtonOperate AddButtonOperate;

        public event ButtonOperate DelteButtonOperate;

        public IconManager(IconDock dock)
        {
            this.InitializeComponent();
            this.PowerList = UserInfo.get_Gnqx();
            this.tab_wb.DragDrop += new DragEventHandler(this.tab_wb_DragDrop);
            this.tab_wb.DragEnter += new DragEventHandler(this.tab_wb_DragEnter);
            this.owner = dock;
            this.lastTree1.AfterCheck += new TreeViewEventHandler(this.lastTree1_AfterCheck);
            this.TreeLoad();
        }

        private void AddIcon_Click(object sender, EventArgs e)
        {
            if ((this.AddButtonOperate != null) && (this.imea != IconManagerEventArgs.IconType.None))
            {
                ButtonControlExEventArgs args = new ButtonControlExEventArgs {
                    Name = this.txt_softExeName.Text,
                    Key = this.txt_selectExePath.Text
                };
                if ((this.imea == IconManagerEventArgs.IconType.Directory) || (this.imea == IconManagerEventArgs.IconType.File))
                {
                    args.Style = ButtonClickStyle.Exe;
                }
                if (this.imea == IconManagerEventArgs.IconType.Url)
                {
                    args.Style = ButtonClickStyle.URL;
                }
                args.Icon = this.pic_softIcon.Image;
                args.IconName = "";
                this.AddButtonOperate(this, args);
            }
        }

        private void but_Directory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txt_selectExePath.Text = dialog.SelectedPath;
                string selectedPath = dialog.SelectedPath;
            }
        }

        private void but_selectExe_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txt_selectExePath.Text = dialog.FileName;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.tabControl1 = new AisinoTAB();
            this.tabPage1 = new TabPage();
            this.tab_wb = new TabPage();
            this.label2 = new AisinoLBL();
            this.but_selDirectory = new AisinoBTN();
            this.label1 = new AisinoLBL();
            this.button1 = new AisinoBTN();
            this.label5 = new AisinoLBL();
            this.txt_softExeName = new AisinoTXT();
            this.pic_softIcon = new AisinoPIC();
            this.label4 = new AisinoLBL();
            this.txt_selectExePath = new AisinoTXT();
            this.but_selectExe = new AisinoBTN();
            this.imageList1 = new ImageList(this.components);
            this.folderBrowserDialog1 = new FolderBrowserDialog();
            this.lastTree1 = new LastTree();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tab_wb.SuspendLayout();
            this.pic_softIcon.BeginInit();
            base.SuspendLayout();
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tab_wb);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x1f2, 0x18a);
            this.tabControl1.TabIndex = 0;
            this.tabPage1.Controls.Add(this.lastTree1);
            this.tabPage1.Location = new Point(4, 0x15);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(490, 0x171);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "系统菜单";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tab_wb.AllowDrop = true;
            this.tab_wb.Controls.Add(this.label2);
            this.tab_wb.Controls.Add(this.but_selDirectory);
            this.tab_wb.Controls.Add(this.label1);
            this.tab_wb.Controls.Add(this.button1);
            this.tab_wb.Controls.Add(this.label5);
            this.tab_wb.Controls.Add(this.txt_softExeName);
            this.tab_wb.Controls.Add(this.pic_softIcon);
            this.tab_wb.Controls.Add(this.label4);
            this.tab_wb.Controls.Add(this.txt_selectExePath);
            this.tab_wb.Controls.Add(this.but_selectExe);
            this.tab_wb.Location = new Point(4, 0x15);
            this.tab_wb.Name = "tab_wb";
            this.tab_wb.Padding = new Padding(3);
            this.tab_wb.Size = new Size(490, 0x171);
            this.tab_wb.TabIndex = 2;
            this.tab_wb.Text = "外部菜单";
            this.tab_wb.UseVisualStyleBackColor = true;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x52, 0x25);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0xd1, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "请输入文件(文件夹)路径或网页地址：";
            this.but_selDirectory.Location = new Point(0xf1, 0x57);
            this.but_selDirectory.Name = "but_selDirectory";
            this.but_selDirectory.Size = new Size(0x4b, 0x17);
            this.but_selDirectory.TabIndex = 9;
            this.but_selDirectory.Text = "选择路径";
            this.but_selDirectory.UseVisualStyleBackColor = true;
            this.but_selDirectory.Click += new EventHandler(this.but_Directory_Click);
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 12f);
            this.label1.Location = new Point(50, 0x25);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x10, 0x10);
            this.label1.TabIndex = 8;
            this.label1.Text = " ";
            this.button1.Enabled = false;
            this.button1.Location = new Point(0x12f, 0xa8);
            this.button1.Name = "button1";
            this.button1.Size = new Size(100, 0x1f);
            this.button1.TabIndex = 7;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.AddIcon_Click);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x27, 0x7f);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "名称";
            this.txt_softExeName.Location = new Point(0x54, 0x7c);
            this.txt_softExeName.Name = "txt_softExeName";
            this.txt_softExeName.Size = new Size(0x13f, 0x15);
            this.txt_softExeName.TabIndex = 4;
            this.pic_softIcon.Location = new Point(0x54, 0x97);
            this.pic_softIcon.Name = "pic_softIcon";
            this.pic_softIcon.Size = new Size(0x30, 0x30);
            this.pic_softIcon.TabIndex = 3;
            this.pic_softIcon.TabStop = false;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x1b, 0x3f);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x29, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "请选择";
            this.txt_selectExePath.Location = new Point(0x54, 0x3b);
            this.txt_selectExePath.Name = "txt_selectExePath";
            this.txt_selectExePath.Size = new Size(0x13f, 0x15);
            this.txt_selectExePath.TabIndex = 1;
            this.txt_selectExePath.TextChanged += new EventHandler(this.txt_selectExePath_TextChanged);
            this.but_selectExe.Location = new Point(0x148, 0x56);
            this.but_selectExe.Name = "but_selectExe";
            this.but_selectExe.Size = new Size(0x47, 0x17);
            this.but_selectExe.TabIndex = 0;
            this.but_selectExe.Text = "选择文件";
            this.but_selectExe.UseVisualStyleBackColor = true;
            this.but_selectExe.Click += new EventHandler(this.but_selectExe_Click);
            this.imageList1.ColorDepth = ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new Size(0x10, 0x10);
            this.imageList1.TransparentColor = Color.Transparent;
            this.lastTree1.CheckBoxes = true;
            this.lastTree1.Dock = DockStyle.Fill;
            this.lastTree1.DrawMode = TreeViewDrawMode.OwnerDrawAll;
            this.lastTree1.ImageIndex = 0;
            this.lastTree1.ImageList = this.imageList1;
            this.lastTree1.Location = new Point(3, 3);
            this.lastTree1.Name = "lastTree1";
            this.lastTree1.SelectedImageIndex = 0;
            this.lastTree1.Size = new Size(0x1e4, 0x16b);
            this.lastTree1.TabIndex = 2;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1f2, 0x18a);
            base.Controls.Add(this.tabControl1);
            base.Name = "IconManager";
            this.Text = "图标管理";
            base.TopMost = true;
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tab_wb.ResumeLayout(false);
            this.tab_wb.PerformLayout();
            this.pic_softIcon.EndInit();
            base.ResumeLayout(false);
        }

        public void InvokeCallBack(IconManagerEventArgs result)
        {
            IconManagerEventArgs args = result;
            this.imea = args.Style;
            if (args.Style != IconManagerEventArgs.IconType.None)
            {
                this.txt_selectExePath.Text = args.Value;
                this.txt_softExeName.Text = args.Name;
                this.pic_softIcon.Image = args.Image;
                this.button1.Enabled = true;
            }
            else
            {
                this.txt_selectExePath.Text = args.Value;
                this.txt_softExeName.Text = string.Empty;
                this.pic_softIcon.Image = null;
                this.button1.Enabled = false;
            }
        }

        private void lastTree1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Nodes.Count == 0)
            {
                ButtonControlExEventArgs args = new ButtonControlExEventArgs {
                    Name = node.Text,
                    Key = node.Name,
                    Style = ButtonClickStyle.System,
                    Icon = this.imageList1.Images[node.ImageKey],
                    IconName = node.ImageKey
                };
                if (e.Node.Checked)
                {
                    if (this.AddButtonOperate != null)
                    {
                        this.AddButtonOperate(this, args);
                    }
                }
                else if (this.DelteButtonOperate != null)
                {
                    this.DelteButtonOperate(this, args);
                }
            }
        }

        private void NodeEx(TreeNode node)
        {
            for (int i = node.Nodes.Count; i > 0; i--)
            {
                TreeNode node2 = node.Nodes[i - 1];
                if (((this.PowerList != null) && !this.PowerList.Contains(node2.Name)) && !UserInfo.get_IsAdmin())
                {
                    node.Nodes.Remove(node2);
                }
                else
                {
                    if (this.owner.IsButtonExists(node2.Name))
                    {
                        node2.Checked = true;
                    }
                    if (node2.Nodes.Count > 0)
                    {
                        this.NodeEx(node2);
                    }
                }
            }
        }

        public void OnRequestCallback(object value)
        {
            try
            {
                IconManagerEventArgs args = new IconManagerEventArgs(value.ToString());
                InvokeCallBackDelegate method = new InvokeCallBackDelegate(this.InvokeCallBack);
                base.BeginInvoke(method, new object[] { args });
            }
            catch
            {
            }
        }

        private void tab_wb_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] data = (string[]) e.Data.GetData(DataFormats.FileDrop);
                this.txt_selectExePath.Text = data[0];
            }
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                string str = (string) e.Data.GetData(DataFormats.Text);
                this.txt_selectExePath.Text = str;
            }
        }

        private void tab_wb_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text) || e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        public void TreeLoad()
        {
            this.lastTree1.Nodes.Clear();
            TreeLoader.Load(this.lastTree1, this, "/Aisino/Tree", false);
            for (int i = this.lastTree1.Nodes.Count; i > 0; i--)
            {
                this.NodeEx(this.lastTree1.Nodes[i - 1]);
            }
        }

        private void txt_selectExePath_TextChanged(object sender, EventArgs e)
        {
            string text = this.txt_selectExePath.Text;
            if ((File.Exists(text) || Directory.Exists(text)) || Regex.IsMatch(text, @"^(http://)?([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?$"))
            {
                new Thread(new ParameterizedThreadStart(this.OnRequestCallback)).Start(text);
            }
        }

        public delegate void ButtonOperate(object sender, ButtonControlExEventArgs e);

        public delegate void InvokeCallBackDelegate(IconManagerEventArgs result);
    }
}

