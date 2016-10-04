namespace Aisino.Fwkp.Fptk.Form
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Microsoft.Win32;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class QiangSet : BaseForm
    {
        private AisinoBTN button1;
        private AisinoBTN button2;
        private AisinoCHK checkBox1;
        private AisinoCHK checkBox2;
        private AisinoCHK checkBox3;
        private AisinoCMB comboBox1;
        private IContainer components;
        private XmlComponentLoader xmlComponentLoader1;

        public QiangSet()
        {
            this.Initialize();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = "";
            if (this.comboBox1.SelectedItem != null)
            {
                str = this.comboBox1.SelectedItem.ToString();
            }
            else
            {
                MessageManager.ShowMsgBox("INP-278101");
                return;
            }
            PropertyUtil.SetValue("COM_SET", str);
            int num = 0;
            if (this.checkBox1.Checked)
            {
                num += 4;
            }
            if (this.checkBox2.Checked)
            {
                num += 2;
            }
            if (this.checkBox3.Checked)
            {
                num++;
            }
            PropertyUtil.SetValue("CPXH_SET", num.ToString());
            base.Close();
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void Initialize()
        {
            this.InitializeComponent();
            this.button1 = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("button1");
            this.button2 = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("button2");
            this.comboBox1 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comboBox1");
            this.checkBox1 = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("checkBox1");
            this.checkBox2 = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("checkBox2");
            this.checkBox3 = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("checkBox3");
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.Click += new EventHandler(this.button2_Click);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(QiangSet));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = System.Drawing.Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x1e4, 0x14c);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Text = "扫描枪信息设置";
            this.xmlComponentLoader1.XMLPath=@"..\Config\Components\Aisino.Fwkp.Fpkj.Form.QiangSet\Aisino.Fwkp.Fpkj.Form.QiangSet.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1e4, 0x14c);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.Name = "QiangSet";
            this.Text = "扫描枪信息设置";
            base.Load += new EventHandler(this.QiangSet_Load);
            base.ResumeLayout(false);
        }

        private void QiangSet_Load(object sender, EventArgs e)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Hardware\DeviceMap\SerialComm");
            if (key != null)
            {
                this.comboBox1.Items.Clear();
                foreach (string str2 in key.GetValueNames())
                {
                    string item = (string) key.GetValue(str2);
                    this.comboBox1.Items.Add(item);
                }
            }
            string str = PropertyUtil.GetValue("COM_SET");
            if (((str != null) || (str != "")) && (this.comboBox1.Items.Count > 0))
            {
                int count = this.comboBox1.Items.Count;
                for (int i = 0; i < count; i++)
                {
                    if (this.comboBox1.Items[i].ToString().Equals(str))
                    {
                        this.comboBox1.SelectedIndex = i;
                    }
                }
            }
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            int result = 0;
            if (int.TryParse(PropertyUtil.GetValue("CPXH_SET"), out result))
            {
                if ((result & 4) > 0)
                {
                    this.checkBox1.Checked = true;
                }
                else
                {
                    this.checkBox1.Checked = false;
                }
                if ((result & 2) > 0)
                {
                    this.checkBox2.Checked = true;
                }
                else
                {
                    this.checkBox2.Checked = false;
                }
                if ((result & 1) > 0)
                {
                    this.checkBox3.Checked = true;
                }
                else
                {
                    this.checkBox3.Checked = false;
                }
            }
        }
    }
}

