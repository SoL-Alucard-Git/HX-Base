namespace Aisino.Fwkp.HomePage.AisinoDock
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;

    public class DockManage : Form
    {
        private PageControl _page;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private IContainer components;
        private ListView listView1;

        public event DockOper OnDockOper;

        public DockManage(PageControl page)
        {
            this._page = page;
            this.InitializeComponent();
            this.ReadDockList();
        }

        private void but_close_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DockManage_Load(object sender, EventArgs e)
        {
        }

        public void HideWindow()
        {
            base.Hide();
        }

        private void InitializeComponent()
        {
            this.listView1 = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.columnHeader3 = new ColumnHeader();
            base.SuspendLayout();
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader3 });
            this.listView1.Dock = DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x134, 0x10f);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.columnHeader1.Text = "序号";
            this.columnHeader1.Width = 0x4d;
            this.columnHeader2.Text = "名称";
            this.columnHeader2.Width = 0x8f;
            this.columnHeader3.Text = "状态";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0x38, 0xa3, 0xdb);
            base.ClientSize = new Size(0x134, 0x10f);
            base.ControlBox = false;
            base.Controls.Add(this.listView1);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "DockManage";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "窗口管理";
            base.Load += new EventHandler(this.DockManage_Load);
            base.ResumeLayout(false);
        }

        private void listView1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (this.OnDockOper != null)
            {
                ListViewItem item = e.Item;
                string name = item.Tag.ToString();
                bool flag = e.Item.Checked;
                item.SubItems[2].Text = flag ? "启用" : "移除";
                this.OnDockOper(flag, name);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            base.Close();
        }

        private void ReadDockList()
        {
            string pageConfigFile = PageXml.PageConfigFile;
            if (File.Exists(pageConfigFile))
            {
                try
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(pageConfigFile);
                    XmlNodeList list = document.SelectNodes("/Config/List/Dock");
                    this.listView1.Items.Clear();
                    int num = 1;
                    foreach (XmlNode node in list)
                    {
                        XmlElement element = node as XmlElement;
                        if (element != null)
                        {
                            ListViewItem item = new ListViewItem(num.ToString()) {
                                Tag = element.GetAttribute("name")
                            };
                            item.SubItems.Add(element.GetAttribute("value"));
                            bool flag = this._page.DockExists(element.GetAttribute("name"));
                            item.Checked = flag;
                            item.SubItems.Add(flag ? "启用" : "移除");
                            this.listView1.Items.Add(item);
                            num++;
                        }
                    }
                }
                catch
                {
                }
                this.listView1.ItemChecked += new ItemCheckedEventHandler(this.listView1_ItemChecked);
            }
        }

        [DllImport("user32")]
        public static extern int ReleaseCapture();
        public void ShowWindow(Point pt)
        {
            ReleaseCapture();
            base.Location = pt;
            base.Show();
            base.BringToFront();
        }

        public delegate void DockOper(bool value, string name);
    }
}

