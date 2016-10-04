namespace Aisino.Fwkp.Hzfp.Form
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class ChaXunTiaoJian : DockForm
    {
        private AisinoDataGrid _AisinoGrid;
        private AisinoBTN but_close;
        private AisinoBTN but_ok;
        private AisinoBTN but_zhuhechaxun;
        private AisinoCHK che_jsrq;
        private AisinoCHK che_ksrq;
        private AisinoMultiCombox com_gfmc;
        private AisinoMultiCombox com_gfsbh;
        private AisinoCMB com_sqdzt;
        private AisinoMultiCombox com_xfmc;
        private AisinoMultiCombox com_xfsbh;
        private IContainer components;
        private DateTimePicker data_jsrq;
        private DateTimePicker data_ksrq;
        public DateTime DateEnd;
        public DateTime DateStart;
        public string Gfmc = string.Empty;
        public string Gfsh = string.Empty;
        public string Sqdh = string.Empty;
        public string Sqdzt = string.Empty;
        private AisinoTXT text_sqdh;
        public string Xfmc = string.Empty;
        public string Xfsh = string.Empty;
        private XmlComponentLoader xmlComponentLoader1;

        public ChaXunTiaoJian()
        {
            this.Initialize();
            this.com_gfmc.Edit=(EditStyle)1;
            this.com_gfsbh.Edit= (EditStyle)1;
            this.com_gfmc.Edit= (EditStyle)1;
            this.com_gfsbh.Edit= (EditStyle)1;
            this.com_sqdzt.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ComboxBind();
            this.SetDataCtrlAttritute();
        }

        private void but_close_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        private void but_ok_Click(object sender, EventArgs e)
        {
            this.DateStart = this.data_ksrq.Value;
            TimeSpan span = new TimeSpan(0x18, 0, 0);
            if (!this.che_ksrq.Checked)
            {
                this.DateStart += span;
            }
            this.DateEnd = this.data_jsrq.Value;
            if (this.che_jsrq.Checked)
            {
                this.DateEnd += span;
            }
            if (this.DateStart > this.DateEnd)
            {
                MessageManager.ShowMsgBox("INP-431379");
            }
            else
            {
                this.Sqdh = this.text_sqdh.Text;
                switch (this.com_sqdzt.SelectedIndex)
                {
                    case 0:
                        this.Sqdzt = "all";
                        break;

                    case 1:
                        this.Sqdzt = "TZD0500";
                        break;

                    case 2:
                        this.Sqdzt = "fail";
                        break;

                    case 3:
                        this.Sqdzt = "TZD0000";
                        break;

                    case 4:
                        this.Sqdzt = "yhx";
                        break;

                    case 5:
                        this.Sqdzt = "other";
                        break;
                }
                this.Gfmc = this.com_gfmc.Text;
                this.Gfsh = this.com_gfsbh.Text;
                this.Xfmc = this.com_xfmc.Text;
                this.Xfsh = this.com_xfsbh.Text;
                base.DialogResult = DialogResult.OK;
            }
        }

        private void but_zhuhechaxun_Click(object sender, EventArgs e)
        {
            this._AisinoGrid.Select(this);
            base.DialogResult = DialogResult.Cancel;
        }

        private void com_gf_DropDown(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = (AisinoMultiCombox) sender;
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetKH", new object[] { combox.Text, 0 });
            if (objArray.Length == 4)
            {
                this.com_gfmc.Text = objArray[0].ToString();
                this.com_gfsbh.Text = objArray[1].ToString();
            }
        }

        private void com_gfmc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
            }
        }

        private void com_gfsbh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
            }
        }

        private void com_gfsbh_OnAutoComplate(object sender, EventArgs e)
        {
            string text = "";
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if ((combox != null) && combox.Name.Equals("com_gfmc"))
            {
                text = this.com_gfmc.Text;
            }
            else if ((combox != null) && combox.Name.Equals("com_gfsbh"))
            {
                text = this.com_gfsbh.Text;
            }
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetKHMore", new object[] { text, 20, "MC,SH,DZDH,YHZH" });
            if ((objArray != null) && (objArray.Length > 0))
            {
                DataTable table = objArray[0] as DataTable;
                if ((combox != null) && (table != null))
                {
                    combox.DataSource=table;
                }
            }
        }

        private void com_gfsbh_OnSelectValue(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                Dictionary<string, string> dictionary = combox.SelectDict;
                this.com_gfmc.Text = dictionary["MC"].ToString();
                this.com_gfsbh.Text = dictionary["SH"].ToString();
            }
        }

        private void com_xf_DropDown(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = (AisinoMultiCombox) sender;
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetKH", new object[] { combox.Text, 0 });
            if (objArray.Length == 4)
            {
                this.com_xfmc.Text = objArray[0].ToString();
                this.com_xfsbh.Text = objArray[1].ToString();
            }
        }

        private void com_xfmc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
            }
        }

        private void com_xfsbh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
            }
        }

        private void com_xfsbh_OnAutoComplate(object sender, EventArgs e)
        {
            string text = "";
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if ((combox != null) && combox.Name.Equals("com_xfmc"))
            {
                text = this.com_xfmc.Text;
            }
            else if ((combox != null) && combox.Name.Equals("com_xfsbh"))
            {
                text = this.com_xfsbh.Text;
            }
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetKHMore", new object[] { text, 20, "MC,SH,DZDH,YHZH" });
            if ((objArray != null) && (objArray.Length > 0))
            {
                DataTable table = objArray[0] as DataTable;
                if ((combox != null) && (table != null))
                {
                    combox.DataSource=table;
                }
            }
        }

        private void com_xfsbh_OnSelectValue(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                Dictionary<string, string> dictionary = combox.SelectDict;
                this.com_xfmc.Text = dictionary["MC"].ToString();
                this.com_xfsbh.Text = dictionary["SH"].ToString();
            }
        }

        private void ComboxBind()
        {
            DataTable table = new DataTable();
            table.Columns.Add("key");
            table.Columns.Add("value");
            DataRow row = table.NewRow();
            row["key"] = "全部";
            row["value"] = 0;
            table.Rows.Add(row);
            row = table.NewRow();
            row["key"] = "未上传";
            row["value"] = 1;
            table.Rows.Add(row);
            row = table.NewRow();
            row["key"] = "审核不通过";
            row["value"] = 2;
            table.Rows.Add(row);
            row = table.NewRow();
            row["key"] = "审核通过";
            row["value"] = 3;
            table.Rows.Add(row);
            row = table.NewRow();
            row["key"] = "已核销";
            row["value"] = 4;
            table.Rows.Add(row);
            row = table.NewRow();
            row["key"] = "其他";
            row["value"] = 5;
            table.Rows.Add(row);
            this.com_sqdzt.ValueMember = "value";
            this.com_sqdzt.DisplayMember = "key";
            this.com_sqdzt.DataSource = table;
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
            this.data_jsrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_jsrq");
            this.data_ksrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_ksrq");
            this.che_jsrq = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("che_jsrq");
            this.che_ksrq = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("che_ksrq");
            this.but_zhuhechaxun = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_zhuhechaxun");
            this.but_ok = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_ok");
            this.but_close = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_close");
            this.text_sqdh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("text_sqdh");
            this.com_sqdzt = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("com_sqdzt");
            this.com_gfsbh = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("aisinoMultiCombox1");
            this.com_gfsbh.IsSelectAll=true;
            this.com_gfsbh.buttonStyle=0;
            this.com_gfsbh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            this.com_gfsbh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.com_gfsbh.Width - 140));
            this.com_gfsbh.ShowText="SH";
            this.com_gfsbh.DrawHead = false;
            this.com_gfmc = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("aisinoMultiCombox4");
            this.com_gfmc.IsSelectAll=true;
            this.com_gfmc.buttonStyle=0;
            this.com_gfmc.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            this.com_gfmc.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.com_gfmc.Width - 140));
            this.com_gfmc.ShowText="MC";
            this.com_gfmc.DrawHead=false;
            this.com_xfsbh = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("aisinoMultiCombox3");
            this.com_xfsbh.IsSelectAll=true;
            this.com_xfsbh.buttonStyle=0;
            this.com_xfsbh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            this.com_xfsbh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.com_gfsbh.Width - 140));
            this.com_xfsbh.ShowText="SH";
            this.com_xfsbh.DrawHead=false;
            this.com_xfmc = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("aisinoMultiCombox2");
            this.com_xfmc.IsSelectAll=true;
            this.com_xfmc.buttonStyle=0;
            this.com_xfmc.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            this.com_xfmc.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.com_gfmc.Width - 140));
            this.com_xfmc.ShowText="MC";
            this.com_xfmc.DrawHead=false;
            this.com_gfsbh.AutoIndex=2;
            this.com_gfsbh.OnButtonClick+=new EventHandler(this.com_gf_DropDown);
            this.com_gfsbh.AutoComplate=(AutoComplateStyle)2;
            this.com_gfsbh.OnAutoComplate+=(new EventHandler(this.com_gfsbh_OnAutoComplate));
            this.com_gfsbh.OnSelectValue+=(new EventHandler(this.com_gfsbh_OnSelectValue));
            this.com_gfmc.AutoIndex=2;
            this.com_gfmc.OnButtonClick+=(new EventHandler(this.com_gf_DropDown));
            this.com_gfmc.AutoComplate=(AutoComplateStyle)2;
            this.com_gfmc.OnAutoComplate+=(new EventHandler(this.com_gfsbh_OnAutoComplate));
            this.com_gfmc.OnSelectValue+=(new EventHandler(this.com_gfsbh_OnSelectValue));
            this.com_xfsbh.AutoIndex=2;
            this.com_xfsbh.OnButtonClick+=(new EventHandler(this.com_xf_DropDown));
            this.com_xfsbh.AutoComplate=(AutoComplateStyle)2;
            this.com_xfsbh.OnAutoComplate+=(new EventHandler(this.com_xfsbh_OnAutoComplate));
            this.com_xfsbh.OnSelectValue+=(new EventHandler(this.com_xfsbh_OnSelectValue));
            this.com_xfmc.AutoIndex=2;
            this.com_xfmc.OnButtonClick+=(new EventHandler(this.com_xf_DropDown));
            this.com_xfmc.AutoComplate = (AutoComplateStyle)2;
            this.com_xfmc.OnAutoComplate+=(new EventHandler(this.com_xfsbh_OnAutoComplate));
            this.com_xfmc.OnSelectValue+=(new EventHandler(this.com_xfsbh_OnSelectValue));
            this.com_xfmc.KeyPress += new KeyPressEventHandler(this.com_xfmc_KeyPress);
            this.com_xfmc.MaxLength=100;
            this.com_gfmc.KeyPress += new KeyPressEventHandler(this.com_gfmc_KeyPress);
            this.com_gfmc.MaxLength=100;
            this.com_xfsbh.KeyPress += new KeyPressEventHandler(this.com_xfsbh_KeyPress);
            this.com_xfsbh.MaxLength=20;
            this.com_gfsbh.KeyPress += new KeyPressEventHandler(this.com_gfsbh_KeyPress);
            this.com_gfsbh.MaxLength=20;
            this.text_sqdh.KeyPress += new KeyPressEventHandler(this.text_sqdh_KeyPress);
            this.text_sqdh.MaxLength = 0x18;
            this.but_close.Click += new EventHandler(this.but_close_Click);
            this.but_ok.Click += new EventHandler(this.but_ok_Click);
            this.but_zhuhechaxun.Click += new EventHandler(this.but_zhuhechaxun_Click);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x166, 0x16e);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath=(@"..\Config\Components\Aisino.Fwkp.Hzfp.Form.ChaXunTiaoJian\Aisino.Fwkp.Hzfp.Form.ChaXunTiaoJian.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x164, 0x1a1);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ChaXunTiaoJian";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "查询条件";
            base.ResumeLayout(false);
        }

        public void SetDataCtrlAttritute()
        {
            int month = base.TaxCardInstance.GetCardClock().Month;
            int year = base.TaxCardInstance.GetCardClock().Year;
            if (year < 0x76c)
            {
                year = DateTime.Now.Year;
            }
            this.data_ksrq.Value = new DateTime(year, month, 1);
            int day = DateTime.DaysInMonth(year, month);
            this.data_jsrq.Value = new DateTime(year, month, day);
            this.DateStart = this.data_ksrq.Value;
            this.DateEnd = this.data_jsrq.Value;
        }

        private void text_sqdh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
            }
        }

        public AisinoDataGrid AisinoGrid
        {
            set
            {
                this._AisinoGrid = value;
            }
        }

        public DateTime DateTimeEnd
        {
            get
            {
                return this.DateEnd;
            }
        }

        public DateTime DateTimeStart
        {
            get
            {
                return this.DateStart;
            }
        }
    }
}

