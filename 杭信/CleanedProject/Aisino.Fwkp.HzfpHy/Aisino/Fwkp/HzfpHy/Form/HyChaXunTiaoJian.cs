namespace Aisino.Fwkp.HzfpHy.Form
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

    public class HyChaXunTiaoJian : DockForm
    {
        private AisinoDataGrid _AisinoGrid;
        private AisinoBTN but_close;
        private AisinoBTN but_ok;
        private AisinoBTN but_zuhechaxun;
        private AisinoCHK che_jsrq;
        private AisinoCHK che_ksrq;
        private AisinoMultiCombox com_cyfmc;
        private AisinoMultiCombox com_cyfsh;
        private AisinoMultiCombox com_spfmc;
        private AisinoMultiCombox com_spfsh;
        private AisinoCMB com_sqdzt;
        private IContainer components;
        public string Cyfmc = string.Empty;
        public string Cyfsh = string.Empty;
        private DateTimePicker data_jsrq;
        private DateTimePicker data_ksrq;
        public DateTime DateEnd;
        public DateTime DateStart;
        public string Spfmc = string.Empty;
        public string Spfsh = string.Empty;
        public string Sqdh = string.Empty;
        public string Sqdzt = string.Empty;
        private AisinoTXT text_sqdh;
        private XmlComponentLoader xmlComponentLoader1;

        public HyChaXunTiaoJian()
        {
            this.Initialize();
            this.com_spfmc.Edit=(EditStyle)1;
            this.com_spfsh.Edit=(EditStyle)1;
            this.com_spfmc.Edit=(EditStyle)1;
            this.com_spfsh.Edit=(EditStyle)1;
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
            if (this.DateStart >= this.DateEnd)
            {
                MessageManager.ShowMsgBox("INP-431401");
            }
            else
            {
                this.Sqdh = this.text_sqdh.Text;
                this.Sqdzt = this.com_sqdzt.SelectedValue.ToString();
                this.Spfmc = this.com_spfmc.Text;
                this.Spfsh = this.com_spfsh.Text;
                this.Cyfmc = this.com_cyfmc.Text;
                this.Cyfsh = this.com_cyfsh.Text;
                base.DialogResult = DialogResult.OK;
            }
        }

        private void but_zuhechaxun_Click(object sender, EventArgs e)
        {
            this._AisinoGrid.Select(this);
            base.DialogResult = DialogResult.Cancel;
        }

        private void com_cyf_DropDown(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = (AisinoMultiCombox) sender;
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSFHR", new object[] { combox.Text, 0 });
            if ((objArray != null) && (objArray.Length >= 2))
            {
                this.com_cyfmc.Text = objArray[0].ToString();
                this.com_cyfsh.Text = objArray[1].ToString();
            }
        }

        private void com_cyf_OnAutoComplate(object sender, EventArgs e)
        {
            string text = "";
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if ((combox != null) && combox.Name.Equals("aisinoMultiCombox2"))
            {
                text = this.com_cyfmc.Text;
            }
            else if ((combox != null) && combox.Name.Equals("aisinoMultiCombox3"))
            {
                text = this.com_cyfsh.Text;
            }
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSFHRMore", new object[] { text, 20, "MC,SH" });
            if ((objArray != null) && (objArray.Length > 0))
            {
                DataTable table = objArray[0] as DataTable;
                if ((combox != null) && (table != null))
                {
                    combox.DataSource=(table);
                }
            }
        }

        private void com_cyf_OnSelectValue(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                Dictionary<string, string> dictionary = combox.SelectDict;
                this.com_cyfmc.Text = dictionary["MC"].ToString();
                this.com_cyfsh.Text = dictionary["SH"].ToString();
            }
        }

        private void com_cyfmc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
            }
            else if (ToolUtil.GetBytes(this.com_cyfmc.Text).Length >= 80)
            {
                e.Handled = true;
            }
        }

        private void com_sh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
            }
            else if (!char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void com_spf_DropDown(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = (AisinoMultiCombox) sender;
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSFHR", new object[] { combox.Text, 0 });
            if ((objArray != null) && (objArray.Length >= 2))
            {
                this.com_spfmc.Text = objArray[0].ToString();
                this.com_spfsh.Text = objArray[1].ToString();
            }
        }

        private void com_spf_OnAutoComplete(object sender, EventArgs e)
        {
            string text = "";
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if ((combox != null) && combox.Name.Equals("aisinoMultiCombox4"))
            {
                text = this.com_spfmc.Text;
            }
            else if ((combox != null) && combox.Name.Equals("aisinoMultiCombox1"))
            {
                text = this.com_spfsh.Text;
            }
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSFHRMore", new object[] { text, 20, "MC,SH" });
            if ((objArray != null) && (objArray.Length > 0))
            {
                DataTable table = objArray[0] as DataTable;
                if ((combox != null) && (table != null))
                {
                    combox.DataSource=(table);
                }
            }
        }

        private void com_spf_OnSelectValue(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                Dictionary<string, string> dictionary = combox.SelectDict;
                this.com_spfmc.Text = dictionary["MC"].ToString();
                this.com_spfsh.Text = dictionary["SH"].ToString();
            }
        }

        private void com_spfmc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
            }
            else if (ToolUtil.GetBytes(this.com_spfmc.Text).Length >= 80)
            {
                e.Handled = true;
            }
        }

        private void ComboxBind()
        {
            DataTable table = new DataTable();
            table.Columns.Add("key");
            table.Columns.Add("value");
            DataRow row = table.NewRow();
            row["key"] = "全部";
            row["value"] = "A00";
            table.Rows.Add(row);
            row = table.NewRow();
            row["key"] = "审核通过，已填开红字发票";
            row["value"] = "YTK00";
            table.Rows.Add(row);
            row = table.NewRow();
            row["key"] = "审核通过，未填开红字发票";
            row["value"] = "WTK00";
            table.Rows.Add(row);
            row = table.NewRow();
            row["key"] = "审核未通过";
            row["value"] = "WSH00";
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
            this.but_zuhechaxun = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_zhuhechaxun");
            this.but_ok = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_ok");
            this.but_close = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_close");
            this.text_sqdh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("text_sqdh");
            this.com_sqdzt = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("com_sqdzt");
            this.com_spfsh = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("aisinoMultiCombox1");
            this.com_spfsh.IsSelectAll=(true);
            this.com_spfsh.buttonStyle=(0);
            this.com_spfsh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            this.com_spfsh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.com_spfsh.Width - 140));
            this.com_spfsh.ShowText=("SH");
            this.com_spfsh.DrawHead=(false);
            this.com_spfmc = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("aisinoMultiCombox4");
            this.com_spfmc.IsSelectAll=(true);
            this.com_spfmc.buttonStyle=(0);
            this.com_spfmc.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            this.com_spfmc.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.com_spfmc.Width - 140));
            this.com_spfmc.ShowText=("MC");
            this.com_spfmc.DrawHead=(false);
            this.com_cyfsh = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("aisinoMultiCombox3");
            this.com_cyfsh.IsSelectAll=(true);
            this.com_cyfsh.buttonStyle=(0);
            this.com_cyfsh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            this.com_cyfsh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.com_spfsh.Width - 140));
            this.com_cyfsh.ShowText=("SH");
            this.com_cyfsh.DrawHead=(false);
            this.com_cyfmc = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("aisinoMultiCombox2");
            this.com_cyfmc.IsSelectAll=(true);
            this.com_cyfmc.buttonStyle=(0);
            this.com_cyfmc.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "SH", 140));
            this.com_cyfmc.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "MC", this.com_spfmc.Width - 140));
            this.com_cyfmc.ShowText=("MC");
            this.com_cyfmc.DrawHead=(false);
            this.com_spfsh.AutoIndex=(1);
            this.com_spfsh.OnButtonClick+=(new EventHandler(this.com_spf_DropDown));
            this.com_spfsh.AutoComplate=((AutoComplateStyle)(AutoComplateStyle)2);
            this.com_spfsh.OnAutoComplate+=(new EventHandler(this.com_spf_OnAutoComplete));
            this.com_spfsh.OnSelectValue+=(new EventHandler(this.com_spf_OnSelectValue));
            this.com_spfmc.AutoIndex=(1);
            this.com_spfmc.OnButtonClick+=(new EventHandler(this.com_spf_DropDown));
            this.com_spfmc.AutoComplate=((AutoComplateStyle)2);
            this.com_spfmc.OnAutoComplate+=(new EventHandler(this.com_spf_OnAutoComplete));
            this.com_spfmc.OnSelectValue+=(new EventHandler(this.com_spf_OnSelectValue));
            this.com_cyfsh.AutoIndex=(1);
            this.com_cyfsh.OnButtonClick+=(new EventHandler(this.com_cyf_DropDown));
            this.com_cyfsh.AutoComplate=((AutoComplateStyle)2);
            this.com_cyfsh.OnAutoComplate+=(new EventHandler(this.com_cyf_OnAutoComplate));
            this.com_cyfsh.OnSelectValue+=(new EventHandler(this.com_cyf_OnSelectValue));
            this.com_cyfmc.AutoIndex=(1);
            this.com_cyfmc.OnButtonClick+=(new EventHandler(this.com_cyf_DropDown));
            this.com_cyfmc.AutoComplate=((AutoComplateStyle)2);
            this.com_cyfmc.OnAutoComplate+=(new EventHandler(this.com_cyf_OnAutoComplate));
            this.com_cyfmc.OnSelectValue+=(new EventHandler(this.com_cyf_OnSelectValue));
            this.com_cyfmc.KeyPress += new KeyPressEventHandler(this.com_cyfmc_KeyPress);
            this.com_spfmc.KeyPress += new KeyPressEventHandler(this.com_spfmc_KeyPress);
            this.com_spfsh.KeyPress += new KeyPressEventHandler(this.com_sh_KeyPress);
            this.com_cyfsh.KeyPress += new KeyPressEventHandler(this.com_sh_KeyPress);
            this.com_spfsh.MaxLength=(20);
            this.com_cyfsh.MaxLength=(20);
            this.but_close.Click += new EventHandler(this.but_close_Click);
            this.but_ok.Click += new EventHandler(this.but_ok_Click);
            this.but_zuhechaxun.Click += new EventHandler(this.but_zuhechaxun_Click);
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(HyChaXunTiaoJian));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x182, 0x1a1);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath=(@"..\Config\Components\Aisino.Fwkp.HzfpHy.Form.HyChaXunTiaoJian\Aisino.Fwkp.HzfpHy.Form.HyChaXunTiaoJian.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x182, 0x1a1);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Location = new Point(0, 0);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "HyChaXunTiaoJian";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "红字货物运输业增值税专用发票信息表查询条件";
            base.ResumeLayout(false);
        }

        public void SetDataCtrlAttritute()
        {
            int month = base.TaxCardInstance.GetCardClock().Month;
            int year = base.TaxCardInstance.GetCardClock().Year;
            if (year < 0x6d9)
            {
                year = DateTime.Now.Year;
            }
            DateTime.DaysInMonth(year, month);
            this.data_ksrq.Value = new DateTime(year, month, 1);
            this.DateStart = this.data_ksrq.Value;
            int num3 = base.TaxCardInstance.GetCardClock().Year;
            if (num3 < 0x6d9)
            {
                num3 = DateTime.Now.Year;
            }
            int day = DateTime.DaysInMonth(num3, month);
            this.data_jsrq.Value = new DateTime(year, month, day);
            this.DateEnd = this.data_jsrq.Value;
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

