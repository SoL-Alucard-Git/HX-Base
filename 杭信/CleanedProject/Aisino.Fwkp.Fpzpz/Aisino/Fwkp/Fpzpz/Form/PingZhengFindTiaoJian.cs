namespace Aisino.Fwkp.Fpzpz.Form
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Fpzpz.Common;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class PingZhengFindTiaoJian : BaseForm
    {
        private CustomStyleDataGrid _CustomStyleDataGrid1;
        private bool _LinkSuc;
        private ILog _Loger = LogUtil.GetLogger<PingZhengFindTiaoJian>();
        private AisinoBTN but_ok;
        private AisinoBTN but_QuXiao;
        private AisinoBTN but_ZhuHeChaXun;
        private AisinoCHK che_gfmc;
        private AisinoCHK che_gfsh;
        private AisinoCHK che_jsrq;
        private AisinoCHK che_ksrq;
        private AisinoCHK che_wlywh;
        private AisinoCMB com_fpzl;
        private AisinoCMB combo_PzType;
        private IContainer components;
        private DateTimePicker data_jsrq;
        private DateTimePicker data_ksrq;
        private AisinoRDO radio_Kprq;
        private AisinoRDO radio_Pzrq;
        private AisinoRDO radio_Wzf;
        private AisinoRDO radio_Zf;
        private AisinoTXT text_Fpdm;
        private AisinoTXT text_Fphm_Q;
        private AisinoTXT text_Fphm_Z;
        private AisinoTXT text_Pzhm_Q;
        private AisinoTXT text_Pzhm_Z;
        private AisinoTXT text_Wlywh;
        private AisinoTXT txt_gfmc;
        private AisinoTXT txt_gfsh;
        private XmlComponentLoader xmlComponentLoader1;

        public PingZhengFindTiaoJian(bool bLinkSuc)
        {
            try
            {
                this._LinkSuc = bLinkSuc;
                this.Initialize();
                this.InitializeCtrl();
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void but_ok_Click(object sender, EventArgs e)
        {
            try
            {
                base.DialogResult = DialogResult.OK;
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void but_QuXiao_Click(object sender, EventArgs e)
        {
            try
            {
                base.Close();
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void but_ZhuHeChaXun_Click(object sender, EventArgs e)
        {
            try
            {
                this._CustomStyleDataGrid1.Select(this);
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
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

        private void Initialize()
        {
            this.InitializeComponent();
            this.text_Fphm_Z = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("text_Fphm_Z");
            this.text_Fphm_Q = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("text_Fphm_Q");
            this.text_Fpdm = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("text_Fpdm");
            this.che_gfsh = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("che_gfsh");
            this.che_gfmc = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("che_gfmc");
            this.txt_gfsh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_gfsh");
            this.txt_gfmc = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_gfmc");
            this.com_fpzl = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("com_fpzl");
            this.but_ok = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_ok");
            this.but_ZhuHeChaXun = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_ZhuHeChaXun");
            this.data_jsrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_jsrq");
            this.data_ksrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_ksrq");
            this.che_jsrq = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("che_jsrq");
            this.che_ksrq = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("che_ksrq");
            this.che_wlywh = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("che_wlywh");
            this.but_QuXiao = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_QuXiao");
            this.text_Pzhm_Z = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("text_Pzhm_Z");
            this.text_Pzhm_Q = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("text_Pzhm_Q");
            this.text_Wlywh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("text_Wlywh");
            this.combo_PzType = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("combo_PzType");
            this.radio_Pzrq = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("radio_Pzrq");
            this.radio_Kprq = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("radio_Kprq");
            this.radio_Zf = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("radio_Zf");
            this.radio_Wzf = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("radio_Wzf");
            this.but_ok.Click += new EventHandler(this.but_ok_Click);
            this.but_QuXiao.Click += new EventHandler(this.but_QuXiao_Click);
            this.but_ZhuHeChaXun.Click += new EventHandler(this.but_ZhuHeChaXun_Click);
            this.radio_Pzrq.CheckedChanged += new EventHandler(this.radio_Pzrq_Kprq_Zf_Wzf_CheckedChanged);
            this.radio_Kprq.CheckedChanged += new EventHandler(this.radio_Pzrq_Kprq_Zf_Wzf_CheckedChanged);
            this.radio_Zf.CheckedChanged += new EventHandler(this.radio_Pzrq_Kprq_Zf_Wzf_CheckedChanged);
            this.radio_Wzf.CheckedChanged += new EventHandler(this.radio_Pzrq_Kprq_Zf_Wzf_CheckedChanged);
            this.text_Pzhm_Z.KeyPress += new KeyPressEventHandler(this.Int_KeyPressEventHandler);
            this.text_Pzhm_Q.KeyPress += new KeyPressEventHandler(this.Int_KeyPressEventHandler);
            this.text_Fphm_Z.KeyPress += new KeyPressEventHandler(this.Int_KeyPressEventHandler);
            this.text_Fphm_Q.KeyPress += new KeyPressEventHandler(this.Int_KeyPressEventHandler);
            this.text_Fpdm.KeyPress += new KeyPressEventHandler(this.Int_KeyPressEventHandler);
        }

        private void InitializeCombFpzl()
        {
            try
            {
                this.com_fpzl.DropDownStyle = ComboBoxStyle.DropDownList;
                this.com_fpzl.Items.Clear();
                for (int i = 0; i < DingYiZhiFuChuan.FPZPZ_Fpzl.Length; i++)
                {
                    this.com_fpzl.Items.Add(DingYiZhiFuChuan.FPZPZ_Fpzl[i]);
                }
                this.com_fpzl.SelectedIndex = 0;
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void InitializeCombPzzl()
        {
            try
            {
                this.combo_PzType.DropDownStyle = ComboBoxStyle.DropDownList;
                this.combo_PzType.Items.Clear();
                this.combo_PzType.Items.Add("全部凭证");
                if (this._LinkSuc)
                {
                    string[] strArray = FaPiaoZhuanPingZheng.GetA6PZType(this._Loger);
                    if ((strArray != null) && (strArray.Length > 0))
                    {
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            this.combo_PzType.Items.Add(strArray[i]);
                        }
                        this.combo_PzType.SelectedIndex = 0;
                    }
                }
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x199, 0x203);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fpzpz.Form.PingZhengFind\Aisino.Fwkp.Fpzpz.Form.PingZhengFind.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x199, 0x203);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "PingZhengFindTiaoJian";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "发票转凭证查询条件设置";
            base.ResumeLayout(false);
        }

        private void InitializeCtrl()
        {
            try
            {
                this.text_Wlywh.MaxLength = 100;
                this.text_Pzhm_Q.MaxLength = 10;
                this.text_Pzhm_Z.MaxLength = 10;
                this.txt_gfmc.MaxLength = 100;
                this.txt_gfsh.MaxLength = 100;
                this.text_Fpdm.MaxLength = 10;
                this.text_Fphm_Q.MaxLength = 8;
                this.text_Fphm_Z.MaxLength = 8;
                this.data_jsrq.MinDate = DingYiZhiFuChuan.MinDate;
                this.data_ksrq.MinDate = DingYiZhiFuChuan.MinDate;
                DateTime cardClock = base.TaxCardInstance.GetCardClock();
                this.data_ksrq.Value = new DateTime(cardClock.Year, cardClock.Month, 1);
                this.data_jsrq.Value = new DateTime(cardClock.Year, cardClock.Month, DateTime.DaysInMonth(cardClock.Year, cardClock.Month));
                this.text_Fphm_Z.Text = string.Empty;
                this.text_Fphm_Q.Text = string.Empty;
                this.text_Fpdm.Text = string.Empty;
                this.txt_gfsh.Text = string.Empty;
                this.txt_gfmc.Text = string.Empty;
                this.text_Pzhm_Z.Text = string.Empty;
                this.text_Pzhm_Q.Text = string.Empty;
                this.text_Wlywh.Text = string.Empty;
                this.che_wlywh.Checked = true;
                this.che_jsrq.Checked = true;
                this.che_ksrq.Checked = true;
                this.radio_Pzrq.Checked = false;
                this.radio_Kprq.Checked = true;
                this.radio_Zf.Checked = false;
                this.radio_Wzf.Checked = true;
                this.InitializeCombFpzl();
                this.InitializeCombPzzl();
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void Int_KeyPressEventHandler(object sender, KeyPressEventArgs e)
        {
            try
            {
                string text = ((AisinoTXT) sender).Text;
                if (((Convert.ToInt32(e.KeyChar) < 0x30) || (Convert.ToInt32(e.KeyChar) > 0x39)) && (((Convert.ToInt32(e.KeyChar) != 0x2e) && (Convert.ToInt32(e.KeyChar) != 8)) && (Convert.ToInt32(e.KeyChar) != 13)))
                {
                    e.Handled = true;
                }
                else if (e.KeyChar == '0')
                {
                    if (text.Length == 0)
                    {
                        e.Handled = true;
                    }
                }
                else if (Convert.ToInt32(e.KeyChar) == 0x2e)
                {
                    e.Handled = true;
                }
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void radio_Pzrq_Kprq_Zf_Wzf_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AisinoRDO ordo = (AisinoRDO) sender;
                if (ordo.Name.Equals("radio_Pzrq"))
                {
                    if (this.radio_Kprq.Checked != !this.radio_Pzrq.Checked)
                    {
                        this.radio_Kprq.Checked = !this.radio_Pzrq.Checked;
                    }
                }
                else if (ordo.Name.Equals("radio_Kprq") && (this.radio_Kprq.Checked != !this.radio_Pzrq.Checked))
                {
                    this.radio_Pzrq.Checked = !this.radio_Kprq.Checked;
                }
                if (ordo.Name.Equals("radio_Wzf"))
                {
                    if (this.radio_Kprq.Checked != !this.radio_Pzrq.Checked)
                    {
                        this.radio_Kprq.Checked = !this.radio_Pzrq.Checked;
                    }
                }
                else if (ordo.Name.Equals("radio_Zf") && (this.radio_Kprq.Checked != !this.radio_Pzrq.Checked))
                {
                    this.radio_Pzrq.Checked = !this.radio_Kprq.Checked;
                }
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
            }
        }

        public AisinoCHK Che_gfmc
        {
            get
            {
                return this.che_gfmc;
            }
        }

        public AisinoCHK Che_gfsh
        {
            get
            {
                return this.che_gfsh;
            }
        }

        public AisinoCHK Che_jsrq
        {
            get
            {
                return this.che_jsrq;
            }
        }

        public AisinoCHK Che_ksrq
        {
            get
            {
                return this.che_ksrq;
            }
        }

        public AisinoCHK Che_wlywh
        {
            get
            {
                return this.che_wlywh;
            }
        }

        public AisinoCMB Com_fpzl
        {
            get
            {
                return this.com_fpzl;
            }
        }

        public AisinoCMB Combo_PzType
        {
            get
            {
                return this.combo_PzType;
            }
        }

        public DateTime Data_jsrq
        {
            get
            {
                return this.data_jsrq.Value;
            }
        }

        public DateTime Data_ksrq
        {
            get
            {
                return this.data_ksrq.Value;
            }
        }

        public CustomStyleDataGrid DataGrid
        {
            set
            {
                this._CustomStyleDataGrid1 = value;
            }
        }

        public AisinoRDO Radio_Kprq
        {
            get
            {
                return this.radio_Kprq;
            }
        }

        public AisinoRDO Radio_Pzrq
        {
            get
            {
                return this.radio_Pzrq;
            }
        }

        public AisinoRDO Radio_Wzf
        {
            get
            {
                return this.radio_Wzf;
            }
        }

        public AisinoRDO Radio_Zf
        {
            get
            {
                return this.radio_Zf;
            }
        }

        public AisinoTXT Text_Fpdm
        {
            get
            {
                return this.text_Fpdm;
            }
        }

        public AisinoTXT Text_Fphm_Q
        {
            get
            {
                return this.text_Fphm_Q;
            }
        }

        public AisinoTXT Text_Fphm_Z
        {
            get
            {
                return this.text_Fphm_Z;
            }
        }

        public AisinoTXT Text_Pzhm_Q
        {
            get
            {
                return this.text_Pzhm_Q;
            }
        }

        public AisinoTXT Text_Pzhm_Z
        {
            get
            {
                return this.text_Pzhm_Z;
            }
        }

        public AisinoTXT Text_Wlywh
        {
            get
            {
                return this.text_Wlywh;
            }
        }

        public AisinoTXT Txt_gfmc
        {
            get
            {
                return this.txt_gfmc;
            }
        }

        public AisinoTXT Txt_gfsh
        {
            get
            {
                return this.txt_gfsh;
            }
        }
    }
}

